import React, { useState } from 'react';
import { MdModeEdit } from 'react-icons/md';
import { useDispatch, useSelector } from 'react-redux';
import { useHistory } from 'react-router';
import { Button } from 'reactstrap';
import { ActionCreators } from '../../store/LandingPageReducer';
import { RootState } from '../../store/store';

export const CustomerAuthorLandingInfo = () =>
{
    const history = useHistory();
    
    const [callForActionAreaCustomer, setCallForActionAreaCustomer] = useState(false);
    const [callForActionAreaAuthor, setCallForActionAreaAuthor] = useState(false);

    return(
    <div className="px-auto">
        <div className="center mt-5">
            <Button className="button-edit-for-info button-change-password" onClick={() => 
                {  
                    setCallForActionAreaCustomer(!callForActionAreaCustomer) 
                }}><MdModeEdit className="edit-icon"/>&nbsp;Let us write your texts now</Button>
            <Button className="button-edit-for-info button-change-password" onClick={() => 
                { 
                    setCallForActionAreaAuthor(!callForActionAreaAuthor) 
                }}><MdModeEdit className="edit-icon"/>&nbsp;Become an author now!</Button>
        </div>

        <div>
            <div className="row-style">
                {callForActionAreaCustomer ? 
                <div className="rounded style-how-does center column-style mt-3" style={{height: '250px', width: '50%'}}>
                    <p className="text-light p-4 center">Students have to take care of several things at the same time.
                Learn for exams, earn money and then hand in the annoying chores by a set deadline. You lose a lot of your important life. 
                Wouldn't it be possible to have the homework written by experts and get good grades? Our authors will be happy to help you. 
                Here you will find professional and discreet ghostwriters. They write your essays, term papers, or even your theses.
                    </p>
                </div>
                : <div style={{width: '50%', visibility: 'hidden'}}/>}
                {callForActionAreaAuthor ?
                <div className="rounded style-how-does center column-style mt-3" style={{height: '250px', width: '50%'}}>
                    <p className="text-light p-4 center">Show the world what you can do and earn money. 
                    In the ghostwriter community you can sell scientific papers, essays and other texts. 
                    The annoying customer acquisition is no longer necessary for you. Let paying customers contact you directly or apply for suitable projects free of charge. 
                    The choice of projects and challenges is limitless and offers you as an author many choices.
                    </p>
                </div> : <div style={{width: '50%', visibility: 'hidden'}}/>}
            </div>
        </div>

        <div>
        <div className="rounded style-how-does center column-style mt-5">
            <h6 className="text-light mb-2">General information on student authors</h6>
            <p className="text-light p-4 center">Studi-Autoren.de is your marketplace for reliable and serious ghostwriting services. 
                Our authors are checked and deliver your desired texts safely and reliably. You will always have an overview.
                Negotiate with the authors so that you create fair conditions for both sides. Our authors have experience in various scientific fields. 
                You are passionate about writing papers, essays and entire master's theses. The author will keep you up to date while he is working and
                you are always in control.
            </p>
        </div>
        <div>
            <div className="row-style">
                <div className="rounded style-how-does center column-style mt-3" style={{height: '250px', width: '50%'}}>
                    <h6 className="text-light mb-2">Enjoy freedom</h6>
                    <p className="text-light p-4 center">Our authors work very professionally 
                    and of course anonymously. They write your student works and stay in the background. 
                    With an automatic plagiarism check, we ensure that there are no difficulties when submitting your work.
                    You can invest the time you gain in other important projects. For example, in your application for a job after successfully completing your studies.
                    </p>
                </div>
                <div className="rounded style-how-does center column-style mt-3" style={{height: '250px', width: '50%'}}>
                    <h6 className="text-light mb-2">Make extra money</h6>
                    <p className="text-light p-4 center">As an author on Studi-Autoren.de you can earn money on the side. 
                    Turn your personal passion into a job and take advantage of our platform. In a lively exchange with customers, 
                    you can negotiate the best conditions for yourself. You have full room to negotiate.
                    </p>
                </div>
            </div>
        </div>
        <div>
            <div className="row-style">
                <div style={{ width: '50%'}}/>
                <div className="rounded style-how-does column-style center mt-3" style={{width: '50%', height: '265px'}}>
                    <h6 className="text-light mb-2">No annoying customer acquisition</h6>
                    <p className="text-light p-4 center">
                    Probably the most annoying task for freelance writers is acquiring customers. 
                    You are represented at various events and portals and you have to draw attention to yourself. 
                    At Studi-Autoren.de you will be found by your customers. As soon as your profile is completely filled out, 
                    it can be discovered by paying customers. Let yourself be contacted and stop worrying about being underutilized. 
                    In a pool of orders, you also have the opportunity to view advertised orders from customers. 
                    Submit your personal offer here and let the customers decide on a collaboration. Of course you have full control over your actions. 
                    Offers made can be accepted or rejected in advance. You negotiate the price directly with the customer.
                    </p>
                </div>
            </div>
            <div className="mt-4 row-style">    
                <div className="center" style={{width: '50%'}}>
                    <Button onClick={() => { history.push('/customer-read-more')}} className="button-edit-for-info center button-change-password"><MdModeEdit className="edit-icon"/>&nbsp;Read more</Button>
                </div>
                <div className="center" style={{width: '50%'}}>
                    <Button onClick={() => { history.push('/author-read-more')}} className="button-edit-for-info button-change-password"><MdModeEdit className="edit-icon"/>&nbsp;Read more</Button>
                </div>
            </div>
        </div>
        </div> 
        <div className="center mt-5">
            <Button onClick={() => { history.push('/create-project')}} className="button-edit-for-info button-change-password"><MdModeEdit className="edit-icon"/>&nbsp;Create your first order now</Button>
            <Button onClick={() => { window.location.href="/register-author" }}  className="button-edit-for-info button-change-password"><MdModeEdit className="edit-icon"/>&nbsp;Register for free</Button>
        </div>
    </div>)
}

export default CustomerAuthorLandingInfo;