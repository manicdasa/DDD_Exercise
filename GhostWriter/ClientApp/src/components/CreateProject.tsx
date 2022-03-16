import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Media, Form, Label, Alert, Button } from 'reactstrap';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import dayjs from 'dayjs';
import '../styles/CreateProject.css'
import { RiPagesFill } from 'react-icons/ri';
import AsyncCreatableSelect from "react-select/async-creatable";
import makeAnimated from 'react-select/animated';
import AsyncSelect from 'react-select/async';
import { useHistory } from 'react-router-dom';
import Modal from 'react-modal';

import { ActionCreatorsForCreateProject } from '../store/CreateProjectReducer';
import { CreateProjectMethod } from '../services/AuthorServices';
import { Languages, Expertise, KindOfWork, HighestDegree, AddCustomKindOfWork, AddCustomExpertiseArea } from '../services/LookupServices';
import { SignInOrRegister } from "./Auth/SignInOrRegister";
import _ from 'lodash';
import type { RootState } from '../../src/store/store';
import { useAlert, positions } from 'react-alert';

let createProjectSchema = yup.object().shape({
    projectTopic: yup.string().required('Required.'),
    pagesNo: yup.number().positive('Number of pages must be above 0.').required('Required.'),
    deadline: yup.date().min(dayjs(), 'Deadline must be a future date.').required('Required.'),
    description: yup.string().required('Required.'),
    pricePerPage: yup.number().min(12, 'Offer must be above 12.').required('Required.'),
    terms: yup.bool().oneOf([true], 'You must accept terms of Service.').required('Required.')
})

const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        innerWidth: 1300
    }
};

interface Expertise 
{
    expertiseAreaIds: number,
    value: number
}

export const CreateProject = () =>
{
    const alert = useAlert();

    const [authModalIsOpen, setAuthModalIsOpen] = useState(false);

    const creatProjectInputs = useSelector((state: RootState)=>state.createProjectReducer.createProjectInputs);
    
    const [selectedKindOfWorkValue, setKindOfWorkSelectedValue] = useState({  name: creatProjectInputs.kindOfWorkId, value: 0  })
    const loadKindOfWorkOptions = (inputValue: any, callback: any) => { KindOfWork(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLoadOptions = _.debounce(loadKindOfWorkOptions, 1500);

    const [selectedExpertiseValue, setExpertiseSelectedValue] = useState<Array<Expertise>>(creatProjectInputs.expertiseAreaIds);
    const loadExpertiseOptions = (inputValue: any, callback: any) => { Expertise(inputValue, alert).then(resp => callback(resp)); }
    const debouncedExpertiseOptions = _.debounce(loadExpertiseOptions, 1500);

    const [selectedLanguageValue, setSelectedLanguageValue] = useState({ name: creatProjectInputs.languageId, value: 0 });
    const loadLanguageOptions = (inputValue: any, callback: any) => { Languages(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLanguageOptions = _.debounce(loadLanguageOptions, 1500);

    const [selectedHighestDegree, setSelectedHighestDegree] = useState({ name: creatProjectInputs.minimumDegreeId, value: 0 });
    const loadOptionsHighestDegree = (inputValue: any, callback: any) => { HighestDegree(inputValue, alert).then(resp => callback(resp)); }
    const debounceHighestDegree = _.debounce(loadOptionsHighestDegree, 1500);

    const history = useHistory();
    
    const dispatch = useDispatch();

    const closeModal = () => 
    { 
        document.getElementById('signin-success')!.style.display='block';
        setAuthModalIsOpen(false); 
    }   

    return(
        <div className="register-user-box row">
            <div className="register-user-left-panel col-sm-4">
                <h3>Create Project</h3>
                <h6>Step 2 of 2</h6>
                <div className="left-panel-text">
                    <p>Please fill in project details and we will find the perfect author for your next big assignment.</p>
                </div>
                <div className="media_left_panel">
                    <Media object src="/images/CreateProject/create_project.png" alt="Typewriter image"></Media>
                </div>
            </div>

            <div className="register-user-right-panel col-sm-8">
                
                <h3>Project Details</h3>
                <br></br>
                <Formik 
                initialValues={{
                                projectTopic: creatProjectInputs.projectTopic, 
                                pagesNo: creatProjectInputs.pagesNo, 
                                deadline: dayjs(creatProjectInputs.deadline).format('YYYY-MM-DD').toString(), 
                                description: creatProjectInputs.description, 
                                pricePerPage: creatProjectInputs.pricePerPage,
                                terms: creatProjectInputs.terms
                               }} 
                onSubmit={(values) => {
                    if(localStorage.getItem('role=Customer')!=null)
                    {
                        dispatch(ActionCreatorsForCreateProject.setInputsCreateProject( 
                        {...values, kindOfWorkId: selectedKindOfWorkValue, 
                            expertiseAreaIds: selectedExpertiseValue,
                            languageId: selectedLanguageValue.name, 
                            minimumDegreeId: selectedHighestDegree.name,
                            terms: false
                        }));
                        
                        CreateProjectMethod(history, 
                        {...values, 
                            kindOfWorkId: selectedKindOfWorkValue.value, 
                            expertiseAreaIds: selectedExpertiseValue.map(e => e.value),
                            languageId: selectedLanguageValue.value, 
                            minimumDegreeId: selectedHighestDegree.value
                        }, alert)
                    }
                    else
                    {
                        setAuthModalIsOpen(true);
                    }
                }}
                validationSchema={createProjectSchema}>

                {({ dirty, isValid, handleSubmit, values }) => {
        
                return(
                    <Form onSubmit={(e)=>{ e.preventDefault(); handleSubmit(); }}>

                        <div className="div-input-row col-sm-12">
                            <div className="div-input-column col-sm-6">
                                <Label for="kindOfWorkId">Kind of work</Label>
                                <Field
                                    as={AsyncSelect}
                                    defaultOptions     
                                    components={makeAnimated()}
                                    value={selectedKindOfWorkValue}
                                    getOptionLabel={(e: any) => e.name}
                                    getOptionValue={(e: any) => e.value}
                                    loadOptions={debouncedLoadOptions}
                                    onChange={(value: any) => 
                                    { 
                                        setKindOfWorkSelectedValue(value);
                                    }}
                                    name="kindOfWorkId">
                                </Field>
                                <ErrorMessage component="p" className="field-colorchange" name="kindOfWorkId"/>
                            </div>
                            
                            <div className="div-input-column col-sm-6">
                                <Label for="expertiseAreaIds">Area of expertise</Label>
                                <Field
                                    as={AsyncSelect}
                                    defaultOptions
                                    isMulti
                                    className="label-wrap"
                                    components={makeAnimated()}
                                    value={selectedExpertiseValue}
                                    getOptionLabel={(e: any) => e.name}
                                    getOptionValue={(e: any) => e.value}
                                    loadOptions={debouncedExpertiseOptions}
                                    onChange={(value: any) => 
                                    { 
                                       setExpertiseSelectedValue(value);
                                    }}
                                    name="expertiseAreaIds">
                                </Field>
                                <ErrorMessage component="p" className="field-colorchange" name="expertiseAreaIds"/>
                            </div>
                        </div>

                        <div className="div-input-row col-sm-12">
                            <div className="div-input-column col-sm-6">
                                <Label for="languageIds">Language</Label>
                                <Field
                                    as={AsyncSelect}
                                    defaultOptions
                                    className="label-wrap"
                                    components={makeAnimated()}
                                    value={selectedLanguageValue}
                                    getOptionLabel={(e: {name: string}) => e.name}
                                    getOptionValue={(e: {value: number}) => e.value}
                                    loadOptions={debouncedLanguageOptions}
                                    onChange={(value: {name: string, value: number}) => setSelectedLanguageValue(value)}
                                    name="languageId">
                                </Field>
                                <ErrorMessage component="p" className="field-colorchange" name="languageIds" />
                            </div>

                            <div className="div-input-column col-sm-6">
                                <Label for="degreeIds">Minimum Degree</Label>
                                <Field
                                    as={AsyncSelect}
                                    defaultOptions
                                    className="label-wrap"
                                    components={makeAnimated()}
                                    value={selectedHighestDegree}
                                    getOptionLabel={(e: {name: string}) => e.name}
                                    getOptionValue={(e: {value: number}) => e.value}
                                    loadOptions={debounceHighestDegree}
                                    onChange={(value: {name: string, value: number}) => setSelectedHighestDegree(value)}
                                    name="degreeIds">
                                </Field>
                            </div>   
                        </div>
 

                        <div className="div-input-row col-sm-12">
                            <div className="div-input-column-element col-sm-12">
                                <Label for="projectTopic">Topic of your work</Label>
                                <Field
                                    name="projectTopic" 
                                    label="Topic of your work" 
                                    type="text"
                                    autoComplete="off" 
                                    required>
                                </Field>
                                <ErrorMessage component="p" className="field-colorchange" name="projectTopic"/>
                            </div>
                        </div>

                        <div className="div-input-row col-sm-12">
                            <div className="div-input-column-element col-sm-6">
                                <Label for="pagesNo">Pages needed</Label>
                                <div className="div-input-row">
                                    <span className="input-group-text mr-2"><RiPagesFill/></span>
                                    <Field
                                        className="form-control"
                                        name="pagesNo" 
                                        label="Pages needed" 
                                        type="number"
                                        autoComplete="off" 
                                        required>
                                    </Field>
                                </div>
                                <ErrorMessage component="p" className="field-colorchange" name="pagesNo"/>
                            </div>
                            
                            <div className="div-input-column col-sm-6">
                                <Label for="deadline">Deadline</Label>
                                <Field
                                    className="form-control"
                                    name="deadline" 
                                    label="Deadline" 
                                    type="date"
                                    autoComplete="off" 
                                    required>
                                </Field>
                                <ErrorMessage component="p" className="field-colorchange" name="deadline"/>
                            </div>
                        </div>

                        <div className="div-input-row col-sm-12">
                            <div className="div-input-column-element col-sm-12">
                                <Label for="description">Project description</Label>
                                <Field
                                    className="form-control"
                                    name="description" 
                                    label="Description" 
                                    type="text"
                                    as="textarea"
                                    autoComplete="off" 
                                    required>
                                </Field>
                                <ErrorMessage component="p" className="field-colorchange" name="description"/>
                            </div>
                        </div>

                        <div className="div-input-row col-sm-12">
                            <div className="div-input-column col-sm-6">
                                <Label for="pricePerPage">Financial offer per page</Label>
                                <div className="div-input-row">
                                    <span className="input-group-text mr-2">&#8364;</span>
                                    <Field
                                        className="form-control"
                                        name="pricePerPage" 
                                        label="Financial offer per page" 
                                        type="number"
                                        autoComplete="off" 
                                        required>
                                    </Field>
                                </div>
                                <ErrorMessage component="p" className="field-colorchange" name="pricePerPage"/>
                            </div>
                            <div className="div-input-column col-sm-6">
                                <Label for="totalValue">Total project value</Label>
                                <div className="div-input-row">
                                    <span className="input-group-text mr-2">&#8364;</span>
                                    <Field
                                        className="form-control"
                                        name="totalValue"
                                        value={values.pagesNo * values.pricePerPage}
                                        disabled={true}
                                        label="Total project value" 
                                        type="number"
                                        autoComplete="off" 
                                        required>
                                    </Field>
                                </div>
                                <ErrorMessage component="p" className="field-colorchange" name="totalValue"/>
                            </div>
                        </div>
                        <br></br>
                        <br></br>
                        <div className="div-input-column col-sm-12">
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
                            <ErrorMessage component="p" className="field-colorchange" name="terms"/>
                        </div>
                        
                        <div className="button-center col-sm-12">
                            <div className="col-sm-6">
                                <Alert id="signin-success" style={{ display: 'none' }} color="success">Sign in was succesfull. Proceed to submit the project.</Alert>
                                <Button color="primary" className="btn btn-primary" disabled={!dirty || !isValid} type="submit">Submit project</Button>
                            </div>
                        </div>

                    </Form>)}}
                    </Formik>
            </div>

            <Modal
                isOpen={authModalIsOpen}
                onRequestClose={() => setAuthModalIsOpen(false)}
                style={customStyles}
                ariaHideApp={false}
                contentLabel="Example Modal"
            >
                <SignInOrRegister onSuccessLogin={closeModal}/>
            </Modal>
        </div>
    )
}

export default CreateProject;