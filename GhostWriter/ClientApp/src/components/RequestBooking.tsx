import React, { useState, useEffect } from 'react';
import { Media, Form, Label, NavLink, Button, Alert } from 'reactstrap';
import { Link } from 'react-router-dom';
import * as yup from 'yup';
import { Formik, ErrorMessage, Field } from 'formik';
import Modal from 'react-modal';
import { RiPagesFill } from 'react-icons/ri';
import { AiFillCheckCircle } from 'react-icons/ai';
import { RiErrorWarningFill } from 'react-icons/ri';
import { useHistory } from 'react-router-dom';
import { useAlert } from 'react-alert';
import '../styles/RequestBooking.css'
import '../styles/AuthorInformations.css'
import { CreateProjectAndOfferToAuthor } from '../services/AuthorServices';
import { SignInOrRegister } from "./Auth/SignInOrRegister";

let createProjectSchema = yup.object().shape(
{
    projectTopic: yup.string().required('Required.'),
    description: yup.string().required('Required.'),
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

export const RequestBooking = (props: any) =>
{    
    const [authModalIsOpen, setAuthModalIsOpen] = useState<boolean>(false);

    const [user, setUser] = useState({id: 0, picturePath: '', pricePerPage: 0, username: '', expertiseAreas: [], directBooking: '', kindOfWorks: [], highestDegree: { id: 0 }});
    const [inputValues, setInputValues] = useState({number: 0, budget: 0, deadline: '', kindOfWork: { name: '', value: 0 }, language: { name: '', value: 0 }, expertise: { name: '', value: 0 }});

    const alert = useAlert();
    const history = useHistory();

    const closeModal = () => 
    {
        document.getElementById('signin-success')!.style.display='block';
        setAuthModalIsOpen(false);
    }

    useEffect(()=>
    {
        if(props.location.userProps === undefined || props.location.valueProps === undefined)
        {
            history.push('/search-author');
        }
        else
        {
            setUser(props.location.userProps);
            setInputValues(props.location.valueProps);
        }
    }, [])

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
                <h4>Author Info</h4>
                <br></br>

                
                <div className="row-style author-info">
                    
                    <div className="column-style center-style user-image"style={{height: '150px'}}>
                        <Media object src={user.picturePath} className="picture-style" alt="Typewriter image"></Media>
                    </div>
                    <div className="ml-5 " style={{ width: '100%'}}>
                        <div className="row-style">
                                <h4>{user.username}</h4>
                        </div>
                        <div className="row-style"><p className=" direct-book-author">{user.directBooking ? 'Direct Booking' : 'Disabled Direct Booking '}</p>
                            <p className=" direct-book-author">{user.directBooking ? <AiFillCheckCircle className="" color='green' /> : <RiErrorWarningFill className="" color='red' />}</p></div>
                        <div className="column-style">
                            <p className="profile-subtitle">Area of expertise:</p>
                            <div className="row-style margin-top">
                                <p className="profile-text">{user.expertiseAreas.map((a: { value: string; description: string }) => a.value).join(', ')}</p>
                                
                                
                            </div>
                        </div>
                        <div className="column-style">
                            <p className="profile-subtitle">Kind of works: </p>
                            <p className="margin-top profile-text">{user.kindOfWorks.map((a: { value: string; description: string }) => a.value).join(', ')}</p>
                        </div>
                    </div>
                </div>

                <Formik 
                initialValues={{
                                projectTopic: '', 
                                description: '', 
                                terms: false
                            }} 
                onSubmit={(values) => {
                    if(localStorage.getItem('role=Customer')!=null)
                    {
                        CreateProjectAndOfferToAuthor(history, user.id, 
                                                            {...values, 
                                                                expertiseAreaIds: [inputValues.expertise.value],
                                                                kindOfWorkId: inputValues.kindOfWork.value,
                                                                languageId: inputValues.language.value,
                                                                minimumDegreeId: user.highestDegree.id,
                                                                plannedBudget: inputValues.budget != undefined ? inputValues.budget * inputValues.number : user.pricePerPage * inputValues.number,
                                                                pricePerPage: inputValues.budget || user.pricePerPage,
                                                                deadline: inputValues.deadline,
                                                                pagesNo: inputValues.number
                            }, alert)
                    }
                    else
                    {
                        setAuthModalIsOpen(true);
                    }
                }}
                validationSchema={createProjectSchema}>

                {({ dirty, isValid, handleSubmit }) => {
        
                        return (
                    
                            <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                                <div className="project-details-title"><h4>Project Details:</h4></div>
                        <div className="div-input-row request-booking-disabled col-sm-12 mt-3 ">
                            <div className="div-input-column col-sm-6">
                                <Label for="kindOfWork">Kind of work</Label>
                                <Field
                                    value={inputValues.kindOfWork.name}
                                    disabled={true}
                                    className="form-control">
                                </Field>
                            </div>
                            
                            <div className="div-input-column col-sm-6">
                                <Label  for="expertise">Area of expertise</Label>
                                <Field
                                    value={inputValues.expertise.name}
                                    disabled={true}
                                    className="form-control">
                                </Field>
                            </div>
                        </div>

                        

                        <div className="div-input-row request-booking-disabled col-sm-12 mt-3">
                            <div className="div-input-column col-sm-6">
                                <Label for="language">Language</Label>
                                <Field
                                    value={inputValues.language.name}
                                    disabled={true}
                                    className="form-control">
                                </Field>
                            </div>
                        </div>

                        <div className="div-input-row request-booking-enabled col-sm-12 mt-3">
                            <div className="div-input-column col-sm-12 mt-3">
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

                        <div className="div-input-row request-booking-enabled col-sm-12 mt-3">
                            <div className="div-input-column col-sm-6">
                                <Label for="pagesNo">Pages needed</Label>
                                <div className="div-input-row">
                                    <span className="input-group-text mr-2"><RiPagesFill/></span>
                                    <Field
                                        className="form-control"
                                        disabled={true}
                                        value={inputValues.number}
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
                                    value={inputValues.deadline}
                                    disabled={true}
                                    label="Deadline" 
                                    type="date"
                                    autoComplete="off" 
                                    required>
                                </Field>
                                <ErrorMessage component="p" className="field-colorchange" name="deadline"/>
                            </div>
                        </div>

                        <div className="div-input-row request-booking-enabled col-sm-12 mt-3">
                            <div className="div-input-column col-sm-12 mt-3">
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

                        <div className="div-input-row request-booking-disabled col-sm-12 mt-3">
                            <div className="div-input-column col-sm-6">
                                <Label for="plannedBudget">Financial offer per page</Label>
                                <div className="div-input-row">
                                    <span className="input-group-text mr-2">&#8364;</span>
                                    <Field
                                        className="form-control"
                                        name="plannedBudget" 
                                        value={inputValues.budget || user.pricePerPage}
                                        onChange={(e: any) => { { setInputValues({ ...inputValues, budget: parseInt(e.target.value) })} }}
                                        label="Financial offer per page" 
                                        type="number"
                                        autoComplete="off" 
                                        required>
                                    </Field>
                                </div>
                                <ErrorMessage component="p" className="field-colorchange" name="plannedBudget"/>
                            </div>
                            <div className="div-input-column col-sm-6">
                                        <Label for="pricePerPage">Total project value</Label>
                                <div className="div-input-row">
                                    <span className="input-group-text mr-2">&#8364;</span>
                                    <Field
                                        className="form-control"
                                        name="pricePerPage"
                                        value={inputValues.budget === undefined ? user.pricePerPage * inputValues.number : inputValues.budget * inputValues.number}
                                        disabled={true}
                                        label="Total project value" 
                                        type="number"
                                        autoComplete="off" 
                                        required>
                                    </Field>
                                </div>
                                        <ErrorMessage component="p" className="field-colorchange" name="pricePerPage"/>
                            </div>
                        </div>
                        <br></br>
                        <div className="div-input-column request-booking-enabled col-sm-12 mt-3">
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
                        <br></br>
                        <div className="button-center col-sm-12">
                            <div className="col-sm-6">
                                <NavLink tag={Link} className="" to="/register">Skip (Create account only)</NavLink>
                            </div>
                            <div className="col-sm-6"><Alert id="signin-success" style={{ display: 'none' }} color="success">Sign in was succesfull. Proceed to request booking.</Alert>
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

export default RequestBooking;