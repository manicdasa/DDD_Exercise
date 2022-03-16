const initialState = {
    customerInformations: 
    { email: "",
      id: 0,
      jobsPostedCnt: 0,
      noActiveBids: 0,
      pagesNoInProgress: 0,
      pagesWrittenSoFar: 0,
      paymentVerified: true,
      totalSpent: 0,
      username: ""
    },
    customerOffers: [
        {
          id: 0,
          dateCreated: "",
          financialOffer: 0,
          financialOfferWithCharges: 0,
          deadline: "",
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
          },
          expertiseAreaListDTOs: [
            {
              id: 0,
              value: "",
              description: ""
            }
          ]
        }],
    customerOffersTotalCount: 1,
    customerActiveProjects: [
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
    }],
    customerOpenProjects: [
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
    }],
    customerOpenProjectsTotalCount: 1,
    loadingValueOpenProjects: false,
    customerActiveProjectsTotalCount: 1,
    customerClosedProjects: [
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
    }],
    customerClosedProjectsTotalCount: 1,
    loadingValue: false,
    loadingValueActiveProjects: false,
    loadingValueClosedProjects: false,
    projectDetailsCustomer: {
        id: 0,
        deadline: "",
        totalPrice: 0,
        totalServiceCharges: 0,
        paymentVerified: true,
        projectTopic: "",
        projectDescription: "",
        pagesNo: 0,
        ratingDTO: {},
        customerDTO: {
          id: 0,
          totalSpent: 0,
          jobsPostedCnt: 0,
          username: ""
        },
        bookingStatus: {
          id: 0,
          value: ""
        },
        authorUsername: "",
        authorId: "",
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
            dateCreated: ""
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
    projectDetailsLoadingValueCustomer: false,        
    proposalDetailsCustomer: {
        id: 0,
        deadline: "",
        totalPrice: 0,
        totalServiceCharges: 0,
        paymentVerified: true,
        projectTopic: "",
        projectDescription: "",
        isPublished: true,
        pagesNo: 0,
        projectStatus: 1,
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
            dateCreated: ""
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
    proposalDetailsLoadingValueCustomer: false, 
    messagesCustomer: 
    [
        {
            id: 0,
            username: "",
            bookingId: 0,
            headProposalId: 0,
            myMessage: true,
            messageText: "",
            dateTimeSent: ""
        }
    ],
    customerBookingChat: 
    [
      {
        bookingId: 0,
        authorUsername: "",
        authorId: 0,
        customerUsername: "",
        bookingStatus: "",
        projectTopic: "",
        lastMessageContent: ""
      }
    ],
    customerBookingChatTotalCount: 1,
    customerBookingChatLoadingValue: false,
    customerMyOffers: [
        {
          id: 0,
          dateCreated: "",
          financialOffer: 0,
          financialOfferWithCharges: 0,
          deadline: "",
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
          },
          expertiseAreaListDTOs: [
            {
              id: 0,
              value: "",
              description: ""
            }
          ]
        }],
    customerMyOffersTotalCount: 1,
    customerMyOffersLoadingValue: false,
    customersProjectBids: 
    [
      {
        id: 0,
        dateCreated: "",
        financialOffer: 0,
        financialOfferWithCharges: 0,
        deadline: "",
        projectTopic: "",
        pagesNo: 0,
        customerUsername: "",
        authorUsername: "",
        authorId: "",
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
        ]
      }
    ]
}


export const ActionTypes =
{
    SET_CUSTOMER_INFO: 'SET_CUSTOMER_INFO',
    SET_CUSTOMER_OFFERS: 'SET_CUSTOMER_OFFERS',
    SET_CUSTOMER_ACTIVE_PROJECTS: 'SET_CUSTOMER_ACTIVE_PROJECTS',
    SET_CUSTOMER_OPEN_PROJECTS: 'SET_CUSTOMER_OPEN_PROJECTS',
    SET_CUSTOMER_OPEN_PROJECTS_TOTALCOUNT: 'SET_CUSTOMER_OPEN_PROJECTS_TOTALCOUNT',
    SET_CUSTOMER_OPEN_PROJECTS_LOADING_VALUE: 'SET_CUSTOMER_OPEN_PROJECTS_LOADING_VALUE',
    SET_CUSTOMER_CLOSED_PROJECTS: 'SET_CUSTOMER_CLOSED_PROJECTS',
    SET_LOADING_VALUE: 'SET_LOADING_VALUE',
    SET_LOADING_VALUE_ACTIVE_PROJECTS: 'SET_LOADING_VALUE_ACTIVE_PROJECTS',
    SET_LOADING_VALUE_CLOSED_PROJECTS: 'SET_LOADING_VALUE_CLOSED_PROJECTS',
    ADD_CUSTOMER_ACTIVE_PROJECTS: 'ADD_CUSTOMER_ACTIVE_PROJECTS',
    ADD_CUSTOMER_OPEN_PROJECTS: 'ADD_CUSTOMER_OPEN_PROJECTS',
    ADD_CUSTOMER_CLOSED_PROJECTS: 'ADD_CUSTOMER_CLOSED_PROJECTS',
    SET_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT: 'SET_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT',
    SET_CUSTOMER_OFFERS_TOTALCOUNT: 'SET_CUSTOMER_OFFERS_TOTALCOUNT',
    SET_CUSTOMER_CLOSED_PROJECTS_TOTALCOUNT: 'SET_CUSTOMER_CLOSED_PROJECTS_TOTALCOUNT',
    ACCEPT_OFFER_CUSTOMER: 'ACCEPT_OFFER_CUSTOMER',
    DECLINE_OFFER_CUSTOMER: 'DECLINE_OFFER_CUSTOMER',
    ADD_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT: 'ADD_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT',
    SET_PROJECT_DETAILS_CUSTOMER: 'SET_PROJECT_DETAILS_CUSTOMER',
    SET_PROJECT_DETAILS_LOADING_VALUE_CUSTOMER: 'SET_PROJECT_DETAILS_LOADING_VALUE_CUSTOMER',    
    SET_PROPOSAL_DETAILS_CUSTOMER: 'SET_PROPOSAL_DETAILS_CUSTOMER',
    SET_PROPOSAL_DETAILS_LOADING_VALUE_CUSTOMER: 'SET_PROPOSAL_DETAILS_LOADING_VALUE_CUSTOMER',
    SET_MESSAGES_CUSTOMER: 'SET_MESSAGES_CUSTOMER',
    CLOSE_MESSAGE_CUSTOMER: 'CLOSE_MESSAGE_CUSTOMER',
    SEND_MESSAGE_CUSTOMER: 'SEND_MESSAGE_CUSTOMER',
    SET_CUSTOMER_BOOKING_CHAT: 'SET_CUSTOMER_BOOKING_CHAT',
    SET_CUSTOMER_BOOKING_CHAT_LOADING_VALUE: 'SET_CUSTOMER_BOOKING_CHAT_LOADING_VALUE',
    SET_CUSTOMER_BOOKING_CHAT_TOTAL_COUNT: 'SET_CUSTOMER_BOOKING_CHAT_TOTAL_COUNT',
    SET_CUSTOMER_MY_OFFERS: 'SET_CUSTOMER_MY_OFFERS',
    SET_CUSTOMER_MY_OFFERS_TOTAL_COUNT: 'SET_CUSTOMER_MY_OFFERS_TOTAL_COUNT',
    SET_CUSTOMER_MY_OFFERS_LOADING_VALUE: 'SET_CUSTOMER_MY_OFFERS_LOADING_VALUE',
    CANCEL_OFFER_CUSTOMER: 'CANCEL_OFFER_CUSTOMER',
    SET_CUSTOMERS_PROJECT_BIDS: 'SET_CUSTOMERS_PROJECT_BIDS',
    RECEIVE_MESSAGE: 'RECEIVE_MESSAGE',
    RECEIVE_CHAT_OBJECT: 'RECEIVE_CHAT_OBJECT',
    REMOVE_CUSTOMER_CHAT_OBJECT: 'REMOVE_CHAT_OBJECT',
    MODIFY_CUSTOMER_CHAT_OBJECT: 'MODIFY_CHAT_OBJECT',
    CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_CUSTOMER: 'CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_CUSTOMER'
}

export const ActionCreators = 
{
    setCustomerInformations: (payload: Object) => ({type: ActionTypes.SET_CUSTOMER_INFO, payload}),
    setCustomerOffers: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_OFFERS, payload}),
    setCustomerActiveProjects: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_ACTIVE_PROJECTS, payload}),
    setCustomerOpenProjects: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_OPEN_PROJECTS, payload}),
    setCustomerOpenProjectsLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_CUSTOMER_OPEN_PROJECTS_LOADING_VALUE, payload}),
    setCustomerOpenProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_OPEN_PROJECTS_TOTALCOUNT, payload}),
    setCustomerClosedProjects: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_CLOSED_PROJECTS, payload}),
    setLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE, payload}),
    setLoadingValueActiveProjects: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE_ACTIVE_PROJECTS, payload}),
    setLoadingValueClosedProjects: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE_CLOSED_PROJECTS, payload}),
    addCustomerActiveProjects: (payload: any) => ({type: ActionTypes.ADD_CUSTOMER_ACTIVE_PROJECTS, payload}),
    addCustomerClosedProjects: (payload: any) => ({type: ActionTypes.ADD_CUSTOMER_CLOSED_PROJECTS, payload}),
    addCustomerOpenProjects: (payload: any) => ({type: ActionTypes.ADD_CUSTOMER_OPEN_PROJECTS, payload}),
    setCustomerOffersTotalCount: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_OFFERS_TOTALCOUNT, payload}),
    setCustomerActiveProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT, payload}),
    setCustomerClosedProjectsTotalCount: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_CLOSED_PROJECTS_TOTALCOUNT, payload}),
    acceptOfferCustomer: (payload: any) => ({type: ActionTypes.ACCEPT_OFFER_CUSTOMER, payload}),
    declineOfferCustomer: (payload: any) => ({type: ActionTypes.DECLINE_OFFER_CUSTOMER, payload}),
    addCustomerActiveProjectsTotalCount: (payload: any) => ({type: ActionTypes.ADD_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT, payload}),
    setProjectDetailsCustomer: (payload: any) => ({type: ActionTypes.SET_PROJECT_DETAILS_CUSTOMER, payload}),
    setProjectDetailsLoadingValueCustomer: (payload: any) => ({type: ActionTypes.SET_PROJECT_DETAILS_LOADING_VALUE_CUSTOMER, payload}),
    setProposalDetailsCustomer: (payload: any) => ({type: ActionTypes.SET_PROPOSAL_DETAILS_CUSTOMER, payload}),
    setProposalDetailsCustomerLoadingValue: (payload: any) => ({type: ActionTypes.SET_PROPOSAL_DETAILS_LOADING_VALUE_CUSTOMER, payload}),
    setMessagesCustomer: (payload: any) => ({type: ActionTypes.SET_MESSAGES_CUSTOMER, payload}),
    closeMessageCustomer: () => ({type: ActionTypes.CLOSE_MESSAGE_CUSTOMER}),
    sendMessageCustomer: (payload: any) => ({type: ActionTypes.SEND_MESSAGE_CUSTOMER, payload}),
    setCustomerBookingChat: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_BOOKING_CHAT, payload}),
    setCustomerBookingChatLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_CUSTOMER_BOOKING_CHAT_LOADING_VALUE, payload}),
    setCustomerBookingChatTotalCount: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_BOOKING_CHAT_TOTAL_COUNT, payload}),
    setCustomerMyOffers: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_MY_OFFERS, payload}),
    setCustomerMyOffersLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE, payload}),
    setCustomerMyOffersTotalCount: (payload: any) => ({type: ActionTypes.SET_CUSTOMER_MY_OFFERS_TOTAL_COUNT, payload}),
    cancelOfferCustomer: (payload: any) => ({type: ActionTypes.CANCEL_OFFER_CUSTOMER, payload}),
    setCustomersProjectBids: (payload: any) => ({ type: ActionTypes.SET_CUSTOMERS_PROJECT_BIDS, payload }),
    receiveMessage: (payload: any) => ({ type: ActionTypes.RECEIVE_MESSAGE, payload }),
    receiveChatObject: (payload: any) => ({type: ActionTypes.RECEIVE_CHAT_OBJECT, payload}),
    removeCustomerChatObject: (payload: any) => ({type: ActionTypes.REMOVE_CUSTOMER_CHAT_OBJECT, payload}),
    modifyCustomerChatObject: (payload: any) => ({type: ActionTypes.MODIFY_CUSTOMER_CHAT_OBJECT, payload}),
    clearMessagesForChatCustomer: (payload: any) => ({ type: ActionTypes.CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_CUSTOMER, payload })
}

export default function customerReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_CUSTOMER_INFO:
            return {...state, customerInformations: {...action.payload}};
        case ActionTypes.SET_CUSTOMER_OFFERS:
            return {...state, customerOffers: [...action.payload]};
        case ActionTypes.SET_CUSTOMER_ACTIVE_PROJECTS:
            return {...state, customerActiveProjects: [...action.payload]};
        case ActionTypes.SET_CUSTOMER_OPEN_PROJECTS:
            return {...state, customerOpenProjects: [...action.payload]};
        case ActionTypes.SET_CUSTOMER_OPEN_PROJECTS_TOTALCOUNT:
            return {...state, customerOpenProjectsTotalCount: action.payload}
        case ActionTypes.SET_CUSTOMER_OPEN_PROJECTS_LOADING_VALUE:
            return {...state, loadingValueOpenProjects: action.payload}
        case ActionTypes.SET_CUSTOMER_CLOSED_PROJECTS:
            return {...state, customerClosedProjects: [...action.payload]};
        case ActionTypes.SET_LOADING_VALUE:
            return {...state, loadingValue: action.payload}
        case ActionTypes.SET_LOADING_VALUE_ACTIVE_PROJECTS:
            return {...state, loadingValueActiveProjects: action.payload}
        case ActionTypes.SET_LOADING_VALUE_CLOSED_PROJECTS:
            return {...state, loadingValueClosedProjects: action.payload}
        case ActionTypes.ADD_CUSTOMER_ACTIVE_PROJECTS:
            return { ...state, customerActiveProjects: [...state.customerActiveProjects, action.payload] }
        case ActionTypes.ADD_CUSTOMER_OPEN_PROJECTS:
            return { ...state, customerOpenProjects: [...state.customerOpenProjects, action.payload] }
        case ActionTypes.ADD_CUSTOMER_CLOSED_PROJECTS:
            return { ...state, customerClosedProjects: [...state.customerClosedProjects, action.payload] }
        case ActionTypes.SET_CUSTOMER_OFFERS_TOTALCOUNT:
            return {...state, customerOffersTotalCount: action.payload}
        case ActionTypes.SET_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT:
            return {...state, customerActiveProjectsTotalCount: action.payload}
        case ActionTypes.SET_CUSTOMER_CLOSED_PROJECTS_TOTALCOUNT:
            return {...state, customerClosedProjectsTotalCount: action.payload}   
        case ActionTypes.ACCEPT_OFFER_CUSTOMER:
            return {...state, customerOffers: state.customerOffers.filter(item => item.id !== action.payload)};
        case ActionTypes.DECLINE_OFFER_CUSTOMER:
            return {...state, customerOffers: state.customerOffers.filter(item => item.id !== action.payload)};
        case ActionTypes.ADD_CUSTOMER_ACTIVE_PROJECTS_TOTALCOUNT:
            return {...state, customerActiveProjectsTotalCount: state.customerActiveProjectsTotalCount+action.payload }
        case ActionTypes.SET_PROJECT_DETAILS_CUSTOMER:
            return {...state, projectDetailsCustomer: {...action.payload}};
        case ActionTypes.SET_PROJECT_DETAILS_LOADING_VALUE_CUSTOMER:
            return {...state, projectDetailsLoadingValueCustomer: action.payload}
        case ActionTypes.SET_PROPOSAL_DETAILS_CUSTOMER:
            return {...state, proposalDetailsCustomer: {...action.payload}};
        case ActionTypes.SET_PROPOSAL_DETAILS_LOADING_VALUE_CUSTOMER:
            return {...state, proposalDetailsLoadingValueCustomer: action.payload}
        case ActionTypes.SET_MESSAGES_CUSTOMER:
            return {...state, messagesCustomer: [...state.messagesCustomer, action.payload]};
        case ActionTypes.CLEAR_MESSAGES_FOR_SPECIFIC_CHAT_CUSTOMER:
            return { ...state, messagesCustomer: [...state.messagesCustomer.filter(message => message.bookingId === action.payload)] }
        case ActionTypes.CLOSE_MESSAGE_CUSTOMER:
            return {...state, messagesCustomer: []};
        case ActionTypes.SEND_MESSAGE_CUSTOMER:
            return { ...state, messagesCustomer: [...state.messagesCustomer, action.payload] }
        case ActionTypes.SET_CUSTOMER_BOOKING_CHAT:
            return {...state, customerBookingChat: [...action.payload]};
        case ActionTypes.SET_CUSTOMER_BOOKING_CHAT_LOADING_VALUE:
            return {...state, customerBookingChatLoadingValue: action.payload}
        case ActionTypes.SET_CUSTOMER_BOOKING_CHAT_TOTAL_COUNT:
            return {...state, customerBookingChatTotalCount: action.payload}
        case ActionTypes.SET_CUSTOMER_MY_OFFERS:
            return {...state, customerMyOffers: [...action.payload]};
        case ActionTypes.SET_CUSTOMER_MY_OFFERS_TOTAL_COUNT:
            return {...state, customerMyOffersTotalCount: action.payload}
        case ActionTypes.SET_CUSTOMER_MY_OFFERS_LOADING_VALUE:
            return {...state, customerMyOffersLoadingValue: action.payload}
        case ActionTypes.CANCEL_OFFER_CUSTOMER:
            return {...state, customerMyOffers: state.customerMyOffers.filter(item => item.id !== action.payload)};
        case ActionTypes.SET_CUSTOMERS_PROJECT_BIDS:
            return { ...state, customersProjectBids: [...action.payload] };
        case ActionTypes.RECEIVE_MESSAGE:
            return { ...state, messagesCustomer: [...state.messagesCustomer, action.payload] }
        case ActionTypes.RECEIVE_CHAT_OBJECT:
            return {...state, customerBookingChatTotalCount: state.customerBookingChatTotalCount + 1, customerBookingChat: [action.payload, ...state.customerBookingChat]}
        case ActionTypes.REMOVE_CUSTOMER_CHAT_OBJECT:
            return {...state, customerBookingChatTotalCount: state.customerBookingChatTotalCount - 1, customerBookingChat: state.customerBookingChat.filter(item => item.bookingId !== action.payload.bookingId)};
        case ActionTypes.MODIFY_CUSTOMER_CHAT_OBJECT:
            return {...state, customerBookingChatTotalCount: state.customerBookingChatTotalCount + 1, customerBookingChat: [action.payload, ...state.customerBookingChat]};
        default:
            return state;
    }
}