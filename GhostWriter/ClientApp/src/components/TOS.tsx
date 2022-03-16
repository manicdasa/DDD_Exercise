import React from 'react';
import { Media } from 'reactstrap';

import '../styles/CreateProject.css'

export const TOS = () =>
{
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
                <h3>Terms of service</h3>
                <br></br>

                <h6>What is Lorem Ipsum?</h6>

                <div>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been
                the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of
                type and scrambled it to make a type specimen book. It has survived not only five centuries, but also
                the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s
                with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop
                publishing software like Aldus PageMaker including versions of Lorem Ipsum.</div>
                <br></br>

                <h6>Where does it come from?</h6>

                <div>Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of
                classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin
                professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words,
                consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical
                literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and
                1.10.33 of "de Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written
                in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance.
                The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", comes from a line in section 1.10.32.</div>

                
            </div>
        </div>
    )
}

export default TOS;