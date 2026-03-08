import { InsertNewTicket, Ticket } from "./Ticket";
import {Mail, SendMail} from "./Mail";

    export const addTicket = async (ticket: InsertNewTicket)=>{
        await fetch("http://localhost:5003/Ticket/add", {
            method: "POST",
            headers: {
                "Content-Type":"application/json"
            },
            body: JSON.stringify(ticket),
        });
    }

    export const getTicketsBySupporterId = async () => {
        var supporterId = 1;
        const response = await fetch(`http://localhost:5003/Ticket/bySupporterId/${supporterId}`);
        const data: Ticket[] = await response.json();
        return data;
    }

    export const getTicketById = async (id: number) => {
        const response = await fetch(`http://localhost:5003/Ticket/byId/${id}`);
        const data: Ticket = await response.json();
        return data;
    }

    export const getMailsForTicketId = async (id: number) => {
        const response = await fetch(`http://localhost:5003/Mail/byTicketId/${id}`);
        const data: Mail[] = await response.json();
        return data;
    }

    export const sendMail = async (mail: SendMail, ticketId: number) => {
        await fetch(`http://localhost:5003/Mail/add/${ticketId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(mail)
        });
    }