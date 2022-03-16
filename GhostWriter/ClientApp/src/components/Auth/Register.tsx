import React, { useState }  from 'react'
import { Alert, Button, Form, Label, Media } from 'reactstrap'
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import { NavLink } from 'reactstrap' 
import { Link } from 'react-router-dom';
import { useHistory } from 'react-router-dom';
import { useAlert } from 'react-alert';
import '../../styles/Register.css'
import { RegisterUserMethod, CheckEmailAvailability, CheckUsernameAvailability } from '../../services/UserServices';

const lowercase = /(?=.*[a-z])/;
const uppercase = /(?=.*[A-Z])/;
const numeric = /(?=.*[0-9])/;
const non_aplhanumeric = /(?=.*[^a-zA-Z0-9])/;

let registerSchema = yup.object().shape({
    username: yup.string().min(7, "Username must be atleast seven characters.").required("Required"),
    email: yup.string()
                .lowercase()
                .email("Must be a valid email.")
                .required("Required"),
    password: yup.string()
                .matches(lowercase, 'One lowercase required.')
                .matches(uppercase, 'One uppercase required.')
                .matches(numeric, 'One numeric simbol required.')
                .matches(non_aplhanumeric, 'One non-alphanumeric simbol is required (ex. {$,!,@,-...})')
                .min(6, "Minimum 6 characters")
                .required("Required"),
    passwordConfirm: yup.string().oneOf([yup.ref('password')], 'Passwords must be the same').required("Required")
});

export const Register = () => 
{
    const history = useHistory();
    const alert = useAlert();
    const [errorMessage, setErrorMessage] = useState("");

    const [usernameStatus, setUsernameStatus] = useState({ available: true, message: '', usernameSuggestions: [] });
    const [emailStatus, setEmailStatus] = useState({ feasible: true, reason: '' })

    const [showUsernameMessageError, setUsernameMessageError] = useState(false);

    return (
        <div className="register-user-box row">

            <div className="register-user-left-panel col-sm-4">
                <h3>Create Account</h3>
                <div className="left-panel-text">
                    <p>Create a customer account at Ghostwritters.com and you will be able create project and hire Ghost Writters for your next big assignment.</p>
                </div>
                <div className="media_left_panel">
                    <Media object src="/images/RegisterUser/user_register.png" alt="Typewriter image"></Media>
                </div>

            </div>

            <div className="register-user-right-panel col-sm-8">
                <h3>Create User Account</h3>
                <br></br>
                <Formik
                    initialValues={{ username: '', email: '', password: '', passwordConfirm: '' }}
                    onSubmit={(values) => {
                        RegisterUserMethod(history, values, alert).then((response) => setErrorMessage(response))
                    }}
                    validationSchema={registerSchema}>

                    {({ values, dirty, isValid, handleSubmit, setFieldValue }) => {


                        return (
                            <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>

                                <div className="div-input-row col-sm-12">
                                    <div className="div-input-column col-sm-6">
                                        <label htmlFor="register_author_username">Username (<strong>Display name</strong>)</label>
                                        <Field
                                            className="form-control"
                                            name="username"
                                            type="text"
                                            onBlur={() => {
                                                registerSchema.fields.username.isValid(values.username)
                                                    .then((response => {
                                                        if (response == true) {
                                                            setUsernameMessageError(false);
                                                            CheckUsernameAvailability(values.username, '', '', '', alert)
                                                                .then((response) => { setUsernameStatus(response?.data) })
                                                        }
                                                        else {
                                                            setUsernameMessageError(true);
                                                        }
                                                    }));
                                            }}
                                            autoComplete="off"
                                            required>
                                        </Field>
                                       { showUsernameMessageError ? <p className="field-colorchange field-username">Username must be atleast seven characters.</p> : "" } 
                                        <ErrorMessage component="p" className="field-colorchange field-username" name="username" />
                                        {usernameStatus.available ? <div></div> : <div>
                                            <p className="field-colorchange">{usernameStatus.message}</p>
                                            <p className="field-colorchange">Available usernames: {usernameStatus.usernameSuggestions.map((u, index: number) => <p className="register-username" onClick={() => { setFieldValue("username", u); setUsernameStatus({ available: true, message: '', usernameSuggestions: [] }) }}> {u + (index === usernameStatus.usernameSuggestions.length - 1 ? '' : ",\xa0")} </p>)}</p>
                                        </div>
                                        }
                                    </div>

                                    <div className="div-input-column col-sm-6 eml-user">
                                        <label htmlFor="register_author_email">Email (<strong>Hidden</strong>)</label>
                                        <Field
                                            className="form-control"
                                            name="email"
                                            type="email"
                                            onBlur={() => {
                                                registerSchema.fields.email.isValid(values.email)
                                                    .then((response => {
                                                        if (response == true) {
                                                            CheckEmailAvailability(values.email, alert)
                                                                .then((response) => { setEmailStatus(response?.data) })
                                                        }
                                                    }));
                                            }}
                                            autoComplete="off"
                                            required>
                                        </Field>
                                        <ErrorMessage component="p" className="field-colorchange" name="email" />
                                        {emailStatus.feasible ? <div></div> : <p className="field-colorchange">{emailStatus.reason}</p>}
                                    </div>
                                </div>

                                <div className="div-input-row col-sm-12 reg-usr-row"> </div>

                                <div className="div-input-row col-sm-12">
                                    <div className="div-input-column col-sm-6">
                                        <Label for="Password">Password</Label>
                                        <Field className="form-input-field-ghw"
                                            variant="outlined"
                                            name="password"
                                            label="Password"
                                            type="password"
                                            autoComplete="off"
                                            required>
                                        </Field>
                                        <ErrorMessage component="p" className="field-colorchange" name="password" />
                                    </div>

                                    <div className="div-input-column col-sm-6">
                                        <Label for="passwordConfirm">Password Again</Label>
                                        <Field
                                            className="form-input-field-ghw"
                                            variant="outlined"
                                            name="passwordConfirm"
                                            label="Password Again"
                                            type="password"
                                            autoComplete="off"
                                            required>
                                        </Field>
                                        <ErrorMessage component="p" className="field-colorchange" name="passwordConfirm" />
                                    </div>
                                </div>

                                <br></br>
                                <div className="button-center col-sm-12">
                                    <div className="col-sm-6">
                                        <NavLink tag={Link} className="txt-link-ghw" to={{ pathname:"/login", redParam: true, redirectParamFromRB: true }}>Sign in instead</NavLink>
                                    </div>

                                    {(errorMessage === "" || errorMessage === undefined) ? <div></div> : <Alert color="danger">{errorMessage}</Alert>}
                                    <div className="col-sm-6 reg-usr-btn">
                                        <Button color="primary" className="btn btn-primary" disabled={!dirty || !isValid} type="submit">Sign Up</Button>
                                    </div>
                                </div>
                            </Form>)
                    }}
                </Formik>
            </div>
        </div>
    )
}

export default Register;