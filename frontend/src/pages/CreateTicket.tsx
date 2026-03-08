import { useState } from 'react'
import { addTicket } from '../API';
import logo from '../assets/logo.png'
import './CreateTicket.css'

// import { useState } from 'react';

function CreateTicket() {
    const [status, setStatus] = useState("idle");


    const createTicket = async()=>{
        setStatus("pending");
        const ticket = {name:(document.getElementById("name") as HTMLInputElement)?.value,
          firstname: (document.getElementById("firstname") as HTMLInputElement)?.value,
          email: (document.getElementById("email") as HTMLInputElement)?.value,
          topic: (document.getElementById("topic") as HTMLInputElement)?.value,
          text: (document.getElementById("textfield") as HTMLTextAreaElement)?.value};
          try {
            await addTicket(ticket);
            setStatus("success");
          } catch (e) {
            console.error(e);
            setStatus("idle");
          }
    }
  return (
    <>
      <div>
        <a target="_blank">
          <img src={logo} className="logo logo-spin" alt="logo" />
        </a>
      </div>
          <h1>F.H.P HelpDesk</h1>
          <div className ="ticketinput">
              <input id="email" placeholder='E-Mail Adresse'/>
              <div className ="name">
                <input id="name" placeholder='Name'/>
                <input id="firstname" placeholder='Vorname'/>
              </div>
              <input id="topic" placeholder='Betreff'/>
              <textarea id="textfield" className="textfield" rows={10} placeholder='Text'/>
          </div>
          <div className='card'>
            {status === "pending" && (
              <Typography variant="body1" color="textSecondary" align="center" sx={{ mt: 2 }}>
                Ticket wird erstellt...
              </Typography>
            )}
            {status === "success" && (
              <div>
              <Typography variant="body1" color="success.main" align="center" sx={{ mt: 2 }}>
                Ticket erfolgreich erstellt! 
              </Typography>
              <button  onClick={()=>setStatus("idle")}>ok</button>
              </div>
            )}
            {status === "idle" && (
              <button  id='createTicket' onClick={async() => createTicket()}>
                neues Ticket erstellen
              </button> 
            )}
          </div>
        </Paper>
     </Box>
    </Grid>
      
  )
}

export default CreateTicket
