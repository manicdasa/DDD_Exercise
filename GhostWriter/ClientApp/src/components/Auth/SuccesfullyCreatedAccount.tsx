import React from 'react'
import { Media } from 'reactstrap'

import '../../styles/SignIn.css'


export const SuccesfullyCreatedAccount = () => 
{
    return (
        <div className="register-signin-box row">
            <div className="register-signin-left-panel col-sm-4">
                <h3>Discover Top Ghost Writers</h3>
                <div className="left-panel-text">
                
                    <p>Create a customer account at GhostWritters.com and you will be able to create projects and hire Ghost Writters for your next big assignment.</p>
                </div>
                    <div className="media_left_panel">
                    <Media object src="/images/Login/account-created.png" alt="Typewriter image"></Media>
                        </div>
            
            </div>
            
            <div className="register-signin-right-panel col-sm-8">
                <h4>Registration Successful!</h4>
                <br></br>
                <h3>Thank you for registering for our services.</h3>
                <br></br>
                <div className="create-account-message">
                <p> The registration process was successful. An email has been sent to you requesting you confirm your email. 
                To continue you'll need to follow the instructions provided. </p>
                <br></br>
                    <p>Once you have confirmed you're email address you can navigate over to the sign-in page and log in there. </p>
                </div>
                
            </div>
            
        </div>
    )
}

export default SuccesfullyCreatedAccount;