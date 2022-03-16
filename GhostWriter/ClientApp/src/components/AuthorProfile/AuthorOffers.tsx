import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Button, Nav, NavItem, NavLink, TabPane, TabContent, Row, Col, Media, Badge } from 'reactstrap';
import classnames from 'classnames';
import { Link, useHistory } from 'react-router-dom';
import ReactLoading from 'react-loading';
import dayjs from 'dayjs';
import ReactPaginate from 'react-paginate';
import { ImBooks } from "react-icons/im";
import { RiTimerFill } from "react-icons/ri";
import { IoBookSharp } from "react-icons/io5";
import { MdChatBubble } from 'react-icons/md';
import { GrChat } from 'react-icons/gr';
import { useAlert } from 'react-alert';
import NumberFormat from 'react-number-format';
import ChatComponent from './ChatComponent';

import { DropProposalAuthor, AcceptProposalAuthor, DropBidAuthor, GetBookingChatInfoAuthorOnPageChange, GetBookingChatInfoAuthor } from '../../services/ProfileServices';
import { ActionCreatorsForUserOffers } from '../../store/AuthorOffersReducer';
import { ActionCreatorsForNotifications } from '../../store/NotificationReducer';
import '../../styles/AuthorOffers.css';
import BidsOffersComponent from "../Common/BidsOffersComponent";
import { ActionCreatorsForSidePanel } from '../../store/SidePanelReducer';
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';

export const AuthorOffers = ({ openChats, numberOfOpenChats}: any) =>
{
    const dispatch = useDispatch();

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

    const history = useHistory();

    const notifications = useSelector((state: any) => state.notificationsReducer.notifications);
    const activeProjectsLength = useSelector((state: any) => state.notificationsReducer.activeProjectsLength);
    const newOffersLength = useSelector((state: any) => state.notificationsReducer.newOffersLength);
    const myBidsLength = useSelector((state: any) => state.notificationsReducer.myBidsLength);

    useEffect(()=>
    { 
        dispatch(ActionCreatorsForNotifications.setNumberOfActiveProjects(notifications.filter((value: any) => value.notificationType === 0 && value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfNewOffers(notifications.filter((value: any) => value.notificationType === 1 && value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfMyBids(notifications.filter((value: any) => value.notificationType === 2 && value.isSeen === false).length));
    }, [notifications]);

    //tabs
    const [activeTab, setActiveTab] = useState('1');

    const toggle = (tab: React.SetStateAction<string>) => 
    {
        if(activeTab !== tab) setActiveTab(tab);
    }


    const PER_PAGE_CHAT = 6;

    const alert = useAlert();
    
    //active projects
    const newOffersNoDisplay =
        <div className="center column-style">
            <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ac.png" alt="Typewriter image"></Media></div>
                <p>You don't have any new offers.</p>
                <p className="nofrs-line"> </p>
            </div>

    const newOffersMapFunction = (value: any, refreshFunction: any) =>
    
        <div id={'offer' + value.id} key={value.id} className='offer-card example-enter'>
            <MdChatBubble color='#4E74DE' />&nbsp;<p className="p-username">@{value.customerUsername}</p>
            <h6 className="h-title"><NavLink tag={Link} to={`/profile/project/&id=${value.projectId}`}>{value.projectTopic}</NavLink></h6>
            <div className="row-style sub">
                <ImBooks className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.kindOfWorkDTO?.value}</p>
                &nbsp;&nbsp;<IoBookSharp className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.pagesNo}</p>
                &nbsp;&nbsp;<RiTimerFill className="sidebar-icons1" color="#909fca" />&nbsp;<p>{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
            </div>
            <div className="div-language">Language: <span className="lang-value"> {value.languageDTO?.value} </span> </div>
            <div className="div-offer">Project value: <span className="lang-value"> &#8364;&nbsp;<NumberFormat value={value.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></span></div>
            <div id={`button-row${value.id}`} className="row-style mt-3">
                <Button id="accept-proposal-button" className="btns-sidebar" color="primary" onClick={() => AcceptProposalAuthor(dispatch, value.id, value, alert).then(() => { refreshFunction(); history.push('/profile/refresh'); })}>Accept Offer</Button>
                <Button className="btns-sidebar ml-3 btn btn-secondary open-chat" onClick={() => 
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
                <Button className="btns-sidebar ml-3" onClick={() => DropProposalAuthor(dispatch, value.id, alert).then(() => { refreshFunction(); history.push('/profile/refresh'); })}>Decline</Button>
                 
            </div>      
              
        </div>
    
    const myBidsNoDisplay = 
    <div className="center column-style">
        <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
            <p>You don't have any new bids.</p>
            <p className="nofrs-line"> </p>
        </div>

    const myBidsMapFunction = (value: any, refreshFunction: any) =>
        <div key={value.id} className='mt-3 example-enter'>
            <div  className='bid-card'>
                <MdChatBubble color='#4E74DE' />&nbsp;<p className="p-username">@{value.customerUsername}</p>
                    <h6 className="h-title"><NavLink tag={Link} to={`/profile/project/&id=${value.projectId}`}>{value.projectTopic}</NavLink></h6>
                    <div className="row-style sub">
                        <ImBooks className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.kindOfWorkDTO.value}</p>
                        &nbsp;&nbsp;<IoBookSharp className="sidebar-icons" color="#909fca" />&nbsp;<p>{value.pagesNo}</p>
                        &nbsp;&nbsp;<RiTimerFill className="sidebar-icons1" color="#909fca" />&nbsp;<p>{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                    </div>
                    <div className="div-language">Language: <span className="lang-value"> {value.languageDTO.value}</span></div>
                <div className="div-offer">Project value: <span className="lang-value"> &#8364;&nbsp;<NumberFormat value={value.financialOffer} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /> </span></div>
                <div id={`button-row-bid${value.id}`} className="row-style mt-3">
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
                        <Button className="ml-3" onClick={()=> DropBidAuthor(dispatch, value.id, alert).then(() => { refreshFunction(); history.push('/profile/refresh') })}>Cancel</Button>
                   
                    </div>       
            </div>
        </div>
    
    const chat = useSelector((state: any)=> state.authorOffersReducer.authorBookingChat);
    const chatTotalCount = useSelector((state: any)=>state.authorOffersReducer.authorBookingChatTotalCount);
    const chatLoadingValue = useSelector((state:any)=> state.authorOffersReducer.authorBookingChatLoadingValue);

    const [currentPageChat, setCurrentPageChat] = useState(0);  

    const pageCountChat = Math.ceil(chatTotalCount / PER_PAGE_CHAT);
    const offset = currentPageChat * PER_PAGE_CHAT;

    useEffect(()=>
    {
        GetBookingChatInfoAuthor(dispatch, alert);
        dispatch(ActionCreatorsForUserOffers.setAuthorBookingChatLoadingValue(true));
    },[])

    return(
        <div>
            <Nav tabs>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(newOffersLength)); dispatch(ActionCreatorsForNotifications.setNumberOfNewOffers(0)) }, 1000); }} to="#">Offers 
                    {newOffersLength !== 0 ? <Badge className="ml-2 badge-side-panel-12" color="danger">{newOffersLength}</Badge> : <div/> }</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(myBidsLength)); dispatch(ActionCreatorsForNotifications.setNumberOfMyBids(0)) }, 1000); }} to="#">My Bids
                    {myBidsLength !== 0 ? <Badge className="ml-2 badge-side-panel-113" color="danger">{myBidsLength}</Badge> : <div/> }</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(activeProjectsLength)); dispatch(ActionCreatorsForNotifications.setNumberOfActiveProjects(0)) }, 1000); }} to="#">Active Projects
                    {activeProjectsLength !== 0 ? <Badge className="ml-2 .badge-side-panel-111" color="danger">{activeProjectsLength}</Badge> : <div/> }</NavLink>
                </NavItem>
            </Nav>

            <TabContent activeTab={activeTab}>
                <TabPane tabId="1">
                        <Row>
                            <Col sm="12">
                                <BidsOffersComponent 
                                    array={authorsOffers}
                                    totalCount={authorsOffersTotalCount}
                                    setArray={setAuthorsOffers}
                                    setTotalCount={setAuthorsOffersTotalCount}
                                    PER_PAGE={4} 
                                    baseUrl="/Proposal/GetAuthorsActiveOffers" 
                                    noProjectsDisplay={newOffersNoDisplay} 
                                    projectsMapFunction={newOffersMapFunction}/>
                            </Col>
                        </Row>
                </TabPane>

                <TabPane tabId="2">
                        <Row>
                            <Col sm="12">
                                <BidsOffersComponent 
                                    array={authorsMyBids}
                                    totalCount={authorsMyBidsTotalCount}
                                    setArray={setAuthorsMyBids}
                                    setTotalCount={setAuthorsMyBidsTotalCount}
                                    PER_PAGE={4} 
                                    baseUrl="/Proposal/GetAuthorsBids" 
                                    noProjectsDisplay={myBidsNoDisplay} 
                                    projectsMapFunction={myBidsMapFunction}/>
                            </Col>
                        </Row>
                </TabPane>

                <TabPane tabId="3">
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
                                                                            <div className="chat-username"> {value.customerUsername} </div>
                                                                            <div className="project-status"><span className="proj-active">{value.bookingStatus.value}</span> &nbsp; <span className="cht-time"></span></div>
                                                                            <div className="chat-project-title"><NavLink tag={Link} to={`/profile/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>{value.projectTopic}</NavLink></div>
                                                                        </div>          
                                                                        <div className='chat-msg'> <p className='msg-text cursor' 
                                                                            onClick={() => 
                                                                                {
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
                                                                                            }}><GrChat opacity="0.3" /> &nbsp;{value.lastMessageContent === "" ? 'Still no messages, say hi.' : value.lastMessageContent}</p> </div>
                                                                    </div>)) }
                                        {chat.length === 0 ? <div/> : <div className="pagination-sidebar">
                                            <p className="page-count-sidebar">Page {currentPageChat + 1} of {pageCountChat === 0 ? 1 : pageCountChat}</p>
                                    <ReactPaginate
                                        previousLabel={"← Previous"}
                                        nextLabel={"Next →"}
                                        pageRangeDisplayed={5}
                                        marginPagesDisplayed={5}
                                        pageCount={pageCountChat}
                                        onPageChange={(selected) => { 
                                                                        setCurrentPageChat(selected.selected); 
                                        }}
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
                { openChats.map((value : any) => <div key={value.headProposalId}>
                        <ChatComponent openChats={openChats} value={value}/>
                                                </div>)
                }
            </div>
        </div>
    );
}

export default AuthorOffers;