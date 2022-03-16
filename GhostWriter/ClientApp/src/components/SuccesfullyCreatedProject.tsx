import React from 'react';
import { Media, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

import '../styles/SignIn.css'


export const SuccesfullyCreatedProject = () => 
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
                <h4>Succesfully created a new project!</h4>
                <br></br>
                <h3>Thank you.</h3>
                <br></br>
                <div className="create-account-message">
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Nesciunt magni, voluptas debitis
                    similique porro a molestias consequuntur earum odio officiis natus, amet hic, iste sed
                    dignissimos esse fuga! Minus, alias.</p>
                <br></br>
                </div>
                <NavLink tag={Link} to="/customer-profile">Proceed to Dashboard.</NavLink>
            </div>
            
        </div>
    )
}

export default SuccesfullyCreatedProject;