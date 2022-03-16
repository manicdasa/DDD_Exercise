import React, { useState } from 'react'
import { Form, Alert } from 'reactstrap'
import { GrUserAdmin, GrUserExpert } from 'react-icons/gr';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import { useHistory } from 'react-router-dom'

import { AdminLoginMethod } from '../../services/AdminServices';
import '../../styles/AdminLogin.css'

let loginSchema = yup.object().shape({
    username: yup.string().required("Required"),
    password: yup.string().required("Required")
});


export const AdminLogin = () => 
{
    const history = useHistory();

    const [errors, setErrors] = useState({ Password: [], Username: []});
    const [wrongPassOrUser, setWrongPassOrUser] = useState('');
    const [errorStatePassword, setErrorStatePassword] = useState(false);
    const [errorStateWrong, setErrorStateWrong] = useState(false);
    const [errorStateUsername, setErrorStateUsername] = useState(false);

    return (<div className="wrapper1-admin-login">
        <div id="formContent-admin-login">
            <div className="login-admin-title"> <h3>Login to Admin Area</h3><p className="admin-subtitle-txt">GhostWriter Dashboard</p></div>
                <div className="user-icon-admin">
                <GrUserExpert opacity="1"/>
                </div>
                <Formik initialValues={{username: '', password: ''}}
                                            onSubmit={(values)=> AdminLoginMethod(history, values, alert)
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
                                            <div className="column-style center">
                                            <Field
                                                className="input-buttons-admin-login usrname"
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
                                                <div className="user-icon-admin-pass">
                                                    <GrUserAdmin opacity="1" />
                                                </div>
                                            <div className="column-style center">
                                            <Field 
                                                className="input-buttons-admin-login"
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
                                            <br/>
                                            <Alert id="signin-error" style={{display: 'none'}} color="danger"></Alert>
                                                { errorStateWrong ? <Alert color="danger">{wrongPassOrUser}</Alert> : <div></div> }
                                                { errorStateUsername ? <Alert color="danger">{errors.Username.map(v=> <p key={v}>{v}</p>) }</Alert> : <div></div> }
                                                { errorStatePassword ? <Alert color="danger">{errors.Password.map(v=> <p key={v}>{v}</p>) }</Alert> : <div></div> }
                                            <input type="submit" className="btn btn-primary admin-login-btn" disabled={!dirty || !isValid} value="Log In"/>
                                            </Form>)}}
                                            </Formik>
  
                </div>
            </div>
    )
}

export default AdminLogin;