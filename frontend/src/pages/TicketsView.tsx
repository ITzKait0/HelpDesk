import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {Ticket} from '../Ticket';
import { getTicketsBySupporterId } from '../API';
import './TicketsView.css';

export const TicketsView = () => {
    const [tickets, setTickets] = useState<Ticket[]>([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate(); 

    useEffect(() => {//onMount
        getTicketsBySupporterId().then((data) => {
            setTickets(data);
            setLoading(false);
        }); 
        const interval = setInterval(getTicketsBySupporterId, 5000);
        return () => clearInterval(interval);
    }, []);

    if (loading) return <div>Loading...</div>;

    return (
        <div className="tickets-container">
            <h1>Tickets</h1>
            <table className="tickets-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Topic</th>
                        <th>Priority</th>
                        <th>From</th>
                        <th>Created</th>
                    </tr>
                </thead>
                <tbody>
                    {tickets.map((ticket) => (
                        <tr key={ticket.id}>
                            <td className="ticket-id" onClick={() => navigate("/ticket/" + ticket.id)}>
                                {ticket.id}
                            </td>
                            <td>{ticket.topic}</td>
                            <td>{ticket.priority}</td>
                            <td>{ticket.firstname+" "+ticket.name}</td>
                            <td>{ticket.created}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};