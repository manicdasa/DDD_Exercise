import React, { useState, useEffect, useRef } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import ReactLoading from 'react-loading';
import dayjs from 'dayjs';
import classNames from "classnames";
import { Container, Media, Button, Label, Form, Badge, NavLink, Input } from "reactstrap";
import { AiFillProject } from 'react-icons/ai';
import { Link } from 'react-router-dom';
import { MessageBox } from 'react-chat-elements'
import Slider from '@material-ui/core/Slider';
import { Popup } from 'react-chat-elements';
import { Formik, ErrorMessage, Field } from 'formik';
import * as yup from 'yup';
import { ImUndo } from 'react-icons/im';
import { RiUserReceivedLine } from 'react-icons/ri';
import NumberFormat from 'react-number-format';
import ScrollableFeed from 'react-scrollable-feed'
import { useAlert} from 'react-alert';

import Sidebar from "../../components/AdminDashboard/Sidebar";

import { GetProjectDetails } from '../../services/AdminServices';
import { GetAllMessages } from '../../services/CustomerServices';
import { AcceptDispute, DeclineDispute } from '../../services/DisputeServices';

let refundSchema = yup.object().shape({
  message: yup.string().required("Required.")
});

let declineDisputeSchema = yup.object().shape({
  message: yup.string().required("Required.")
});


export const DashboardConcreteProject = () =>
{
  const alert = useAlert();

  const chatContainer: any = React.createRef();
  const messagesEndRef = useRef<HTMLInputElement>(null);

  const [sidebarIsOpen, setSidebarOpen] = useState(true);
  const toggleSidebar = () => setSidebarOpen(!sidebarIsOpen);

  const [popupCustomer, showPopupCustomer] = useState(false);
  const [popupDeclineDispute, showPopupDeclineDispute] = useState(false);
  const [popupPartial, showPopupPartial] = useState(false);
    
  const [pageRangeValue, setPageRangeValue] = useState(0);
  const [pageRangeValue1, setPageRangeValue1] = useState(0);
  const [valueRefund, setValueRefund] = useState(0);

  const [enabledInputs, setEnabledInputs] = useState(false);
  const [enabledInputsAuthor, setEnabledInputsAuthor] = useState(false);

  const [valueifcheckedauthor, setvalueifcheckedauthor] = useState(0);

  const [enabledSlider, setEnabledSlider] = useState(false);

  function valuetext(value: any) 
  {
      return `${value}%`;
  }

  const messages = useSelector((state: any) => state.customerReducer.messagesCustomer);

  const { id } : any = useParams();

  

  const [projects, setProjects] = useState({
        id: 0,
        deadline: "",
        totalPrice: 0,
        totalServiceCharges: 0,
        paymentVerified: true,
        projectTopic: "",
        projectDescription: "",
        pagesNo: 0,
        disputeMessage: "",
        customerDTO: {
          id: 0,
          totalSpent: 0,
          jobsPostedCnt: 0,
          username: ""
        },
        bookingStatus: { id: 0, value: '' },
        authorUsername: "",
        languageDTO: {
          id: 0,
          value: ""
        },
        kindOfWorkDTO: {
          id: 0,
          value: "",
          description: ""
        },
        expertiseAreaDTOs: [
          {
            id: 0,
            value: "",
            description: ""
          }
        ],
        documentDTOs: [
          {
            id: 0,
            publicName: "",
            dateCreated: ""
          }
        ],
        proposalDetailsDTOs: [
          {
            id: 0,
            financialOffer: 0,
            financialOfferWithCharges: 0,
            proposalType: "",
            proposalStatus: "",
            dateCreated: "",
            customerUsername: "",
            authorUsername: "",
            authorId: ""
          }
        ],
        customerPublicInfoDTO: {
          id: 0,
          totalSpent: 0,
          jobsPostedCnt: 0,
          username: "",
          paymentVerified: true
        }
  });

  const [loading, setLoading] = useState<boolean>(false);

  const dispatch = useDispatch();

  let declineDisputePopup = (Method: any) => 
  (<div className="switch-buttons center">
      <div className="login-form-elements-container">
          <Formik initialValues={{ message: '' }}
                  onSubmit={(values: any) => { Method(id, values.message, alert);   }}
                  validationSchema={declineDisputeSchema}>

              {({ dirty, isValid, handleSubmit }) => {

                  return (
                      <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>

                          <div className="label-element center column-style">
                              <Label for="message">Reason for declining dispute</Label>
                              <Field
                                  className="form-control"
                                  variant="outlined"
                                  placeholder="Message..."
                                  name="message"
                                  label="message"
                                  type="text"
                                  as="textarea"
                                  autoComplete="off"
                                  required>
                              </Field>
                              <ErrorMessage component="p" className="field-colorchange" name="message" />
                          </div>

                          <p className="del">--</p>
                          <div className="btncreate">
                              <br></br>
                              <button type="submit" className="btn btn-primary ml-5" disabled={!dirty || !isValid} >Confirm</button>
                          </div>
                      </Form>)
              }}
          </Formik>
      </div>
  </div>)

    
  let popupContent = (Method: any, amount: any, amountAuthor: any) => 
  (<div className="switch-buttons center">
      <div className="login-form-elements-container">
          <Formik initialValues={{ message: '' }}
              onSubmit={(values: any) => { Method(id, amount, amountAuthor, values.message, alert); }}
                  validationSchema={refundSchema}>

              {({ dirty, isValid, handleSubmit }) => {
                  return (
                      <Form onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>

                          <div className="label-element center column-style">
                              <div className="center mb-3"><div>Are you sure you want to refund 
                                &#8364;&nbsp;{amount}&nbsp;
                                to customer and pay &#8364;&nbsp;{amountAuthor}&nbsp; to author?</div></div>
                              <Label for="message">Refund Message</Label>
                              <Field
                                  className="form-control"
                                  variant="outlined"
                                  placeholder="Refund Message"
                                  name="message"
                                  label="message"
                                  type="text"
                                  as="textarea"
                                  autoComplete="off"
                                  required>
                              </Field>
                              <ErrorMessage component="p" className="field-colorchange" name="message" />
                          </div>

                          <p className="del">--</p>
                          <div className="btncreate">
                              <br></br>
                              <button type="submit" className="btn btn-primary ml-5" disabled={!dirty || !isValid} >Confirm</button>
                          </div>
                      </Form>)
              }}
          </Formik>
      </div>
  </div>)

  useEffect(()=>
  {
        setLoading(true);
        GetProjectDetails(dispatch,id, alert).then((data)=>
            { 
                setProjects(data);
                setLoading(false);
            });
        GetAllMessages(dispatch, id, alert);
  }, [])

  return(
    <div className="App wrapper">
      <div className="main-dashboard">
              <Container fluid className={classNames("content", { "is-open": sidebarIsOpen })} >
                  
            <h4 className="dashboard-table-title"><AiFillProject /> &nbsp; Project Details</h4>
            <div>
            {loading ?
                <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                    <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF' />
                </div> :
            <div className="row content-container" style={{border: '0px solid silver'}}>
              <div className="register-signin-left-panel col-sm-7 project-content">
                <h4>{projects.projectTopic}</h4> 
                <div className="sub-cont row">
                    <div className="sub-txt-bold">Kind of work:</div> &nbsp; <div className="sub-txt-lite"> {projects.kindOfWorkDTO.value}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Expertise area:</div> &nbsp;<div className="sub-txt-lite">{projects.expertiseAreaDTOs?.map((u: { value: any; description: any }) => u.value).join(', ')}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Language:</div> &nbsp; <div className="sub-txt-lite"> {projects.languageDTO.value}</div>
                </div>
                <br></br>
                <h3>Project description</h3>
                <p>{projects.projectDescription}</p>
                
              </div>
              { projects.bookingStatus.id === 4 ? <div className="register-signin-right-panel col-sm-5 column-style project-info dispute"><div>
                <h4>Customer Dispute message</h4>
                <div className="usr-disp-msg"><p>{projects.disputeMessage}</p>
                <div className="row-style">
                  <Button className="btn btn-primary refund-btn" onClick={()=> showPopupDeclineDispute(true)}><RiUserReceivedLine/>&nbsp; Decline Dispute</Button>
                </div>
                                  </div>
                                  {/*<div className="fullrefund-cont">
                                      <div className="row full-rfnd">
                                          <h4>Full Refund </h4> <h4 className="">&#8364;&nbsp;<NumberFormat value={projects.totalPrice} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></h4>
                                          </div>
                                      
                <div className="row-style refund-btns center ">
                                          <Button className="btn btn-primary refund-btn" onClick={() => showPopupCustomer(true)}><RiUserReceivedLine/>&nbsp;Full refund to Customer</Button>
                                      </div>
                                  </div>*/}
                                  <div className="refund-partial-cont">
                                      <h4> Refund </h4>
                                      <div className="slider-cont">
                <Slider 
                      defaultValue={0} 
                      getAriaValueText={valuetext} 
                      valueLabelFormat={valuetext}
                      value={enabledInputs ? 0 : Math.round(pageRangeValue)}
                      disabled={enabledSlider}
                      onChange={(event: any, newValue: any) => { setPageRangeValue(newValue); setPageRangeValue1(newValue); }}
                      aria-labelledby="discrete-slider-always" 
                      step={1} 
                      marks={[
                        {
                          value: 0,
                        },
                        {
                          value: 100,
                        },
                      ]} 
                      valueLabelDisplay="on" />
                </div>
                
                <label>Manual refund to customer:<input className="ml-2" name="isGoing" type="checkbox" checked={enabledInputs} onChange={(event: any) => { setvalueifcheckedauthor(0); setPageRangeValue(0); setEnabledSlider(event.target.checked); setEnabledInputs(event.target.checked) }} /> </label>
                <div className="mt-3 column-style rounded center" style={{ border: '0px solid silver' }}>
                    <div className="row rfnd-txt">
                        <div className="refund-cust"><span className=""><span className="bold-text">Refund to customer: </span>&#8364;&nbsp;
                        <NumberFormat 
                          value={((pageRangeValue / 100) * projects.totalPrice)}  
                          thousandSeparator={true} 
                          decimalScale={2} 
                          fixedDecimalScale={true} 
                          onValueChange={(values: any) => { setPageRangeValue(values.floatValue * 100 / projects.totalPrice) }}
                        />
                        </span>
                        {((pageRangeValue / 100) * projects.totalPrice) > projects.totalPrice 
                        ? <p className="field-colorchange">The amount you entered is greater than project price.</p> : ''}</div>
                        <div className="refund-cust"><span className=""><span className="bold-text">Payment to author: </span>&#8364;&nbsp;
                        <NumberFormat 
                          value={enabledInputs ? 
                          valueifcheckedauthor  
                          : (((projects.totalPrice-(pageRangeValue / 100) * projects.totalPrice) / (projects.totalPrice/projects.pagesNo)) * (0.81*(projects.totalPrice / projects.pagesNo)-9))
                        }  
                          thousandSeparator={true} 
                          decimalScale={2} 
                          fixedDecimalScale={true}
                          onValueChange={(values: any) => 
                          { 
                            if(enabledInputs)
                            {
                              setvalueifcheckedauthor(values.floatValue);
                            }
                            else
                            {
                              setPageRangeValue(((projects.totalPrice - (values.floatValue / (0.81*(projects.totalPrice / projects.pagesNo)-9)) * (projects.totalPrice/projects.pagesNo)) / projects.totalPrice)*100) 
                            }
                          }}
                        />
                        </span>
                                                  {(((projects.totalPrice - (pageRangeValue / 100) * projects.totalPrice) / (projects.totalPrice / projects.pagesNo)) * (0.81 * (projects.totalPrice / projects.pagesNo) - 9)) > (((projects.totalPrice - (0 / 100) * projects.totalPrice) / (projects.totalPrice / projects.pagesNo)) * (0.81 * (projects.totalPrice / projects.pagesNo) - 9)) && valueifcheckedauthor === 0
                        ? <p className="field-colorchange">The amount you entered is greater than project price.</p> : ''}
                        </div>
                        </div>
                    <Button
                      size="sm" 
                      className="btn btn-primary btn-refund-partial" 
                      color="danger" 
                      onClick={() => showPopupPartial(true)}
                      disabled=
                      {((projects.totalPrice-(pageRangeValue / 100) * projects.totalPrice) / (projects.totalPrice/projects.pagesNo)) * (0.81*(projects.totalPrice / projects.pagesNo)-9) < 0 
                      ? true 
                      : (pageRangeValue / 100) * projects.totalPrice < 0 ? true : false }
                    ><ImUndo />&nbsp;&nbsp;Confirm</Button>
                </div>
                                  </div>
              </div></div> : <div className="register-signin-right-panel col-sm-5 column-style project-info dispute center"><span className="badge-text booking-status"><Badge color='primary'>{projects.bookingStatus.value}</Badge></span></div> }
              
              <div className="col-7 chat-cont dispute"><h4>Chat history: </h4>
                  <div className="msg-cont dispute">
                  {
                    <div ref={chatContainer} className="chat-box" id="chat-div">
                  <ScrollableFeed>
                    {messages.length === 0 && <MessageBox position={'left'} type={'text'} text={'Still no messages, say Hi!'} date={new Date()} />}
                    {messages.map((x: any) => (
                        <MessageBox
                            key={x.id}
                            id={x.id}
                            position={'left'}
                            title={x.username}
                            type={'text'}
                            text={x.messageText}
                            date={new Date(x.dateTimeSent + (x.dateTimeSent.slice(-1) === "Z" ? "" : "Z"))}
                        />))
                    }
                    </ScrollableFeed>
                    <div ref={messagesEndRef} />
                  </div>
                  }
                  </div>
              </div>
              <div className="col-5 docs-cont-disp row">
                                  <div className="docs-dispute-cont column-style">
                  <h4 className="" >Documents: </h4>
                  {projects.documentDTOs.length === 0 ? <div className=" column-style">
                    <div className=" mt-2"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                        <p className="no-docs-txt">There are no documents.</p>
                                <p className="nofrs-line"> </p>
                                      </div> : <div className="upload-files-container dispute-docs-admin">
                                              <div className="document-div docs-disp-admin" id="style-8">{projects.documentDTOs.map((x: any) => <div className="uploaded-docs-admin-dispute" key={x.id}><Link to="#" >{x.publicName}</Link><br /></div>)}</div>
                    </div>}
                  <div className="upload-files-container">
                  </div>
                </div>
                <div className="req-cont column-style">
                  <h4>Requirements:</h4>
                  <p className="deadline-txt-cont"><span className="bold-text"> Deadline: </span>{dayjs(projects.deadline).format('DD.MM.YYYY').toString()}</p>
                  <p className="pages-txt-cont" ><span className="bold-text">Pages number:</span> {projects.pagesNo}</p>
                                      <p className="price-txt-cont" ><span className="bold-text-price">Price:</span> &#8364;&nbsp;<NumberFormat value={projects.totalPrice} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
                </div>
              </div>
            </div>}
            </div></Container></div>
            
            <Popup show={popupCustomer} header='Refund to customer.'
              headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showPopupCustomer(false); }}]}
              renderContent={() => { return popupContent(AcceptDispute, projects.totalPrice, 0) }}/>
              
              
            <Popup show={popupDeclineDispute} header='Decline dispute'
              headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showPopupDeclineDispute(false); }}]}
              renderContent={() => { return declineDisputePopup(DeclineDispute)}}/>

              
            <Popup show={popupPartial} header='Partial Customer Refund.'
              headerButtons={[{type: 'transparent', color:'black', text: 'close', onClick: () => { showPopupPartial(false); }}]}
              renderContent={() => { return popupContent(AcceptDispute, ((pageRangeValue / 100) * projects.totalPrice).toFixed(2), 
              valueifcheckedauthor != 0 ? valueifcheckedauthor : (((projects.totalPrice-(pageRangeValue / 100) * projects.totalPrice) / (projects.totalPrice/projects.pagesNo)) * (0.81*(projects.totalPrice/projects.pagesNo)-9)).toFixed(2))}}/>
            </div>)

            
}

export default DashboardConcreteProject;