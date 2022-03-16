import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch, } from 'react-redux';
import { AiFillCheckCircle } from 'react-icons/ai';
import { Media, Button, Nav, NavItem, NavLink, TabPane, TabContent, Row, Col, Label, Form, Alert, UncontrolledCollapse, CardBody, Card } from 'reactstrap';
import Rating from '@material-ui/lab/Rating';
import classnames from 'classnames';
import { RiErrorWarningFill } from 'react-icons/ri';
import ReactLoading from 'react-loading';
import { useHistory } from 'react-router-dom';
import { MdModeEdit, MdLockOutline, MdPhotoCamera, MdKeyboardArrowDown } from "react-icons/md";
import { BsDot } from 'react-icons/bs';
import { Popup } from 'react-chat-elements'
import * as yup from 'yup';
import dayjs from 'dayjs';
import { Formik, ErrorMessage, Field } from 'formik';
import NumberFormat from 'react-number-format';
import Avatar from 'react-avatar-edit';
import { useAlert } from 'react-alert';

import { BsFilePost } from 'react-icons/bs';

import '../../styles/AuthorInformations.css';
import { ActionCreatorsForUser } from '../../store/AuthorInformationsReducer';
import { GetAuthorPrivateInfo } from '../../services/ProfileServices';
import { PasswordChange, ChangeAuthorsPicture } from '../../services/UserServices';

const lowercase = /(?=.*[a-z])/;
const uppercase = /(?=.*[A-Z])/;
const numeric = /(?=.*[0-9])/;
const maxPicSize = 1048576;

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

export const AuthorInformation = () =>
{
    const [errors, setErrors] = useState({ OldPassword: [], NewPassword: [] });
    const [wrongOldPass, setWrongOldPass] = useState('');
    const [errorStateWrong, setErrorStateWrong] = useState(false);
    const [errorStateOldPassword, setErrorStateOldPassword] = useState(false);
    const [errorStateNewPassword, setErrorStateNewPassword] = useState(false);

    const [changePasswordModalIsOpen, setchangePasswordModalIsOpen] = useState(false);
    const [changeProfilePictureModalIsOpen, setchangeProfilePictureModalIsOpen] = useState(false);

    const alert = useAlert();

    const showChangePasswordPopup = () =>
    {
        setchangePasswordModalIsOpen(false);
    }

    let popupChangePassword =

        <div className="switch-buttons">
            <div className="login-form-elements-container chng-pass-container">
                <Formik initialValues={{ password: '', newPassword: '', passwordConfirm: '' }}
                    onSubmit={(values) => PasswordChange(values.password, values.newPassword, showChangePasswordPopup, alert)
                        .then((response) => {
                            if (response.success == false) {
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

    const [activeTab, setActiveTab] = useState('1');

    const toggle = (tab: React.SetStateAction<string>) => {
        if(activeTab !== tab) setActiveTab(tab);
    }
    
    const author = useSelector((state: any) => state.authorInformationsReducer.authorInformations);
    const loadingValue = useSelector((state: any) => state.authorInformationsReducer.loadingValue);

    const dispatch = useDispatch();
    const history = useHistory();

    useEffect(()=>
    {
        GetAuthorPrivateInfo(dispatch, alert);
        dispatch(ActionCreatorsForUser.setLoadingValue(true)); 
    },[]);

    const [preview, setPreview] = useState("");

    const onClose = () => {
        setPreview("")
    }

    const onCrop = (preview: any) => {
        setPreview(preview)
    }

    const onBeforeFileLoad = (elem: any) => {
        if (elem.target.files[0].size > maxPicSize) {
            alert("File is too big!");
            elem.target.value = "";
        };
    }

    const closeModalProfilePicture = () =>
    {
        setchangeProfilePictureModalIsOpen(false);
    }

    let popupProfilePicture =
        <div className="avatar-cont" style={{ marginLeft: '' }}>
            <Avatar
                width={250}
                height={530}
                onCrop={onCrop}
                onClose={onClose}
                onBeforeFileLoad={onBeforeFileLoad}
            />
        </div>

    return(
        <div >
            { loadingValue ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                             </div> 
                            : 
        <div className="row-style">
                    <div className="column-style center-style" style={{ height: '200px' }}>

                        {preview === "" ? <Media object src={author.picturePath} className="picture-style" alt="Typewriter image"></Media> : <Media object src={preview} className="picture-style" alt="Typewriter image"></Media>}
                        <Button className="button-edit button-change-picture" onClick={() => setchangeProfilePictureModalIsOpen(true)}><MdPhotoCamera className="edit-icon" /></Button>                         

                        <Popup
                            show={changeProfilePictureModalIsOpen}
                            header='Change Profile Picture'
                            headerButtons={[{
                                type: 'transparent',
                                color: 'black',
                                text: 'close',
                                className: 'close-btn-ava',
                                onClick: () => {
                                    setchangeProfilePictureModalIsOpen(false);
                                    setPreview("");
                                }
                            }]}
                            footerButtons={[{
                                color: 'white',
                                backgroundColor: '#4E74DE',
                                text: "Set avatar",
                                onClick: () => {                                   
                                    ChangeAuthorsPicture(preview, closeModalProfilePicture, alert);
                                }
                            }]}
                            renderContent={() => { return popupProfilePicture }}
                        />

                        <br></br>
                        <Rating value={author.reviewRating} size="small" precision={0.1} readOnly/>
                        <p>{author.reviewRating} of 5.0</p>
                    </div>
            <div className="ml-5 " style={{ width: '100%'}}>
                <div className="row-style user-edit-profile">
                            <h3>{author.username}</h3>
                            <div >
                                <Button className="button-edit button-change-password" onClick={() => history.push('/profile/edit-author')}><MdModeEdit className="edit-icon" />&nbsp;Edit profile</Button>
                                <Button className="button-edit button-change-password" onClick={() => setchangePasswordModalIsOpen(true)}><MdLockOutline className="edit-icon" />&nbsp;Change password</Button> 
                                
                            </div>
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
                                renderContent={() => { return popupChangePassword }}
                            />
                </div>
                        <br/>
                        <div className="column-style">
                             <div className="row-style margin-top">
                                  <p>{author.directBooking ? 'Direct Booking' : 'Disabled Direct Booking '}</p>
                                  <p className="ml-1 mb-2">{author.directBooking ? <AiFillCheckCircle className="mb-1" color='green'/> : <RiErrorWarningFill className="mb-1" color='red'/>}</p>
                             </div>
                        </div>

                        <div className="column-style">
                            <p className="profile-subtitle">Highest degree:</p>
                            <div className="row-style margin-top txt-p-ghw">
                                 <p>{author.highestDegree.value}</p>
                            </div>
                        </div>

                        <div className="column-style">
                            <p className="profile-subtitle">Birth Date:</p>
                            <div className="row-style margin-top txt-p-ghw">
                                <p>{dayjs(author.birthDate).format('DD.MM.YYYY').toString()}</p>
                            </div>
                        </div>

                        <div className="row-style txt-p-ghw">
                            <p className="profile-subtitle">Pages per day:</p>
                            <div className="">
                                <p>&nbsp;{author.pagesPerDay}</p>
                            </div>
                            <p> &nbsp; | &nbsp; </p>
                            <p className="profile-subtitle">Price per page:</p>
                            <div className="row-style txt-p-ghw">
                                <p>&nbsp;&#8364;&nbsp;<NumberFormat value={author.pricePerPage} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
                            </div>
                        </div>

                        <p id="toggler" className="author-info-btn profile-subtitle"><span><BsFilePost className="svg-icon-auth-info" size="19" color="" /> &nbsp;Author Informations</span><MdKeyboardArrowDown className="edit-icon-auth-info" /></p>
                        <br></br>
                        <UncontrolledCollapse toggler="#toggler">
                            <Card className="card">
                                <CardBody className="card-body">
                                    <Nav tabs className="tabs-author-info">
                                        <NavItem>
                                            <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); }} to="#">Introduction</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); }} to="#">Kind Of Work</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink className={classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); }} to="#">Area of expertise</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink className={classnames({ active: activeTab === '4' })} onClick={() => { toggle('4'); }} to="#">Languages</NavLink>
                                        </NavItem>
                                    </Nav>

                                    <TabContent className="tabs-content-author-info" activeTab={activeTab}>
                                        <TabPane tabId="1">
                                            <Row>
                                                <Col sm="12">
                                                    <div className="column-style">
                                                        <p> </p>
                                                        <div className="row-style margin-top">
                                                            <p>{author.profileIntroduction} </p>
                                                        </div>
                                                    </div>
                                                </Col>
                                            </Row>
                                        </TabPane>

                                        <TabPane tabId="2">
                                            <Row>
                                                <Col sm="12">
                                                    {author.kindOfWorks?.map((a: { value: any; description: any }) =>
                                                        (<li key={a.value}><BsDot />{a.value}</li>))}
                                                </Col>
                                            </Row>
                                        </TabPane>

                                        <TabPane tabId="3">
                                            <Row>
                                                <Col sm="12">
                                                    {author.expertiseAreas?.map((a: { value: any; description: any }) =>
                                                        (<li key={a.value}><BsDot />{a.value}</li>))}
                                                </Col>
                                            </Row>
                                        </TabPane>

                                        <TabPane tabId="4">
                                            <Row>
                                                <Col sm="12">
                                                    {author.languages?.map((a: { value: any; description: any }) =>
                                                        (<li key={a.value}><BsDot />{a.value}</li>))}
                                                </Col>
                                            </Row>
                                        </TabPane>
                                    </TabContent>

                                </CardBody>
                            </Card>
                        </UncontrolledCollapse>
              </div>


             </div>}
        </div>
    )
}
export default AuthorInformation;