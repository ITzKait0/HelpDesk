import { useState } from 'react'
import logo from './assets/logo.png'
import viteLogo from '/vite.svg'
import './App.css'


class Kunde {
    constructor(name, vorname, tel, adresse, plz) {
        this.name = name;
        this.vorname = vorname;
        this.tel = tel;
        this.adresse = adresse;
        this.plz = plz;
    }
}

class Ticket {
    constructor(name, firstname, email, topic, text) {
        this.name = name;
        this.firstname = firstname;
        this.email = email;
        this.topic = topic;
        this.text = text;
    }
}
function App() {
    const [status, setStatus] = useState("idle");
    const addTicket = async (ticket)=>{
        console.log(
  "VALUE:",
  ticket
);
        await fetch("http://localhost:5003/Ticket/add", {
            method: "POST",
            headers: {
                "Content-Type":"application/json"
            },
            body: JSON.stringify(ticket),
        });
    }

    const createTicket = async()=>{
        setStatus("pending");
        console.log(
  "TEXTFIELD VALUE:",
  document.getElementById("textfield")?.value
);
        const ticket = {name:document.getElementById("name").value,
          firstname: document.getElementById("firstname").value,
          email: document.getElementById("email").value,
          topic: document.getElementById("topic").value,
          text: document.getElementById("textfield").value};
          try {
            await addTicket(ticket);
            setStatus("success"); // ✅ nach Erfolg
          } catch (e) {
            console.error(e);
            setStatus("idle"); // oder "error"
          }
    }
  return (
    <>
      <div>
        <a href="https://www.youtube.com/watch?v=dQw4w9WgXcQ&list=RDdQw4w9WgXcQ&start_radio=1" target="_blank">
          <img src={logo} className="logo logo-spin" alt="logo" />
        </a>
      </div>
          <h1>F.H.P HelpDesk</h1>
          <div class ="ticketinput">
              <input id="email" placeholder='E-Mail Adresse'/>
              <div class ="name">
                <input id="name" placeholder='Name'/>
                <input id="firstname" placeholder='Vorname'/>
              </div>
              <input id="topic" placeholder='Betreff'/>
              <textarea id="textfield" class="textfield" rows="10" placeholder='Text'/>
          </div>
          <div className="card">
              {status ==="pending" &&(
                <div>Ticket wird erstellt...</div>
              )}
              {status ==="success" && (
                <div>
                  <div>Ticket erstellt!</div>
                  <button onClick={()=>{
                    setStatus("idle")}}>
                      ok
                  </button>
                </div>
              )}
              {status ==="idle" && (
                <button id ="createTicket"onClick={async() => createTicket()}>
                  neues Ticket erstellen         
                </button>
              )}
              
      </div>
    </>
  )
}

export default App
