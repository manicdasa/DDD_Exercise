import React, { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom';
import { Button, Media } from 'reactstrap'
import { useAlert } from 'react-alert';
import '../../styles/SignIn.css'
import { ConfirmEmail } from '../../services/UserServices';

const useQueryParams = () => {
   const location = useLocation();
    return new URLSearchParams(location.search);
}

export const EmailConfimed = () => 
{
    const queryParams = useQueryParams();
    const alert = useAlert();
    const [response, setResponse] = useState<any>({ success: null, message: '' });
    
    const [confirmedEmail, setConfirmedEmail] = useState(false);

    return (
        <div className="register-signin-box row">
            <div className="register-signin-left-panel col-sm-4">
                <h3>Discover Top Ghost Writers</h3>
                <div className="left-panel-text">

                    <p>Create a customer account at GhostWritters.com and you will be able to create projects and hire Ghost Writters for your next big assignment.</p>
                </div>
                <div className="media_left_panel">
                    <Media object src="/images/Login/email-confirmed.png" alt="Typewriter image"></Media>
                </div>

            </div>
            { confirmedEmail === false ? 
            <div>
                <div className="register-signin-right-panel col-sm-8">
                    <div className="center column-style">
                        <p className='center'>Please click the button below to confirm your email address.</p>
                            <Button className="center" color="primary" onClick={() => { ConfirmEmail(queryParams.get('username'), queryParams.get('token'), alert).then((response) => { setConfirmedEmail(true); setResponse(response); }) }}>Confirm Email</Button>
                    </div>
                </div>
            </div> : <div>
            { response.success === true ?
                <div className="register-signin-right-panel col-sm-8">
                    <h4>Registration Successful!</h4>
                    <br></br>
                    <h3>Thank you for confiming your email.</h3>
                    <br></br>
                    <div className="create-account-message">
                        <p> The registration was successful. You can now start using all of the GhostWriter services.  </p>
                    </div>

                    <br></br>
                    <p><a className="btn btn-primary nav-link" href="/login">Go to sign-in page</a></p>
                    {/*
                <NavLink tag={Link} className="" to="/login">Please proceed to the Login page to continue!</NavLink> 
                */}
                </div> : <div className="register-signin-right-panel col-sm-8">

                    <div className="create-account-message">
                        <h4>Registration was not successful!</h4>
                        <br></br>
                        <h3>Please try again later.</h3>
                    </div>

                    <br></br>
                    <p><a className="btn btn-primary nav-link" href="/login">Go to sign-in page</a></p>
                    {/*
                <NavLink tag={Link} className="" to="/login">Please proceed to the Login page to continue!</NavLink> 
                */}
                </div>}</div>}


        </div>
    )
}

export default EmailConfimed;