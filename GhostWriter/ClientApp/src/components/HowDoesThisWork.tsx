import React from 'react';
import '../styles/HowDoesThisWork.css';
import { ActionCreators } from '../store/LandingPageReducer'
import { useDispatch } from 'react-redux';
import { Media } from 'reactstrap';
import { AiOutlineCloseCircle } from 'react-icons/ai';

export const HowDoesThisWork = () =>
{
    const dispatch = useDispatch();

    return (
        <div className="how-container">

            <p className="text-light mr-2 mt-3">How does this work?<span><AiOutlineCloseCircle style={{ color: 'white', marginLeft: '10px' }} className="cursor" onClick={() => { dispatch(ActionCreators.setValue(false)) }}></AiOutlineCloseCircle></span></p>
            {/*<AiOutlineCloseCircle style={{ color: 'white' }} className="cursor" onClick={() => { dispatch(ActionCreators.setValue(false)) }}></AiOutlineCloseCircle>*/}
            
            <div className="style-how-does row center"> 
                <br></br>
                <div className="column mr-4 icn">
                    <Media object src="/images/HowDoesThisWork/1.png" alt="Typewriter image"></Media>
                    <div className="txt-how">Enter the details for your project (Essay, Thesis, Dissertation...)</div>
                </div>
                <div className="column mr-4">
                    <Media className="" object src="/images/HowDoesThisWork/ar.png" alt="Typewriter image"></Media>
                </div>
                <div className="column mr-4 icn">
                    <Media object src="/images/HowDoesThisWork/2.png" alt="Typewriter image"></Media>
                    <div className="txt-how">Set up your anonymous profile to find perfect author for your work</div>
                </div>
                <div className="column mr-4">
                    <Media className="" object src="/images/HowDoesThisWork/ar.png" alt="Typewriter image"></Media>
                </div>
                <div className="column mr-4 icn">
                    <Media object src="/images/HowDoesThisWork/3.png" alt="Typewriter image"></Media>
                    <div className="txt-how">Optional: fill in project details and let ghostwriters offer you their service</div>
                </div>
                <div className="column mr-4">
                    <Media className="" object src="/images/HowDoesThisWork/ar.png" alt="Typewriter image"></Media>
                </div>
                <div className="column mr-4 icn">
                    <Media object src="/images/HowDoesThisWork/4.png" alt="Typewriter image"></Media>
                    <div className="txt-how">Request ghostwriteres that match your project requirements</div>
                </div>
            </div>
        </div>
    )
}

export default HowDoesThisWork;