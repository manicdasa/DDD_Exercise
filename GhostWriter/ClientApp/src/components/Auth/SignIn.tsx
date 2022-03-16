import React, { useState, useEffect } from 'react'
import '../../styles/SignIn.css'
import { TabContent, TabPane, Row, Col, Nav, NavItem, NavLink, Button, Media, Form, Label, Alert } from 'reactstrap'
import { Link } from 'react-router-dom';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import classnames from 'classnames';
import { LoginUser, LoginAuthor, ForgotPassword, EmailConfirmation } from '../../services/UserServices';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { useAlert } from 'react-alert';
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

export const SignIn = () => 
{
    const [activeTab, setActiveTab] = useState('1');
    const dispatch = useDispatch();
    const alert = useAlert();
    const [errors, setErrors] = useState({ Password: [], Username: []});
    const [wrongPassOrUser, setWrongPassOrUser] = useState('');
    const [errorStatePassword, setErrorStatePassword] = useState(false);
    const [errorStateWrong, setErrorStateWrong] = useState(false);
    const [errorStateUsername, setErrorStateUsername] = useState(false);
    
    const [errorsAuthor, setErrorsAuthor] = useState({ Password: [], Username: []});
    const [wrongPassOrUserAuthor, setWrongPassOrUserAuthor] = useState('');
    const [errorStatePasswordAuthor, setErrorStatePasswordAuthor] = useState(false);
    const [errorStateWrongAuthor, setErrorStateWrongAuthor] = useState(false);
    const [errorStateUsernameAuthor, setErrorStateUsernameAuthor] = useState(false);

    const [requestPasswordChange, setrequestPasswordChange] = useState(false);
    const [requestEmailConfirmation, setrequestEmailConfirmation] = useState(false);

    const toggle = (tab: React.SetStateAction<string>) => 
    {
        if(activeTab !== tab) setActiveTab(tab);
    }   

    const history = useHistory();

    useEffect(()=>
        {
            if(localStorage.getItem('role=Ghostwriter') != undefined)
            {
                window.location.href = "/profile";
            }
            else if(localStorage.getItem('role=Customer') != undefined)
            {
                window.location.href = "/customer-profile";
            }
            else if(localStorage.getItem('role=Admin') != undefined)
            {   
                window.location.href = "/dashboard";
            }
        }, [])

    return (
        <div className="register-signin-box row">
            <div className="register-signin-left-panel col-sm-4">
                <h3>Discover Top Ghost Writers</h3>
                <div className="left-panel-text">
                
                    <p>Create a customer account at GhostWritters.com and you will be able to create projects and hire Ghost Writters for your next big assignment.</p>
                </div>
                    <div className="media_left_panel">
                    <Media object src="/images/Login/login_image.png" alt="Typewriter image"></Media>
                        </div>
            
            </div>
            
            <div className="register-signin-right-panel col-sm-8">
                <div className="form-container-">
                <h3 className="login-form-title">Sing in to GHW</h3>
                    <div className="login-form-container">
                        
                        <div className="switch-buttons">
                            <Nav className="login-form-nav" tabs>
                        <NavItem>
                            <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); }} to="#">Sign In as User</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); }} to="#">Sign In as Author</NavLink>
                        </NavItem>
                    </Nav>
                    <TabContent activeTab={activeTab}>
                        <TabPane tabId="1">
                            <Row>
                                <Col sm="12">
                                    <div className="login-title">Create Projects and Find Ghostwritters</div>
                                        
                                    <div className="login-form-elements-container">
                                                
                                        <Formik initialValues={{username: '', password: ''}}
                                            onSubmit={(values)=> LoginUser(history, dispatch, values, alert)
                                                                    .then((response)=>
                                                                    {
                                                                        if(response.errors != null)
                                                                        {
                                                                            setErrors({...errors, Password: response.errors.Password || [], Username: response.errors.Username || []}) 
                                                                        }
                                                                        else
                                                                        {
                                                                            setWrongPassOrUser(response.message);
                                                                        }
                                                                        if(response.message != null) { setErrorStateWrong(true); } else { setErrorStateWrong(false); }
                                                                        if(response.errors.Password != null) { setErrorStatePassword(true); } else { setErrorStatePassword(false); }
                                                                        if(response.errors.Username != null) { setErrorStateUsername(true); } else { setErrorStateUsername(false); } 
                                                                    })}
                                            validationSchema={loginSchema}>

                                        {({ dirty, isValid, handleSubmit }) => {
                

                                        return(
                                            <Form onSubmit={(e)=>{e.preventDefault(); handleSubmit(); }}>
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
                                                <ErrorMessage component="p" className="field-colorchange" name="username"/>
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
                                                <ErrorMessage component="p" className="field-colorchange" name="password"/>
                                                </div>
                                                <div className="frgt-pass">
                                                    <NavLink tag={Link} className={classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); }} to="#">Forgot your password?</NavLink>
                                                </div>
                                                <div className="frgt-pass email-confitm">
                                                    <NavLink tag={Link} className={classnames({ active: activeTab === '4' })} onClick={() => { toggle('4'); }} to="#">Email confirmation</NavLink>
                                                </div>

                                                <div className="login-btn"> 
                                                    <Alert id="signin-error" style={{display: 'none'}} color="danger"></Alert>
                                                    { errorStateWrong ? <Alert color="danger">{wrongPassOrUser}</Alert> : <div></div> }
                                                    { errorStateUsername ? <Alert color="danger">{errors.Username.map(v=> <p key={v}>{v}</p>) }</Alert> : <div></div> }
                                                    { errorStatePassword ? <Alert color="danger">{errors.Password.map(v=> <p key={v}>{v}</p>) }</Alert> : <div></div> }

                                                    <div className="space-"></div>
                                                    <div className="row-style mt-2 signin-creataccount">
                                                        <div className="create-acc-usr-btn">
                                                            <Button color="primary" className="btn-signin" disabled={!dirty || !isValid} type="submit">Sign In</Button>
                                                            </div>
                                                        <div className="create-acc-usr-link">
                                                            <span className="span-notmem-txt">Not a member?</span><br></br><span onClick={() => history.push('/register')} className='accept-tos'>Create User Account now!</span>
                                                            </div>
                                                    </div>
                                                    </div>

                                            </Form>)}}
                                        </Formik>
                                        
                                    </div>
                                </Col>
                            </Row>
                        </TabPane>
                        <TabPane tabId="2">
                            <Row>
                                        <Col sm="12">
                                            <div className="login-title">Sign In to Offer Ghostwriting Services</div>
                                            <div className="login-form-elements-container">
                                                
                                        <Formik initialValues={{username: '', password: ''}}
                                            onSubmit={(values)=> LoginAuthor(history, dispatch, values, alert)
                                                                    .then((response)=>
                                                                    {
                                                                        if(response.errors != null)
                                                                        {
                                                                            setErrorsAuthor({...errors, Password: response.errors.Password || [], Username: response.errors.Username || []}) 
                                                                        }
                                                                        else
                                                                        {
                                                                            setWrongPassOrUserAuthor(response.message);
                                                                        }
                                                                        if(response.message != null) { setErrorStateWrongAuthor(true); } else { setErrorStateWrongAuthor(false); }
                                                                        if(response.errors.Password != null) { setErrorStatePasswordAuthor(true); } else { setErrorStatePasswordAuthor(false); } 
                                                                        if(response.errors.Username != null) { setErrorStateUsernameAuthor(true); } else { setErrorStateUsernameAuthor(false); } 
                                                                    })}
                                            validationSchema={loginSchema}>

                                        {({ dirty, isValid, handleSubmit}) => {
                

                                        return(
                                            <Form onSubmit={(e)=>{ e.preventDefault(); handleSubmit(); }}>
                                                <div className="label-element">
                                                    <Label for="username">Username</Label>
                                                </div>
                                                <div className="form-element">
                                                <Field
                                                    variant="outlined" 
                                                    placeholder="Username"
                                                    name="username" 
                                                    type="text"
                                                    label="Username" 
                                                    autoComplete="off" 
                                                    required>      
                                                </Field>
                                                <ErrorMessage component="p" className="field-colorchange error" name="username"/>
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
                                                <ErrorMessage component="p" className="field-colorchange" name="password"/>
                                                </div>
                                                <div className="frgt-pass">
                                                    <NavLink tag={Link} className={classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); }} to="#">Forgot your password?</NavLink>
                                                </div>
                                                <div className="frgt-pass email-confitm">
                                                    <NavLink tag={Link} className={classnames({ active: activeTab === '4' })} onClick={() => { toggle('4'); }} to="#">Email confirmation</NavLink>
                                                </div>

                                                <div className="login-btn">
                                                    <Alert id="signin-errorauthor" style={{display: 'none'}} color="danger"></Alert>
                                                    { errorStateWrongAuthor ? <Alert color="danger">{wrongPassOrUserAuthor}</Alert> : <div></div> }
                                                    { errorStateUsernameAuthor ? <Alert color="danger">{errorsAuthor.Username.map(v=> <p key={v}>{v}</p>) }</Alert> : <div></div> }
                                                    { errorStatePasswordAuthor ? <Alert color="danger">{errorsAuthor.Password.map(v=> <p key={v}>{v}</p>) }</Alert> : <div></div> }
                                                    
                                                    <div className="space-"></div>
                                                    <div className="row-style mt-2 signin-creataccount">
                                                        <div className="create-acc-usr-btn">
                                                            
                                                                <Button color="primary" className="btn-signin" disabled={!dirty || !isValid} type="submit">Sign In</Button>
                                                            </div>
                                                        <div className="create-acc-usr-link">
                                                            <span className="span-notmem-txt">Not a member?</span><br></br><span onClick={() => window.location.href="/register-author"} className='accept-tos'>Create Author Account now!</span>
                                                            </div>

                                                    </div>
                                                    </div>
                                            </Form>)}}
                                        </Formik>
                                    </div>
                                </Col>
                            </Row>
                        </TabPane>
                                <TabPane tabId="3">
                                    <div className="tab-3-cont">
                                        <div className="pass-frgt-container col-sm-12">
                                    <div className="forms">
                                                <div className="lost-pass-title">Forgot your password?</div>
                                                <div className="formik">
                                        <Formik initialValues={{email: ''}}
                                                onSubmit={(values) => ForgotPassword(history, values.email, alert).then((response) => setrequestPasswordChange(true))}
                                                validationSchema={passwordSchema}>

                                        {({ dirty, isValid, handleSubmit }) => {
                
                                        return(
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
                                                    
                                                <ErrorMessage component="p" className="field-colorchange" name="email"/>
                                                </div>
                                                <div className="login-btn">
                                                    {requestPasswordChange ? <div>
                                                        <div className="password-change-successful">Your request for password change was successful!</div>                                                       
                                                    </div> : ''}
                                                    <Button color="primary" disabled={!dirty || !isValid} type="submit">Request password change</Button>
                                                </div>
                                            </Form>)}}
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
                                                <div className="lost-pass-title">Email confirmation</div>
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
                </div>
            </div>
            
        </div>
    )
}

export default SignIn;