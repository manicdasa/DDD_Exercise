import React, { useEffect, useState } from 'react';
import { Route, Switch, useHistory } from 'react-router-dom';

import CustomerInformations from './CustomerInformations';
import CustomerProjects from './CustomerProjects';
import CustomerOffers from './CustomerOffers';
import '../../styles/Profile.css';
import CustomerOpenBookingPage from './CustomerOpenBookingPage';
import CustomerOpenProjectPage from './CustomerOpenProjectPage';
import RedirectComponent from '../Common/RedirectComponent';
import EditProjectComponent from './EditProjectComponent';
import Payment from '../Payment';
import AuthorInfo from './AuthorInfo';
import AssignProjectToAuthor from './AssignProjectToAuthor';
import { useDispatch, useSelector } from 'react-redux';
import { ActionCreatorsForChatComponent } from '../../store/ChatComponentReducer';

const CustomerRightSide = ({match}: any) =>
{
    return(
        <div className="register-user-box row">
            <div className="column-style">
                <CustomerInformations/>
                <CustomerProjects match={match}/>
            </div>
        </div>)
}

export const CustomerProfile = ({match}: any) => 
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
        else if(localStorage.getItem('role=Customer')===null && localStorage.getItem('role=Ghostwriter')!=null)
        {
            history.push('/profile');
        }
    }, [])

    return(
        <div className="row-style mt-3">
            <div className="register-user-box row mr-5">
                <CustomerOffers openChats={openChats} numberOfOpenChats={numberOfOpenChats}/>
            </div>
            <Switch>
                <Route path={`/customer-profile`} component={CustomerRightSide} exact />
                <Route path={`/customer-profile/booking/&id=:id&param=:headProposalId`} component={CustomerOpenBookingPage} exact />
                <Route path={`/customer-profile/project/&id=:id`} exact><CustomerOpenProjectPage openChats={openChats} numberOfOpenChats={numberOfOpenChats}/></Route>
                <Route path={`/customer-profile/project/modify`} component={EditProjectComponent} exact />
                <Route path={`/customer-profile/refresh`} component={RedirectComponent}/>
                <Route path={`/customer-profile/payment`} component={Payment}/>
                <Route path={`/customer-profile/preview-author&id=:id`} component={AuthorInfo}/>
                <Route path={`/customer-profile/assign-project`} component={AssignProjectToAuthor}/>
            </Switch>
        </div>
    );
}

export default CustomerProfile;