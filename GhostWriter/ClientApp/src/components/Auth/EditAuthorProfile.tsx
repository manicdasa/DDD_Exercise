import React, { useState } from 'react';
import AsyncSelect from 'react-select/async';
import { useSelector } from 'react-redux';
import { Media, Form, Label, Alert, NavLink} from 'reactstrap';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import dayjs from 'dayjs';
import _ from 'lodash';
import makeAnimated from 'react-select/animated';
import { useHistory, Link } from 'react-router-dom';
import { BiArrowBack } from "react-icons/bi";
import { RiPagesFill } from 'react-icons/ri';
import AsyncCreatableSelect from "react-select/async-creatable";
import { useAlert } from 'react-alert';

import { Languages, Expertise, KindOfWork, HighestDegree, AddCustomExpertiseArea, AddCustomKindOfWork } from '../../services/LookupServices';
import { EditAuthor } from '../../services/AuthorServices';
import "../../styles/RegisterAuthor.css"

let editAuthorProfileSchema = yup.object().shape({
    firstName: yup.string().required('Required.'),
    lastName: yup.string().required('Required.'),
    birthDate: yup.date()
                .max(dayjs().subtract(18, 'year').format('YYYY-MM-DD HH:mm:ss'), 'You must be 18+ years old to register.')
                .required("Required"),
    profileIntroduction: yup.string().required('Required.'),
    directBooking: yup.string(),
});

const colourStyles = {
    multiValue: (styles: any, { data }: any):any => 
    {
      if(data.value === -1)
        return { ...styles, backgroundColor: 'orange',};
      else
        return { ...styles, backgroundColor: 'white',};
    }
  };

interface Expertise {
    id: number,
    value: string,
    description: string
}

export const EditAuthorProfile = () => 
{  
    const alert = useAlert();
    
    const author = useSelector((state: any) => state.authorInformationsReducer.authorInformations);

    const [selectedLanguageValue, setSelectedLanguageValue] = useState<Array<Expertise>>(author.languages?.map((area: { id: number, value: string }) =>
        ({
            "name": area.value,
            "value": area.id
        })));
    const loadLanguageOptions = (inputValue: any, callback: any) => { Languages(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLanguageOptions = _.debounce(loadLanguageOptions, 1500);

    const [selectedExpertiseValue, setExpertiseSelectedValue] = useState<Array<Expertise>>(author.expertiseAreas?.map((area: { id: number, value: string }) => 
        ({
            "name": area.value,
            "value": area.id
        })));
    const loadExpertiseOptions = (inputValue: any, callback: any) => { Expertise(inputValue, alert).then(resp => callback(resp)); }
    const debouncedExpertiseOptions = _.debounce(loadExpertiseOptions, 1500);

    const [selectedKindOfWorkValue, setKindOfWorkSelectedValue] = useState<Array<Expertise>>(author.kindOfWorks?.map((area: {id: number, value: string}) =>
        ({
            "name": area.value,
            "value": area.id
        })));
    const loadKindOfWorkOptions = (inputValue: any, callback: any) => { KindOfWork(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLoadOptions = _.debounce(loadKindOfWorkOptions, 1500);

    const [selectedHighestDegree, setSelectedHighestDegree] = useState({ "name": author.highestDegree.value, "value": author.id });
    const loadOptionsHighestDegree = (inputValue: any, callback: any) => { HighestDegree(inputValue, alert).then(resp => callback(resp)); }
    const debounceHighestDegree = _.debounce(loadOptionsHighestDegree, 1500);

    const [errorMessage, setErrorMessage] = useState("");

    const history = useHistory();

    return (
        <div className="register-author-box editprofile-author row">
            {/*
            <div className="register-author-left-panel col-sm-4">
                <h3>Become an author.</h3>
                <div className="left-panel-text">
                    <p>Become an author on our platform to offer academic work for our customers.</p>
                    <p>Please fill in as much details as possible so that your profile can be edited.</p>
                </div>
                <div className="media_left_panel">
                    <Media object src="/images/RegisterAuthor/author_register.png" alt="Typewriter image" ></Media>
                </div> 
            </div>*/}

            <div className="register-author-right-panel edit-author-profile col-sm-12">
                <div className="back-link-cont">
                    <NavLink className="back-link" tag={Link} to={"/profile"}>< BiArrowBack size="17" className="back-icon" /> &nbsp; Back</NavLink>
                </div>
                <h4 className="edit-auth-profile-h">Edit Author Profile</h4>
 
                <Formik 
                    initialValues={{                       
                        firstName: author.firstName,
                        lastName: author.lastName,
                        birthDate: dayjs(author.birthDate).format('YYYY-MM-DD').toString(),
                        directBooking: author.directBooking ? 'true' : 'false',
                        profileIntroduction: author.profileIntroduction,
                        pagesPerDay: author.pagesPerDay,                       
                    }}
                    onSubmit={(values) => { 
                        EditAuthor(history, { 
                        ...values,
                        kindOfWorks: selectedKindOfWorkValue.map(e => e),
                        expertiseAreas: selectedExpertiseValue.map(e => e),
                        highestDegree: selectedHighestDegree.value,
                        languageIds: selectedLanguageValue.map(e => e.value)
                        }, alert)}}
                    validationSchema={editAuthorProfileSchema}>

                    {({ handleSubmit }) => {
                        return (
                            <Form className="edit-auth-form" onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>

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

                                <p className="del">--</p>

                                <div className="col-sm-6">
                                    <label>Date Of Birth</label>
                                    <Field
                                        className="form-control"
                                        name="birthDate"
                                        type="date"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="birthDate" />
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
                                        getOptionLabel={(e: { name: string }) => e.name}
                                        getOptionValue={(e: { value: number }) => e.value}
                                        loadOptions={debounceHighestDegree}
                                        onChange={(value: {name: string, value: number}) => setSelectedHighestDegree(value)}
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
                                        getOptionLabel={(e: { name: string }) => e.name}
                                        getOptionValue={(e: { value: number }) => e.value}
                                        loadOptions={debouncedLanguageOptions}
                                        onChange={(value: Array<Expertise>) => setSelectedLanguageValue(value)}
                                        name="languages">
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="languages" />
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
                                    <label htmlFor="register_author_price_per_page">Pages per day</label>
                                    <div className="input-group-prepend">
                                        <span className="input-group-text mr-1"><RiPagesFill /></span>
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

                                <div className="register-author-create-profile col-sm-12">

                                    <ErrorMessage component="p" className="field-colorchange" name="terms" />

                                    {(errorMessage === "" || errorMessage === undefined) ? <div></div> : <Alert color="danger">{errorMessage}</Alert>}

                                    <p className="del">--</p>
                                    <div className="btncreate edit-author-profile-btn-cont">                                              
                                        <button type="submit" className="btn btn-primary btn-edit-auth-profile">Edit Author Profile</button>
                                    </div>
                                </div>
                            </Form>)
                    }}
                </Formik>
            </div>
        </div>
    );
}

export default EditAuthorProfile;

