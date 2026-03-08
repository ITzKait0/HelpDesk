import React, { useState } from 'react';
import { sendMail } from '../API';
import { SendMail } from '../Mail';
export const SendMailComp = ({ receiverEmail, subject, ticketId }: { receiverEmail: string; subject: string; ticketId: number }) => {
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState('');

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setLoading(true);
  
        const mail = new SendMail();
        mail.From = "fhp.enterprise.estates@gmail.com";
        mail.To = receiverEmail;
        mail.Subject = subject;
        mail.Text = message;
        await sendMail(mail, ticketId);
        alert('Email sent successfully!');
        setLoading(false);
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>To: {receiverEmail}</label>
            </div>
            <div>
                <label>Subject: {subject}</label>
            </div>
            <textarea
                value={message}
                onChange={(e) => setMessage(e.target.value)}
                placeholder="Write your message here..."
                required
            />
            <button type="submit" disabled={loading}>
                {loading ? 'Sending...' : 'Send'}
            </button>
        </form>
    );
};