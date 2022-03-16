import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from '../NavMenu';
import "./LandingPageLayout.css"
import CookieConsent from "react-cookie-consent";
import { BsFillInfoCircleFill } from 'react-icons/bs';

export default class LandingPageLayout extends React.PureComponent<{}, { children?: React.ReactNode }> {
    public render() {
        return (
            <div className="landing-layout-box">
                
                <React.Fragment>
                    
                    <NavMenu/>
                    
                        
                        {this.props.children}
                    <CookieConsent
                        location="bottom"
                        buttonText="I understand"
                        buttonId="btn-consent"
                        buttonStyle={{ backgroundColor: "#4E74DE", color: "white", borderRadius: "5%" }}
                        //declineButtonText="I decline"
                        //declineButtonStyle={{ backgroundColor: "#4E74DE", borderRadius: "5%" }}
                        //enableDeclineButton
                        //onDecline={() => { alert("You must accept cookies!") }}
                        cookieName="ghostWriterCookie"
                    >
                        < BsFillInfoCircleFill size="24" color="#4E74DE" /> &nbsp; This website uses cookies to enhance the user experience.
                    </CookieConsent>   
                        
                    </React.Fragment>

            </div>
        );
    }
}