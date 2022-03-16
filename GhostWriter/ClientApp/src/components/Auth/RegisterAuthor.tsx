import React, { useState } from 'react';
import AsyncSelect from 'react-select/async';
import { Media, Form, Label, FormGroup, FormText, Alert } from 'reactstrap';
import { TabContent, TabPane, Nav, NavItem, NavLink, Card, Button, CardTitle, CardText, Row, Col } from 'reactstrap';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import dayjs from 'dayjs';
import _ from 'lodash';
import makeAnimated from 'react-select/animated';
import { useHistory } from 'react-router-dom';
import { RiPagesFill } from 'react-icons/ri';
import Avatar from 'react-avatar-edit'
import { MdPhotoCamera } from "react-icons/md";
import { Popup } from 'react-chat-elements';
import classnames from 'classnames';
import { useAlert } from 'react-alert';
import AsyncCreatableSelect from "react-select/async-creatable";

import { RegisterAuthorMethodPayPal, RegisterAuthorMethodIBan, CheckEmailAvailability, CheckUsernameAvailability  } from '../../services/UserServices';
import { Languages, Expertise, KindOfWork, HighestDegree, AddCustomKindOfWork, AddCustomExpertiseArea } from '../../services/LookupServices';
import "../../styles/RegisterAuthor.css"

declare const window: any;

const lowercase = /(?=.*[a-z])/;
const uppercase = /(?=.*[A-Z])/;
const numeric = /(?=.*[0-9])/;
const non_aplhanumeric = /(?=.*[^a-zA-Z0-9])/;
const maxPicSize = 1048576;

let registerAuthorSchema = yup.object().shape({
    profilePicture: yup.string(),
    firstName: yup.string().required('Required.'),
    lastName: yup.string().required('Required.'),
    email: yup.string()
              .lowercase()
        .email('Must be a valid email address.')
        .required('Required.').nullable(),
    username: yup.string()
                  .min(7, 'Username must be atleast seven characters.')
                  .required('Required.'),
    password: yup.string().matches(lowercase, 'One lowercase required.')
                          .matches(uppercase, 'One uppercase required.')
                          .matches(numeric, 'One numeric simbol required.')
                          .matches(non_aplhanumeric, 'One non-alphanumeric simbol is required (ex. {$,!,@,-...})')
                          .min(6, "Minimum 6 characters")
                          .required("Required"),
    passwordConfirm: yup.string().oneOf([yup.ref('password')], 'Passwords must be the same').required("Required"),
    dateOfBirth: yup.date()
                .max(dayjs().subtract(18, 'year').format('YYYY-MM-DD HH:mm:ss'), 'You must be 18+ years old to register.')
                .required("Required"),
    profileIntroduction: yup.string().required('Required.'),
    highestDegreeId: yup.number().required('Required.'),
    kindOfWorkIds: yup.array().of(yup.object()),
    languageIds: yup.array().of(yup.object()),
    expertiseAreaIds: yup.array().of(yup.object()),
    pricePerPage: yup.number().positive('Number must be above 0.').required('Required.'),
    directBooking: yup.string(),
    pagesPerDay: yup.number().positive('Number must be above 0.').required('Required.'),
    terms: yup.bool().oneOf([true], 'You must accept terms of Service.').required('Required.')
});

const colourStyles = 
{
    multiValue: (styles: any, { data }: any):any => 
    {
      if(data.value === -1)
        return { ...styles, backgroundColor: '#FFE98F',};
      else
        return { ...styles, backgroundColor: 'white',};
    }
};

export const RegisterAuthor = () => 
{
    var IBANValidate = require('iban');
    const [activeTab, setActiveTab] = useState('1');

    const [ibanMessage, setIbanMessage] = useState(false);

    const toggle = (tab: React.SetStateAction<string>) => 
    {
        if(activeTab !== tab) setActiveTab(tab);
    }    

    const alert = useAlert();
    const [iban, setiban] = useState('');    
    
    const handleChangeIban = (event : any) => 
    {
        setiban(event.target.value);    
        if(IBANValidate.isValid(event.target.value)===false)
        {
            setIbanMessage(true);
        }
        else
        {
            setIbanMessage(false);
        }
    }

    const [showUsernameMessageError, setUsernameMessageError] = useState(false);

    const [selectedLanguageValue, setSelectedLanguageValue] = useState<any[]>([]);
    const loadLanguageOptions = (inputValue: any, callback: any) => { Languages(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLanguageOptions = _.debounce(loadLanguageOptions, 1500);

    const [selectedExpertiseValue, setExpertiseSelectedValue] = useState<any[]>([]);
    const loadExpertiseOptions = (inputValue: any, callback: any) => { Expertise(inputValue, alert).then(resp => callback(resp)); }
    const debouncedExpertiseOptions = _.debounce(loadExpertiseOptions, 1500);

    const [selectedKindOfWorkValue, setKindOfWorkSelectedValue] = useState<any[]>([])
    const loadKindOfWorkOptions = (inputValue: any, callback: any) => { KindOfWork(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLoadOptions = _.debounce(loadKindOfWorkOptions, 1500);

    const [selectedHighestDegree, setSelectedHighestDegree] = useState({ name: '', value: 0 });
    const loadOptionsHighestDegree = (inputValue: any, callback: any) => { HighestDegree(inputValue, alert).then(resp => callback(resp)); }
    const debounceHighestDegree = _.debounce(loadOptionsHighestDegree, 1500);

    const [errorMessage, setErrorMessage] = useState("");

    const [usernameStatus, setUsernameStatus] = useState({ available: true, message: '', usernameSuggestions: [] });
    const [emailStatus, setEmailStatus] = useState({ feasible: true, reason: '' });

    const history = useHistory();

    const [changeProfilePictureModalIsOpen, setchangeProfilePictureModalIsOpen] = useState(false);

    const [preview, setPreview] = useState("");

    const onClose = () => {
        setPreview("")
    }

    const onCrop = (preview: any) => {
        setPreview(preview)
    }

    const onBeforeFileLoad = (elem: any) => {
        if (elem.target.files[0].size > maxPicSize) {
            alert.error('Picture size too big.')
            elem.target.value = "";
        };
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

    return (
        
        <div className="register-author-box row">

            <div className="register-author-left-panel col-sm-4">
                <h3>Become an author.</h3>
                <div className="left-panel-text">
                    <p>Become an author on our platform to offer academic work for our customers.</p>
                    <p>Please fill in as much details as possible so that your profile can be found by potential cusomers.</p>
                </div>
                <div className="media_left_panel">
                    <Media object src="/images/RegisterAuthor/author_register.png" alt="Typewriter image" ></Media>
                </div>
            </div>

            <div className="register-author-right-panel col-sm-8">
                <h3>Create Author Account</h3>

                <Formik
                    initialValues={{
                        profilePicture: '',
                        firstName: '',
                        lastName: '',
                        email: '',
                        username: '',
                        password: '',
                        passwordConfirm: '',
                        dateOfBirth: dayjs().format('YYYY-MM-DD').toString(),
                        profileIntroduction: '',
                        highestDegreeId: 0,
                        languageIds: [],
                        pricePerPage: 1,
                        directBooking: 'false',
                        pagesPerDay: 1,
                        terms: false
                    }}
                    onSubmit={(values) => {
                        if(selectedKindOfWorkValue.length !== 0 && selectedExpertiseValue.length !== 0 && selectedLanguageValue.length !==0 && selectedHighestDegree.value !== 0)
                        {
                            if(window.CODE != undefined)
                            {
                                RegisterAuthorMethodPayPal(history, {
                                    ...values,
                                    profilePicture: preview,
                                    highestDegreeId: selectedHighestDegree.value,
                                    kindOfWorks: selectedKindOfWorkValue.map(e => e),
                                    expertiseAreas: selectedExpertiseValue.map(e => e),
                                    languageIds: selectedLanguageValue.map(e => e.value),
                                    paypalCode: window.CODE
                                }, alert )
                            }
                            else if(IBANValidate.isValid(iban) === true)
                            {
                                RegisterAuthorMethodIBan(history, {
                                    ...values,
                                    profilePicture: preview,
                                    highestDegreeId: selectedHighestDegree.value,
                                    kindOfWorks: selectedKindOfWorkValue.map(e => e),
                                    expertiseAreas: selectedExpertiseValue.map(e => e),
                                    languageIds: selectedLanguageValue.map(e => e.value),
                                    iban: iban
                                }, alert) 
                            }
                            else
                            {
                                alert.error('You have to set up your payment method.')
                            }
                        }
                        else
                        {
                            alert.error('Field Highest Degree, Kind Of Work, Area Of Expertise or Language must not be empty.')
                        }
                    }}
                    validationSchema={registerAuthorSchema}>

                    {({ dirty, isValid, handleSubmit, values, setFieldValue }) => {
                        return (
                            <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                                
                            <div className="upload-img">
                                <div className="register-author-image-upload-placeholder">
                                    <FormGroup>                                           
                                            {preview === "" ?
                                                <div>
                                                    <div className="empty-img-container-author"></div>
                                                    <Button className="button-edit button-change-profile-picture" onClick={() => setchangeProfilePictureModalIsOpen(true)}><MdPhotoCamera className="edit-icon" /></Button>
                                                </div>
                                                : <div>
                                                    <img className="upload-image" style={{ float: 'left', width: '150px', height: '150px', position: 'absolute' }} src={preview} />
                                                    <Button className="button-edit button-change-profile-picture" onClick={() => setchangeProfilePictureModalIsOpen(true)}><MdPhotoCamera className="edit-icon" /></Button>
                                                </div>}
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
                                                    text: "Set Avatar",
                                                    onClick: () => {
                                                        setchangeProfilePictureModalIsOpen(false);
                                                        setPreview(preview);
                                                    }
                                                }]}
                                                renderContent={() => { return popupProfilePicture }}
                                            />
                                        
                                    </FormGroup>
                                </div>
                                <div className="clr"></div>
                                <div className="form-group-name col-sm-6">
                                    <label htmlFor="register_author_firstname">First Name</label>
                                    <Field
                                        className="form-control"
                                        name="firstName"
                                        type="text"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="firstName" />
                                </div>
                            </div>
                                <div className="form-group-name col-sm-6">
                                    <label htmlFor="register_author_lastname">Last Name</label>
                                    <Field
                                        className="form-control"
                                        name="lastName"
                                        type="text"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="lastName" />
                                </div>

                                <div className="form-group-email col-sm-6">
                                    <label htmlFor="register_author_email">Email</label>
                                    <Field
                                        className="form-control"
                                        name="email"
                                        type="email"
                                        onBlur={() => {
                                            registerAuthorSchema.fields.email.isValid(values.email)
                                                .then((response => {
                                                    if (response == true) {
                                                        CheckEmailAvailability(values.email, alert)
                                                            .then((response) => { setEmailStatus(response?.data) })
                                                    }
                                                    else {
                                                        setEmailStatus({ feasible: false, reason: 'Must be a valid email address.' });
                                                    }
                                                }));
                                        }}
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="email" />
                                    {emailStatus.feasible ? <div></div> : <p className="field-colorchange">{emailStatus.reason}</p>}
                                </div>

                                <div className="form-group-username col-sm-6">
                                    <label htmlFor="register_author_username">Username</label>
                                    <Field
                                        className="form-control"
                                        name="username"
                                        type="text"
                                        onBlur={() => {
                                            registerAuthorSchema.fields.username.isValid(values.username)
                                                .then((response => {
                                                    if (response == true) {
                                                        setUsernameMessageError(false);
                                                        CheckUsernameAvailability(values.username, values.firstName, values.lastName, values.dateOfBirth, alert)
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
                                        <div className="field-colorchange">Available usernames: {usernameStatus.usernameSuggestions.map((u, index: number) => <p key={index} className="register-author-username" onClick={() => { setFieldValue("username", u); setUsernameStatus({ available: true, message: '', usernameSuggestions: [] }) }}> {u + (index === usernameStatus.usernameSuggestions.length - 1 ? '' : ",\xa0")} </p>)}</div>
                                    </div>
                                    }
                                </div>

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

                                <p className="del">--</p>

                                <div className="col-sm-6">
                                    <label>Date Of Birth</label>
                                    <Field
                                        className="form-control"
                                        name="dateOfBirth"
                                        type="date"
                                        max={dayjs().format("YYYY-MM-DD")}
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="dateOfBirth" />
                                </div>

                                <div className="div-input-row col-sm-12 mt-3">
                                    <div className="div-input-column col-sm-12">
                                        <Label for="profileIntroduction">Profile introduction</Label>
                                        <Field
                                            className="form-control"
                                            name="profileIntroduction"
                                            label="profileIntroduction"
                                            type="text"
                                            as="textarea"
                                            autoComplete="off"
                                            required>
                                        </Field>
                                        <ErrorMessage component="p" className="field-colorchange" name="profileIntroduction" />
                                    </div>
                                </div>

                                <div className="degree col-sm-12">
                                    <label>Highest Degree</label>
                                    <Field
                                        as={AsyncSelect}
                                        defaultOptions
                                        value={selectedHighestDegree}
                                        getOptionLabel={(e: any) => e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debounceHighestDegree}
                                        onChange={(value: any) => { setSelectedHighestDegree(value) }}
                                        name="highestDegreeId">
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="highestDegreeId" />
                                </div>

                                <div className="degree col-sm-12">
                                    <label>Kind Of Work</label>
                                    <Field
                                        as={AsyncCreatableSelect}
                                        defaultOptions
                                        isMulti
                                        styles={colourStyles}
                                        components={makeAnimated()}
                                        value={selectedKindOfWorkValue}
                                        getOptionLabel={(e: any) => e.label || e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debouncedLoadOptions}
                                        onChange={(value: any) => 
                                        { 
                                            if(value.length !== 0 && value[value.length-1].__isNew__ !== undefined)
                                            {
                                                alert.success(<div className="alert-success-ghw">You have successfully added a custom value. Waiting for admin approval.</div>);
                                                value[value.length-1].name = value[value.length-1].label;
                                                value[value.length-1].value = -1;
                                                delete value[value.length-1].label;
                                                delete value[value.length-1].__isNew__;
                                                setKindOfWorkSelectedValue(value);
                                            }
                                            else
                                            {
                                                setKindOfWorkSelectedValue(value);
                                            }
                                        }}
                                        name="kindOfWorkIds">
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="kindOfWorkIds" />
                                </div>

                                <div className="language col-sm-12">
                                    <label htmlFor="register_author_language">Language</label>
                                    <Field
                                        as={AsyncSelect}
                                        defaultOptions
                                        isMulti
                                        components={makeAnimated()}
                                        value={selectedLanguageValue}
                                        getOptionLabel={(e: any) => e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debouncedLanguageOptions}
                                        onChange={(value: any) => setSelectedLanguageValue(value)}
                                        name="languageIds">
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="languageIds" />
                                </div>

                                <div className="area col-sm-12">
                                    <label>Area of expertise</label>
                                    <Field
                                        as={AsyncCreatableSelect}
                                        defaultOptions
                                        isMulti
                                        styles={colourStyles}
                                        components={makeAnimated()}
                                        value={selectedExpertiseValue}
                                        getOptionLabel={(e: any) => e.label || e.name}
                                        getOptionValue={(e: any) => e.value}
                                        loadOptions={debouncedExpertiseOptions}
                                        onChange={(value: any) => 
                                        { 
                                            if(value.length !== 0 && value[value.length-1].__isNew__ !== undefined)
                                            {
                                                alert.success(<div className="alert-success-ghw">You have successfully added a custom value. Waiting for admin approval.</div>);
                                                value[value.length-1].name = value[value.length-1].label;
                                                value[value.length-1].value = -1;
                                                delete value[value.length-1].label;
                                                delete value[value.length-1].__isNew__;
                                                setExpertiseSelectedValue(value);
                                            }
                                            else
                                            {
                                                setExpertiseSelectedValue(value);
                                            }
                                        }}
                                        name="expertiseAreaIds">
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="expertiseAreaIds" />
                                </div>

                                <div className="price-group col-sm-12">
                                    <label htmlFor="register_author_price_per_page">Price per page</label>
                                    <div className="input-group-prepend">
                                        <span className="input-group-text mr-1">&#8364;</span>
                                        <div className="price-input">
                                            <Field
                                                className="form-control"
                                                name="pricePerPage"
                                                type="number"
                                                autoComplete="off"
                                                required>
                                            </Field>
                                            <ErrorMessage component="p" className="field-colorchange" name="pricePerPage" />
                                        </div>
                                        <div className="price-text">* just for orientation, exact price will be negotiated with every client individually</div>
                                    </div>

                                </div>

                                <div className="price-group col-sm-12">
                                    <label htmlFor="register_author_price_per_page">Pages per day</label>
                                    <div className="input-group-prepend">
                                        <span className="input-group-text mr-1"><RiPagesFill/></span>
                                        <div className="price-input">
                                            <Field
                                                className="form-control"
                                                name="pagesPerDay"
                                                type="number"
                                                autoComplete="off"
                                                required>
                                            </Field>
                                            <ErrorMessage component="p" className="field-colorchange" name="pagesPerDay" />
                                        </div>                                        
                                    </div>                                   
                                </div>

                                <p className="del">--</p>
                                <div id="my-radio-group" className="col-sm-12">
                                    <label htmlFor="register_author_direct_booking">Direct booking?</label>
                                </div>
                                <div className="form-group col-sm-12">
                                    <div role="group" aria-labelledby="my-radio-group" className="form-group-check col-sm-4">
                                        <div className="div-input-row">
                                            <Label for="yes">
                                                <Field
                                                    className="mr-2"
                                                    type="radio"
                                                    name="directBooking"
                                                    value="true"
                                                    autoComplete="off"
                                                    required>
                                                </Field>Yes
                                </Label>
                                        </div>

                                        <div className="div-input-row">
                                            <Label for="no">
                                                <Field
                                                    className="mr-2"
                                                    type="radio"
                                                    name="directBooking"
                                                    value="false"
                                                    autoComplete="off"
                                                    required>
                                                </Field>No
                                </Label>
                                        </div>

                                    </div>
                                    <div className="description col-sm-8"><p className="descp">* If you accept direct booking, customers can buy directly what will speed up the process. You will have 24 hours to accept the assignment, otherwise it will be cancelled automatically.</p></div>
                                </div>

                                <p className="del">--</p> 
                                <div>   
                                <Nav tabs className="author-projects-tabs">
                                <div id="my-radio-group" className="col-sm-12">
                                    <label>Set up your payment method</label>
                                </div>
                                    <NavItem>
                                        <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); }} to="#">PayPal</NavLink>
                                    </NavItem>
                                    <NavItem>
                                        <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); }} to="#">IBAN</NavLink>
                                    </NavItem>
                                </Nav> 
                                </div>  
                                <TabContent activeTab={activeTab}>
                                    <TabPane tabId="1">
                                        <div id="paypal-container"/> 
                                    </TabPane>
                                    <TabPane tabId="2">
                                        <textarea className="form-control" placeholder="Example: DE75512108001245126199" value={iban} onChange={handleChangeIban}/>  
                                        { ibanMessage ? <p className="field-colorchange center">IBAN that you have entered is invalid.</p> : <p/>  }
                                    </TabPane>
                                </TabContent>          
                                <div className="register-author-create-profile col-sm-12">
                                    <div className="div-input-row">
                                        <Label>
                                            <Field
                                                className="mr-2"
                                                name="terms"
                                                label="I accept terms of Service"
                                                type="checkbox"
                                                autoComplete="off"
                                                required>
                                            </Field>
                                        </Label><p>I accept</p><p onClick={() => window.open("/tos", "_blank")} className='accept-tos'>terms of Service</p>
                                    </div>
                                    <ErrorMessage component="p" className="field-colorchange" name="terms" />

                                    {(errorMessage === "" || errorMessage === undefined) ? <div></div> : <Alert color="danger" className="alert-create-autor-page">{errorMessage}</Alert>}
                                    <p className="del">--</p>
                                    <div className="btncreate">
                                        <button type="submit" className="btn btn-primary" disabled={!dirty || !isValid} >Create Author Profile</button>
                                    </div>
                                </div>
                            </Form>)
                    }}
                </Formik>
            </div>
        </div>
    );
};

export default RegisterAuthor;

