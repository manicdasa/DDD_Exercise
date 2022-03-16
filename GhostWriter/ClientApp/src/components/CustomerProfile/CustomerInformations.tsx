import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Button, NavItem, NavLink, Form, Label, Alert } from 'reactstrap';
import { Link } from 'react-router-dom';
import Rating from '@material-ui/lab/Rating';
import { AiFillCheckCircle } from 'react-icons/ai';
import { BiWorld } from 'react-icons/bi';
import { FaEuroSign, FaProjectDiagram } from 'react-icons/fa';
import { MdCreateNewFolder, MdLockOutline } from "react-icons/md";
import { Popup } from 'react-chat-elements'
import { Formik, ErrorMessage, Field } from 'formik';
import * as yup from 'yup';
import { useHistory } from 'react-router-dom';
import NumberFormat from 'react-number-format';
import { useAlert } from 'react-alert'; 

import { GoBook } from 'react-icons/go';
import { MdLibraryBooks } from 'react-icons/md';

import { GetCustomerInformations } from '../../services/CustomerServices';
import { PasswordChange } from '../../services/UserServices';
import '../../styles/CustomerInformations.css'
import { RiErrorWarningFill, RiBarChartBoxFill, RiFolderChartFill, RiBarChart2Fill, RiDraftFill} from 'react-icons/ri';

const lowercase = /(?=.*[a-z])/;
const uppercase = /(?=.*[A-Z])/;
const numeric = /(?=.*[0-9])/;

let changePasswordSchema = yup.object().shape({
    password: yup.string().required("Required"),
    newPassword: yup.string()
        .matches(lowercase, 'One lowercase required.')
        .matches(uppercase, 'One uppercase required.')
        .matches(numeric, 'One numeric simbol required.')
        .min(6, "Minimum 6 characters")
        .required("Required"),
    passwordConfirm: yup.string().oneOf([yup.ref('newPassword')], 'Passwords must be the same').required("Required")
});


export const CustomerInformations = () =>
{    
    const [errors, setErrors] = useState({ OldPassword: [], NewPassword: [] });
    const [wrongOldPass, setWrongOldPass] = useState('');
    const [errorStateWrong, setErrorStateWrong] = useState(false);
    const [errorStateOldPassword, setErrorStateOldPassword] = useState(false);
    const [errorStateNewPassword, setErrorStateNewPassword] = useState(false);

    const showChangePasswordPopup = () =>
    {
        setchangePasswordModalIsOpen(false);
    }

    const alert = useAlert();

    let popupContent =

        <div className="switch-buttons">
            <div className="login-form-elements-container chng-pass-container">
                <Formik initialValues={{ password: '', newPassword: '', passwordConfirm: '' }}
                        onSubmit={(values) => PasswordChange(values.password, values.newPassword, showChangePasswordPopup, alert)
                        .then((response) =>
                        {
                            if (response.success == false) 
                            {
                                if (response.errors != null) {
                                    setErrors({ ...errors, OldPassword: response.errors.OldPassword || [], NewPassword: response.errors.NewPassword || [] })
                                }
                                else {
                                    setWrongOldPass(response.message);
                                }
                                if (response.message != null) { setErrorStateWrong(true); } else { setErrorStateWrong(false); }
                                if (response.errors?.OldPassword != null) { setErrorStateOldPassword(true); } else { setErrorStateOldPassword(false); }
                                if (response.errors?.NewPassword != null) { setErrorStateNewPassword(true); } else { setErrorStateNewPassword(false); }
                            }
                        })}
                    validationSchema={changePasswordSchema}>

                    {({ dirty, isValid, handleSubmit }) => {

                        return (
                            <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>

                                <div className="label-element">
                                    <Label for="oldPassword">Old Password</Label>
                                    <Field
                                        className="form-control chng-pass"
                                        variant="outlined"
                                        placeholder="Password"
                                        name="password"
                                        label="oldPassword"
                                        type="password"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="oldPassword" />
                                </div>

                                <p className="del">--</p>
                                <div className="label-element">
                                    <Label for="newPassword">New Password</Label>
                                    <Field
                                        className="form-control chng-pass"
                                        variant="outlined"
                                        placeholder="New Password"
                                        name="newPassword"
                                        label="NewPassword"
                                        type="password"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="newPassword" />
                                </div>

                                <p className="del">--</p>
                                <div className="label-element">
                                    <Label for="passwordConfirm">Repeat New Password</Label>
                                    <Field
                                        className="form-control chng-pass"
                                        variant="outlined"
                                        placeholder="New Password"
                                        name="passwordConfirm"
                                        label="passwordConfirm"
                                        type="password"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="passwordConfirm" />
                                </div>
                                
                                <p className="del">--</p>
                                <div className="btncreate">
                                    <Alert id="signin-error" style={{ display: 'none' }} color="danger"></Alert>
                                    {errorStateWrong ? <Alert color="danger">{wrongOldPass}</Alert> : <div></div>}
                                    {errorStateOldPassword ? <Alert color="danger">{errors.OldPassword.map(v => <p key={v}>{v}</p>)}</Alert> : <div></div>}
                                    {errorStateNewPassword ? <Alert color="danger">{errors.NewPassword.map(v => <p key={v}>{v}</p>)}</Alert> : <div></div>}
                                    <br></br>
                                    <button type="submit" className="btn btn-primary" disabled={!dirty || !isValid} >Confirm</button>
                                    
                                </div>

                            </Form>)
                    }}
                </Formik>
            </div>
        </div>

    const dispatch = useDispatch();
  
    const customer = useSelector((state: any) => state.customerReducer.customerInformations);

    const [changePasswordModalIsOpen, setchangePasswordModalIsOpen] = useState(false);

    useEffect(()=>
    {
        GetCustomerInformations(dispatch, alert);
    }, []);

    return(
            <div className="column-style container-stats">
                <div className="" style={{ width: '100%'}}>
                    <div className="row-style customer-username ">
                        <h3>{customer.username}</h3>
                    <Button className="button-edit" onClick={() => setchangePasswordModalIsOpen(true)}><MdLockOutline className="edit-icon" />&nbsp;Change password</Button>

                    <Popup
                        show={changePasswordModalIsOpen}
                        header='Change Password'
                        headerButtons={[{
                            type: '',
                            color: 'black',
                            text: 'X',
                            className: 'close-btn-modal',
                            onClick: () => {
                                setchangePasswordModalIsOpen(false);
                                setErrorStateWrong(false);
                                setErrorStateOldPassword(false);
                                setErrorStateNewPassword(false);
                            }
                        }]}                        
                        renderContent={() => { return popupContent }}
                     />

                    </div>
                </div>
            <div className="row-style rating">
                    <div className="column-style center-style">                        
                        <div className="row-style">
                            <p>{customer.paymentVerified ? <AiFillCheckCircle className="mb-1" color='green' /> : <RiErrorWarningFill className="mb-1" color='red' />}</p>
                            <p className="ml-2">{customer.paymentVerified ? 'Payment Verified' : 'Payment not verified '}</p>
                        </div>
                    </div>
                <p className="delimiter  center-style"> | </p>
                    <div className="column-style ">
                    <div className="row-style ">
                            <NavItem>
                            <NavLink className="create-project-link cursor" onClick={()=>window.location.href='/create-project'} ><MdCreateNewFolder className="mb-1" color='#4E74DE' /> &nbsp;Create Project</NavLink>
                            </NavItem>
                        
                        </div>
                    </div>
                </div>
                <div className="row-style customer-stats">
                    
                <div className="country-stats">
                    <div className="row lang-icon">
                        <RiDraftFill className="mb-1 " color='#3e5aa7' />
                    </div>
                    <div className="column text">
                        <div className="user-stats-sub">Pages being written right now:</div>
                        <p className="stat-text dshbrd-stat">{customer.pagesNoInProgress}</p>
                    </div>
                    </div>
                    <div className="row-style ">
                        
                    <div className="spent-stats">
                        <div className="row spent-icon">
                            <RiBarChart2Fill className="mb-1 " color='#3e5aa7' />
                        </div>
                        <div className="column text">
                            <div className="user-stats-sub">Active bids: &nbsp; </div>
                            <p className="stat-text dshbrd-stat">{customer.noActiveBids}</p>
                            </div>
                        </div>
                     </div>
                    <div className="row-style">
                        
                    <div className="project-stats">
                        <div className="row stats-icon">
                            < RiFolderChartFill className="mb-1 " color='#3e5aa7' />
                            
                        </div>
                        <div className="column text">
                            <div className="user-stats-sub">{customer.jobsPostedCnt} projects posted</div>
                            <div className="stat-text dshbrd-stat tltp">PSW so far: &nbsp;{customer.pagesWrittenSoFar}<p ></p><span className="tooltiptext">Pages Successfully Written</span></div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>


    )
}

export default CustomerInformations;