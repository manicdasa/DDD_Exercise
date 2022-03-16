import React from 'react';
import { Media } from 'reactstrap';

export const MobileComponent = () =>
{
    return(
        <div className="m-container">
            <div className="m-content">
                <Media object src="/images/mobile-logo.png" alt="Site logo" className="logo-mobile"></Media>
                <div className="m-text">
                    <br></br><br></br>
                    <p>We are sorry, <b>Studi Autoren</b> is not optimized for mobile devices yet. <br></br><br></br>Please view this website<br></br> on a desktop computer.</p>
                    <br></br><br></br>
                </div>

                
                <div className="m-media">
                    <Media object src="/images/Login/desktop3.png" alt="Typewriter image"></Media>
                </div>
            
            </div>
        </div>)
}

export default MobileComponent;