const initialState = {
    authorActiveProjects: 
    [
        {
            id: 0,
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
    authorActiveProjectsTotalCount: 1,
    authorLiveBroadcasts: 
    [
        {
            id: 0,
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
    authorLiveBroadcastTotalCount: 1,
    authorClosedProjects: 
    [
        {
            id: 0,
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
    authorClosedProjectsTotalCount: 1,
    loadingValueActiveProjects: false,
    loadingValueClosedProjects: false,
    loadingValueLiveBroadcasts: false,
    projectDetails: {
        id: 0,
        deadline: "",
        totalPrice: 0,
        totalServiceCharges: 0,
        paymentVerified: true,
        projectTopic: "",
        projectDescription: "",
        feePerPage: 0,
        taxPercentage: 0,
        pagesNo: 0,
        bookingStatus: {
          id: 0,
          value: ""
        },
        customerDTO: {
          id: 0,
          totalSpent: 0,
          jobsPostedCnt: 0,
          username: ""
        },
        authorUsername: "",
        languageDTO: {
          id: 0,
          value: ""
        },
        kindOfWorkDTO: {
          id: 0,
          Value: "",
          description: ""
        },
        expertiseAreaDTOs: [
          {
            id: 0,
            value: "",
            description: ""
          }
        ],
        documentDTOs: [
          {
            id: 0,
            publicName: "",
            isFinalVersion: true,
            dateCreated: ""
          }
        ]
    },
    projectDetailsLoadingValue: false,
    proposalDetails: {
        id: 0,
        isPublished: true,
        deadline: "",
        dateCreated: "",
        plannedBudget: 0,
        maxBudget: 0,
        projectTopic: "",
        description: "",
        feePerPage: 0,
        taxPercentage: 0,
        projectStatus: 1,
        pagesNo: 0,
        customerId: 0,
        customerUsername: "",
        minimumDegreeDTO: {
          id: 0,
          value: "",
          description: ""
        },
        languageDTO: {
          id: 0,
          value: ""
        },
        kindOfWorkDTO: {
          id: 0,
          value: "",
          description: ""
        },
        expertiseAreaListDTOs: [
          {
            id: 0,
            value: "",
            description: ""
          }
        ],
        proposalDetailsDTOs: [
          {
            id: 0,
            financialOffer: 0,
            financialOfferWithCharges: 0,
            proposalType: "",
            proposalStatus: "",
            dateCreated: "",
            customerUsername: "",
            authorUsername: "",
            authorId: ""
          }
        ],
        customerPublicInfoDTO: {
          id: 0,
          totalSpent: 0,
          jobsPostedCnt: 0,
          username: "",
          paymentVerified: true
        }
    },
    proposalDetailsLoadingValue: false,
    messages: 
    [
        {
            id: 0,
            conversationId: 0,
            headProposalId: 0,
            bookingId: 0,
            isLogMessage: true,
            username: "",
            myMessage: true,
            messageText: "",
            dateTimeSent: ""
        }
    ]
}

export const ActionTypes =
{
    SET_LOADING_VALUE_ACTIVE_PROJECTS: 'SET_LOADING_VALUE_ACTIVE_PROJECTS',
    SET_LOADING_VALUE_CLOSED_PROJECTS: 'SET_LOADING_VALUE_CLOSED_PROJECTS',
    SET_LOADING_VALUE_LIVE_BROADCASTS: 'SET_LOADING_VALUE_LIVE_BROADCASTS',
    SET_AUTHOR_ACTIVE_PROJECTS: 'SET_AUTHOR_ACTIVE_PROJECTS',
    SET_AUTHOR_CLOSED_PROJECTS: 'SET_AUTHOR_CLOSED_PROJECTS',
    SET_AUTHOR_LIVE_BROADCASTS: 'SET_AUTHOR_LIVE_BROADCASTS',
    ADD_AUTHOR_LIVE_BROADCASTS: 'ADD_AUTHOR_LIVE_BROADCASTS',
    ADD_AUTHOR_ACTIVE_PROJECTS: 'ADD_AUTHOR_ACTIVE_PROJECTS',
    ADD_AUTHOR_CLOSED_PROJECTS: 'ADD_AUTHOR_CLOSED_PROJECTS',
    SET_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT: 'SET_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT',
    SET_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT: 'SET_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT',
    SET_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT: 'SET_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT',
    ADD_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT: 'ADD_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT',
    ADD_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT: 'ADD_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT',
    ADD_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT: 'ADD_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT',
    SET_PROJECT_DETAILS: 'SET_PROJECT_DETAILS',
    SET_PROJECT_DETAILS_LOADING_VALUE: 'SET_PROJECT_DETAILS_LOADING_VALUE',
    SET_PROPOSAL_DETAILS: 'SET_PROPOSAL_DETAILS',
    SET_PROPOSAL_DETAILS_LOADING_VALUE: 'SET_PROPOSAL_DETAILS_LOADING_VALUE',
    SET_MESSAGES: 'SET_MESSAGES',
    CLOSE_MESSAGE: 'CLOSE_MESSAGE',
    SEND_MESSAGE: 'SEND_MESSAGE',
    RECEIVE_MESSAGE: 'RECEIVE_MESSAGE',
    RECEIVE_MESSGES_FOR_PROFILE_CHAT: 'RECEIVE_MESSGES_FOR_PROFILE_CHAT',
    CLEAR_MESSAGES_FOR_SPECIFIC_CHAT: 'CLEAR_MESSAGES_FOR_SPECIFIC_CHAT',
    CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_AUTHOR: 'CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_AUTHOR'
}

export const ActionCreators = 
{
    setLoadingValueActiveProjects: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE_ACTIVE_PROJECTS, payload}),
    setLoadingValueClosedProjects: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE_CLOSED_PROJECTS, payload}),
    setLoadingValueLiveBroadcasts: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE_LIVE_BROADCASTS, payload}),
    setAuthorActiveProjects: (payload: any) => ({type: ActionTypes.SET_AUTHOR_ACTIVE_PROJECTS, payload}),
    setAuthorClosedProjects: (payload: any) => ({type: ActionTypes.SET_AUTHOR_CLOSED_PROJECTS, payload}),
    setAuthorLiveBroadcasts: (payload: any) => ({type: ActionTypes.SET_AUTHOR_LIVE_BROADCASTS, payload}),
    addAuthorLiveBroadcasts: (payload: any) => ({type: ActionTypes.ADD_AUTHOR_LIVE_BROADCASTS, payload}),
    addAuthorActiveProjects: (payload: any) => ({type: ActionTypes.ADD_AUTHOR_ACTIVE_PROJECTS, payload}),
    addAuthorClosedProjects: (payload: any) => ({type: ActionTypes.ADD_AUTHOR_CLOSED_PROJECTS, payload}),
    setAuthorLiveBroadcastsTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT, payload}),
    setAuthorActiveProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT, payload}),
    setAuthorClosedProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT, payload}),
    addAuthorActiveProjectsTotalCount: (payload: any) => ({type: ActionTypes.ADD_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT, payload}),
    addAuthorClosedProjectsTotalCount: (payload: any) => ({type: ActionTypes.ADD_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT, payload}),
    addAuthorLiveBroadcastsTotalCount: (payload: any) => ({type: ActionTypes.ADD_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT, payload}),
    setProjectDetails: (payload: any) => ({type: ActionTypes.SET_PROJECT_DETAILS, payload}),
    setProjectDetailsLoadingValue: (payload: any) => ({type: ActionTypes.SET_PROJECT_DETAILS_LOADING_VALUE, payload}),
    setProposalDetails: (payload: any) => ({type: ActionTypes.SET_PROPOSAL_DETAILS, payload}),
    setProposalDetailsLoadingValue: (payload: any) => ({type: ActionTypes.SET_PROPOSAL_DETAILS_LOADING_VALUE, payload}),
    setMessages: (payload: any) => ({type: ActionTypes.SET_MESSAGES, payload}),
    closeMessage: () => ({type: ActionTypes.CLOSE_MESSAGE}),
    sendMessage: (payload: any) => ({ type: ActionTypes.SEND_MESSAGE, payload }),
    receiveMessage: (payload: any) => ({ type: ActionTypes.RECEIVE_MESSAGE, payload }),
    receiveMessagesForProfileChat: (payload: any) => ({ type: ActionTypes.RECEIVE_MESSGES_FOR_PROFILE_CHAT, payload }),
    clearMessagesForSpecificChat: (payload: any) => ({ type: ActionTypes.CLEAR_MESSAGES_FOR_SPECIFIC_CHAT, payload }),
    clearMessagesForSpecificChatAuthor: (payload: any) => ({ type: ActionTypes.CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_AUTHOR, payload })
}

export default function authorActiveProjectsReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_LOADING_VALUE_ACTIVE_PROJECTS:
            return {...state, loadingValueActiveProjects: action.payload}
        case ActionTypes.SET_LOADING_VALUE_CLOSED_PROJECTS:
            return {...state, loadingValueClosedProjects: action.payload}
        case ActionTypes.SET_LOADING_VALUE_LIVE_BROADCASTS:
            return {...state, loadingValueLiveBroadcasts: action.payload}
        case ActionTypes.SET_AUTHOR_ACTIVE_PROJECTS:
            return {...state, authorActiveProjects: [...action.payload]};
        case ActionTypes.SET_AUTHOR_CLOSED_PROJECTS:
            return {...state, authorClosedProjects: [...action.payload]};
        case ActionTypes.SET_AUTHOR_LIVE_BROADCASTS:
            return {...state, authorLiveBroadcasts: [...action.payload]};
        case ActionTypes.ADD_AUTHOR_LIVE_BROADCASTS:
            return { ...state, authorLiveBroadcasts: [...state.authorLiveBroadcasts, action.payload] }
        case ActionTypes.ADD_AUTHOR_ACTIVE_PROJECTS:
            return { ...state, authorActiveProjects: [...state.authorActiveProjects, action.payload] }
        case ActionTypes.ADD_AUTHOR_CLOSED_PROJECTS:
            return { ...state, authorClosedProjects: [...state.authorClosedProjects, action.payload] }
        case ActionTypes.SET_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT:
            return {...state, authorLiveBroadcastTotalCount: action.payload}
        case ActionTypes.SET_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT:
            return {...state, authorActiveProjectsTotalCount: action.payload}
        case ActionTypes.SET_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT:
            return {...state, authorClosedProjectsTotalCount: action.payload}
        case ActionTypes.ADD_AUTHOR_ACTIVE_PROJECTS_TOTALCOUNT:
            return {...state, authorActiveProjectsTotalCount: state.authorActiveProjectsTotalCount+action.payload }
        case ActionTypes.ADD_AUTHOR_CLOSED_PROJECTS_TOTALCOUNT:
            return {...state, authorClosedProjectsTotalCount: state.authorClosedProjectsTotalCount+action.payload }
        case ActionTypes.ADD_AUTHOR_LIVE_BROADCASTS_TOTALCOUNT:
            return {...state, authorLiveBroadcastTotalCount: state.authorLiveBroadcastTotalCount+action.payload }
        case ActionTypes.SET_PROJECT_DETAILS:
            return {...state, projectDetails: {...action.payload}};
        case ActionTypes.SET_PROJECT_DETAILS_LOADING_VALUE:
            return {...state, projectDetailsLoadingValue: action.payload}
        case ActionTypes.SET_PROPOSAL_DETAILS:
            return {...state, proposalDetails: {...action.payload}};
        case ActionTypes.SET_PROPOSAL_DETAILS_LOADING_VALUE:
            return {...state, proposalDetailsLoadingValue: action.payload}
        case ActionTypes.SET_MESSAGES:
            return {...state, messages: [...state.messages, action.payload]};
        case ActionTypes.CLOSE_MESSAGE:
            return {...state, messages: []};
        case ActionTypes.SEND_MESSAGE:
            return { ...state, messages: [...state.messages, action.payload] }
        case ActionTypes.RECEIVE_MESSAGE:
            return { ...state, messages: [...state.messages, action.payload] }
        case ActionTypes.RECEIVE_MESSGES_FOR_PROFILE_CHAT:
            return { ...state, messages: [...state.messages, action.payload] }
        case ActionTypes.CLEAR_MESSAGES_FOR_SPECIFIC_CHAT:
            return { ...state, messages: [...state.messages.filter(message => message.headProposalId !== action.payload)] }
        case ActionTypes.CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_AUTHOR:
            return { ...state, messages: [...state.messages.filter(message => message.bookingId === action.payload)] }
        default:
            return state;
    }
}