import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Nav, NavItem, NavLink, TabContent, TabPane, Row, Col, Button, Media, Badge } from 'reactstrap';
import classnames from 'classnames';
import ReactLoading from 'react-loading';
import dayjs from 'dayjs';
import ReactPaginate from 'react-paginate';
import { ImBooks } from "react-icons/im";
import { RiTimerFill } from "react-icons/ri";
import { IoBookSharp } from "react-icons/io5";
import { MdChatBubble } from 'react-icons/md';
import { GrChat } from 'react-icons/gr';
import ReactTooltip from 'react-tooltip';
import { useAlert } from 'react-alert';
import NumberFormat from 'react-number-format';
import CustomerChatComponent from './CustomerChatComponent';

import { Link, useHistory } from 'react-router-dom';

import { AuthorPublicInformation } from '../../components/AuthorProfile/AuthorPublicInformation';
import { ActionCreatorsForSidePanel } from '../../store/SidePanelReducer';
import { ActionCreatorsForNotifications } from '../../store/NotificationReducer';
import BidsOffersComponent from "../Common/BidsOffersComponent";
import { ActionCreators } from '../../store/CustomerReducer';
import { MarkNotificationsAsSeen } from '../../services/NotificationServices';
import { AcceptProposalCustomer, DropProposalCustomer, GetBookingChatInfoCustomerOnPageChange, GetBookingChatInfoCustomer, CancelOfferCustomer } from '../../services/CustomerServices';
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';

export const CustomerOffers = ({ openChats, numberOfOpenChats}: any) =>
{    
    //authors offers reducers
    const authorsOffers = useSelector((state: any) => state.sidePanelReducer.authorsOffers);
    const authorsOffersTotalCount = useSelector((state: any) => state.sidePanelReducer.authorsOffersTotalCount);

    const setAuthorsOffers = (offers: any) => { dispatch(ActionCreatorsForSidePanel.setAuthorsOffers(offers)); }

    const setAuthorsOffersTotalCount = (offersTotalCount: any) => { dispatch(ActionCreatorsForSidePanel.setAuthorsOffersTotalCount(offersTotalCount)); }

    //authors my bids reducers
    const authorsMyBids = useSelector((state: any) => state.sidePanelReducer.authorsMyBids);
    const authorsMyBidsTotalCount = useSelector((state: any) => state.sidePanelReducer.authorsMyBidsTotalCount);

    const setAuthorsMyBids = (bids: any) => { dispatch(ActionCreatorsForSidePanel.setAuthorsMyBids(bids)); }

    const setAuthorsMyBidsTotalCount = (bidsTotalCount: any) => { dispatch(ActionCreatorsForSidePanel.setAuthorsMyBidsTotalCount(bidsTotalCount)); }
    
    const [activeTab, setActiveTab] = useState('0');
    
    const notifications = useSelector((state: any) => state.notificationsReducer.notifications);
    const activeProjectsLength = useSelector((state: any) => state.notificationsReducer.activeProjectsLength);
    const newOffersLength = useSelector((state: any) => state.notificationsReducer.newOffersLength);
    const myBidsLength = useSelector((state: any) => state.notificationsReducer.myBidsLength);

    const alert = useAlert();

    useEffect(()=>
    { 
        dispatch(ActionCreatorsForNotifications.setNumberOfActiveProjects(notifications.filter((value: any) => value.notificationType === 0 && value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfNewOffers(notifications.filter((value: any) => value.notificationType === 1 && value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfMyBids(notifications.filter((value: any) => value.notificationType === 2 && value.isSeen === false).length));
    }, [notifications]);



    const toggle = (tab: any) => 
    {
        MarkNotificationsAsSeen(parseInt(tab), alert);
        if(activeTab !== tab) setActiveTab(tab);
    }

    
    
    const history = useHistory();

    const PER_PAGE_CHAT = 6;
    const dispatch = useDispatch();

    const [popUpStateNewOffers, setPopUpStateNewOffers] = useState(false);
    const [activePopupNewOffers, setActivePopupNewOffers] = useState(-1);
  
    const [popUpStateMyOffers, setPopUpStateMyOffers] = useState(false);
    const [activePopupMyOffers, setActivePopupMyOffers] = useState(-1);

    const [popUpStateChat, setPopUpStateChat] = useState(false);
    const [activePopupChat, setActivePopupChat] = useState(-1);

    const offersNoDisplay =
        <div className="center column-style">
            <div className="img-nooffers">
            <Media style={{ width: '', height: '' }} object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
            <p className="nooffers-text">You don't have any new bids.</p><p className="nofrs-line"></p>
        </div>

    const offersMapFunction = (value: any, refreshFunction: any) =>
        <div id={'offer'+value.id} key={value.id} className="offer-card example-enter">
            <MdChatBubble color='#4E74DE' className="mt-1 chat-svg-sidebar"/>&nbsp;
                <NavLink className="cursor author-tooltip-nav-link-sidebar" 
                         onMouseEnter={()=> { setPopUpStateNewOffers(true); setActivePopupNewOffers(value.id)} } 
                         onMouseLeave={()=> { setPopUpStateNewOffers(false); setActivePopupNewOffers(-1); } } 
                         data-tip="" data-for={`${value.authorId}`} data-type="light" onClick={()=> { history.push(`/customer-profile/preview-author&id=${value.authorId}`) }}>{value.authorUsername}</NavLink>                    
            { (popUpStateNewOffers && activePopupNewOffers === value.id) ? <ReactTooltip id={`${value.authorId}`} getContent={() => { return <AuthorPublicInformation value={value.authorId} /> }} /> : <div/> }
            <h6 className="h-title"><NavLink tag={Link} to={`/customer-profile/project/&id=${value.projectId}`}>{value.projectTopic}</NavLink></h6>
            <div className="row-style sub">
                <ImBooks className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.kindOfWorkDTO.value}</p>
                &nbsp;&nbsp;<IoBookSharp className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.pagesNo}</p>
                &nbsp;&nbsp;<RiTimerFill className="sidebar-icons1" color="#909fca" />&nbsp;<p>{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
            </div>
            <div className="div-language">Language: <span className="lang-value"> {value.languageDTO.value} </span> </div>
            <div className="div-offer">Project value: <span className="lang-value"> &#8364;&nbsp;<NumberFormat value={value.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></span></div>
            <div id={`button-row${value.id}`} className="row-style mt-3">
                <Button id="accept-proposal-button" className="btns-sidebar" color="primary" onClick={() => AcceptProposalCustomer(dispatch, value.id, value, alert).then(() => { refreshFunction();  history.push( { pathname: '/customer-profile/payment', state: { paymentDetails: value }}) })}>Accept Bid</Button>
                <Button className="btns-sidebar ml-3 btn btn-secondary open-chat" onClick={() => 
                {
                    if(value.isNewReceiveBid != undefined)
                    {
                        dispatch(ActionCreatorsForSidePanel.changeNegotiateNewStateBid(value));
                    }
                    if (openChats.length !== numberOfOpenChats) {
                        if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) 
                        {
                            dispatch(ActionCreatorsForChatComponent.setOpenChats(value))
                        }
                    }
                    else {
                        dispatch(ActionCreatorsForChatComponent.removeFirstElement())
                        if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) 
                        {
                            dispatch(ActionCreatorsForChatComponent.setOpenChats(value))
                        }
                    }
                }}><span className="neg-btn">Negotiate</span> {value.isNewReceiveBid === true ? <Badge className="ml-1 new-msg" color="danger">.</Badge> : ''}</Button>
                <Button className="btns-sidebar ml-3" onClick={()=>DropProposalCustomer(dispatch, value.id, alert).then(()=> { refreshFunction(); } )}>Decline</Button>                 
            </div>
        </div>

    const myOffersNoDisplay = 
        <div className="center column-style">
            <div className="img-nooffers">
            <Media style={{ width: '', height: '' }} object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
            <p className="nooffers-text">You don't have any offers.</p><p className="nofrs-line"></p>
        </div>

    const myOffersMapFunction = (value: any, refreshFunction: any) =>
        <div id={'offer'+value.id} key={value.id} className="offer-card example-enter">
            <MdChatBubble color='#4E74DE' className="mt-1 chat-svg-sidebar"/>&nbsp;
            <NavLink className="cursor author-tooltip-nav-link-sidebar" 
                     onMouseEnter={()=> { setPopUpStateMyOffers(true); setActivePopupMyOffers(value.id)} } 
                     onMouseLeave={()=> { setPopUpStateMyOffers(false); setActivePopupMyOffers(-1); } } 
                     data-tip="" data-for={`${value.authorId}`} data-type="light" onClick={()=> { history.push(`/customer-profile/preview-author&id=${value.authorId}`) }}>{value.authorUsername}</NavLink>                    
        { (popUpStateMyOffers && activePopupMyOffers === value.id) ? <ReactTooltip id={`${value.authorId}`} getContent={() => { return <AuthorPublicInformation value={value.authorId} /> }} /> : <div/> }
            <h6 className="h-title"><NavLink tag={Link} to={`/customer-profile/project/&id=${value.projectId}`}>{value.projectTopic}</NavLink></h6>
            <div className="row-style sub">
                <ImBooks className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.kindOfWorkDTO.value}</p>
                &nbsp;&nbsp;<IoBookSharp className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.pagesNo}</p>
                &nbsp;&nbsp;<RiTimerFill className="sidebar-icons1" color="#909fca" />&nbsp;<p>{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
            </div>
            <div className="div-language">Language: <span className="lang-value"> {value.languageDTO.value} </span> </div>
            <div className="div-offer">Project value: <span className="lang-value"> &#8364;&nbsp;<NumberFormat value={value.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></span></div>
            <div id={`button-rows-cancel${value.id}`} className="row-style mt-3">
                <Button className="btns-sidebar ml-0 btn btn-secondary open-chat" onClick={() => 
                {
                    if(value.isNewReceiveOffer != undefined)
                    {
                        dispatch(ActionCreatorsForSidePanel.changeNegotiateNewState(value));
                    }
                    if (openChats.length !== numberOfOpenChats) {
                        if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) 
                        {
                            dispatch(ActionCreatorsForChatComponent.setOpenChats(value))
                        }
                    }
                    else {
                        dispatch(ActionCreatorsForChatComponent.removeFirstElement())
                        if (openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined) 
                        {
                            dispatch(ActionCreatorsForChatComponent.setOpenChats(value))
                        }
                    }
                }}><span className="neg-btn">Negotiate</span> {value.isNewReceiveOffer === true ? <Badge className="ml-1 new-msg" color="danger">.</Badge> : ''}</Button> 
                <Button className="btns-sidebar ml-3 btn btn-secondary" onClick={()=>CancelOfferCustomer(dispatch, value.id, alert).then(()=>{ refreshFunction(); })}>Cancel</Button>                        
               
            </div>
        </div>

    const chat = useSelector((state: any)=> state.customerReducer.customerBookingChat);
    const chatTotalCount = useSelector((state: any)=>state.customerReducer.customerBookingChatTotalCount);
    const chatLoadingValue = useSelector((state:any)=> state.customerReducer.customerBookingChatLoadingValue);

    const [currentPageChat, setCurrentPageChat] = useState(0);  
    const pageCountChat = Math.ceil(chatTotalCount / PER_PAGE_CHAT);
    const offset = currentPageChat * PER_PAGE_CHAT;

    useEffect(()=> 
    {
        dispatch(ActionCreators.setCustomerBookingChatLoadingValue(true));
        GetBookingChatInfoCustomer(dispatch, alert);
    }, [])
    
    return(
        <div>
            <Nav tabs className="customer-nav-tabs">
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '0' })} onClick={() => { toggle('0'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(myBidsLength)); dispatch(ActionCreatorsForNotifications.setNumberOfMyBids(0)) }, 1000); }} to="#">New Bids
                    {myBidsLength !== 0 ? <Badge className="ml-2 badge-side-panel-12" color="danger">{myBidsLength}</Badge> : <div/> }</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(newOffersLength)); dispatch(ActionCreatorsForNotifications.setNumberOfNewOffers(0)) }, 1000) }} to="#">My Offers
                    {newOffersLength !== 0 ? <Badge className="ml-2 badge-side-panel-112" color="danger">{newOffersLength}</Badge> : <div/> }</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(activeProjectsLength)); dispatch(ActionCreatorsForNotifications.setNumberOfActiveProjects(0)) }, 1000); }} to="#">Active Projects
                    {activeProjectsLength !== 0 ? <Badge className="ml-2 badge-side-panel-111" color="danger">{activeProjectsLength}</Badge> : <div/> }</NavLink>
                </NavItem>
            </Nav>

            <TabContent activeTab={activeTab}>
                <TabPane tabId="0">
                    <Row>
                        <Col sm="12">
                            <BidsOffersComponent 
                                array={authorsMyBids}
                                totalCount={authorsMyBidsTotalCount}
                                setArray={setAuthorsMyBids}
                                setTotalCount={setAuthorsMyBidsTotalCount} 
                                PER_PAGE={4}
                                baseUrl="/Proposal/GetCustomersNewOffers" 
                                noProjectsDisplay={offersNoDisplay} 
                                projectsMapFunction={offersMapFunction}/>
                        </Col>
                    </Row>
                </TabPane>
                <TabPane tabId="1">
                    <Row>
                        <Col sm="12">
                            <BidsOffersComponent
                                array={authorsOffers}
                                totalCount={authorsOffersTotalCount}
                                setArray={setAuthorsOffers}
                                setTotalCount={setAuthorsOffersTotalCount}
                                PER_PAGE={4} 
                                baseUrl="/Proposal/GetCustomersGeneratedOffers" 
                                noProjectsDisplay={myOffersNoDisplay} 
                                projectsMapFunction={myOffersMapFunction}/>
                        </Col>
                    </Row>
                </TabPane>
                <TabPane tabId="2">
                    <Row>
                        <Col sm="12">
                                <div className="mt-3">
                                    { chatLoadingValue ? <div style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                                                        <ReactLoading className="mt-5" type='spinningBubbles' color='#0080FF'/>
                                                     </div> 
                                    : 
                                    <div> 
                                        {chat.length === 0 ? <div className="center column-style">
                                                                <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ac.png" alt="Typewriter image"></Media></div>
                                                                <p>You don't have any new messages.</p>
                                                                <p className="nofrs-line"> </p>
                                                                </div> : 
                                                                chat.slice(offset, offset + PER_PAGE_CHAT).map((value:any)=>(
                                                                    <div className="profile-image-upload-placeholder example-enter" key={value.bookingId}>
                                                                        <div className="upload-container">
                                                                            <MdChatBubble color='#4E74DE' className="mt-1 chat-svg-sidebar"/>&nbsp;
                                                                            <NavLink className="cursor author-tooltip-nav-link-sidebar" 
                                                                                onMouseEnter={()=> { setPopUpStateChat(true); setActivePopupChat(value.bookingId)} } 
                                                                                onMouseLeave={()=> { setPopUpStateChat(false); setActivePopupChat(-1); } } 
                                                                                data-tip="" data-for={`${value.authorId}`} data-type="light" onClick={()=> { history.push(`/customer-profile/preview-author&id=${value.authorId}`) }}>@{value.authorUsername}</NavLink>                    
                                                                            { (popUpStateChat && activePopupChat === value.bookingId) ? <ReactTooltip id={`${value.authorId}`} getContent={() => { return <AuthorPublicInformation value={value.authorId} /> }} /> : <div/> }
                                                                            <div className="project-status sidebar-chat"><span className="proj-active">{value.bookingStatus.value}</span> &nbsp; <span className="cht-time"></span></div>
                                                                            <div className="chat-project-title"><NavLink className="project-title-link" tag={Link} to={`/customer-profile/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>{value.projectTopic}</NavLink></div>
                                                                        </div>
                                                                        <div className="chat-msg"> <p className='msg-text cursor' onClick={() => {
                                                                                            if(openChats.length !== numberOfOpenChats) 
                                                                                            { 
                                                                                                if(openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined)
                                                                                                {
                                                                                                    dispatch(ActionCreatorsForChatComponent.setOpenChats(value))
                                                                                                }
                                                                                            } 
                                                                                            else
                                                                                            {
                                                                                                dispatch(ActionCreatorsForChatComponent.removeFirstElement())
                                                                                                if(openChats.find((element: any) => element.headProposalId === value.headProposalId) === undefined)
                                                                                                {
                                                                                                    dispatch(ActionCreatorsForChatComponent.setOpenChats(value))
                                                                                                }
                                                                                            }
                                                                                            }}><GrChat opacity="0.3" /> &nbsp; {value.lastMessageContent === "" ? 'Still no messages, say hi.' : value.lastMessageContent}</p> </div>
                                                                </div>)) }
                                            {chat.length === 0 ? <div/> :<div className="pagination-sidebar">
                                            <p className="page-count-sidebar">Page {currentPageChat + 1} of {pageCountChat === 0 ? 1 : pageCountChat}</p>
                                    <ReactPaginate
                                        previousLabel={"← Previous"}
                                        nextLabel={"Next →"}
                                        pageRangeDisplayed={5}
                                        marginPagesDisplayed={5}
                                        pageCount={pageCountChat}
                                        onPageChange={(selected) => { 
                                            setCurrentPageChat(selected.selected); }}
                                        breakClassName={'page-item'}
                                        breakLinkClassName={'page-link'}
                                        containerClassName={'pagination'}
                                        pageClassName={'page-item-class'}
                                        pageLinkClassName={'page-link'}
                                        previousClassName={'page-item'}
                                        previousLinkClassName={'page-link'}
                                        nextClassName={'page-item'}
                                        nextLinkClassName={'page-link'}
                                        activeClassName={'active'}
                                            />
                                            
                                        </div>}
                                        
                                    </div> }  
                                </div>
                        </Col>
                    </Row>
                </TabPane>
            </TabContent>
            <div className="row" style={{ justifyContent: 'space-evenly', width: '400px' }}>
                {openChats.map((value: any) => <div className="chat-bottom" key={value.headProposalId}>
                    <CustomerChatComponent openChats={openChats} value={value}/></div>)}
            </div>
        </div>
    )
}

export default CustomerOffers;