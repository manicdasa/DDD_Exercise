
import React, { useState } from 'react';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router';
import { Media, Button, NavLink } from 'reactstrap';
import { bool } from 'yup';
import { useAlert } from 'react-alert';
import { AnonymizeAuthor, AnonymizeAuthorCheck, AnonymizeCustomer, AnonymizeCustomerCheck } from '../services/ProfileServices';
import { Popup } from 'react-chat-elements';
import '../styles/CreateProject.css'
import { Link } from 'react-router-dom';

export const Support = () =>
{
    const history = useHistory();
    const alert = useAlert();

    const [loggedIn, setLoggedIn] = useState(false);
    const [isAuthor, setIsAuthor] = useState(false);
    const [isCustomer, setIsCustomer] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);
    const [buttonVisible, setButtonVisible] = useState(false);
    const [anonimizeModalIsOpen, setAnonimizeModalIsOpen] = useState(false);
    const dispatch = useDispatch();

    const username = localStorage.getItem('user');

    useEffect(() => {
        if (localStorage.getItem('token') != undefined) {
            setLoggedIn(true);

            if (localStorage.getItem('role=Admin') != undefined) {
                setIsAdmin(true);
                setIsCustomer(false);
                setIsAuthor(false);

            }
            else if (localStorage.getItem('role=Ghostwriter') != undefined) {
                setIsCustomer(false);
                setIsAdmin(false);
                setIsAuthor(true);
                AnonymizeAuthorCheck(dispatch, alert).then(res => {
                    setButtonVisible(res.success);

                });
            }
            else {
                setIsAdmin(false);
                setIsAuthor(false);
                setIsCustomer(true);
                AnonymizeCustomerCheck(dispatch, alert).then(res => {
                    setButtonVisible(res.success);

                });
            }
        }
        else {
            setLoggedIn(false);
        }
    }, [])

    return(
        <div className="register-user-box row">
            <div className="register-user-left-panel col-sm-4">
                <h3>Ghost Writer Service</h3>
                <div className="left-panel-text">
                    <p>Become an Author on our platform to offer academic work for our customers.</p>
                    <p>Please fill in as much details as possible so that your profile can be dound by porential customers.</p>
                </div>
                
                    
                
                <div className="media_left_panel">
                    <Media object src="/images/FAQ/tos.png" alt="Typewriter image"></Media>
                </div>
            </div>

            <div className="register-user-right-panel col-sm-8">
                <h3>Support</h3>
                <br />

                <h6> Do you need help? </h6>

                <div> Do you have any questions about our service or do you need technical help?
                No problem! You can contact us here.
                Some questions arise more frequently, which is why we have summarized them clearly on our FAQ page.
                    Take a look at our question and answer page. You might find the answer here. </div>
                <br />

                <NavLink tag={Link} className="" to="/ faq">FAQ</NavLink>

                <br />

                <div> Our main goal is user-friendliness.
                    For this reason we are available to you around the clock. Use the following address to get in touch with our support team: </div>

                <br />

                <h6> "[SUPPORT MAIL]" </h6>


                <br />

                {loggedIn ?
                    <div>
                            {isAuthor ?
                            <div>
                                <Button     
                                    className="btn btn-primary btn-lg" disabled={!buttonVisible} onClick={() => setAnonimizeModalIsOpen(true)}>Deactivate account</Button>
                                <Popup
                                    show={anonimizeModalIsOpen}
                                    header='Are you sure you want to deactivate your account'
                                    footerButtons={[{
                                        color: 'white',
                                        backgroundColor: '#4E74DE',
                                        text: "Yes",
                                        onClick: () => {
                                            AnonymizeAuthor(alert);
                                        }
                                    },
                                    {
                                        color: 'white',
                                        backgroundColor: '#4E74DE',
                                        text: "No",
                                        onClick: () => {
                                            setAnonimizeModalIsOpen(false);
                                        }
                                        }]}
                                
                                />
                            </div>
                            :
                            isCustomer?
                            <div>
                                < Button
                                        className="btn btn-primary btn-lg" disabled={!buttonVisible} onClick={() => setAnonimizeModalIsOpen(true)}>Deactivate account</Button>
                                <Popup
                                    show={anonimizeModalIsOpen}
                                    header='Are you sure you want to deactivate your account'
                                    footerButtons={[{
                                        color: 'white',
                                        backgroundColor: '#4E74DE',
                                        text: "Yes",
                                        onClick: () => {
                                            AnonymizeCustomer(alert);
                                        }
                                    },
                                    {
                                        color: 'white',
                                        backgroundColor: '#4E74DE',
                                        text: "No",
                                        onClick: () => {
                                            setAnonimizeModalIsOpen(false);
                                        }
                                    }]}

                                />
                                </div>

                            :
                            <div></div>}
                    </div>
                    :
                    <div>
                    </div>} 
                
            </div>
        </div>
    )
}

export default Support;