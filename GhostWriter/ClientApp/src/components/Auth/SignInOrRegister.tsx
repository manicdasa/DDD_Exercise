import React, { useState } from 'react'
import '../../styles/SignIn.css'
import { TabContent, TabPane, Row, Col, Nav, NavItem, NavLink, Button, Form, Label, Alert } from 'reactstrap'
import { Link } from 'react-router-dom';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import classnames from 'classnames';
import { LoginUserOnCreateProject, RegisterOnCreateProject, ForgotPassword, EmailConfirmation } from '../../services/UserServices';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { useAlert } from 'react-alert';

const lowercase = /(?=.*[a-z])/;
const uppercase = /(?=.*[A-Z])/;
const numeric = /(?=.*[0-9])/;

let loginSchema = yup.object().shape({
    username: yup.string().required("Required"),
    password: yup.string().required("Required")
});

let passwordSchema = yup.object().shape({
    email: yup.string()
              .lowercase()
              .email("Must be a valid email.")
              .required("Required")
});

let emailConfirmationSchema = yup.object().shape({
    username: yup.string()
        .lowercase()
        .email("Must be a valid email.")
        .required("Required")
});


let registerSchema = yup.object().shape({
    username: yup.string().min(6, "Username must be atleast six characters.").required("Required"),
    email: yup.string()
        .lowercase()
        .email("Must be a valid email.")
        .required("Required"),
    password: yup.string()
        .matches(lowercase, 'One lowercase required.')
        .matches(uppercase, 'One uppercase required.')
        .matches(numeric, 'One numeric simbol required.')
        .min(6, "Minimum 6 characters")
        .required("Required"),
    passwordConfirm: yup.string().oneOf([yup.ref('password')], 'Passwords must be the same').required("Required")
});


export const SignInOrRegister = ({onSuccessLogin} : any) => 
{
    const [activeTab, setActiveTab] = useState('1');
    const dispatch = useDispatch();
    const alert = useAlert();
    const [errors, setErrors] = useState({ Password: [], Username: []});
    const [wrongPassOrUser, setWrongPassOrUser] = useState('');
    const [errorStatePassword, setErrorStatePassword] = useState(false);
    const [errorStateWrong, setErrorStateWrong] = useState(false);
    const [errorStateUsername, setErrorStateUsername] = useState(false);

    const [wrongRegister, setWrongRegister] = useState('');
    const [errorStateRegister, setErrorStateRegister] = useState(false);

    const [requestPasswordChange, setrequestPasswordChange] = useState(false);
    const [requestEmailConfirmation, setrequestEmailConfirmation] = useState(false);

    const toggle = (tab: React.SetStateAction<string>) => 
    {
        if(activeTab !== tab) setActiveTab(tab);
    }    

    const history = useHistory();

    const [registerState, setRegisterState] = useState(false);

    const onRegisterSuccess = () =>
    {
        setRegisterState(true);
    }

    return (

        <div className="login-form-container">

            <div className="switch-buttons">
                <Nav tabs>
                    <NavItem>
                        <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); }} to="#">Sign In</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); }} to="#">Register</NavLink>
                    </NavItem>
                </Nav>
                <TabContent activeTab={activeTab}>
                    <TabPane tabId="1">
                        <Row>
                            <Col sm="12">

                                <div className="login-form-elements-container">

                                    <Formik initialValues={{ username: '', password: '' }}
                                        onSubmit={(values) => LoginUserOnCreateProject(history, dispatch, values, onSuccessLogin, alert)
                                            .then((response) => {
                                                if (response.errors != null) {
                                                    setErrors({ ...errors, Password: response.errors.Password || [], Username: response.errors.Username || [] })
                                                }
                                                else {
                                                    setWrongPassOrUser(response.message);
                                                }
                                                if (response.message != null) { setErrorStateWrong(true); } else { setErrorStateWrong(false); }
                                                if (response.errors.Password != null) { setErrorStatePassword(true); } else { setErrorStatePassword(false); }
                                                if (response.errors.Username != null) { setErrorStateUsername(true); } else { setErrorStateUsername(false); }
                                            })}
                                        validationSchema={loginSchema}>

                                        {({ dirty, isValid, handleSubmit }) => {


                                            return (
                                                <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                                                    <div className="signin-title col-sm-12">Sign in to Ghostwriter service</div>
                                                    <div className="label-element">
                                                        <Label for="username">Username</Label>
                                                    </div>
                                                    <div className="form-element">
                                                        <Field
                                                            variant="outlined"
                                                            placeholder="Username"
                                                            name="username"
                                                            label="Username"
                                                            type="text"
                                                            autoComplete="off"
                                                            required>
                                                        </Field>
                                                        <ErrorMessage component="p" className="field-colorchange" name="username" />
                                                    </div>


                                                    <div className="label-element">

                                                        <Label for="password">Password</Label>
                                                    </div>
                                                    <div className="form-element">

                                                        <Field
                                                            variant="outlined"
                                                            placeholder="Password"
                                                            name="password"
                                                            label="Password"
                                                            type="password"
                                                            autoComplete="off"
                                                            required>
                                                        </Field>
                                                        <ErrorMessage component="p" className="field-colorchange" name="password" />
                                                    </div>
                                                    <div className="frgt-pass">
                                                        <NavLink tag={Link} className={classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); }} to="#">Forgot your password?</NavLink>
                                                    </div>
                                                    <div className="frgt-pass">
                                                        <NavLink tag={Link} className={classnames({ active: activeTab === '4' })} onClick={() => { toggle('4'); }} to="#">Email confirmation</NavLink>
                                                    </div>

                                                    <div className="login-btn">
                                                        { registerState ? <Alert color="success">Registration was succesfull. Please confirm email and sign in to continue.</Alert> : <div/>}
                                                        <Alert id="signin-error" style={{ display: 'none' }} color="danger"></Alert>
                                                        {errorStateWrong ? <Alert color="danger">{wrongPassOrUser}</Alert> : <div></div>}
                                                        {errorStateUsername ? <Alert color="danger">{errors.Username.map(v => <p key={v}>{v}</p>)}</Alert> : <div></div>}
                                                        {errorStatePassword ? <Alert color="danger">{errors.Password.map(v => <p key={v}>{v}</p>)}</Alert> : <div></div>}
                                                        <Button color="primary" disabled={!dirty || !isValid} type="submit">Sign In</Button>
                                                    </div>

                                                </Form>)
                                        }}
                                    </Formik>

                                </div>
                            </Col>
                        </Row>
                    </TabPane>
                    <TabPane tabId="2">

                        <Formik
                            initialValues={{ firstName: '', lastName: '', username: '', email: '', password: '', passwordConfirm: '' }}
                            onSubmit={(values) => {
                                RegisterOnCreateProject(values, onRegisterSuccess, alert).then((res)=>{ setWrongRegister(res); setErrorStateRegister(true)});
                                if(registerState===true)
                                {
                                    setErrorStateRegister(false);
                                    toggle('1');
                                }
                                //RegisterUserMethod(redirectParam, redirectParamFromRB, history, values).then((response) => setErrorMessage(response))
                            }}
                            validationSchema={registerSchema}>

                            {({ values, dirty, isValid, handleSubmit, setFieldValue }) => {


                                return (
                                    <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>

                                        <div className="signin-title col-sm-12">Create account on Ghostwriter</div>
                                        <div className="label-element">
                                            <label htmlFor="register_author_username">Username <span className="lbl-light">(Display name)</span></label>
                                        </div>
                                        <div className="form-element">
                                                <Field
                                                className="form-control"
                                                placeholder="Username"
                                                    name="username"
                                                    type="text"
                                                    //onBlur={() => {
                                                    //    registerSchema.fields.username.isValid(values.username)
                                                    //        .then((response => {
                                                    //            if (response == true) {
                                                    //                CheckUsernameAvailability(values.username, '', '', '')
                                                    //                    .then((response) => { setUsernameStatus(response?.data) })
                                                    //            }
                                                    //            else {
                                                    //                return;
                                                    //            }
                                                    //        }));
                                                    //}}
                                                    autoComplete="off"
                                                    required>
                                                </Field>
                                                <ErrorMessage component="p" className="field-colorchange field-username" name="username" />
                                                {
                                                //    usernameStatus.available ? <div></div> : <div>
                                                //    <p className="field-colorchange">{usernameStatus.message}</p>
                                                //    <p className="field-colorchange">Available usernames: {usernameStatus.usernameSuggestions.map((u, index: number) => <p className="register-username" onClick={() => { setFieldValue("username", u); setUsernameStatus({ available: true, message: '', usernameSuggestions: [] }) }}> {u + (index === usernameStatus.usernameSuggestions.length - 1 ? '' : ",\xa0")} </p>)}</p>
                                                //</div>
                                                }
                                            </div>

                                        <div className="label-element">
                                            <label htmlFor="register_author_email">Email <span className="lbl-light">(Hidden)</span></label>
                                            </div>
                                            <div className="form-element">
                                                <Field
                                                className="form-control"
                                                placeholder="Email"
                                                    name="email"
                                                    type="email"
                                                    //onBlur={() => {
                                                    //    registerSchema.fields.email.isValid(values.email)
                                                    //        .then((response => {
                                                    //            if (response == true) {
                                                    //                CheckEmailAvailability(values.email)
                                                    //                    .then((response) => { setEmailStatus(response?.data) })
                                                    //            }
                                                    //        }));
                                                    //}}
                                                    autoComplete="off"
                                                    required>
                                                </Field>
                                                <ErrorMessage component="p" className="field-colorchange" name="email" />
                                                {
                                                    //emailStatus.feasible ? <div></div> : <p className="field-colorchange">{emailStatus.reason}</p>
                                                }
                                            </div>
                                        

                                        <div className="div-input-row col-sm-12"> </div>

                                        
                                        <div className="label-element">
                                            <Label for="Password">Password</Label>
                                        </div>
                                        <div className="form-element">
                                                <Field
                                                variant="outlined"
                                                placeholder="Password"
                                                    name="password"
                                                    label="Password"
                                                    type="password"
                                                    autoComplete="off"
                                                    required>
                                                </Field>
                                            <ErrorMessage component="p" className="field-colorchange" name="password" />
                                        </div>
                                            
                                        
                                            <div className="label-element">
                                            <Label for="passwordConfirm">Repeat Password</Label>
                                          </div>
                                            <div className="form-element">
                                                <Field
                                                variant="outlined"
                                                placeholder="Repeat Password"
                                                    name="passwordConfirm"
                                                    label="Password Again"
                                                    type="password"
                                                    autoComplete="off"
                                                    required>
                                                </Field>
                                                <ErrorMessage component="p" className="field-colorchange" name="passwordConfirm" />
                                            </div>
                                            

                                        <br></br>
                                        <div className="login-btn">

                                            {
                                                //(errorMessage === "" || errorMessage === undefined) ? <div></div> : <Alert color="danger">{errorMessage}</Alert>
                                            }
                                            
                                                {errorStateRegister ? <Alert color="danger">{wrongRegister}</Alert> : <div></div>}
                                                <Button color="primary" className="btn btn-primary" disabled={!dirty || !isValid} type="submit">Sign Up</Button>
                                            
                                        </div>
                                    </Form>)
                            }}
                        </Formik>
                    </TabPane>
                    <TabPane tabId="3">
                        <div className="tab-3-cont">
                            <div className="pass-frgt-container col-sm-12">
                                <div className="forms">
                                    <div className="signin-title col-sm-12">Forgot your password?</div>
                                    <div className="formik">
                                        <Formik initialValues={{ email: '' }}
                                            onSubmit={(values) => ForgotPassword(history, values.email, alert).then((response) => setrequestPasswordChange(true))}
                                            validationSchema={passwordSchema}>

                                            {({ dirty, isValid, handleSubmit }) => {

                                                return (
                                                    <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                                                        <div className="label-element">
                                                            <Label for="email">Email</Label>
                                                        </div>
                                                        <div className="form-element">
                                                            <Field
                                                                placeholder="Email"
                                                                variant="outlined"
                                                                name="email"
                                                                label="Email"
                                                                type="email"
                                                                autoComplete="off"
                                                                required>
                                                            </Field>
                                                            <ErrorMessage component="p" className="field-colorchange" name="email" />
                                                        </div>
                                                        <div className="login-btn">
                                                            {requestPasswordChange ? <div>
                                                                <div className="password-change-successful">Your request for password change was successful!</div>
                                                            </div> : ''}
                                                            <Button color="primary" disabled={!dirty || !isValid} type="submit">Request password change</Button>
                                                        </div>
                                                    </Form>)
                                            }}
                                        </Formik>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </TabPane>
                    <TabPane tabId="4">
                        <div className="tab-3-cont">
                            <div className="pass-frgt-container col-sm-12">
                                <div className="forms">
                                    <div className="signin-title col-sm-12">Email confirmation</div>
                                    <div className="formik">
                                        <Formik initialValues={{ username: '' }}
                                            onSubmit={(values) => EmailConfirmation(history, values, alert).then((response) => setrequestEmailConfirmation(true))}
                                            validationSchema={emailConfirmationSchema}>

                                            {({ dirty, isValid, handleSubmit }) => {

                                                return (
                                                    <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                                                        <div className="label-element">
                                                            <Label for="username">Email</Label>
                                                        </div>
                                                        <div className="form-element">
                                                            <Field
                                                                placeholder="Email"
                                                                variant="outlined"
                                                                name="username"
                                                                label="Email"
                                                                type="email"
                                                                autoComplete="off"
                                                                required>
                                                            </Field>

                                                            <ErrorMessage component="p" className="field-colorchange" name="username" />
                                                        </div>
                                                        <div className="login-btn">
                                                            {requestEmailConfirmation ? <div>
                                                                <div className="password-change-successful">Your request for email confirmation was successful!</div>
                                                            </div> : ''}
                                                            <Button color="primary" disabled={!dirty || !isValid} type="submit">Request email confirmation</Button>
                                                        </div>
                                                    </Form>)
                                            }}
                                        </Formik>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </TabPane>
                </TabContent>
            </div>
        </div>
    )
}

export default SignInOrRegister;