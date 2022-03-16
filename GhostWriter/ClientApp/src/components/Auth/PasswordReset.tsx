import React, { useState, useEffect } from 'react'
import { Alert, Button, Form, Media } from 'reactstrap'
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import { useHistory, useLocation } from 'react-router-dom';
import { useAlert } from 'react-alert';
import '../../styles/Register.css'
import "../../styles/RegisterAuthor.css"
import '../../styles/CreateProject.css'
import { ResetPassword } from '../../services/UserServices';

const lowercase = /(?=.*[a-z])/;
const uppercase = /(?=.*[A-Z])/;
const numeric = /(?=.*[0-9])/;

let registerSchema = yup.object().shape({
    username: yup.string().required("Required"),
    password: yup.string()
        .matches(lowercase, 'One lowercase required.')
        .matches(uppercase, 'One uppercase required.')
        .matches(numeric, 'One numeric simbol required.')
        .min(6, "Minimum 6 characters")
        .required("Required"),
    passwordConfirm: yup.string().oneOf([yup.ref('password')], 'Passwords must be the same').required("Required")
});

const useQueryParams = () => {
    const location = useLocation();
    return new URLSearchParams(location.search);
}

export const PasswordReset = () => {

    const history = useHistory();  
    const alert = useAlert();
    const queryParams = useQueryParams();

    const [errorMessage, setErrorMessage] = useState("");

    const [passwordChangeCompleted, setpasswordChangeCompleted] = useState<boolean>(false);

    useEffect(() => {
        if (queryParams.get('token') === null) {
            history.push('/');
        }
    });

    return (
        <div className="register-user-box row">

            <div className="register-user-left-panel col-sm-4">
                <h3>Password reset</h3>
                <div className="left-panel-text">
                    <p>Please fill in the required entries and your password will be reset.</p>
                </div>
                <div className="media_left_panel">
                    <Media object src="/images/RegisterUser/user_register.png" alt="Typewriter image"></Media>
                </div>

            </div>

            <div className="register-user-right-panel col-sm-8">
                <h3>Reset User Password</h3>                                             

                <br></br>
                <Formik
                    initialValues={{ username:  queryParams.get('username'), password: '', passwordConfirm: '' }}
                    onSubmit={(values) => ResetPassword(history, { ...values, token: queryParams.get('token') }, alert).then((response) => setpasswordChangeCompleted(true))}
                    validationSchema={registerSchema}>

                    {({ dirty, isValid, handleSubmit }) => {

                       return (
                            <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>                            
 
                                <div className="form-group-username col-sm-6">
                                    <label htmlFor="register_author_username">Username</label>
                                    <Field
                                        className="form-control"
                                        disabled={true}
                                        name="username"
                                        type="text"                                       
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange field-username" name="username" />                                                    
                                </div> 

                                <div className="div-input-row col-sm-12"></div>                                     
                                                              
                                <div className="form-group-pass col-sm-6">
                                    <label htmlFor="register_author_password">Password</label>
                                    <Field
                                        className="form-control"
                                        name="password"
                                        type="password"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="password" />
                                </div>

                                <div className="form-group-pass col-sm-6">
                                    <label htmlFor="register_author_password_repeat">Repeat Password</label>
                                    <Field
                                        className="form-control"
                                        name="passwordConfirm"
                                        type="password"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="passwordConfirm" />
                                </div>                                                                                                                         

                                <br></br> 
                                <div className="button-center button-position col-sm-12">      
                                    {passwordChangeCompleted ? <div>
                                        <div className="password-change-successful">Your payment was successful!</div>
                                        <div className="payment-successful-frontPage" onClick={() => history.push("/")} >Click here to go to Home page </div>
                                    </div> : ''}
                                    {(errorMessage === "" || errorMessage === undefined) ? <div></div> : <Alert color="danger">{errorMessage}</Alert>}
                                    <div className="col-sm-6"></div>
                                    <div className="col-sm-6">
                                        <Button color="primary" className="btn btn-primary" disabled={!dirty || !isValid} type="submit">Reset</Button>
                                    </div>                                   
                                </div>
                            </Form>)
                    }}
                </Formik>
            </div>
        </div>
    )
}

export default PasswordReset;