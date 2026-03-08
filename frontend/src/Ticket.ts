export class Ticket {
    id: number = 0;
    ticketNr: number = 0;
    topic: string = "";
    name: string = "";
    firstname: string = "";
    email: string = "";
    priority: Priority = Priority.NON_VALUE;
    text: string = "";
    supporterId: number | null = null;
    created: string = "";
}

export class InsertNewTicket{
    email: string = "";
    name: string = "";
    firstname: string = "";
    topic: string = "";
    text: string = "";
}

enum Priority {
        NON_VALUE = 0,
        SEHR_NIEDRIG = 1,
        NIEDRIG = 2,
        MITTEL = 3,
        HOCH = 4,
        SEHR_HOCH = 5
}