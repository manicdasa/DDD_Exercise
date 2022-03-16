import React, { useEffect, useState } from 'react';
import AsyncSelect from 'react-select/async';
import { Media, Form, Label, Alert, NavLink, Button } from 'reactstrap';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import _ from 'lodash';
import makeAnimated from 'react-select/animated';
import { useHistory, Link } from 'react-router-dom';
import { RiPagesFill } from 'react-icons/ri';
import AsyncCreatableSelect from "react-select/async-creatable";
import { AddCustomKindOfWork, Expertise, HighestDegree, KindOfWork, Languages, AddCustomExpertiseArea } from '../../services/LookupServices';
import dayjs from 'dayjs';
import { EditProject } from '../../services/ProjectServices';
import { useAlert } from 'react-alert';
import { FaMoneyBillWaveAlt } from 'react-icons/fa';
import { CgDanger } from 'react-icons/cg';



import { Popup } from 'react-chat-elements'
import { BiArrowBack } from 'react-icons/bi';
import { useSelector } from 'react-redux';

let editProjectSchema = yup.object().shape({
    projectTopic: yup.string().required('Required.'),
    deadline: yup.date().required('Required.'),
    maxBudget: yup.number().required('Required.'),
    description: yup.string().required('Required'),
    pagesNo: yup.number().required('Required.'),
});

interface Expertise 
{
    id: number,
    value: string,
    description: string
}

export const EditProjectComponent = (props: any) =>
{
    const history = useHistory();

    const project = useSelector((state: any) => state.customerReducer.proposalDetailsCustomer); 

    const [editValues, setEditValues] = useState({});

    const alert = useAlert();   
    
    const [selectedKindOfWorkValue, setKindOfWorkSelectedValue] = useState<any>({ 'name': project.kindOfWorkDTO?.value, 'value': project.kindOfWorkDTO?.id })
    const loadKindOfWorkOptions = (inputValue: any, callback: any) => { KindOfWork(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLoadOptions = _.debounce(loadKindOfWorkOptions, 1500);

    const [selectedExpertiseValue, setExpertiseSelectedValue] = useState<any>(project.expertiseAreaListDTOs?.map((area: { id: number, value: string }) => 
    ({
        "name": area.value,
        "value": area.id
    })));
    
    const loadExpertiseOptions = (inputValue: any, callback: any) => { Expertise(inputValue, alert).then(resp => callback(resp)); }
    const debouncedExpertiseOptions = _.debounce(loadExpertiseOptions, 1500);

    const [selectedLanguageValue, setSelectedLanguageValue] = useState<any>({ 'name': project.languageDTO?.value, 'value': project.languageDTO?.id });
    const loadLanguageOptions = (inputValue: any, callback: any) => { Languages(inputValue, alert).then(resp => callback(resp)); }
    const debouncedLanguageOptions = _.debounce(loadLanguageOptions, 1500);

    const [selectedHighestDegree, setSelectedHighestDegree] = useState<any>({ 'name': project.minimumDegreeDTO?.value, 'value': project.minimumDegreeDTO?.id });
    const loadOptionsHighestDegree = (inputValue: any, callback: any) => { HighestDegree(inputValue, alert).then(resp => callback(resp)); }
    const debounceHighestDegree = _.debounce(loadOptionsHighestDegree, 1500);

    const [errorMessage, setErrorMessage] = useState("");

    //popup confirm
    const [confirmModifyProject, setConfirmModifyProject] = useState(false);
    const closeConfirmModifyProject = () =>
    {
        setConfirmModifyProject(false);
    }
    
    let popupContent =
        <div className="danger-modal-wind-cont">
            <div className="dnager-icon-cont"> < CgDanger /> </div><div className="danger-text-cont"> <p className="center field-colorchange modify-project-alert-text">By modifying project, all it's existing bids and offers will be deleted.<br></br> <br></br>Are you sure you want to continue?</p> </div>
            <div className="row-style btns-modify-proj">
                <Button color="primary" className="ghw-primary-btn" onClick={() => { EditProject(editValues, alert, history) }}>Confirm</Button>
                <Button color="secondary" className="ml-3 outline-btn-ghw" onClick={()=> setConfirmModifyProject(false)}>Decline</Button>
            </div>
        </div>

 if(project.id === 0)
    {
        history.goBack();
        return <div/>;
    }

    return (
        <div className="register-user-box customer row editproject-container">

            {/*<div className="register-author-left-panel col-sm-4">
                <h3>Modify Project</h3>
                

                <div className="media_left_panel">
                    <Media object src="/images/RegisterAuthor/author_register.png" alt="Typewriter image" ></Media>
                </div>
            </div> */}

            <div className="register-signin-left-panel col-sm-12 customer project-content editproject-page">
                
                <div className="back-link-cont">
                    <NavLink className="back-link cursor" onClick={()=>{history.goBack();}}>< BiArrowBack size="17" className="back-icon" /> &nbsp; Back</NavLink>
                </div>
                <h4 className="edit-project-h">Modify Project</h4>
                <Formik
                    initialValues={
                        {
                            id: project.id,
                            deadline:  dayjs(project.dateCreated).format('YYYY-MM-DD').toString(),
                            maxBudget: project.maxBudget,
                            projectTopic: project.projectTopic,
                            description: project.description,
                            pagesNo: project.pagesNo,
                            minimumDegreeId: selectedHighestDegree.value,
                            languageId: selectedLanguageValue.value,
                            kindOfWorkId: selectedKindOfWorkValue.value,
                            expertiseAreaIds: selectedExpertiseValue.map((e: any) => e.value)
                        }
                    }
                    onSubmit={(values) => 
                    {
                        if(project.proposalDetailsDTOs.length === 0)
                        {
                            EditProject({
                                ...values,
                                minimumDegreeId: selectedHighestDegree.value,
                                languageId: selectedLanguageValue.value,
                                kindOfWorkId: selectedKindOfWorkValue.value,
                                expertiseAreaIds: selectedExpertiseValue.map((e: any) => e.value)
                            }, alert, history)
                        }
                        else
                        {
                            setEditValues(
                            {
                                ...values,
                                minimumDegreeId: selectedHighestDegree.value,
                                languageId: selectedLanguageValue.value,
                                kindOfWorkId: selectedKindOfWorkValue.value,
                                expertiseAreaIds: selectedExpertiseValue.map((e: any) => e.value)
                            });
                            setConfirmModifyProject(true);
                        }
                        }}
                    validationSchema={editProjectSchema}>

                    {({ handleSubmit }) => {
                        return (
                            <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>

                                <div className="clr"></div>
                                
                                <div className="div-input-row col-sm-12 mt-3">
                                    
                                    <div className="div-input-column col-sm-12">
                                        <Label for="projectTopic">Project Topic</Label>
                                        <Field
                                            className="form-control"
                                            name="projectTopic"                                           
                                            label="projectTopic"
                                            type="text"
                                            autoComplete="off"
                                            required>
                                        </Field>
                                        <ErrorMessage component="p" className="field-colorchange" name="projectTopic" />
                                    </div>
                                </div>

                                <div className="div-input-row col-sm-12 mt-3">
                                    <div className="div-input-column col-sm-12 editproject-desc">
                                        <Label for="description">Project Description</Label>
                                        <Field
                                            className="form-control"
                                            name="description"                                           
                                            label="description"
                                            type="text"
                                            as="textarea"
                                            autoComplete="off"
                                            required>
                                        </Field>
                                        <ErrorMessage component="p" className="field-colorchange" name="description" />
                                    </div>
                                </div>

                                <div className="col-sm-6 editproject-deadline">
                                    <label>Deadline</label>
                                    <Field
                                        className="form-control"
                                        name="deadline"
                                        type="date"
                                        autoComplete="off"
                                        required>
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="deadline"/>
                                </div>

                                <div className="degree col-sm-12">
                                        <label className="lbl_padd">Minimum Degree</label>
                                        <Field
                                            as={AsyncSelect}
                                            defaultOptions
                                            className="label-wrap"
                                            components={makeAnimated()}
                                            value={selectedHighestDegree}
                                            getOptionLabel={(e: {name: string}) => e.name}
                                            getOptionValue={(e: {value: number}) => e.value}
                                            loadOptions={debounceHighestDegree}
                                            onChange={(value: Array<Expertise>) => setSelectedHighestDegree(value)}
                                            name="degreeIds">
                                        </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="highestDegreeId" />
                                </div>

                                <div className="degree col-sm-12">
                                    <label>Kind Of Work</label>
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
                                        name="kindOfWorkIds">
                                    </Field>
                                    <ErrorMessage component="p" className="field-colorchange" name="kindOfWorkIds" />
                                </div>

                                <div className="language col-sm-12">
                                    <label htmlFor="register_author_language">Language</label>
                                    <Field
                                        as={AsyncSelect}
                                        defaultOptions
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
                                        as={AsyncSelect}
                                        defaultOptions
                                        isMulti
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
                                    <ErrorMessage component="p" className="field-colorchange" name="expertiseAreaIds" />
                                </div>

                               <div className="price-group col-sm-12">
                                    <label htmlFor="register_author_price_per_page">Number of Pages</label>
                                    <div className="input-group-prepend">
                                        <span className="input-group-text mr-1"><RiPagesFill /></span>
                                        <div className="price-input">
                                            <Field
                                                className="form-control"
                                                name="pagesNo"
                                                type="number"
                                                autoComplete="off"
                                                required>
                                            </Field>
                                            <ErrorMessage component="p" className="field-colorchange" name="pagesNo" />
                                        </div>
                                    </div>
                                </div>

                                
                               <div className="price-group col-sm-12">
                                    <label htmlFor="register_author_price_per_page">Max budget</label>
                                    <div className="input-group-prepend">
                                        <span className="input-group-text mr-1"><FaMoneyBillWaveAlt /></span>
                                        <div className="price-input">
                                            <Field
                                                className="form-control"
                                                name="maxBudget"
                                                type="number"
                                                autoComplete="off"
                                                required>
                                            </Field>
                                            <ErrorMessage component="p" className="field-colorchange" name="maxBudget" />
                                        </div>
                                    </div>
                                </div>

                                <p className="del">--</p>

                                <div className="register-author-create-profile col-sm-12">

                                    <ErrorMessage component="p" className="field-colorchange" name="terms" />

                                    {(errorMessage === "" || errorMessage === undefined) ? <div></div> : <Alert color="danger">{errorMessage}</Alert>}

                                    <p className="del">--</p>
                                    <div className="btncreate edit-project-btn-cont">                                              
                                        <button type="submit" className="btn btn-primary btn-edit-auth-profile">Save Changes</button>
                                    </div>
                                </div>
                            </Form>)
                    }}
                </Formik>
                
                <Popup
                        show={confirmModifyProject}
                        header='Modify project'
                        headerButtons={[{
                            type: '',
                            color: '#4E74DE',
                            text: 'X',
                            className:'close-btn-modal',
                            onClick: () => {
                                setConfirmModifyProject(false);
                            }
                        }]}                        
                        renderContent={() => { return popupContent }}
                     />
            </div>
        </div>
    );
}

export default EditProjectComponent;