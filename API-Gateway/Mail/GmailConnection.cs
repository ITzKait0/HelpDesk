using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Encodings;
using System.ComponentModel;
using System.Diagnostics;
using API_Gateway.Controllers;
namespace API_Gateway.Mail
{
    public class GmailConnection
    {

        private readonly MailSettings _mailSettings;
        private readonly API_Gateway.Controllers.MailService.MailServiceClient _mailServiceClient;

        public GmailConnection(IOptions<MailSettings> mailSettings, API_Gateway.Controllers.MailService.MailServiceClient mailServiceClient)
        {
            _mailSettings = mailSettings.Value;
            _mailServiceClient=mailServiceClient;
        }

        public async Task<bool> SendAsync(string to,string subject,string text)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_mailSettings.GmailUser));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = text };

                using var client = new SmtpClient();
                await client.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailSettings.GmailUser, _mailSettings.GmailPasswort);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while Sending Email: "+ex.Message);
            }
        }

        public async Task<List<Mail>> PollAsync()
        {
            try
            {
                using var client = new ImapClient();

                await client.ConnectAsync(_mailSettings.ImapHost, _mailSettings.ImapPort, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(_mailSettings.GmailUser, _mailSettings.GmailPasswort);

                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadWrite);

                var mailIds = await inbox.SearchAsync(MailKit.Search.SearchQuery.NotSeen);
                Console.WriteLine($"Ungelesene Mails im Posteingang: {mailIds.Count}");
                List<Mail> mails = new List<Mail>();

                foreach(var uid in mailIds)
                {
                    MimeMessage mimeMessage = await inbox.GetMessageAsync(uid);        
                    mails.Add(new Mail()
                    {
                        Id = 0,
                        TicketId = 0,
                        From = mimeMessage.From.Mailboxes.FirstOrDefault()?.Address,
                        Subject = mimeMessage.Subject ?? String.Empty,
                        Text = mimeMessage.TextBody.Split("<")[0],
                        SendDate = mimeMessage.Date.DateTime
                    });
                    await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true);
                }
               
                foreach(var mail in mails)
                {
                    await _mailServiceClient.addMailAsync(new pAddMailRequest
                    {

                        From = mail.From,
                        Subject = mail.Subject,
                        Text = mail.Text,
                        SendDate = mail.SendDate.ToString()
                    });
                }
                await client.DisconnectAsync(true);
                return mails;
            }
            catch (Exception ex) {
                throw new Exception("Error occured while polling Emails: "+ex.Message);
            }
        }
}
}
