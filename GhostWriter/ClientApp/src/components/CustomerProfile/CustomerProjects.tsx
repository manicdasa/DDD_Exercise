import React, { useState } from 'react';
import { Nav, NavItem, NavLink, TabContent, TabPane, Row, Media } from 'reactstrap';
import classnames from 'classnames';
import { MdChatBubble, MdLanguage, MdOpenInNew, MdInfo} from 'react-icons/md';
import { FaRegClock } from 'react-icons/fa';
import dayjs from 'dayjs';
import _ from 'lodash';
import { VscFolderActive } from "react-icons/vsc";
import { ImBooks } from "react-icons/im";
import { RiTimerFill, RiBroadcastFill, RiPagesLine, RiFolderKeyholeFill, RiFolderHistoryFill, RiFolderOpenFill } from "react-icons/ri";
import { IoBookSharp } from "react-icons/io5";
import { GoGlobe } from "react-icons/go";
import { Link, useHistory } from 'react-router-dom';
import ReactTooltip from 'react-tooltip';
import NumberFormat from 'react-number-format';

import AuthorPublicInformation from '../AuthorProfile/AuthorPublicInformation';
import SearchableProjects from "../Common/SearchableProjects";


export const CustomerProjects = ({ match }: any) => 
{
    const history = useHistory();
    const [activeTab, setActiveTab] = useState('1');

    const toggle = (tab: React.SetStateAction<string>) => {
        if (activeTab !== tab) setActiveTab(tab);
    }
    
    const [popUpState, setPopUpState] = useState(false);
    const [activePopup, setActivePopup] = useState(-1);

    const [popUpStateClosedProjects, setPopUpStateClosedProjects] = useState(false);
    const [activePopupClosedProjects, setActivePopupClosedProjects] = useState(-1);

    //active projects
    const activeNoProjectsDisplay =
        <div className="center column-style">
            <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ac.png" alt="Typewriter image"></Media></div>
            <p>You don't have any active projects.</p>
            <p className="nofrs-line"> </p>
        </div>

    const activeProjectsMapFunction = (value: { projectTopic: string, bookingId: number, headProposalId: number, authorId: number, kindOfWorkDTO: { value: string }, languageDTO: { value: string}, pagesNo: number, deadline: Date, totalPrice: number, authorUsername: string} ) =>
        <div className="column list" key={value.bookingId}>
            <div className="title-price row"><div className="title-proj" ><h5>< RiFolderHistoryFill size="19" className="active-proj-ico" />&nbsp;&nbsp;{value.projectTopic}</h5></div><div className="price-proj" ><h5>&#8364;&nbsp;<NumberFormat value={value.totalPrice} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></h5></div></div>
            <div className="proj-details row">
                <div className="row ml-1 list">
                    <MdChatBubble color='#4E74DE' className="mt-1" />&nbsp;<NavLink className="cursor author-tooltip-nav-link-sidebar" onMouseEnter={()=> { setPopUpState(true); setActivePopup(value.bookingId)} } onMouseLeave={()=> { setPopUpState(false); setActivePopup(-1); } } data-tip="" data-for={`${value.authorId}`} data-type="light" onClick={()=> { history.push(`/customer-profile/preview-author&id=${value.authorId}`) }}>@{value.authorUsername}</NavLink>                    
                    { (popUpState && activePopup === value.bookingId) ? <ReactTooltip id={`${value.authorId}`} getContent={() => { return <AuthorPublicInformation value={value.authorId} /> }} /> : <div/> }
                    <p className="ml-3">< ImBooks color="#909fca" />&nbsp;{value.kindOfWorkDTO.value}</p>
                    <p className="ml-3"><GoGlobe color="#909fca" />&nbsp;{value.languageDTO.value}</p>
                    <p className="ml-3"><RiTimerFill color="#909fca" />&nbsp;{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                    <p className="ml-3"><IoBookSharp color="#909fca" />&nbsp;{value.pagesNo}</p>
                </div>
                <div className="open-proj"><NavLink tag={Link} to={`${match.url}/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>Open project</NavLink></div>
            </div>
        </div>

    //open projects
    const openNoProjectsDisplay =
        <div className="center column-style">
            <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_op.png" alt="Typewriter image"></Media></div>
            <p>Looks like you don't have any projects.</p>
            <p className="nofrs-line"> </p>
        </div>

    const openProjectsMapFunction = (value: { projectTopic: string, id: number, maxBudget : number, projectId: number, kindOfWorkDTO: { value: string }, languageDTO: { value: string }, pagesNo: number, deadline: Date}) =>
        <div className="column list" key={value.projectId}>
            <div className="title-price row"><div className="title-proj" ><h5>< RiFolderOpenFill size="18" className="open-project-icon" />&nbsp;&nbsp;{value.projectTopic}</h5></div><div className="price-proj" ><h5>&#8364;&nbsp;<NumberFormat value={value.maxBudget} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></h5></div></div>
            <div className="proj-details row">
                <div className="row ml-1 list open-projects-tab-customer">
                    <p className="ml-3">< ImBooks color="#909fca" />&nbsp;{value.kindOfWorkDTO.value}</p>
                    <p className="ml-3"><GoGlobe color="#909fca" />&nbsp;{value.languageDTO.value}</p>
                    <p className="ml-3"><RiTimerFill color="#909fca" />&nbsp;{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                    <p className="ml-3"><IoBookSharp color="#909fca" />&nbsp;{value.pagesNo}</p>

                </div>
                <div className="open-proj"> <NavLink tag={Link} to={`${match.url}/project/&id=${value.projectId}`}>Open project</NavLink></div>
            </div>
        </div>

    //closed projects
    const closedNoProjectsDisplay =
        <div className="center column-style">
            <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ar.png" alt="Typewriter image"></Media></div>
            <p>You dont have any closed projects.</p>
            <p className="nofrs-line"></p>
        </div>

    const closedProjectsMapFunction = (value: { projectTopic: string, bookingId: number, headProposalId: number, authorId: number, kindOfWorkDTO: { value: string }, languageDTO: { value: string }, pagesNo: number, deadline: Date, totalPrice: number, authorUsername: string }) =>
        <div className="column list" key={value.bookingId}>
            <div className="title-price row"><div className="title-proj" ><h5>< RiFolderKeyholeFill size="20" className="broadcast-icon" />&nbsp;&nbsp;{value.projectTopic}</h5></div><div className="price-proj" ><h5>&#8364;&nbsp;<NumberFormat value={value.totalPrice} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></h5></div></div>
            <div className="proj-details row">
                <div className="row ml-1 list">
                    <MdChatBubble color='#4E74DE' className="mt-1" />&nbsp;<NavLink className="cursor author-tooltip-nav-link-sidebar" onMouseEnter={()=> { setPopUpStateClosedProjects(true); setActivePopupClosedProjects(value.bookingId)} } onMouseLeave={()=> { setPopUpStateClosedProjects(false); setActivePopupClosedProjects(-1); } } data-tip="" data-for={`${value.authorId}`} data-type="light" onClick={()=> { history.push(`/customer-profile/preview-author&id=${value.authorId}`) }}>@{value.authorUsername}</NavLink>                    
                    { (popUpStateClosedProjects && activePopupClosedProjects === value.bookingId) ? <ReactTooltip id={`${value.authorId}`} getContent={() => { return <AuthorPublicInformation value={value.authorId} /> }} /> : <div/> }
                    <p className="ml-3">< ImBooks color="#909fca" />&nbsp;{value.kindOfWorkDTO.value}</p>
                    <p className="ml-3"><GoGlobe color="#909fca" />&nbsp;{value.languageDTO.value}</p>
                    <p className="ml-3"><RiTimerFill color="#909fca" />&nbsp;{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                    <p className="ml-3"><IoBookSharp color="#909fca" />&nbsp;{value.pagesNo}</p>
                   
                    
                    
                </div>
                <div className="open-proj"><NavLink tag={Link} to={`${match.url}/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>Open project</NavLink></div>
            </div>
     
        </div>

    return (
        <div>
            <Nav className="customer-nav-projects" tabs>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); }} to="#">Active projects</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); }} to="#">Open projects</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); }} to="#">Closed projects</NavLink>
                </NavItem>
            </Nav>

            <TabContent activeTab={activeTab}>
                <TabPane tabId="1">
                    <Row>
                        <SearchableProjects match={match} baseUrl="/Booking/GetCustomersActiveProjects" noProjectsDisplay={activeNoProjectsDisplay} projectsMapFunction={activeProjectsMapFunction} />
                    </Row>
                </TabPane>
                <TabPane tabId="2">
                    <Row>
                        <SearchableProjects match={match} baseUrl="/Project/GetCustomersOpenProjects" noProjectsDisplay={openNoProjectsDisplay} projectsMapFunction={openProjectsMapFunction}/>
                    </Row>
                </TabPane>
                <TabPane tabId="3">
                    <Row>
                        <SearchableProjects match={match} baseUrl="/Booking/GetCustomersClosedProjects" noProjectsDisplay={closedNoProjectsDisplay} projectsMapFunction={closedProjectsMapFunction} />
                    </Row>
                </TabPane>
            </TabContent>

        </div>)
}

export default CustomerProjects;