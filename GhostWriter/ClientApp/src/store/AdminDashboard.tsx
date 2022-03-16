const initialState = {
    dashboardStats: 
    {
        activeProjects: 0,
        disputeProjects: 0,
        newProjects: 0,
        newUsers: 0
    },
    loadingValueStats: false,
    projectsDatatable: 
    [
        {
            id: 0,
            bookingStatus: "",
            deadline: "",
            totalPrice: 0,
            totalServiceCharges: 0,
            projectTopic: "",
            pagesNo: 0,
            customerUsername: "",
            authorUsername: "",
            languageDTO: {
              id: 0,
              value: ""
            },
            kindOfWorkDTO: {
              id: 0,
              value: "",
              description: ""
            }
        }
    ],
    projectsDatatableTotalCount: 0,
    authorStats: 
    [
        {
          username: "",
          firstName: "",
          lastName: "",
          dateRegistered: "",
          noActiveProjects: 0,
          noClosedProjects: 0
        }
    ],
    authorStatsTotalCount: 0,
    customerStats: 
    [
        {
          username: "",
          dateRegistered: "",
          noActiveProjects: 0,
          noClosedProjects: 0
        }
    ],
    customerStatsTotalCount: 0,
    newProjects: 
    [
        {
            id: 0,
            bookingStatus: "",
            dueDate: "",
            dateCreated: "",
            projectTopic: "",
            customerUsername: "",
            authorUsername: "",
            authorId: 0
        }
    ],
    newProjectsTotalCount: 0,
    activeProjects: [
        {
            id: 0,
            bookingStatus: "",
            dueDate: "",
            dateCreated: "",
            projectTopic: "",
            customerUsername: "",
            authorUsername: "",
            authorId: 0
        }
    ],
    activeProjectsTotalCount: 0,
    projectsInDispute: [
        {
          id: 0,
          bookingStatus: "",
          dueDate: "",
          dateCreated: "",
          projectTopic: "",
          customerUsername: "",
          authorUsername: "",
          authorId: 0
        }
    ],
    projectsInDisputeTotalCount: 0,
    archivedProjects: [
        {
          id: 0,
          bookingStatus: "",
          dueDate: "",
          dateCreated: "",
          projectTopic: "",
          customerUsername: "",
          authorUsername: "",
          authorId: 0
        }
    ],
    archivedProjectsTotalCount: 0,
    newUsers: [
        {
          username: "",
          dateRegistered: "",
          noActiveProjects: 0,
          noClosedProjects: 0,
          id: 0,
          noProjectsCreated: 0,
          lastProjectTitle: "",
          lastProjectId: 0,
          noUnassignedProjects: 0
        }
    ],
    newUsersTotalCount: 0,
    activeUsers: [
        {
          username: "",
          dateRegistered: "",
          noActiveProjects: 0,
          noClosedProjects: 0,
          id: 0,
          noProjectsCreated: 0,
          lastProjectTitle: "",
          lastProjectId: 0,
          noUnassignedProjects: 0
        }
    ],
    activeUsersTotalCount: 0,
    inactiveUsers: [
        {
          username: "",
          dateRegistered: "",
          noActiveProjects: 0,
          noClosedProjects: 0,
          id: 0,
          noProjectsCreated: 0,
          lastProjectTitle: "",
          lastProjectId: 0,
          noUnassignedProjects: 0
        }
    ],
    inactiveUsersTotalCount: 0,
    newAuthors: [
        {
          username: "string",
          firstName: "string",
          lastName: "string",
          dateRegistered: "2021-04-12T17:03:31.027Z",
          noActiveProjects: 0,
          noClosedProjects: 0,
          id: 0,
          noTotalProjects: 0,
          lastProjectTitle: "string",
          lastProjectId: 0
        }
    ],
    newAuthorsTotalCount: 0,
    activeAuthors: [
        {
          username: "string",
          firstName: "string",
          lastName: "string",
          dateRegistered: "2021-04-12T17:03:31.027Z",
          noActiveProjects: 0,
          noClosedProjects: 0,
          id: 0,
          noTotalProjects: 0,
          lastProjectTitle: "string",
          lastProjectId: 0
        }
    ],
    activeAuthorsTotalCount: 0,
    inactiveAuthors: [
        {
          username: "string",
          firstName: "string",
          lastName: "string",
          dateRegistered: "2021-04-12T17:03:31.027Z",
          noActiveProjects: 0,
          noClosedProjects: 0,
          id: 0,
          noTotalProjects: 0,
          lastProjectTitle: "string",
          lastProjectId: 0
        }
    ],
    inactiveAuthorsTotalCount: 0
}

export const ActionTypes =
{
    SET_DASHBOARD_STATS: 'SET_DASHBOARD_STATS',
    SET_PROJECTS_DATATABLE: 'SET_PROJECTS_DATATABLE',
    SET_PROJECTS_DATATABLE_TOTALCOUNT: 'SET_PROJECTS_DATATABLE_TOTALCOUNT',
    SET_AUTHOR_STATS: 'SET_AUTHOR_STATS',
    SET_CUSTOMER_STATS: 'SET_CUSTOMER_STATS',
    SET_AUTHOR_STATS_TOTAL_COUNT: 'SET_AUTHOR_STATS_TOTAL_COUNT',
    SET_CUSTOMER_STATS_TOTAL_COUNT: 'SET_CUSTOMER_STATS_TOTAL_COUNT',
    SET_LOADING_VALUE_STATS: 'SET_LOADING_VALUE_STATS',

    SET_NEW_PROJECTS: 'SET_NEW_PROJECTS',
    SET_NEW_PROJECTS_TOTAL_COUNT: 'SET_NEW_PROJECTS_TOTAL_COUNT',

    SET_ACTIVE_PROJECTS: 'SET_ACTIVE_PROJECTS',
    SET_ACTIVE_PROJECTS_TOTAL_COUNT: 'SET_ACTIVE_PROJECTS_TOTAL_COUNT',

    SET_PROJECTS_IN_DISPUTE: 'SET_PROJECTS_IN_DISPUTE',
    SET_PROJECTS_IN_DISPUTE_TOTAL_COUNT: 'SET_PROJECTS_IN_DISPUTE_TOTAL_COUNT',

    SET_ARCHIVED_PROJECTS: 'SET_ARCHIVED_PROJECTS',
    SET_ARCHIVED_PROJECTS_TOTAL_COUNT: 'SET_ARCHIVED_PROJECTS_TOTAL_COUNT',
    
    SET_NEW_USERS: 'SET_NEW_USERS',
    SET_NEW_USERS_TOTAL_COUNT: 'SET_NEW_USERS_TOTAL_COUNT',

    SET_ACTIVE_USERS: 'SET_ACTIVE_USERS',
    SET_ACTIVE_USERS_TOTAL_COUNT: 'SET_ACTIVE_USERS_TOTAL_COUNT',

    SET_INACTIVE_USERS: 'SET_INACTIVE_USERS',
    SET_INACTIVE_USERS_TOTAL_COUINT: 'SET_INACTIVE_USERS_TOTAL_COUINT',

    SET_NEW_AUTHORS: 'SET_NEW_AUTHORS',
    SET_NEW_AUTHORS_TOTAL_COUNT: 'SET_NEW_AUTHORS_TOTAL_COUNT',

    SET_ACTIVE_AUTHORS: 'SET_ACTIVE_AUTHORS',
    SET_ACTIVE_AUTHORS_TOTAL_COUNT: 'SET_ACTIVE_AUTHORS_TOTAL_COUNT',

    SET_INACTIVE_AUTHORS: 'SET_INACTIVE_AUTHORS',
    SET_INACTIVE_AUTHORS_TOTAL_COUINT: 'SET_INACTIVE_AUTHORS_TOTAL_COUINT',
    
}

export const ActionCreators = 
{
    setDashboardStats: (payload: Object) => ({type: ActionTypes.SET_DASHBOARD_STATS, payload}),
    setProjectsDatatable: (payload: Object) => ({type: ActionTypes.SET_PROJECTS_DATATABLE, payload}),
    setProjectsDatatableTotalCount: (payload: any) => ({type: ActionTypes.SET_PROJECTS_DATATABLE_TOTALCOUNT, payload}),
    setAuthorStats: (payload: Object) => ({type: ActionTypes.SET_AUTHOR_STATS, payload}),
    setCustomerStats: (payload: Object) => ({type: ActionTypes.SET_CUSTOMER_STATS, payload}),
    setAuthorStatsTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHOR_STATS_TOTAL_COUNT, payload}),
    setCustomerStatsTotalCount: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_STATS_TOTAL_COUNT, payload}),
    setLoadingValueStats: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE_STATS, payload}),

    setNewProjects: (payload: any) => ({type: ActionTypes.SET_NEW_PROJECTS, payload}),
    setNewProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_NEW_PROJECTS_TOTAL_COUNT, payload}),

    setActiveProjects: (payload: any) => ({type: ActionTypes.SET_ACTIVE_PROJECTS, payload}),
    setActiveProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_ACTIVE_PROJECTS_TOTAL_COUNT, payload}),

    setProjectsInDispute: (payload: any) => ({type: ActionTypes.SET_PROJECTS_IN_DISPUTE, payload}),
    setProjectsInDisputeTotalCount: (payload: any) => ({type: ActionTypes.SET_PROJECTS_IN_DISPUTE_TOTAL_COUNT, payload}),

    setArchivedProjects: (payload: any) => ({type: ActionTypes.SET_ARCHIVED_PROJECTS, payload}),
    setArchivedProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_ARCHIVED_PROJECTS_TOTAL_COUNT, payload}),

    setNewUsers: (payload: any) => ({type: ActionTypes.SET_NEW_USERS, payload}),
    setNewUsersTotalCount: (payload: any) => ({type: ActionTypes.SET_NEW_USERS_TOTAL_COUNT, payload}),

    setActiveUsers: (payload: any) => ({type: ActionTypes.SET_ACTIVE_USERS, payload}),
    setActiveUsersTotalCount: (payload: any) => ({type: ActionTypes.SET_ACTIVE_USERS_TOTAL_COUNT, payload}),

    setInactiveUsers: (payload: any) => ({type: ActionTypes.SET_INACTIVE_USERS, payload}),
    setInactiveUsersTotalCount: (payload: any) => ({type: ActionTypes.SET_INACTIVE_USERS_TOTAL_COUINT, payload}),
    
    setNewAuthors: (payload: any) => ({type: ActionTypes.SET_NEW_AUTHORS, payload}),
    setNewAuthorsTotalCount: (payload: any) => ({type: ActionTypes.SET_NEW_AUTHORS_TOTAL_COUNT, payload}),

    setActiveAuthors: (payload: any) => ({type: ActionTypes.SET_ACTIVE_AUTHORS, payload}),
    setActiveAuthorsTotalCount: (payload: any) => ({type: ActionTypes.SET_ACTIVE_AUTHORS_TOTAL_COUNT, payload}),

    setInactiveAuthors: (payload: any) => ({type: ActionTypes.SET_INACTIVE_AUTHORS, payload}),
    setInactiveAuthorsTotalCount: (payload: any) => ({type: ActionTypes.SET_INACTIVE_AUTHORS_TOTAL_COUINT, payload}),
}

export default function adminDashboardReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_DASHBOARD_STATS:
            return {...state, dashboardStats: {...action.payload}};
        case ActionTypes.SET_PROJECTS_DATATABLE:
            return {...state, projectsDatatable: [...action.payload]};
        case ActionTypes.SET_PROJECTS_DATATABLE_TOTALCOUNT:
            return {...state, projectsDatatableTotalCount: action.payload}
        case ActionTypes.SET_AUTHOR_STATS:
            return {...state, authorStats: [...action.payload]};
        case ActionTypes.SET_AUTHOR_STATS_TOTAL_COUNT:
            return {...state, authorStatsTotalCount: action.payload}
        case ActionTypes.SET_CUSTOMER_STATS:
            return {...state, customerStats: [...action.payload]};
        case ActionTypes.SET_CUSTOMER_STATS_TOTAL_COUNT:
            return {...state, customerStatsTotalCount: action.payload}
        case ActionTypes.SET_LOADING_VALUE_STATS:
            return {...state, loadingValueStats: action.payload};
        case ActionTypes.SET_NEW_PROJECTS:
            return {...state, newProjects: [...action.payload]};
        case ActionTypes.SET_NEW_PROJECTS_TOTAL_COUNT:
            return {...state, newProjectsTotalCount: action.payload}
        case ActionTypes.SET_ACTIVE_PROJECTS:
            return {...state, activeProjects: [...action.payload]};
        case ActionTypes.SET_ACTIVE_PROJECTS_TOTAL_COUNT:
            return {...state, activeProjectsTotalCount: action.payload}
        case ActionTypes.SET_PROJECTS_IN_DISPUTE:
            return {...state, projectsInDispute: [...action.payload]};
        case ActionTypes.SET_PROJECTS_IN_DISPUTE_TOTAL_COUNT:
            return {...state, projectsInDisputeTotalCount: action.payload}
        case ActionTypes.SET_ARCHIVED_PROJECTS:
            return {...state, archivedProjects: [...action.payload]};
        case ActionTypes.SET_ARCHIVED_PROJECTS_TOTAL_COUNT:
            return {...state, archivedProjectsTotalCount: action.payload}
        
        case ActionTypes.SET_NEW_USERS:
            return {...state, newUsers: [...action.payload]};
        case ActionTypes.SET_NEW_USERS_TOTAL_COUNT:
            return {...state, newUsersTotalCount: action.payload}        

        case ActionTypes.SET_ACTIVE_USERS:
            return {...state, activeUsers: [...action.payload]};
        case ActionTypes.SET_ACTIVE_USERS_TOTAL_COUNT:
            return {...state, activeUsersTotalCount: action.payload}
 
        case ActionTypes.SET_INACTIVE_USERS:
            return {...state, inactiveUsers: [...action.payload]};
        case ActionTypes.SET_INACTIVE_USERS_TOTAL_COUINT:
            return {...state, inactiveUsersTotalCount: action.payload}   
            
        case ActionTypes.SET_NEW_AUTHORS:
            return {...state, newAuthors: [...action.payload]};
        case ActionTypes.SET_NEW_AUTHORS_TOTAL_COUNT:
            return {...state, newAuthorsTotalCount: action.payload}        
    
        case ActionTypes.SET_ACTIVE_AUTHORS:
            return {...state, activeAuthors: [...action.payload]};
        case ActionTypes.SET_ACTIVE_AUTHORS_TOTAL_COUNT:
            return {...state, activeAuthorsTotalCount: action.payload}
     
        case ActionTypes.SET_INACTIVE_AUTHORS:
            return {...state, inactiveAuthors: [...action.payload]};
        case ActionTypes.SET_INACTIVE_AUTHORS_TOTAL_COUINT:
            return {...state, inactiveAuthorsTotalCount: action.payload}          
        default:
            return state;
    }
}