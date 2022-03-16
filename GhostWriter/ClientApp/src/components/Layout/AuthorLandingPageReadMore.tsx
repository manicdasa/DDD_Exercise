import React from 'react';
import { Media } from 'reactstrap';

export const AuthorLandingPageReadMore = () =>
{
    return(
    <div className="register-user-box row">
        <div className="register-user-left-panel col-sm-4">
            <div className="media_left_panel">
                <Media object src="/images/FAQ/tos.png" alt="Typewriter image"></Media>
            </div>
        </div>

        <div className="register-user-right-panel col-sm-8">
            <h3>Appreciative offers for authors</h3>
            <br></br>

            <div>It is not always easy for people who see their passion in writing and want to turn their hobby into a job. After all, they have to assert 
                themselves in the market and acquire customers. Acquisition is a tough process and, despite the effort, does not always lead to success. 
                We will be happy to help you make your acquisition process easier. There are solvent customers on our platform who are just waiting to place
                an order with you. Let yourself be found or actively search for your desired jobs in our job pool.
            </div>
            <br></br>

            <h6 className="mt-4">Get serious jobs</h6>

            <div className="mt-2">We work seriously and discreetly. For you as an author, this means that we guarantee your payment according to our terms and conditions. 
                With us, only customers can post their projects whose solvency has been checked. As soon as the customer has accepted your work, you will receive your fee.</div>

            <h6 className="mt-4">Get your approval</h6>

            <div className="mt-2">Ghostwriting is a business where you stay in the background. If references are asked for, it is difficult to provide them. 
                After all, you are bound to secrecy. On our platform you can collect your references and present them to your customers. Receive
                 ratings for your work and present them on your profile. Show them to other customers and build your image in our community.</div>
                 
            <h6 className="mt-4">Exchange with the customer</h6>
            <div className="mt-2">
            So that the briefing is clear, we have developed a form that should leave no questions unanswered. However, detailed questions often need to be clarified. 
            In order to guarantee you and the customer the best possible usability, we have developed an easy-to-use chat solution for you. Here you can clarify questions about 
            the housework and, if necessary, renegotiate. We give you a lot of freedom for a smooth cooperation.
            </div>
        </div>
    </div>)
}

export default AuthorLandingPageReadMore;