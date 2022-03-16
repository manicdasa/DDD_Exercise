import React, { useEffect, useState } from 'react';
import { Route, Switch, useHistory } from 'react-router-dom'
import '../../styles/Profile.css';
import { AuthorInformation } from './AuthorInformation';
import { AuthorProjects } from './AuthorProjects'
import { AuthorOffers } from './AuthorOffers'
import RedirectComponent from '../Common/RedirectComponent';
import EditAuthorProfile from '../Auth/EditAuthorProfile';
import OpenProposalPage from './OpenProposalPage';
import OpenBookingPage from './OpenBookingPage';
import OpenProjectPage from './OpenProjectPage';
import { useDispatch, useSelector } from 'react-redux';
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';

const ProfileRightSide = ({match}: any) =>
{
    return(
        <div className="register-user-box row">
            <div className="column"> 
                <AuthorInformation/>
                <AuthorProjects match={match}/> 
            </div> 
        </div>)
}

export const Profile = ({match}: any) =>
{      
    const history = useHistory();
    const dispatch = useDispatch();
    
    const openChats =  useSelector((state: any) => state.chatComponentsReducer.openChats.filter((element: any) => { return element.headProposalId != 0 } ));
    const numberOfOpenChats = useSelector((state: any) => state.chatComponentsReducer.numberOfOpenChats);


    useEffect(()=>
    {
        dispatch(ActionCreatorsForChatComponent.initialChatState())
        if(localStorage.getItem('token')===null)
        {
            history.push('/login');
        }
        else if(localStorage.getItem('role=Ghostwriter')===null && localStorage.getItem('role=Customer')!=null)
        {
            history.push('/customer-profile');
        }
    },[])

    return(
        <div className="row-style mt-3">
            <div className="register-user-box row mr-5 sidebar">
                <AuthorOffers openChats={openChats} numberOfOpenChats={numberOfOpenChats}/>
            </div>
            <Switch>
                <Route path={`/profile`} component={ProfileRightSide} exact />
                <Route path={`/profile/booking/&id=:id&param=:headProposalId`} component={OpenBookingPage} exact />
                <Route path={`/profile/project/&id=:id`} exact><OpenProjectPage openChats={openChats} numberOfOpenChats={numberOfOpenChats}/></Route>
                <Route path={`/profile/edit-author`} component={EditAuthorProfile} exact/>
                <Route path={`/profile/refresh`} component={RedirectComponent}/>
            </Switch>
        </div>)
}   

export default Profile;