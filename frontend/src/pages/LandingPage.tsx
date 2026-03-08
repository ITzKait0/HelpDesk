import React from 'react';
import { useNavigate } from 'react-router-dom';
import logo from '../assets/logo.png';
import './LandingPage.css';

export default function LandingPage() {
    const navigate = useNavigate();

    return (
        <div className="landing-container">
            <img src={logo} alt="Logo" className="logo" />
            <div className="button-group">
                <button onClick={() => navigate('/create-ticket')} className="btn btn-primary">
                    Create Ticket
                </button>
                <button onClick={() => navigate('/tickets')} className="btn btn-secondary">
                    View Tickets
                </button>
            </div>
        </div>
    );
}