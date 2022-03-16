import React, { useState, useEffect, useRef } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { NavLink, Button, Media, Form, Badge, Card, CardBody, Label } from 'reactstrap';
import { Link } from 'react-router-dom';
import dayjs from 'dayjs';
import { RiSendPlaneFill } from 'react-icons/ri';
import ReactLoading from 'react-loading';
import { MessageBox } from 'react-chat-elements'
import 'react-chat-elements/dist/main.css';
import { BiArrowBack } from "react-icons/bi";
import { MdChatBubble } from "react-icons/md";
import { FaAward } from "react-icons/fa"



import { RiUserLine } from "react-icons/ri";
import { FiUpload } from "react-icons/fi";
import { Input } from 'react-chat-elements';
import NumberFormat from 'react-number-format';
import { Formik, ErrorMessage, Field } from 'formik';
import { Popup } from 'react-chat-elements';
import * as yup from 'yup';
import { useAlert } from 'react-alert';
import ReactTooltip from 'react-tooltip';
import { BsFileCheck, BsFileEarmarkCheck, BsFileText } from "react-icons/bs";
import { invokeConnection } from '../Helpers/SignalRMiddleware';

import AuthorPublicInformation from '../AuthorProfile/AuthorPublicInformation';
import { GetBookingDetailsCustomer, GetAllMessages, SendMessage } from '../../services/CustomerServices';
import { ActionCreators } from '../../store/CustomerReducer';
import { DownloadDocument } from '../../services/CustomerServices';
import { UploadDocument } from '../../services/ProfileServices';
import { CreateDispute, ConfirmProject, CancelProject } from '../../services/CustomerServices';
import ScrollableFeed from 'react-scrollable-feed'

import { makeStyles, withStyles } from '@material-ui/core/styles';
import Rating from '@material-ui/lab/Rating';
import Box from '@material-ui/core/Box';
import { RateAuthor } from '../../services/AuthorServices';

let refundSchema = yup.object().shape({
    message: yup.string().required("Required.")
});

const labels : any = {
    1: 'Bad',
    2: 'Poor',
    3: 'Ok',
    4: 'Good',
    5: 'Excellent',
  };

const useStyles = makeStyles({
    root: {
      width: 200,
      display: 'flex',
      alignItems: 'center',
    },
  });

const StyledRating = withStyles({
    iconFilled: {
      color: '#DAA520',
    },
    iconHover: {
      color: '#DAA520',
    },
})(Rating);

export const CustomerOpenProjectPage = () => 
{

    const queryString = require('query-string');
    const stateParams : any = queryString.parse(window.location.pathname);

    const chatContainer: any = React.createRef();

    const dispatch = useDispatch();

    const activeProject = useSelector((state: any) => state.customerReducer.projectDetailsCustomer);
    const loadingValue = useSelector((state: any) => state.customerReducer.projectDetailsLoadingValueCustomer);

    //rating
    const [value, setValue] = useState<any>(activeProject.ratingDTO?.starRating);
    const [hover, setHover] = useState<any>(-1);
    const classes = useStyles();

    const [popupCustomer, showPopupCustomer] = useState(false);
    const [popupCustomerDispute, showPopupCustomerDispute] = useState(false);
    const [popupCancelProject, showpopupCancelProject] = useState(false);

    const closePopupCancelProject = () =>
    {
        showpopupCancelProject(false);
    }

    const [selectedFile, setSelectedFile] = useState();
    const [isFilePicked, setIsFilePicked] = useState(false);

    var testttt = localStorage.getItem('token');
    const messages = useSelector((state: any) => state.customerReducer.messagesCustomer.filter((x: any) => x.headProposalId === parseInt(stateParams.param)));
    const [messageText, setMessageText] = useState('');

    const messagesEndRef = useRef<HTMLInputElement>(null);
    const chatInputRef = useRef<any>(null);    
    
    const changeHandler = (event: any) => {
        setSelectedFile(event.target.files[0]);
        setIsFilePicked(true);
    };

    const alert = useAlert();
    const history = useHistory();

    const [popUpState, setPopUpState] = useState(false);
    const [activePopup, setActivePopup] = useState(-1);
    

  let popupContent = () => 
  (<div className="switch-buttons center">
      <div className="dispute-form-elements-container">
          <Formik initialValues={{ message: '' }}
                  onSubmit={(values: any) => CreateDispute(stateParams.id, values.message, alert).then(()=> history.push('/customer-profile/refresh') )}
                  validationSchema={refundSchema}>

              {({ dirty, isValid, handleSubmit }) => {

                  return (
                      <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
                          <div className="switch-buttons center">
                              <div className="payment-popup-container center column-style">
                                  <div className="label-element center column-style">
                                      
                                      <Label for="message" className="txt-popup-payment">Please submit your message to our dispute resolution center</Label>
                              <Field
                                          className="form-control dispute-msg"
                                  variant="outlined"
                                  name="message"
                                  label="message"
                                  type="text"
                                  as="textarea"
                                  autoComplete="off"
                                  required>
                              </Field>
                              <ErrorMessage component="p" className="field-colorchange" name="message" />
                          </div>

                          
                          <div className="btncreate-dispute-msg">
                              <br></br>
                              <button type="submit" className="btn btn-primary" disabled={!dirty || !isValid} >Open dispute</button>
                                  </div>
                                  </div>
                              </div>
                      </Form>)
              }}
          </Formik>
      </div>
  </div>)
  
    let popupContentCancelProject = () => 
        (<div className="switch-buttons center">
            <Button onClick={()=> CancelProject(stateParams.id, alert, history, closePopupCancelProject).then(()=>history.push('/customer-profile/refresh'))} color="primary">Confirm</Button>
            <Button className="ml-3" onClick={()=>showpopupCancelProject(false)} color="secondary">Cancel</Button>
        </div>)

    const handleSubmission = () => {
        const errordocument = document.getElementById('error-document')!;
        if (isFilePicked != false) {
            UploadDocument(activeProject.bookingId, selectedFile, alert).then(()=>history.push('/customer-profile/refresh'));
            errordocument.style.display = 'none';
        }
        else {
            errordocument.style.color = 'red';
            errordocument.style.display = 'block';
        }
    };

    const handleMessageInput = (event: any) => {
        setMessageText(event.target.value);
    }

    useEffect(() => {
        GetBookingDetailsCustomer(dispatch, stateParams.id, alert);
        GetAllMessages(dispatch, stateParams.id, alert);
        dispatch(ActionCreators.setProjectDetailsLoadingValueCustomer(true));
        invokeConnection(stateParams.param);
    }, [stateParams.id, history.location.key])

    useEffect(() => { setValue(activeProject.ratingDTO?.starRating) }, [activeProject])
    
    return (
        <div className="row-style">
            { loadingValue ? <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF' />
            </div> :
                <div className="register-user-box customer row">
                    <div className="register-signin-left-panel col-sm-9 customer project-content">
                        
                        <div className="back-link-cont">
                            <NavLink className="back-link" tag={Link} to={"/customer-profile"}>< BiArrowBack size="24" className="back-icon" /> &nbsp; Back</NavLink>
                        </div>
                        <h4>{activeProject.projectTopic}</h4>
                        <div className="badge-text booking-status"><Badge color='info'>{activeProject.bookingStatus.value}</Badge></div>
                        <div className="sub-cont row">
                            <div className="sub-txt-bold">Kind of work:</div> &nbsp; <div className="sub-txt-lite"> {activeProject.kindOfWorkDTO.value}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Expertise area:</div> &nbsp;<div className="sub-txt-lite">{activeProject.expertiseAreaDTOs.map((u: { value: any; description: any }) => u.value).join(', ')}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Language:</div> &nbsp;<div className="sub-txt-lite"> {activeProject.languageDTO.value}</div>
                        </div>                       
                        <br></br>
                        <div className="project-content-inner">
                            <div className="project-details">
                        <h4>Project description</h4>
                                <p>{activeProject.projectDescription}</p>
                                </div>
                                
                            <div className="final-docs-">
                        { activeProject.bookingStatus.id === 2 || activeProject.bookingStatus.id === 6 ? 
                                    <Card className=" mt-3 cursor row-style final-docs-card ">
                                        <CardBody className=" final-doc-container">
                                        <div className="card-text-final ">
                                            <BsFileEarmarkCheck className="svg-final-submit" /> &nbsp; Final version has been submmited: 
                                        </div>
                                            <Button className="btn-acc" color="primary" size="lg" onClick={() => ConfirmProject(stateParams.id, alert).then(()=> history.push('/customer-profile/refresh'))}><BsFileCheck className="svg-final-file" />&nbsp;&nbsp;Accept Final Version</Button>
                                            <Button className="btn-dsp ml-3 btn btn-secondary "  size="lg" onClick={()=> showPopupCustomer(true)}>Open Dispute</Button>
                                        </CardBody>
                                    </Card> : <div />}
                                { activeProject.bookingStatus.id === 3 || activeProject.bookingStatus.id === 5  ? 
                                    <Card className=" mt-3 cursor row-style final-docs-card ">
                                        <CardBody className=" final-doc-container">
                                            <div className="card-text-final rating-author-stars">
                                                <FaAward className="svg-final-submit" /> &nbsp; { activeProject.ratingDTO === null ? <p>Choose the rating you want to give to the author</p> : <p>Authors rating:</p> }
                                        </div>
                                            <div className={classes.root}>
                                                {value !== null && <Box mr={2}>{labels[hover !== -1 ? hover : value]}</Box>}
                                            <StyledRating name="hover-feedback" disabled={activeProject.ratingDTO !== null ? true : false} value={value} precision={1} onChange={(event, newValue) => {  
                                                                                                                                                if(activeProject.ratingDTO === null) 
                                                                                                                                                { 
                                                                                                                                                   RateAuthor({ starRating: newValue, comment: labels[hover !== -1 ? hover : value], bookingId: stateParams.id}, history, alert); 
                                                                                                                                                } 
                                                                                                                                            } }
                                                onChangeActive={(event, newHover) => {
                                                    setHover(newHover);
                                                }}/>
                                           
                                        </div>
                                        </CardBody>
                                    </Card> : <div/> }
                                </div>
                            </div>
                    </div>
                    <div className="register-signin-right-panel col-sm-3 column-style project-info">
                        <h4>Requirements:</h4>
                        <p className="deadline-txt-cont"><span className="bold-text"> Date: </span>{dayjs(activeProject.deadline).format('DD.MM.YYYY').toString()}</p>
                        <p className="pages-txt-cont" ><span className="bold-text">Pages number:</span> {activeProject.pagesNo}</p>
                        <p className="price-txt-cont" ><span className="bold-text-price">Price:</span> &#8364;&nbsp;<NumberFormat value={activeProject.totalPrice} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
                        { activeProject.bookingStatus.id === 4 
                        ||  activeProject.bookingStatus.id === 3 
                        ||  activeProject.bookingStatus.id === 0
                        || activeProject.bookingStatus.id === 5 ? <div/> : 
                        <div className="center column-style"><Button className="cancel-project btn-cancel"  onClick={()=> showPopupCustomerDispute(true)}>Cancel project</Button>
                        <p className="open-dispute-txt">(Open dispute)</p></div> }
                        { activeProject.bookingStatus.id === 0 ? 
                        <div className="center column-style"><Button className="cancel-project btn-cancel"  onClick={()=> showpopupCancelProject(true)}>Cancel project</Button></div> : <div/>}
                        <h4 className="client-about-title customer" >Author: </h4>
                        <p className="client-username cursor" onMouseEnter={() => { setPopUpState(true); setActivePopup(activeProject.authorId) }} onMouseLeave={() => { setPopUpState(false); setActivePopup(-1); }} data-tip="" data-for={`${activeProject.authorId}`} data-type="light" onClick={()=> { history.push(`/customer-profile/preview-author&id=${activeProject.authorId}`) }}><RiUserLine /> &nbsp; <span className="username-reight-sidebar">{activeProject.authorUsername}</span>{/*&nbsp;<BsBoxArrowUpRight className="author-hover-icon" color="#4E74DE" size="14" /> */}</p>
                        {(popUpState && activePopup === activeProject.authorId) ? <ReactTooltip id={`${activeProject.authorId}`} getContent={() => { return <AuthorPublicInformation value={activeProject.authorId} /> }} /> : <div />}

                        
                        { activeProject.bookingStatus.id === 0 ?
                            <div className="col-sm-12 payment-btn-div"><NavLink className="payment-nav-link" tag={Link} to={{ pathname: "/customer-profile/payment", paymentDetails: activeProject }}><button type="button" className="btn btn-primary btn-lg payment-button">Make Payment</button></NavLink></div>
                            : <div className="center"><span className="badge-text booking-status customer"><Badge className="badge-right-sidebar" color='info'>Payment was made successfully.</Badge></span></div>}

                    </div>
                    <div className="col-sm-9 customer chat-cont"><h4>Messages: </h4>
                        <div className="msg-cont customer">
                        {
                            <div ref={chatContainer} className="chat-box customer" id="chat-div">

                                {messages.length === 0 && <MessageBox position={'left'} type={'text'} text={'Still no messages, say Hi!'} date={new Date()} />}
                                <ScrollableFeed>
                                {messages.map((x: any) => x.isLogMessage === true ? <h4 key={x.id} className="center"><Badge color="secondary">{x.messageText}</Badge></h4> : x.myMessage === false ? (
                                    <MessageBox
                                        key={x.id}
                                        id={x.id}
                                        position={'left'}
                                        type={'text'}
                                        text={x.messageText}
                                        date={new Date(x.dateTimeSent + (x.dateTimeSent.slice(-1) === "Z" ? "" : "Z"))}
                                    />) : (<MessageBox
                                        key={x.id}
                                        id={x.id}
                                        position={'right'}
                                        type={'text'}

                                        text={x.messageText}
                                        
                                        date={new Date(x.dateTimeSent + (x.dateTimeSent.slice(-1) === "Z" ? "" : "Z"))}
                                    />))
                                }
                                </ScrollableFeed>
                            </div>
                        }
                        { activeProject.bookingStatus.id === 3 || activeProject.bookingStatus.id === 5 ? 
                            <div className="center"><span className="badge-text booking-status"><Badge color='warning'>You cannot chat with customer while project is in this status.</Badge></span></div> : <div>
                        <div className="input-chat-icon">< MdChatBubble /></div>
                        <Form onSubmit={(e)=>{e.preventDefault(); if (messageText != '') { SendMessage(dispatch, activeProject.bookingId, stateParams.param, messageText, alert); setMessageText(''); if (chatInputRef.current != null) { chatInputRef.current.clear() } } }}>
                                <Input placeholder="Your message..." id="message-input" class="chat-input" value={messageText} ref={chatInputRef} onChange={handleMessageInput} rightButtons={
                                <Button className="chat-send" color='primary' type="submit" text='Send'><RiSendPlaneFill /> </Button>
                        }></Input></Form></div>}
                        </div>
                    </div>
                    <div className="col-sm-3 docs-cont customer"><div>
                        <h4 className="docs-title customer" >Documents: </h4>
                        <div className="upload-files-container">
                            {activeProject.documentDTOs.length === 0 ? <div className="center column-style file-list customer-page">
                                <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                                <p className="no-docs-txt">There are no documents.</p>
                                <p className="nofrs-line"> </p>
                            </div> :  
                                <div className="upload-files-container">
                               <div>
                                        <div className="active document-div ml-1" id="style-8">{activeProject.documentDTOs.filter((x: any) => x.isFinalVersion === false).map((x: any) =>
                                            <div key={x.id} className="upload-docs-a"><BsFileText className="mr-1 mb-1 upl-docs-svg" /><div className="final-docs-submitted"><Link to="#" onClick={() => DownloadDocument(x.id, x.publicName, alert)}>{x.publicName}</Link></div><br /></div>)}
                                        </div>
                                        <h4 className="docs-title final-version-docs" >Final version: </h4>
                                    <div className="final-document-div" id="style-8">{activeProject.documentDTOs.filter((x: any) => x.isFinalVersion === true).map((x: any) =>
                                        <div key={x.id}><BsFileEarmarkCheck className="mr-1 mb-1 final-docs-svg" /><div className="final-docs-submitted"><Link to="#" className="" onClick={() => DownloadDocument(x.id, x.publicName, alert)}>{x.publicName} | (FINAL VERSION)</Link></div><br /></div>)}
                                </div>
                               </div>

                                </div>}
                                { activeProject.bookingStatus.id === 1 ?
                                <div className="upload">
                                    <input className="input-upload" type="file" name="file" id="fileUpload" onChange={changeHandler} />
                                    <button className="btn-upload" onClick={handleSubmission}><FiUpload className="project-upload-icons" /></button>
                                </div> : 
                                <div className="center"><span className="badge-text booking-status"><Badge color='warning'>You cannot upload documents.</Badge></span></div>}
                        </div>
                        <div>
                            <p id="error-document" style={{ display: 'none' }}>Please select document before uploading.</p>
                        </div>
                    </div>
                    </div>
                    <Popup show={popupCustomer} 
                       header='Refund to customer.'
                       headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showPopupCustomer(false); }}]}
                       renderContent={() => { return popupContent() }}/>

                    <Popup show={popupCustomerDispute} 
                       header='Dispute'
                       headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showPopupCustomerDispute(false); }}]}
                       renderContent={() => { return popupContent() }}/>

                    
                    <Popup show={popupCancelProject} 
                       header='Are you sure you want to cancel this project?'
                       headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showpopupCancelProject(false); }}]}
                       renderContent={() => { return popupContentCancelProject() }}/>
                </div>}
        </div>)
}

export default CustomerOpenProjectPage;

