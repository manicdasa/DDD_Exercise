import React, { useState, useEffect, useRef } from 'react';
import { useHistory, useLocation, useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { NavLink, Button, Media, Form, Badge, UncontrolledCollapse } from 'reactstrap';
import { Link } from 'react-router-dom';
import dayjs from 'dayjs';
import { AiFillCheckCircle } from 'react-icons/ai';
import ReactLoading from 'react-loading';
import { RiErrorWarningFill, RiUserLine } from 'react-icons/ri';
import { MessageBox, Input } from 'react-chat-elements'
import 'react-chat-elements/dist/main.css';
import { FiUpload } from "react-icons/fi";
import { FaFileUpload } from "react-icons/fa";
import { BiArrowBack } from "react-icons/bi";
import { RiSendPlaneFill } from "react-icons/ri";
import { GrDocumentDownload, GrDocument } from 'react-icons/gr';
import { BsFileText, BsFileEarmarkCheck } from 'react-icons/bs';
import ScrollableFeed from 'react-scrollable-feed'

import { MdChatBubble, MdKeyboardArrowDown } from "react-icons/md";
import NumberFormat from 'react-number-format';
import { useAlert } from 'react-alert';
import { invokeConnection } from '../Helpers/SignalRMiddleware';

import { DownloadDocument, GetBookingDetails, SubmitFinalVersion, UploadDocument, GetAllMessages, SendMessage } from '../../services/ProfileServices';
import { ActionCreators } from '../../store/AuthorActiveProjectsReducer';
import { GiPriceTag } from 'react-icons/gi';

export const OpenBookingPage = () => 
{
    const queryString = require('query-string');
    const stateParams : any = queryString.parse(window.location.pathname);
    
    const history = useHistory();

    const dispatch = useDispatch();
    const chatContainer: any = React.createRef();

    const alert = useAlert();

    const activeProject = useSelector((state: any) => state.authorActiveProjectsReducer.projectDetails);
    const loadingValue = useSelector((state: any) => state.authorActiveProjectsReducer.projectDetailsLoadingValue);

    const messages = useSelector((state: any) => state.authorActiveProjectsReducer.messages.filter((x: any) => x.headProposalId === parseInt(stateParams.param)));
    
    const [messageText, setmessageText] = useState('');
    const [connection, setConnection] = useState<any>(null);

    const chatInputRef = useRef<any>(null);

    const handleMessageInput = (event: any) => 
    {
        setmessageText(event.target.value);
    }

    const [selectedFile, setSelectedFile] = useState();
    const [isFilePicked, setIsFilePicked] = useState(false);
    const [selectedFileFinalVersion, setSelectedFileFinalVersion] = useState();
    const [isFilePickedFinalVersion, setIsFilePickedFinalVersion] = useState(false);

    const changeHandler = (event: any) => {
        setSelectedFile(event.target.files[0]);
        setIsFilePicked(true);
    };

    const changeHandler1 = (event: any) => {
        setSelectedFileFinalVersion(event.target.files[0]);
        setIsFilePickedFinalVersion(true);
    };

    const handleSubmission = () => 
    {
        const errordocument = document.getElementById('error-document')!;
        if (isFilePicked != false) {
            UploadDocument(activeProject.bookingId, selectedFile, alert).then(() => history.push('/profile/refresh'));
            errordocument.style.display = 'none';
        }
        else {
            errordocument.style.color = 'red';
            errordocument.style.display = 'block';
        }
    };

    const submitFinalVersion = () => {
        const errordocument = document.getElementById('error-document')!;
        if (isFilePickedFinalVersion != false) {
            SubmitFinalVersion(activeProject.bookingId, selectedFileFinalVersion, alert).then(() => history.push('/profile/refresh'));
            errordocument.style.display = 'none';
        }
        else {
            errordocument.style.color = 'red';
            errordocument.style.display = 'block';
        }
    };

    useEffect(() => {
        GetBookingDetails(dispatch, stateParams.id, alert);
        GetAllMessages(dispatch, stateParams.id, alert);
        dispatch(ActionCreators.setProjectDetailsLoadingValue(true));
        invokeConnection(stateParams.param);
    }, [stateParams.id, history.location.key])

    if (activeProject.customerDTO === null) 
    {
        return <div></div>
    }

    return (
        <div className="row-style register-user-box proposal-page row">
            { loadingValue ? <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF' />
            </div> :
                <div className="">
                    <div className="register-signin-left-panel col-sm-9 proposal-left project-content">
                        <div className="back-link-cont row-style col">
                            <NavLink className="back-link booking-status-span txt-link-ghw" tag={Link} to={"/profile"}>< BiArrowBack size="24" className="back-icon" /> &nbsp; Back</NavLink>
                        </div>
                        <h4 className="project-title-details">{activeProject.projectTopic}</h4>
                        <span className="badge-text booking-status"><Badge color='info'>{activeProject.bookingStatus.value}</Badge></span>
                        <div className="sub-cont row">
                            <div className="sub-txt-bold">Kind of work:</div> &nbsp; <div className="sub-txt-lite"> {activeProject.kindOfWorkDTO.value}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Expertise area:</div> &nbsp;<div className="sub-txt-lite">{activeProject.expertiseAreaDTOs.map((u: { value: any; description: any }) => u.value).join(', ')}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Language:</div> &nbsp;<div className="sub-txt-lite"> {activeProject.languageDTO.value}</div>
                        </div>
                        <br></br>
                        <div className="project-cont">
                            <h4 className="proj-desc-title">Project description</h4>
                            <p>{activeProject.projectDescription}</p>
                        </div>

                    </div>
                    <div className="register-signin-right-panel col-sm-3 proposal-right project-info">
                        <h4>Requirements:</h4>
                        <div className="column-style mrt-15">
                        <p className="pages-txt-cont"><span className="bold-text"> Deadline: </span>{dayjs(activeProject.deadline).format('DD.MM.YYYY').toString()}</p>
                        <p className="pages-txt-cont" ><span className="bold-text">Pages number:</span> {activeProject.pagesNo}</p>
                            <div id="toggler" className="author-info-btn profile-subtitle toggle-price">
                                <span>
                                    <div className="gross-amnt-cont">Gross Amount:</div><div className="gross-amnt-total">&#8364;&nbsp;<NumberFormat value={activeProject.totalPrice} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /><span className="tooltiptext1">Price Breakdown</span></div>&nbsp;&nbsp;&nbsp;<MdKeyboardArrowDown color="#4E74DE" className="edit-icon-auth-info gross-show" />
                                    {/*<MdKeyboardArrowDown className="edit-icon-auth-info gross-show" />*/}
                                </span>
                                
                            </div>
                            <UncontrolledCollapse toggler="#toggler" className="price-breakdown-container">
                                <div className="breakdown-title-box">Price Breakdown</div>
                                <div className="price-txt-cont" ><span className="bold-text-price">Taxes:</span><p className="tax-p-tag">&nbsp;&#8364;&nbsp;<NumberFormat value={activeProject.totalPrice * activeProject.taxPercentage / 100} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> ({activeProject.taxPercentage}%)</p></div>
                                <div className="price-txt-cont" ><span className="bold-text-price">Fees:</span><p className="p-price-breakdown">&nbsp;&#8364;&nbsp;<NumberFormat value={activeProject.feePerPage * activeProject.pagesNo} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> (&#8364;{activeProject.feePerPage} / page)</p></div>
                            </UncontrolledCollapse>
                            </div>
                        <h4 className="client-about-title" >About the client: </h4>
                        <p className="client-username"><RiUserLine /> &nbsp; {activeProject.customerDTO.username}</p>
                        <div className="row-style">
                            <p>{activeProject.paymentVerified ? <AiFillCheckCircle className="mb-1" color='green' /> : <RiErrorWarningFill className="mb-1" color='red' />}</p>
                            <p className="ml-2 payment-status-about">&nbsp;{activeProject.paymentVerified ? 'Payment Verified' : 'Payment not verified '}</p>
                        </div>

                    </div>
                    <div className="col-sm-9 chat-cont customer"><h4>Messages: </h4>
                        <div className="msg-cont customer">
                        {
                            <div ref={chatContainer} className="chat-box customer" id="chat-div">
                                <ScrollableFeed>
                                {messages.length === 0 ? <MessageBox position={'left'} type={'text'} text={'Still no messages, say Hi!'} date={new Date()} /> :
                                
                                    messages.map((x: any) => x.isLogMessage === true ? <h4 key={x.id} className="center"><Badge color="secondary">{x.messageText}</Badge></h4> : x.myMessage === false ? (
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
                            <div className="center no-chat-msg-badge"><span className="badge-text booking-status"><Badge color='primary'>You cannot chat with customer while project is in this status.</Badge></span></div> : <div>
                            <div className="input-chat-icon">< MdChatBubble /></div>
                                    <Form onSubmit={(e) => { e.preventDefault(); if (messageText != '') { SendMessage(dispatch, activeProject.bookingId, activeProject.headProposalId, messageText, alert); setmessageText(''); if (chatInputRef.current != null) { chatInputRef.current.clear() } } }}>
                                <Input placeholder="Your message..." id="message-input" class="chat-input" value={messageText} ref={chatInputRef} onChange={handleMessageInput} rightButtons={
                                    <Button className="chat-send" color='primary' type="submit" text='Send'><RiSendPlaneFill /> </Button>
                            }></Input></Form></div>}
                        </div>
                    </div>

                        <div className="col-sm-3 docs-cont customer">

                        <div>
                            <h4 className="docs-title customer" >Documents: </h4>
                            <div className="upload-files-container">
                            {activeProject.documentDTOs.length === 0 ?
                                    <div className="center column-style file-list customer-page">
                                    <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                                    <p className="no-docs-txt">There are no documents.</p>
                                    <p className="nofrs-line"> </p>
                                    </div> : <div className="upload-files-container">
                                        <div className="active document-div ml-1" id="style-8">{activeProject.documentDTOs.filter((x:any)=>x.isFinalVersion===false).map((x: any) => 
                                            <div key={x.id} className="upload-docs-a"><BsFileText className="mr-1 mb-1 upl-docs-svg" /><Link className="final-docs-submitted" to="#" onClick={() => DownloadDocument(x.id, x.publicName, alert)}>{x.publicName}</Link><br /></div>)}
                                        </div>
                                        <h4 className="docs-title final-version-docs" >Final version: </h4>
                                        <div className="final-document-div" id="style-8">{activeProject.documentDTOs.filter((x:any)=>x.isFinalVersion===true).map((x: any) => 
                                            <div key={x.id}><BsFileEarmarkCheck className="mr-1 mb-1 final-docs-svg" /><Link to="#" className="bold-text" onClick={() => DownloadDocument(x.id, x.publicName, alert)}>{x.publicName} | (FINAL VERSION)</Link><br /></div>   )}
                                            </div>
                                        </div>
                            }
                            { activeProject.bookingStatus.id === 1 ? 
                            <div className="upload-files-container">
                                <div className="upload">
                                    <p className="center">Upload Document</p>
                                    {/*<label id="fileLabel" className="label-img-name">Choose file</label>*/}
                                    <input className="input-upload" type="file" name="file" id="fileUpload" onChange={changeHandler} />
                                    <button className="btn-upload" onClick={handleSubmission}>< FiUpload className="project-upload-icons" /></button>
                                </div>
                                <div className="line-di"></div>
                                <div className="upload-final">
                                    <p className="center">Submit Final Version</p>
                                    {/*<label id="fileLabel1" className="label-final-doc">Submit final version</label>*/}
                                    <input className="input-upload-final" type="file" name="file1" id="fileUpload1" onChange={changeHandler1} />
                                    <button className="btn-upload-final" onClick={submitFinalVersion}>< FaFileUpload className="project-upload-icons" /></button>
                                </div>
                            </div> : <div className="center"><span className="badge-text booking-status"><Badge color='warning'>You cannot upload documents.</Badge></span></div>}
                                </div>
                                <div>
                                <p id="error-document" style={{ display: 'none' }}>Please select document before uploading.</p>
                            </div>
                        </div></div>
                    </div>}</div>)
}


export default OpenBookingPage;

