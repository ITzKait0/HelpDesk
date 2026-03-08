namespace API_Gateway.Mail
{
    public class GmailPollingService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GmailPollingService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var gmailConnection = scope.ServiceProvider.GetRequiredService<GmailConnection>();

                    var mails = await gmailConnection.PollAsync();

                    foreach (var mail in mails)
                    {
                        Console.WriteLine($"Neue Mail: {mail.Subject}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Polling-Fehler: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}