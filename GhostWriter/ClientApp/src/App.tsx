import * as React from 'react';
import {  Route, Switch } from 'react-router';
import { Router } from 'react-router-dom';
import MainLayout from './components/Layout/MainLayout';
import LandingPageLayout from './components/Layout/LandingPageLayout';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import RegisterAuthor from './components/Auth/RegisterAuthor';
import Register from './components/Auth/Register';
import SignIn from './components/Auth/SignIn';

import SearchAuthor from './components/SearchAuthor';
import LandingPage from './components/LandingPage';
import Profile from './components/AuthorProfile/Profile'
import CreateProject from './components/CreateProject';
import FAQ from './components/FAQ';
import TOS from './components/TOS';
import RequestBooking from './components/RequestBooking';
import CustomerProfile from './components/CustomerProfile/CustomerProfile';

import './custom.css'
import SuccesfullyCreatedAccount from './components/Auth/SuccesfullyCreatedAccount';
import EmailConfimed from './components/Auth/EmailConfirmed';
import AuthenticationLayout from './components/Layout/AuthenticationLayout';
import PasswordReset from './components/Auth/PasswordReset';
import SuccesfullyCreatedProject from './components/SuccesfullyCreatedProject';
import AuthorPublicInformation from './components/AuthorProfile/AuthorPublicInformation'
import Dashboard from './components/AdminDashboard/Dashboard';
import AdminLogin from './components/AdminDashboard/AdminLogin';

import AdminLayout from './components/Layout/AdminLayout';
import DashboardConcreteProject from './components/AdminDashboard/DashboardConcreteProject';
import DashboardUnpaidProjects from './components/AdminDashboard/DashboardUnpaidProjects'
import { setupSignalRConnection } from '../src/components/Helpers/SignalRMiddleware';
import MobileComponent from './components/MobileComponent';
import RedirectComponent from './components/Common/RedirectComponent';
import { useDispatch } from 'react-redux';
import { isMobile } from "react-device-detect";

import { createBrowserHistory } from 'history';
import SidebarWrapper from './components/AdminDashboard/SidebarWrapper';
import DataTableComponent from './components/AdminDashboard/DataTableComponent';

//columns import
import { activeAuthorsColumns, activeProjectsColumns, activeUsersColumns, archivedProjectsColumns, areasOfExpertiseColumns, authorsColumns, inactiveAuthorsColumns, inactiveUsersColumns, inDisputeProjectColumns, kindOfWorkColumns, newAuthorsColumns, newProjectsColumns, newUsersColumns, notificationsCenterColumns, projectsColumns, usersColumns } from './components/AdminDashboard/AdminColumns';
import Support from './components/Support';
import CustomerLandingPageReadMore from './components/Layout/CustomerLandingPageReadMore';
import AuthorLandingPageReadMore from './components/Layout/AuthorLandingPageReadMore';

export const App = () =>
{
    const dispatch = useDispatch();
    if(localStorage.getItem('token') != null)
    {
        const setupEventsHub = setupSignalRConnection(() => localStorage.getItem('token'));
        dispatch(setupEventsHub);
    }

    if(isMobile) 
    {
        return <div><MobileComponent/></div>
    }

    const history=createBrowserHistory();

    return (
    <Router history={history}>
        <Switch> 
            <Route exact path='/'>
                    <LandingPageLayout>
                    <Route component={LandingPage} />
                </LandingPageLayout>
            </Route>
            <Route path={["/profile", "/customer-profile"]}>
                <AuthenticationLayout>
                    <Switch>
                        <Route path={`/customer-profile`} component={CustomerProfile} exact />
                        <Route path={`/customer-profile/booking/&id=:id&param=:headProposalId`} component={CustomerProfile} exact />
                        <Route path={`/customer-profile/project/&id=:id`} component={CustomerProfile} exact />
                        <Route path={`/customer-profile/project/modify`} component={CustomerProfile} exact />
                        <Route path={`/customer-profile/refresh`} component={CustomerProfile}/>
                        <Route path={`/customer-profile/payment`} component={CustomerProfile}/>
                        <Route path={`/customer-profile/preview-author&id=:id`} component={CustomerProfile} exact />
                        <Route path={`/customer-profile/assign-project`} component={CustomerProfile} exact />
        
                        <Route path={`/profile`} component={Profile} exact />
                        <Route path={`/profile/booking/&id=:id&param=:headProposalId`} component={Profile} exact />
                        <Route path={`/profile/project/&id=:id`} component={Profile} exact />
                        <Route path={`/profile/edit-author`} component={Profile} exact/>
                        <Route path={`/profile/refresh`} component={Profile}/>
                    </Switch>
                </AuthenticationLayout>
            </Route>
            <Route exact path='/dashboard/login' component={AdminLogin}/>
            <Route path={["/dashboard"]}>
                <AdminLayout>
                    <Switch>
                        <Route
                            path="/dashboard"
                            render={({ match: { url } }) => (
                            <>
                                <SidebarWrapper>
                                    <Switch>
                                        <Route path={`${url}`} component={Dashboard} exact />
                                        <Route path={`${url}/notifications`} component={() => <DataTableComponent columns={notificationsCenterColumns} path='/Notification/GetUserNotifications' componentName='Dashboard Notifications Center' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/users`} component={() => <DataTableComponent columns={usersColumns} path='/Admin/GetCustomerStats' componentName='Dashboard Users' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/authors`} component={() => <DataTableComponent columns={authorsColumns} path='/Admin/GetAuthorStats' componentName='Dashboard Authors' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/projects`} component={() => <DataTableComponent columns={projectsColumns} path='/Admin/GetProjectsDatatable' componentName='Dashboard Projects' columnSort="deadline"/>} exact />
                                        <Route path={`${url}/projects/id=:id`} component={DashboardConcreteProject} exact />
                                        <Route path={`${url}/statistics/new-projects`} component={() => <DataTableComponent columns={newProjectsColumns} path='/Admin/GetNewProjects' componentName='Dashboard New Projects' columnSort=""/>} exact />
                                        <Route path={`${url}/statistics/unpaid-projects`} component={DashboardUnpaidProjects} exact />
                                        <Route path={`${url}/statistics/active-projects`} component={() => <DataTableComponent columns={activeProjectsColumns} path='/Admin/GetActiveProjects' componentName='Dashboard Active Projects' columnSort=""/>} exact />
                                        <Route path={`${url}/statistics/in-dispute`} component={() => <DataTableComponent columns={inDisputeProjectColumns} path='/Admin/GetProjectsInDispute' componentName='Dashboard Projects In Dispute' columnSort=""/>} exact />
                                        <Route path={`${url}/statistics/archived-projects`} component={() => <DataTableComponent columns={archivedProjectsColumns} path='/Admin/GetArchivedProjects' componentName='Dashboard Archived Authors' columnSort=""/>} exact />
                                        <Route path={`${url}/statistics/new-users`} component={() => <DataTableComponent columns={newUsersColumns} path='/Admin/GetNewUsers' componentName='Dashboard New Users' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/statistics/user-activity`} component={() => <DataTableComponent columns={activeUsersColumns} path='/Admin/GetActiveUsers' componentName='Dashboard Active Users' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/statistics/inactive-users`} component={() => <DataTableComponent columns={inactiveUsersColumns} path='/Admin/GetInactiveUsers' componentName='Dashboard Inactive Users' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/statistics/new-authors`} component={() => <DataTableComponent columns={newAuthorsColumns} path='/Admin/GetNewAuthors' componentName='Dashboard New Authors' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/statistics/author-activity`} component={() => <DataTableComponent columns={activeAuthorsColumns} path='/Admin/GetActiveAuthors' componentName='Dashboard Active Authors' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/statistics/inactive-authors`} component={() => <DataTableComponent columns={inactiveAuthorsColumns} path='/Admin/GetInactiveAuthors' componentName='Dashboard Inactive Authors' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/kinds-of-work`} component={() => <DataTableComponent columns={kindOfWorkColumns} path='/Lookup/GetCustomPendingKindsOfWork' componentName='Dashboard Kind Of Works' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/areas-of-expertise`} component={() => <DataTableComponent columns={areasOfExpertiseColumns} path='/Lookup/GetCustomPendingExpertiseAreas' componentName='Dashboard Areas Of Expertise' columnSort="dateRegistered"/>} exact />
                                        <Route path={`${url}/refresh`} component={RedirectComponent}/>
                                    </Switch>
                                </SidebarWrapper>
                            </>
                            )}
                        />
                    </Switch>
                </AdminLayout>
            </Route>
            <Route>
                <MainLayout>
                    <Switch>
                        <Route path='/counter' component={Counter} />
                        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
                        <Route path='/register-author' component={RegisterAuthor} />
                        <Route path='/search-author' component={SearchAuthor}/>
                        <Route path='/login' component={SignIn} />
                        <Route path='/register' component={Register} />
                        <Route path='/create-project' component={CreateProject} />
                        <Route path='/faq' component={FAQ} />
                        <Route path='/tos' component={TOS} />
                        <Route path='/support' component={Support} />
                        <Route path='/account-created' component={SuccesfullyCreatedAccount} />
                        <Route path='/created-project' component={SuccesfullyCreatedProject} />
                        <Route path='/email-confirmed' component={EmailConfimed} />
                        <Route path='/request-booking' component={RequestBooking} />
                        <Route path='/reset-password' component={PasswordReset} />
                        <Route path='/authors/id=:id' component={AuthorPublicInformation} exact/>
                        <Route path='/refresh' component={RedirectComponent}/>
                        <Route path='/customer-read-more' component={CustomerLandingPageReadMore}/>
                        <Route path='/author-read-more' component={AuthorLandingPageReadMore}/>
                    </Switch>
                </MainLayout>
            </Route>
        </Switch>
            </Router>)
};
