import React from 'react';
import { Media } from 'reactstrap';

export const CustomerLandingPageReadMore = () =>
{
    return(
    <div className="register-user-box row">
        <div className="register-user-left-panel col-sm-4">
            <div className="media_left_panel">
                <Media object src="/images/FAQ/tos.png" alt="Typewriter image"></Media>
            </div>
        </div>

        <div className="register-user-right-panel col-sm-8">
            <h3>Your work - our job</h3>
            <br></br>

            <div>We know all too well the problem of timing that many students experience.
                 We always have different tasks and have to take care of some things at the same time. 
                 This involves tedious tasks such as writing homework. These take up most of the time and tie us to the desk.
            </div>
            <br></br>

            <h6 className="mt-4">Our solution</h6>

            <div className="mt-2">This is over with a ghostwriter! Let our experienced ghostwriters write your text. They have already written countless texts and 
            are experts in their field. Since you already have an overview of scientific literature, you can work time-efficiently and reliably. This ensures that you have a 
            perfectly crafted text by the deadline.</div>

            <h6 className="mt-4">Create your project</h6>

            <div className="mt-2">Posting your project is free of charge. Set up your project and wait for suitable ghostwriters to apply for your text. 
            Then you choose the author who best fits your specifications. This works quickly and unbureaucratically via an easy-to-use interface. Thanks 
            to our intuitive chat function, you can be in contact with your ghostwriter at any time. Inquire about the status of your work and make sure 
            everything is going according to plan. You have the best possible control.</div>
                 
            <h6 className="mt-4">You determine the price</h6>
            <div className="mt-2">
            We know that your budget has to stay within a certain framework. Therefore, you set your budget for your housework right from the start. 
            The author and we stick to the price you would like to pay when you discontinue your project. We and our authors work transparently and openly show you our prices. 
            Renegotiations are possible, we communicate these openly. 
            <p className="font-weight-bold">There are no hidden costs!</p>
            </div>      

            <h6 className="mt-4">Don't be afraid of plagiarism</h6>
            <div className="mt-2">
            Our authors work professionally and reliably. Every text that you commission is unique and is written especially 
            for you by our authors. In order to keep this promise of quality, we automatically check every text that is sold.
            </div>          
        </div>
    </div>)
}

export default CustomerLandingPageReadMore;