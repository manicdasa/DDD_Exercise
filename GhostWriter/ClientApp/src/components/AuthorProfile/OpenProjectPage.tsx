import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { NavLink, Button, Input, Media, UncontrolledCollapse, Card, CardBody } from 'reactstrap';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import dayjs from 'dayjs';
import ReactLoading from 'react-loading';
import { BiArrowBack } from "react-icons/bi";
import { RiUserLine } from "react-icons/ri";
import NumberFormat from 'react-number-format';
import { IoMdAddCircle } from "react-icons/io";
import { TiCancel } from "react-icons/ti";
import { useAlert } from 'react-alert';
import { MdEuroSymbol, MdKeyboardArrowDown } from "react-icons/md";

import { GetProjectDetails, GetAuthorsLastBids, CreateBid, CancelBid, AcceptOffer, DeclineOffer, GetAuthorsLastOffers } from '../../services/ProfileServices';
import { ActionCreators} from '../../store/AuthorActiveProjectsReducer'
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';

export const OpenProjectPage = ({ openChats, numberOfOpenChats}: any) =>
{
    const queryString = require('query-string');
    const stateParams : any = queryString.parse(window.location.pathname);
    const alert = useAlert();
    const history = useHistory();

    const dispatch = useDispatch();

    const [offer, setOffer] = useState(0);
    
    const handleChangeOffer = (event : any) => 
    {
        setOffer(event.target.value);    
    }

    const liveBroadcast = useSelector((state: any) => state.authorActiveProjectsReducer.proposalDetails); 
    const loadingValue = useSelector((state: any) => state.authorActiveProjectsReducer.proposalDetailsLoadingValue); 

    const bids = useSelector((state: any) => state.authorOffersReducer.authorsLastBids); 
    const offers = useSelector((state: any) => state.authorOffersReducer.authorsLastOffer);

    const [text, setText] = useState('');

    const [readOnly, setReadOnly] = useState(false);

    useEffect(()=>
    {
        GetProjectDetails(dispatch, stateParams.id, alert);
        GetAuthorsLastBids(dispatch, stateParams.id, alert);
        GetAuthorsLastOffers(dispatch, stateParams.id, alert);
        dispatch(ActionCreators.setProposalDetailsLoadingValue(true));
    }, [stateParams.id, history.location.key] )

    useEffect(()=> {
        if(liveBroadcast.projectStatus != 1)
        {
            setReadOnly(true);
        }
        else if(liveBroadcast.projectStatus === 1)
        { 
            setReadOnly(false);
        }
    }, [liveBroadcast])

    return(
        <div className="row-style register-user-box proposal-page row">
            { loadingValue ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                             </div> :
            <div>
              <div className="register-signin-left-panel col-sm-9 proposal-left project-content">
                <div className="back-link-cont">
                  <NavLink className="back-link" tag={Link} to={"/profile"}>< BiArrowBack size="24" className="back-icon" /> &nbsp; Back</NavLink>
                </div>
                <h4>{liveBroadcast.projectTopic}</h4>
                <div className="sub-cont row">
                    <div className="sub-txt-bold">Kind of work:</div> &nbsp; <div className="sub-txt-lite"> {liveBroadcast.kindOfWorkDTO.value}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Expertise area:</div> &nbsp;<div className="sub-txt-lite">{liveBroadcast.expertiseAreaListDTOs?.map((u: { value: any; description: any }) => u.value).join(', ')}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Language:</div> &nbsp;<div className="sub-txt-lite"> {liveBroadcast.languageDTO.value}</div>
                </div>               
                <br></br>
                        <h4>Project description</h4>
                        <p className="project-text-details">{liveBroadcast.description}</p>
                
              { readOnly === true ? 
                                    <Card className=" mt-3 cursor row-style final-docs-card read-only-card">
                                <CardBody className=" final-doc-container read-only-cardbody">
                                    <div className="card-text-final read-only-cardtext">
                                        <div className="icon-info-readonly-cont"><TiCancel className="svg-readonly-cancel" /> </div> <div className="text-readonly-cont" >This project is no longer active for bids/offers. Here you can only preview the proposal history.</div>
                                        </div>
                                        </CardBody>
                                    </Card> : <div />}
              </div>
                    <div className="register-signin-right-panel col-sm-3 proposal-right project-info">
                        <h4>Requirements: </h4>
                        <div className="column-style mrt-15">
                            <p className="pages-txt-cont" ><span className="bold-text">Number of pages: </span>{liveBroadcast.pagesNo}</p>
                            <p className="pages-txt-cont" ><span className="bold-text">Deadline: </span>{dayjs(liveBroadcast.deadline).format('DD.MM.YYYY').toString()}</p>
                            <p className="pages-txt-cont" ><span className="bold-text">Max budget: </span>&#8364;&nbsp;<NumberFormat value={liveBroadcast.maxBudget} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
                        </div>
                        <h4 className="client-about-title">About the client</h4>
                        <p className="deadline-txt-cont"><span className="bold-text">Customer username: </span><br></br><RiUserLine /> &nbsp; <span className="usrname-usr-info">{liveBroadcast.customerUsername}</span></p>
                
                    </div>
                    <div className="col-sm-9 chat-cont">
                        
                      </div>
                    <div className={(readOnly === true ? 'overlap-readonly col-sm-3 docs-cont offers-section-bl' : 'col-sm-3 docs-cont offers-section-bl')}><div>
                    { JSON.stringify(offers) === '{}'  ? 
                      <div>
                        { JSON.stringify(bids) === '{}' ? 
                        (<div className="column-style center">
                                        <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nobids.png" alt="Typewriter image"></Media></div>
                                        <p className="project-text-details txt-center">There are no bids currently.</p>
                                        <div className="bid-input-field">
                                            <div className="eur-bid-input"><MdEuroSymbol /></div>
                                            <Input className="input-bid-field" type="text" placeholder="0" onChange={handleChangeOffer}></Input>
                                        </div>
                          <br/>
                                        <p style={{ fontWeight: 'bold', textAlign: 'center' }} className="center  field-colorchange">{text}</p>
                                        <Button color="primary" className="add-bid-btn" onClick={() => CreateBid(dispatch, stateParams.id, offer, alert).then((response: any) => { setText(response); history.push('/profile/refresh'); })}><IoMdAddCircle className="add-bid-icon" /> &nbsp; Add bid</Button>
                        </div>) 
                      : <div className="column-style">
                      <h4>My bids</h4> 
                      <div key={bids.id} className="column-style">
                      <div id="toggler" className="author-info-btn profile-subtitle">
                                <span>
                                                    <div className="gross-amnt-cont">Gross amount:</div><div className="gross-amnt-total">&nbsp;&#8364;&nbsp;<NumberFormat value={bids.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> <span className="tooltiptext1">Bid Breakdown</span></div>&nbsp;&nbsp;&nbsp;<MdKeyboardArrowDown color="#4E74DE" className="edit-icon-auth-info gross-show" />
                                </span>
                                
                            </div>
                                            <UncontrolledCollapse toggler="#toggler" className="price-breakdown-container">
                                                <div className="breakdown-title-box">Bid Breakdown</div>
                                                <div className="price-txt-cont" ><div className="bold-text-price">Taxes:</div><p className="tax-p-tag">&nbsp;&#8364;&nbsp;<NumberFormat value={bids.financialOffer * bids.taxPercentage / 100} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> ({bids.taxPercentage}%)</p></div>
                                                <div className="price-txt-cont" ><div className="bold-text-price">Fees:</div><p className="p-price-breakdown">&nbsp;&#8364;&nbsp;<NumberFormat value={bids.feePerPage * liveBroadcast.pagesNo} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> (&#8364;{bids.feePerPage} / page)</p></div>
                                            </UncontrolledCollapse>
                                            
                        
                                        </div>
                                        <div className="date-cancel-bid-cont">
                                        <p className="pages-txt-cont"><span className="bold-text">Date created: </span>{dayjs(bids.dateCreated).format('DD.MM.YYYY').toString()}</p>
                    <div className="row-style center">
                        <Button className="btns-sidebar right ml-3 btn btn-secondary open-chat" onClick={() => {
                        if (openChats.length !== numberOfOpenChats) {
                            if (openChats.find((element: any) => element.headProposalId === bids.headProposalId) === undefined) 
                            {
                                dispatch(ActionCreatorsForChatComponent.setOpenChats({...bids, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id }))
                            }
                        }
                        else {
                            dispatch(ActionCreatorsForChatComponent.removeFirstElement())
                            if (openChats.find((element: any) => element.headProposalId === bids.headProposalId) === undefined) 
                            {
                                dispatch(ActionCreatorsForChatComponent.setOpenChats({...bids, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id }))
                            }
                        }
                        }}>Negotiate</Button>  
                      <Button className="ml-2" color="primary" onClick={()=>CancelBid(dispatch, bids.id, alert).then(() => history.push('/profile/refresh')) }>Cancel bid</Button></div>
                      </div></div>}
                      </div> 
                      : <div>{ JSON.stringify(offers) === '{}' ? 
                      <div/> : <div className="offers-details-title">
                                    <h4>Offers</h4><div id="toggler" className="author-info-btn profile-subtitle">
                                <span>
                                            <div className="gross-amnt-cont">Gross Amount:</div><div className="gross-amnt-total">&#8364;&nbsp;<NumberFormat value={offers.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /><span className="tooltiptext1">Price Breakdown</span></div>&nbsp;&nbsp;&nbsp;<MdKeyboardArrowDown color="#4E74DE" className="edit-icon-auth-info gross-show" />
                                            {/*<MdKeyboardArrowDown className="edit-icon-auth-info gross-show" />*/} 
                                </span>
                                
                            </div>
                                    <UncontrolledCollapse toggler="#toggler" className="price-breakdown-container">
                                        <div className="breakdown-title-box">Price Breakdown</div>
                                        <div className="price-txt-cont" ><span className="bold-text-price">Taxes:</span><p className="tax-p-tag">&#8364;&nbsp;<NumberFormat value={offers.financialOffer * offers.taxPercentage / 100} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> ({offers.taxPercentage}%)</p></div>
                                        <div className="price-txt-cont" ><span className="bold-text-price">Fees:</span><p className="p-price-breakdown">&#8364;&nbsp;<NumberFormat value={offers.feePerPage * liveBroadcast.pagesNo} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> (&#8364;{offers.feePerPage}/page)</p></div>
                            </UncontrolledCollapse>
                            <div className="row-style mrt-15">
                                        <Button color="" className="btns-sidebar right btn btn-primary" onClick={() => AcceptOffer(dispatch, offers.id, alert).then(() => history.push('/profile/refresh'))}>Accept</Button>
                                        <Button className="btns-sidebar right ml-3 btn btn-secondary open-chat" onClick={() => {
                                            if (openChats.length !== numberOfOpenChats) {
                                                if (openChats.find((element: any) => element.headProposalId === offers.headProposalId) === undefined) {
                                                    dispatch(ActionCreatorsForChatComponent.setOpenChats({ ...offers, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id }))
                                                }
                                            }
                                            else {
                                                dispatch(ActionCreatorsForChatComponent.removeFirstElement())
                                                if (openChats.find((element: any) => element.headProposalId === offers.headProposalId) === undefined) {
                                                    dispatch(ActionCreatorsForChatComponent.setOpenChats({ ...offers, projectTopic: liveBroadcast.projectTopic, projectId: stateParams.id }))
                                                }
                                            }
                                        }}>Negotiate</Button>
                                        
                                    </div>
                                    <Button color="" className="btns-sidebar right ml-3 btn btn-secondary mt-3" onClick={() => DeclineOffer(dispatch, offers.id, alert).then(() => history.push('/profile/refresh'))}>Decline</Button>
                            <div className="center mt-2">
                            
                            </div> 
                  </div> }</div> } </div></div>
        
      </div>}</div>)
}

export default OpenProjectPage;