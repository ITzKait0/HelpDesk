import { useState } from 'react'
import './App.css'


import { BrowserRouter, Routes, Route } from "react-router-dom";
import { TicketsView} from './pages/TicketsView';
import CreateTicket from './pages/CreateTicket';
import LandingPage from './pages/LandingPage';
import TicketView from './pages/TicketView';
function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/tickets" element={<TicketsView />} />
        <Route path="/create-ticket" element={<CreateTicket />} />
        <Route path="/ticket/:ticketId" element={<TicketView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;