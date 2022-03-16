import React, { useState } from 'react';
import { Nav, NavItem, NavLink, TabPane, TabContent, Row, Media } from 'reactstrap';
import classnames from 'classnames';
import { Link } from 'react-router-dom';
import _ from 'lodash';
import dayjs from 'dayjs';
import { MdChatBubble } from 'react-icons/md';
import { FaRegClock } from 'react-icons/fa';
import { RiPagesLine, RiFolderKeyholeFill } from 'react-icons/ri';
import { MdLanguage } from 'react-icons/md';
import { ImBooks } from "react-icons/im";
import { RiTimerFill, RiBroadcastFill } from "react-icons/ri";
import { IoBookSharp } from "react-icons/io5";
import { GoGlobe } from "react-icons/go";
import { VscFolderActive } from "react-icons/vsc";
import NumberFormat from 'react-number-format';

import '../../styles/AuthorProjects.css';
import SearchableProjects from "../Common/SearchableProjects";
import PrepopulateSearchProject from '../Common/PrepopulateSearchProjects';
import { useSelector } from 'react-redux';

export const AuthorProjects = ({match}: any) =>
{
    const [activeTab, setActiveTab] = useState('1');
    const toggle = (tab: React.SetStateAction<string>) => 
    {
        if(activeTab !== tab) setActiveTab(tab);
    }    

    const author = useSelector((state: any) => state.authorInformationsReducer.authorInformations);

    //active projects
    const activeNoProjectsDisplay =
        <div className="center column-style">
            <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ac.png" alt="Typewriter image"></Media></div>
            <p>You don't have any active projects.</p>
            <p className="nofrs-line"> </p>
        </div>

    const activeProjectsMapFunction = (value: any) =>
        <div className="column list" key={value.bookingId}>
            <div className="title-price row"><div className="title-proj" ><h5>< VscFolderActive size="19" className="active-proj-ico" />&nbsp;{value.projectTopic}</h5></div><div className="price-proj" ><h5>&#8364;&nbsp;<NumberFormat value={value.totalPrice} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></h5></div></div>
            <div className="proj-details row">
                <div className="row ml-1 list">
                    <p className="msg-user-p"><MdChatBubble color='#4E74DE' />&nbsp;@{value.customerUsername}</p>
                    <p className="ml-3">< ImBooks color="#909fca" />&nbsp;{value.kindOfWorkDTO.value}</p>
                    <p className="ml-3"><GoGlobe color="#909fca" />&nbsp;{value.languageDTO.value}</p>
                    <p className="ml-3"><RiTimerFill color="#909fca" />&nbsp;{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                    <p className="ml-3"><IoBookSharp color="#909fca" />&nbsp;{value.pagesNo}</p>
                    <p className="ml-3"></p>

                </div>
                <div className="open-proj"><NavLink tag={Link} to={`${match.url}/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>Open project</NavLink></div>
            </div>
        </div>

    //live broadcasts
    const liveNoProjectsDisplay =
        <div className="center column-style">
            <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ac.png" alt="Typewriter image"></Media></div>
            <p>You don't have any live broadcasts.</p>
            <p className="nofrs-line"> </p>
        </div>

    const liveProjectsMapFunction = (value: any) =>
        <div className="column list" key={value.id}>
            <div className="title-price row"><div className="title-proj" ><h5>< RiBroadcastFill size="20" className="broadcast-icon" />&nbsp;{value.projectTopic}</h5></div><div className="price-proj" ><h5>&#8364;&nbsp;<NumberFormat value={value.maxBudget} displayType={'text'} thousandSeparator={true} decimalScale={2} fixedDecimalScale={true} /></h5></div></div>
            <div className="proj-details row">
                <div className="row ml-1 list">
                    <p className="msg-user-p"><MdChatBubble color='#4E74DE' />&nbsp;@{value.customerUsername}</p>
                    <p className="ml-3">< ImBooks color="#909fca" />&nbsp;{value.kindOfWorkDTO.value}</p>
                    <p className="ml-3"><GoGlobe color="#909fca" />&nbsp;{value.languageDTO.value}</p>
                    <p className="ml-3"><RiTimerFill color="#909fca" />&nbsp;{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                    <p className="ml-3"><IoBookSharp color="#909fca" />&nbsp;{value.pagesNo}</p>

                </div>
                <div className="open-proj"> <NavLink tag={Link} to={`${match.url}/project/&id=${value.id}`}>Open live broadcast</NavLink></div>
            </div>
        </div>

                        
    //closed projects
    const closedNoProjectsDisplay =
        <div className="center column-style">
            <div className="img-noclosedproj"><Media style={{ width: '', height: '' }} width="196" object src="/images/nooffers_ar.png" alt="Typewriter image"></Media></div>
            <p>You don't have any closed projects.</p>
            <p className="nofrs-line"> </p>
        </div> 

    const closedProjectsMapFunction = (value: any) =>
        <div className="column list" key={value.bookingId}>
            <div className="title-price row"><div className="title-proj" ><h5>< RiFolderKeyholeFill size="20" className="broadcast-icon" />&nbsp;&nbsp;{value.projectTopic}</h5></div><div className="price-proj" ><h5>&#8364;&nbsp;{value.totalPrice}</h5></div></div>
            <div className="proj-details row">
                <div className="row ml-1 list">
                    <p><MdChatBubble color='#4E74DE' className="mt-1" />&nbsp;@{value.customerUsername}</p>
                    <p className="ml-3">< ImBooks color="#909fca" />&nbsp;{value.kindOfWorkDTO.value}</p>
                    <p className="ml-3"><GoGlobe color="#909fca" />&nbsp;{value.languageDTO.value}</p>
                    <p className="ml-3"><RiTimerFill color="#909fca" />&nbsp;{dayjs(value.deadline).format('DD.MM.YYYY').toString()}</p>
                    <p className="ml-3"><IoBookSharp color="#909fca" />&nbsp;{value.pagesNo}</p>
                    
                </div>
                <div className="open-proj"><NavLink tag={Link} to={`${match.url}/booking/&id=${value.bookingId}&param=${value.headProposalId}`}>Open project</NavLink></div>
            </div>
        </div>

    return(
        <div>
            <Nav tabs className="author-projects-tabs">
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '1' })} onClick={() => { toggle('1'); }} to="#">Live broadcast</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '2' })} onClick={() => { toggle('2'); }} to="#">Active projects</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink className={classnames({ active: activeTab === '3' })} onClick={() => { toggle('3'); }} to="#">Closed projects</NavLink>
                </NavItem>
            </Nav>
            
            <TabContent activeTab={activeTab}>
                <TabPane tabId="1">
                    <Row>
                        <PrepopulateSearchProject author={author} match={match} baseUrl="/Project/GetAuthorsLiveBroadcast" noProjectsDisplay={liveNoProjectsDisplay} projectsMapFunction={liveProjectsMapFunction} />
                    </Row>
                </TabPane>
                <TabPane tabId="2">
                    <Row>
                        <SearchableProjects match={match} baseUrl="/Booking/GetAuthorsActiveProjects" noProjectsDisplay={activeNoProjectsDisplay} projectsMapFunction={activeProjectsMapFunction} />
                    </Row>
                </TabPane>
                <TabPane tabId="3">
                    <Row>
                        <SearchableProjects match={match} baseUrl="/Booking/GetAuthorsClosedProjects" noProjectsDisplay={closedNoProjectsDisplay} projectsMapFunction={closedProjectsMapFunction} />
                    </Row>
                </TabPane>
            </TabContent>
        </div>
    );
}

export default AuthorProjects;