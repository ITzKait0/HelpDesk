import { useState } from 'react'
import logo from './assets/logo.png'
import viteLogo from '/vite.svg'
import './App.css'
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import Toolbar from '@mui/material/Toolbar';
import Box from "@mui/material/Box";
import TextField from '@mui/material/TextField';
import Divider from '@mui/material/Divider';
import Icon from '@mui/material/Icon';
import IconButton from '@mui/material/IconButton';

// import { useState } from 'react';



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
const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };
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
            setStatus("success");
          } catch (e) {
            console.error(e);
            setStatus("idle");
          }
    }
    
  return (    
    <Grid  sx={{
    minHeight: "100vh",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    background: "linear-gradient(180deg, #1e1e1e, #e0e0e0)"
  }} >
      <AppBar 
        position='fixed'
        sx={{background: "linear-gradient(90deg, #c08517 10%, #4A2395 50%, #3B1A7A 100%)"}}
      >
        
        <Toolbar 
          sx={{
            display: "flex", 
            justifyContent: "center"
         }}
        >
        {/* <button sx={{ gap: 20 }} className='logo react' onClick={()=>window.open("https://www.fhp-unternehmensgruppe.de/")}>
          <IconButton>
            <Icon>
              <Box component="img"  alt="Vite Logo" sx={{ height: 30, gap: 20 }} />
            </Icon>
          </IconButton>
        </button> */}
          
          <Box component=
            "img" src={logo} 
            alt='Logo' 
            sx={{height: 70}}
          />
          <Typography 
            margin={3} 
            variant='h5'
          >
            FHP Enterprise Estates
          </Typography>
        </Toolbar>
      </AppBar>
      <Box sx={{ 
    pt: '100px',                    // mehr Abstand zur AppBar (je nach AppBar-Höhe anpassen)
    px: { xs: 2, sm: 4, md: 6 },    // seitlicher Padding je nach Bildschirmgröße
    minHeight: 'calc(100vh - 100px)', // fast volle Höhe minus AppBar
    display: 'flex',
    justifyContent: 'center',
    alignItems: { xs: 'flex-start', md: 'center' }, // oben auf Mobil, mittig auf Desktop
  }}>
        <Paper elevation={6}                     // etwas stärkerer Schatten
    sx={{
      width: '100%',
      maxWidth: { 
        xs: '100%',                   // auf Handy fast volle Breite
        sm: '90%', 
        md: '800px',                  // max. Breite auf Desktop
        lg: '9000px'                   // noch etwas größer auf sehr breiten Bildschirmen
      },
      mx: 'auto',                     // zentriert horizontal
      p: { xs: 3, sm: 5 },            // mehr Innenabstand
      borderRadius: 4,                // runderer Rand
      backgroundColor: 'background.paper', // passt sich Theme an (weiß/grau)
    }}>
          <Typography variant="h4"
      component="h1"
      align="center"
      gutterBottom
      sx={{ mb: 4 }} >
            F.H.P Help Desk
          </Typography>
          <Divider/>
          <div class= "ticketinput">
            <input id='email' placeholder='E-Mail Adresse'/>
            <div class="nameinput">
              <input id='name' placeholder='Name'/>
              <input id='firstname' placeholder='Vorname'/>
            </div>
            <input id='topic' placeholder='Thema'/>
            <TextField id="textfield" label="Beschreibung" multiline rows={4} variant="outlined" />
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

export default App
