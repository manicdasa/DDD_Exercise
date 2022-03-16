import React from 'react';
import { IoInformationCircleSharp } from "react-icons/io5";

import '../../styles/AlertTemplate.css'

export const AlertTemplate = ({ style, options, message, close }: any) =>
{
    //Klase gde ima options.type mogu da budu 
    //new-message-box-info / new-message-box-success / new-message-box-danger
    //tip-icon-info / tip-icon-success / tip-icon-danger
    //tip-box-info / tip-box-success / tip-box-danger
    return(
            <div className="row">
                <div className="col-xs-12 col-sm-6 col-sm-offset-3">
                    <div className="new-message-box" style={{width: '550px'}}>
                         <div className={"new-message-box-"+`${options.type === 'error' ? 'danger' : options.type}}`}>
                        <div className={"info-tab tip-icon-" + `${options.type === 'error' ? 'danger' : options.type}}`} title="error"><i></i></div>
                        <div className={"tip-box-" + `${options.type === 'error' ? 'danger' : options.type}`}> <IoInformationCircleSharp size="24" /> &nbsp;
                                 <p>{message}</p>
                             </div>
                         </div>
                     </div>
                </div>
            </div>)
}

export default AlertTemplate;

