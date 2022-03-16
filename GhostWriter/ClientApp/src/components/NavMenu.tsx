import React, { useState, useEffect } from 'react';
import { Media, Navbar, NavbarBrand, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem, Nav, TabContent, TabPane, Row, Col, CardTitle, Card, CardText, Button, Badge } from 'reactstrap';
import { Link, useHistory } from 'react-router-dom';
import { AiOutlineMenu } from 'react-icons/ai';
import { useDispatch, useSelector } from 'react-redux';
import { RiUserLine, RiDashboardFill, RiTimerFill, RiCalendarLine } from "react-icons/ri";
import { IoLogOutOutline, IoNotificationsOutline } from "react-icons/io5";
import { BsQuestionCircle, BsFileText } from "react-icons/bs";
import { CgNotifications } from 'react-icons/cg';
import { useAlert } from 'react-alert';
import { BiLogIn, BiDockLeft } from "react-icons/bi";
import { stopConnection, } from '../components/Helpers/SignalRMiddleware'

import './NavMenu.css';
import { ActionCreators } from '../store/LandingPageReducer';
import { ActionCreatorsForNotifications } from '../store/NotificationReducer';
import { MarkNotificationsAsSeen } from '../services/NotificationServices';

import classnames from 'classnames';
import dayjs from 'dayjs';
import { ActionCreatorsForChatComponent } from '../store/ChatComponentReducer';

export const NavMenu = () => 
{
    //notifications
    const dispatch = useDispatch();
    const alert = useAlert();
    const openChats =  useSelector((state: any) => state.chatComponentsReducer.openChats.filter((element: any) => { return element.headProposalId != 0 } ));
    const numberOfOpenChats = useSelector((state: any) => state.chatComponentsReducer.numberOfOpenChats);

    const notifications = useSelector((state: any) => state.notificationsReducer.notifications);
    const notifiLength = useSelector((state: any) => state.notificationsReducer.notificationsLength);
    const activeProjectsLength = useSelector((state: any) => state.notificationsReducer.activeProjectsLength);
    const newOffersLength = useSelector((state: any) => state.notificationsReducer.newOffersLength);
    const myBidsLength = useSelector((state: any) => state.notificationsReducer.myBidsLength);
    const liveBroadcastLength = useSelector((state: any) => state.notificationsReducer.liveBroadcastsLength);

    useEffect(()=>
    { 
        dispatch(ActionCreatorsForNotifications.setNotificationsLength(notifications.filter((value: any) => value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfActiveProjects(notifications.filter((value: any) => value.notificationType === 0 && value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfNewOffers(notifications.filter((value: any) => value.notificationType === 1 && value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfMyBids(notifications.filter((value: any) => value.notificationType === 2 && value.isSeen === false).length));
        dispatch(ActionCreatorsForNotifications.setNumberOfLiveBroadcast(notifications.filter((value: any) => value.notificationType === 3 && value.isSeen === false).length));
    }, [notifications]);

    const [url, setUrl] = useState('');

    const [collapsed, setCollapsed] = useState(true);
    const toggleNavbar = () => setCollapsed(!collapsed);

    const [activeTab, setActiveTab] = useState('0');

    const toggle = (tab: any) => {
        MarkNotificationsAsSeen(parseInt(tab), alert);
      if(activeTab !== tab) setActiveTab(tab);
    }


    const history = useHistory();

    const [loggedIn, setLoggedIn] = useState(false);
    const [isAuthor, setIsAuthor] = useState(false);
    const [isCustomer, setIsCustomer] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);

    const username = localStorage.getItem('user');

    useEffect(()=>{
        dispatch(ActionCreatorsForNotifications.getNotifications());
        if(localStorage.getItem('token') != undefined)
        {
            setLoggedIn(true);

            if(localStorage.getItem('role=Admin') != undefined)
            {
                setIsAdmin(true);
                setIsCustomer(false);
                setIsAuthor(false);
            }
            else if(localStorage.getItem('role=Ghostwriter') != undefined)
            {
                setIsCustomer(false);
                setIsAdmin(false);
                setIsAuthor(true);
                setUrl('/profile')
            }
            else
            {
                setIsAdmin(false);
                setIsAuthor(false);
                setIsCustomer(true);
                setUrl('/customer-profile')
            }
        }
        else
        {
            setLoggedIn(false);
        }
    }, [])

    return (
        <header>
            <Navbar className="front" color="faded" light>

                {loggedIn ?
                    <div>
                        {isAuthor ?
                            <div>
                                <NavbarBrand tag={Link} to="/profile">
                                    <Media object src="/images/logo-web1.png" alt="Site logo" className="logo"></Media>
                                </NavbarBrand>
                            </div>
                            :
                            <div>
                                <NavbarBrand tag={Link} to="/customer-profile">
                                    <Media object src="/images/logo-web1.png" alt="Site logo" className="logo"></Media>
                                </NavbarBrand>
                            </div>}
                    </div>
                    :
                    <div>
                        <NavbarBrand tag={Link} to="/">
                            <Media object src="/images/logo-web1.png" alt="Site logo" className="logo"></Media>
                        </NavbarBrand>
                    </div>}
                

                { isAuthor ? <div/> : <div className="row">
                    <NavItem>
                        <NavLink className="text-light main-nav-text" tag={Link} to="/">Search ghostwriter</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink className="text-light main-nav-text" tag={Link} to="/create-project">Offer project</NavLink>
                    </NavItem>
                </div> }
                <div className="row">
                    {!isAuthor ?
                        <div className="col-sm-8">
                            <NavItem>
                                <NavLink className="text-light cursor HdTw main-nav-text" onClick={() => { dispatch(ActionCreators.setValue(true)) && history.push("/") }}>How does this work?</NavLink>
                            </NavItem>
                        </div> : <div className="col-sm-8"></div>
                    }
                    
                    { loggedIn ? <div className="col-sm-4"></div> :<div className="col-sm-4">
                        <NavItem>
                            <a className="btn btn-primary regAuth main-nav-text" onClick={() => window.location.href="/register-author" }>Become an author</a>
                        </NavItem>
                    </div> }
                </div>
                {loggedIn ? <div className="">
                <UncontrolledDropdown nav inNavbar>
                    <DropdownToggle className="brg-menu" nav caret onClick={()=> { 
                            MarkNotificationsAsSeen(0, alert);
                         setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(activeProjectsLength)); dispatch(ActionCreatorsForNotifications.setNumberOfActiveProjects(0)) }, 1000); }}>
                        {notifiLength !== 0 ? 
                            <div>
                            <div className="notifier new-badge-notifi">
                                <IoNotificationsOutline size={27} color='white'/>
                                <div className="badge-notifi">{notifiLength}</div>
                            </div>
                          </div>
                        : <IoNotificationsOutline size={27} color='white'/> }
                        </DropdownToggle>
                        <DropdownMenu className="dropdown-notification" right>
                            <div>
                                <Nav tabs className="nav-notifications-cont" style={{  }}>
                        <NavItem>
                            <NavLink className={"notifi-tab-styling " + classnames({ active: activeTab === '0' })} onClick={() => { toggle('0'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(activeProjectsLength)); dispatch(ActionCreatorsForNotifications.setNumberOfActiveProjects(0)) }, 1000); }}>Active Projects 
                            &nbsp; {activeProjectsLength !== 0 ? 
                                    <Badge className="ml-2" color="danger">{activeProjectsLength}</Badge> : <div/> }
                            </NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink className={"notifi-tab-styling " + classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(newOffersLength)); dispatch(ActionCreatorsForNotifications.setNumberOfNewOffers(0)) }, 1000); }}>Offers
                            &nbsp; {newOffersLength !== 0 ? 
                                    <Badge className="ml-2" color="danger">{newOffersLength}</Badge> : <div/> }</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink className={"notifi-tab-styling " + classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(myBidsLength)); dispatch(ActionCreatorsForNotifications.setNumberOfMyBids(0)); }, 1000); }}>Bids
                            &nbsp; {myBidsLength !== 0 ? 
                                    <Badge className="ml-2" color="danger">{myBidsLength}</Badge> : <div/> }</NavLink>
                        </NavItem>
                        { isAuthor ? 
                        <NavItem>
                            <NavLink className={"notifi-tab-styling " + classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); setTimeout(() => { dispatch(ActionCreatorsForNotifications.reduceNotificationsLength(liveBroadcastLength)); dispatch(ActionCreatorsForNotifications.setNumberOfLiveBroadcast(0)); }, 1000); }}>Live Broadcast
                            &nbsp; {liveBroadcastLength !== 0 ? 
                                    <Badge className="ml-2" color="danger">{liveBroadcastLength}</Badge> : <div/> }</NavLink>
                        </NavItem> : <div/> }
                    </Nav>
                                <TabContent className="tab-content-notifications" activeTab={activeTab}>
                                    <TabPane id="chat-div" className="tabpan-container notification-box" tabId="0">
                                        <div className="column-style proj-details msg-not-cont">
                                { notifications.filter((value: any) => value.notificationType === 0 ).length === 0 ? 
                                <div className="center column-style file-list">
                                    <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                                    <p className="no-docs-txt">You dont have any notifications.</p>
                                    <p className="nofrs-line"> </p>
                                </div> : notifications.filter((value: any) => value.notificationType === 0).reverse().map((value: any, index: any) =>
                                    <div className="cursor msg-cont-details" key={'NO' + index} onClick={() => { history.push(url + value.detailsLink) }}>
                                        <Col sm="12">
                                            <div className="title-price column">
                                                <div className="title-proj notification-title-proj" >
                                                    <p className={(value.isSeen === true ? "notification-title-proj-icon" : "text-notification-title-proj-icon-unseen text-primary ")}><CgNotifications size="19" className="" /></p>
                                                    <p className={(value.isSeen === true ? "text-notification-style notif-float" : "text-notification-style-unseen notif-float")}>{value.message}</p>
                                                </div>
                                                <div className="msg-panel-date-time"><RiCalendarLine className={(value.isSeen === true ? "calendar-notif-mesg" : "calendar-notif-mesg-unseen ")} />{dayjs(value.dateTimeCreated).format('DD.MM.YYYY').toString()}</div>
                                            </div>
                                        </Col>
                                        {/*<Col sm="12">
                                            <div className="title-price column">
                                                <div className="title-proj notification-title-proj" >
                                                    <p className={(value.isSeen === true ? "text-notification-style" : "text-notification-style-unseen")}><CgNotifications size="19" className="active-proj-ico text-primary" />&nbsp;{value.message}</p>
                                                </div>
                                                <p className="msg-panel-date-time"><RiCalendarLine className="text-primary" />&nbsp;{dayjs(value.dateTimeCreated).format('DD.MM.YYYY').toString()}</p>
                                            </div>
                                        </Col>*/}
                                        </div>
                                    )
                                }
                            </div>
                        </TabPane>
                                    <TabPane id="chat-div" className="tabpan-container notification-box" tabId="1">
                            <div className="column-style proj-details msg-not-cont">
                                { notifications.filter((value: any) => value.notificationType === 1).length === 0 ? 
                                <div className="center column-style file-list">
                                    <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                                    <p className="no-docs-txt">You dont have any notifications.</p>
                                    <p className="nofrs-line"> </p>
                                </div> : notifications.filter((value: any) => value.notificationType === 1).reverse().map((value: any, index: any) =>
                                    <div className="cursor msg-cont-details" key={'NO'+index} onClick={() => { 
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
                                    }}>
                                            <Col sm="12">
                                            <div className="title-price column">
                                                <div className="title-proj notification-title-proj" >
                                                    <p className={(value.isSeen === true ? "notification-title-proj-icon" : "text-notification-title-proj-icon-unseen text-primary ")}><CgNotifications size="19" className=" " /></p>
                                                    <p className={(value.isSeen === true ? "text-notification-style notif-float" : "text-notification-style-unseen notif-float")}>{value.message}</p>
                                                </div>
                                                <div className="msg-panel-date-time"><RiCalendarLine className={(value.isSeen === true ? "calendar-notif-mesg" : "calendar-notif-mesg-unseen ")} />{dayjs(value.dateTimeCreated).format('DD.MM.YYYY').toString()}</div>
                                            </div>
                                            </Col>
                                        </div>
                                )}
                            </div>
                        </TabPane>
                                    <TabPane id="chat-div" className="tabpan-container notification-box" tabId="2">
                                        <div className="column-style proj-details msg-not-cont">
                                { notifications.filter((value: any) => value.notificationType === 2).length === 0 ? 
                                <div className="center column-style file-list">
                                    <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                                    <p className="no-docs-txt">You dont have any notifications.</p>
                                    <p className="nofrs-line"> </p>
                                </div> : notifications.filter((value: any) => value.notificationType === 2).reverse().map((value: any, index: any) =>
                                    <div className="cursor msg-cont-details" key={'NO'+index} onClick={() => { 
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
                                    }}>
                                            <Col sm="12">
                                            <div className="title-price column">
                                                <div className="title-proj notification-title-proj" >
                                                    <p className={(value.isSeen === true ? "notification-title-proj-icon" : "text-notification-title-proj-icon-unseen text-primary ")}><CgNotifications size="19" className="" /></p>
                                                    <p className={(value.isSeen === true ? "text-notification-style notif-float" : "text-notification-style-unseen notif-float")}>{value.message}</p>
                                                </div>
                                                <div className="msg-panel-date-time"><RiCalendarLine className={(value.isSeen === true ? "calendar-notif-mesg" : "calendar-notif-mesg-unseen ")} />{dayjs(value.dateTimeCreated).format('DD.MM.YYYY').toString()}</div>
                                            </div>
                                            </Col>
                                        </div>
                                )}
                            </div>
                        </TabPane>
                        { isAuthor ? 
                                        <TabPane id="chat-div" className="tabpan-container notification-box" tabId="3">
                                            <div className="column-style proj-details msg-not-cont">
                            { notifications.filter((value: any) => value.notificationType === 3).length === 0 ? 
                                <div className="center column-style file-list">
                                    <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="103" object src="/images/nooffers.png" alt="Typewriter image"></Media></div>
                                    <p className="no-docs-txt">You dont have any notifications.</p>
                                    <p className="nofrs-line"> </p>
                                </div> : notifications.filter((value: any) => value.notificationType === 3).reverse().map((value: any, index: any) =>
                                <div className="cursor msg-cont-details" key={'NO'+index} onClick={() => history.push(url+value.detailsLink)}>
                                        {/* <Col sm="12">
                                            <div className="title-price column">
                                                <div className="title-proj notification-title-proj" >
                                                    <p className={(value.isSeen === true ? "text-notification-style" : "text-notification-style-unseen")}><CgNotifications size="19" className="active-proj-ico text-primary" />&nbsp;{value.message}</p>
                                                </div>
                                                <p className="msg-panel-date-time"><RiCalendarLine className="text-primary" />&nbsp;{dayjs(value.dateTimeCreated).format('DD.MM.YYYY').toString()}</p>
                                            </div>
                                    </Col> */}
                                        <Col sm="12">
                                            <div className="title-price column">
                                                <div className="title-proj notification-title-proj" >
                                                    <p className={(value.isSeen === true ? "notification-title-proj-icon" : "text-notification-title-proj-icon-unseen text-primary ")}><CgNotifications size="19" className="" /></p>
                                                    <p className={(value.isSeen === true ? "text-notification-style notif-float" : "text-notification-style-unseen notif-float")}>{value.message}</p>
                                                </div>
                                                <div className="msg-panel-date-time"><RiCalendarLine className={(value.isSeen === true ? "calendar-notif-mesg" : "calendar-notif-mesg-unseen ")} />{dayjs(value.dateTimeCreated).format('DD.MM.YYYY').toString()}</div>
                                            </div>
                                        </Col>
                                </div>
                                )}
                        </div>
                        </TabPane> : <div/> }
                    </TabContent>
                    </div>
                    </DropdownMenu>
                </UncontrolledDropdown></div> : <div/> }
                
                <UncontrolledDropdown nav inNavbar>

                    <DropdownToggle className="brg-menu" nav caret>
                        {loggedIn ? 
                            <div> 
                                { isAuthor ? 
                                    <div className="row lgd-user-text"><span style={{ color: 'white' }}><RiUserLine /> &nbsp; {username} &nbsp; |</span><span className="center user-text" style={{ color: '#ffc107' }}>&nbsp; Author</span></div> 
                                : isAdmin ? 
                                        <div className="row lgd-user-text"><span style={{ color: 'white' }}><RiUserLine /> &nbsp; {username} &nbsp; |</span><span className="center user-text" style={{ color: 'red' }}>&nbsp; Admin</span></div> 
                                        : <div className="row lgd-user-text"><span style={{ color: 'white' }}><RiUserLine /> &nbsp; {username} &nbsp; |</span><span className="center user-text" style={{ color: '#4e74de' }}>&nbsp; User</span></div> 
                             }  </div> : <div></div>}
                        <AiOutlineMenu size={28} color='white' />
                    </DropdownToggle>
                    
                    <DropdownMenu right>
                        { loggedIn ? 
                        <div>
                            <DropdownItem>
                                { isAdmin ? 
                                <NavItem>
                                    <NavLink tag={Link} to='/dashboard'>< RiDashboardFill size="18" className="dropdown-icons"/> &nbsp; Dashboard</NavLink>
                                </NavItem> :
                                isAuthor ?  
                                <NavItem>
                                    <NavLink tag={Link} to='/profile'>< RiDashboardFill size="18"  className="dropdown-icons" /> &nbsp; Dashboard</NavLink>
                                </NavItem> :  
                                <NavItem>
                                    <NavLink tag={Link} to='/customer-profile'>< RiDashboardFill size="18"  className="dropdown-icons" /> &nbsp; Dashboard</NavLink>
                                </NavItem> }
                            </DropdownItem>
                           
                            <DropdownItem>
                                <NavItem>
                                        <NavLink tag={Link} className="" to="/faq"> < BsQuestionCircle size="18" className="dropdown-icons" /> &nbsp; FAQ</NavLink>
                                </NavItem>
                            </DropdownItem>
                            <DropdownItem>
                                <NavItem>
                                        <NavLink tag={Link} className="" to="/tos"> <BsFileText size="18" className="dropdown-icons" /> &nbsp; TOS</NavLink>
                                </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="" to="/support"> <BsFileText size="18" className="dropdown-icons" /> &nbsp; Contact Support</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <div className="menu-line"></div>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="" to="/" onClick={() => { localStorage.clear(); setLoggedIn(false); setIsCustomer(false); setIsAuthor(false); stopConnection(dispatch); }}>< IoLogOutOutline size="18" className="dropdown-icons" /> &nbsp; Logout</NavLink>
                                    </NavItem>
                                </DropdownItem>
                        </div> : 
                        <div>
                            <DropdownItem>
                                <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/login">< BiLogIn size="18" className="dropdown-icons" /> &nbsp; Log In</NavLink>
                                </NavItem>
                            </DropdownItem>
                            <DropdownItem>
                                <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/register">< BiDockLeft size="18" className="dropdown-icons" /> &nbsp;  Register</NavLink>
                                </NavItem>
                            </DropdownItem>
                            <DropdownItem>
                                <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/faq">< BsQuestionCircle size="18" className="dropdown-icons" /> &nbsp; FAQ</NavLink>
                                </NavItem>
                            </DropdownItem>
                            <DropdownItem>
                                <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/tos"><BsFileText size="18" className="dropdown-icons" /> &nbsp;TOS</NavLink>
                                </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/support"> <BsFileText size="18" className="dropdown-icons" /> &nbsp; Contact Support</NavLink>
                                    </NavItem>
                                </DropdownItem>

                        </div>}
                    </DropdownMenu>
                </UncontrolledDropdown>
            </Navbar>
        </header>
    );
}

export default NavMenu;
