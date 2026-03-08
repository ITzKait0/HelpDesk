import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getTicketById, getMailsForTicketId } from '../API';
import { Ticket } from '../Ticket';
import {Mail} from '../Mail';
import './TicketView.css';
import { SendMailComp } from './SendMailComp';

export default function TicketView() {
    const { ticketId } = useParams<{ ticketId: string }>();
    const [ticket, setTicket] = useState<Ticket | null>(null);
    const [mails, setMails] = useState<Mail[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [sendingMail, setSendingMail] = useState(false);
    useEffect(() => {
        const fetchData = async () => {
            try {
                if (!ticketId) throw new Error('No ticket ID provided');
                
                const ticketData = await getTicketById(parseInt(ticketId));
                setTicket(ticketData);

                const mailsData = await getMailsForTicketId(parseInt(ticketId));
                setMails(mailsData);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Failed to load data');
            } finally {
                setLoading(false);
            }
        };
        fetchData();

    }, [ticketId]);
    useEffect(() => {

        if(ticketId){
            const interval = setInterval(() => getMailsForTicketId(parseInt(ticketId)), 5000); // Refresh data every 5 seconds
            return () => clearInterval(interval);
        }
        

    },[]);
    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div className="ticket-view">
            {ticket && (
                <div className="ticket-info">
                    <h1>{ticket.topic}</h1>
                </div>
            )}

            <div className="mails-section">
                <h2>Messages</h2>
                <button onClick={() => setSendingMail(true)} className="btn btn-primary">
                    Respond
                </button>
                {sendingMail && <SendMailComp receiverEmail={ticket?.email || ''} subject={"Re: "+ticket?.topic} ticketId={parseInt(ticketId!)} />}
                {mails.length > 0 ? (
                    mails.map((mail) => (
                        <div key={mail.id} className="mail-item">
                            <div className="mail-meta">
                                <strong>{mail.from}</strong>
                                <span>{mail.sendDate}</span>
                            </div>

                            <div className="mail-text">
                                {mail.text}
                            </div>
                        </div>
                    ))
                ) : (
                    <p>No messages yet</p>
                )}
            </div>
        </div>
    );
}