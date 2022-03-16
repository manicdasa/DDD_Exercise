import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { NavLink, Button, Input, Media, UncontrolledCollapse } from 'reactstrap';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import dayjs from 'dayjs';
import ReactLoading from 'react-loading';
import { BiArrowBack } from "react-icons/bi";
import { RiUserLine } from "react-icons/ri";
import NumberFormat from 'react-number-format';
import { IoMdAddCircle } from "react-icons/io";
import { MdEuroSymbol, MdKeyboardArrowDown } from "react-icons/md";
import { useAlert } from 'react-alert';

import { GetProposalDetails, GetAuthorsLastBids, CreateBid, CancelBid, AcceptOffer, DeclineOffer, GetAuthorsLastOffers } from '../../services/ProfileServices';
import { ActionCreators} from '../../store/AuthorActiveProjectsReducer'
import AuthorOffers from './AuthorOffers';

export const OpenProjectPage = () =>
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

    useEffect(()=>
    {
        GetProposalDetails(dispatch, stateParams.id, alert);
        GetAuthorsLastBids(dispatch, stateParams.id, alert);
        GetAuthorsLastOffers(dispatch, stateParams.id, alert);
        dispatch(ActionCreators.setProposalDetailsLoadingValue(true));
    }, [stateParams.id, history.location.key])

    return(
      <div className="row-style register-user-box proposal-page row">
            { loadingValue ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                             </div> :
            <div>
              <div className="register-signin-left-panel col-sm-9 project-content">
                <div className="back-link-cont">
                  <NavLink className="back-link" tag={Link} to={"/profile"}>< BiArrowBack size="24" className="back-icon" /> &nbsp; Back</NavLink>
                </div>
                <h4>{liveBroadcast.projectTopic}</h4>
                <div className="sub-cont row">
                    <div className="sub-txt-bold">Kind of work:</div> &nbsp; <div className="sub-txt-lite"> {liveBroadcast.kindOfWorkDTO.value}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Expertise area:</div> &nbsp;<div className="sub-txt-lite">{liveBroadcast.expertiseAreaListDTOs?.map((u: { value: any; description: any }) => u.value).join(', ')}</div>&nbsp;&nbsp;&nbsp;&nbsp;<div className="sub-txt-bold">Language:</div> &nbsp;<div className="sub-txt-lite"> {liveBroadcast.languageDTO.value}</div>
                </div>               
                <br></br>
                <h3>Project description</h3>
                <p>{liveBroadcast.description}</p>
                
              </div>
                    <div className="register-signin-right-panel col-sm-3 column-style project-info">
                        <h4>Requirements: </h4>
                        <div className="column-style">
                            <p className="pages-txt-cont" ><span className="bold-text">Number of pages: </span>{liveBroadcast.pagesNo}</p>
                            <p className="pages-txt-cont" ><span className="bold-text">Deadline: </span>{dayjs(liveBroadcast.deadline).format('DD.MM.YYYY').toString()}</p>
                            <p id="toggler" className="author-info-btn profile-subtitle">
                                <span>
                                    <p>Gross amount:&nbsp;&#8364;&nbsp;<NumberFormat value={liveBroadcast.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></p>
                                </span>
                                <MdKeyboardArrowDown className="center edit-icon-auth-info" />
                            </p>
                            <UncontrolledCollapse toggler="#toggler">
                            <p className="price-txt-cont" ><span className="bold-text-price">Taxes:</span>&nbsp;&#8364;&nbsp;<NumberFormat value={liveBroadcast.financialOffer * liveBroadcast.taxPercentage / 100} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> ({liveBroadcast.taxPercentage}%)</p>
                            <p className="price-txt-cont" ><span className="bold-text-price">Fees:</span>&nbsp;&#8364;&nbsp;<NumberFormat value={liveBroadcast.feePerPage * liveBroadcast.pagesNo} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> (&#8364;{liveBroadcast.feePerPage} per page)</p>
                            </UncontrolledCollapse>
                        </div>
                        <h4 className="client-about-title">About the client</h4>
                        <p className="deadline-txt-cont"><span className="bold-text">Customer username: </span><br></br><RiUserLine /> &nbsp; <span className="usrname-usr-info">{liveBroadcast.customerUsername}</span></p>
                
                    </div>
                    <div className="col-sm-9 chat-cont">
                        
                      </div>
                    <div className="col-sm-3 docs-cont"><div>
                    { liveBroadcast.isPublished ? 
                      <div>
                        { JSON.stringify(bids) === '{}' ? 
                        (<div className="column-style center">
                          <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nobids.png" alt="Typewriter image"></Media></div>
                                        <p>There are no bids currently.</p>
                                        <div className="bid-input-field">
                                            <div className="eur-bid-input"><MdEuroSymbol /></div>
                                            <Input className="input-bid-field" type="text" placeholder="0" onChange={handleChangeOffer}></Input>
                                        </div>
                          <br/>
                                        <p style={{ fontWeight: 'bold', textAlign: 'center' }} className="center mt-1 field-colorchange">{text}</p>
                                        <Button color="primary" className="add-bid-btn" onClick={() => CreateBid(dispatch, stateParams.id, offer, alert).then((response: any) => { setText(response); history.push('/profile/refresh'); })}><IoMdAddCircle className="add-bid-icon" /> &nbsp; Add bid</Button>
                        </div>) 
                      : <div className="center column-style">
                      <h3>My bids</h3> 
                      <div key={bids.id} className="column-style">
                        <p className="pages-txt-cont"><span className="bold-text">Bid: </span>${bids.financialOffer}</p>
                        <p className="pages-txt-cont"><span className="bold-text">Date created: </span>{dayjs(bids.dateCreated).format('DD.MM.YYYY').toString()}</p>
                      </div>
                      <Button color="primary" onClick={()=>CancelBid(dispatch, bids.id, alert).then(() => history.push('/profile/refresh'))}>Cancel bid</Button>
                      </div>}
                      </div> 
                      : <div>{ JSON.stringify(offers) === '{}' ? 
                      (<div className="column-style center">
                        <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                        <p>There are no offers currently.</p></div>) : <div className="column-style">
                      <p>You were offered: </p><h6>${offers.financialOffer}</h6>
                      <div className="row-style">
                                        <Button color="primary" className="mr-1" onClick={() => AcceptOffer(dispatch, offers.id, alert).then(()=>history.push('/profile/refresh'))}>Accept</Button>
                                        <Button color="secondary" onClick={() => DeclineOffer(dispatch, offers.id, alert).then(()=>history.push('/profile/refresh'))}>Decline</Button>
                      </div>
                  </div> }</div> } </div></div>
        
      </div>}</div>)
}

export default OpenProjectPage;