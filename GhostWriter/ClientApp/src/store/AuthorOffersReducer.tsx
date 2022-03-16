const initialState = {
    authorOffers: [
    {
        id: 0,
        deadline: "",
        plannedBudget: 0,
        maxBudget: 0,
        projectTopic: "",
        pagesNo: 0,
        customerUsername: "",
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
    authorOffersTotalCount: 1,
    loadingValue: false,
    authorBids: [
    {
        id: 0,
        deadline: "",
        plannedBudget: 0,
        maxBudget: 0,
        projectTopic: "",
        pagesNo: 0,
        customerUsername: "",
        languageDTO: {
            id: 0,
            value: ""
        },
        kindOfWorkDTO: {
            id: 0,
            value: "",
            description: ""
        },
        proposalStatus: ""
    }],
    authorBidsTotalCount: 1,
    authorsLastBids: 
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
    },
    authorsLastOffer: 
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
    },
    loadingValueBids: false,
    authorBookingChat: 
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
    authorBookingChatTotalCount: 1,
    authorBookingChatLoadingValue: false
}

export const ActionTypes =
{
    SET_AUTHOR_OFFERS: 'SET_AUTHOR_OFFERS',
    SET_AUTHOR_OFFERS_TOTAL_COUNT: 'SET_AUTHOR_OFFERS_TOTAL_COUNT',
    ACCEPT_OFFER_AUTHOR: 'ACCEPT_OFFER_AUTHOR',
    DECLINE_OFFER: 'DECLINE_OFFER',
    DECLINE_BID: 'DECLINE_BID',
    SET_LOADING_VALUE: 'SET_LOADING_VALUE',
    SET_AUTHOR_BIDS: 'SET_AUTHOR_BIDS',
    SET_AUTHOR_BIDS_TOTAL_COUNT: 'SET_AUTHOR_BIDS_TOTAL_COUNT',
    SET_AUTHORS_LAST_BIDS: 'SET_AUTHORS_LAST_BIDS',
    SET_AUTHORS_LAST_OFFER: 'SET_AUTHORS_LAST_OFFER',
    SET_LOADING_VALUE_BIDS: 'SET_LOADING_VALUE_BIDS',
    SET_AUTHOR_BOOKING_CHAT: 'SET_AUTHOR_BOOKING_CHAT',
    SET_AUTHOR_BOOKING_CHAT_LOADING_VALUE: 'SET_AUTHOR_BOOKING_CHAT_LOADING_VALUE',
    SET_AUTHOR_BOOKING_CHAT_TOTAL_COUNT: 'SET_AUTHOR_BOOKING_CHAT_TOTAL_COUNT',
    RECEIVE_AUTHOR_CHAT_OBJECT: 'RECEIVE_AUTHOR_CHAT_OBJECT',
    INCREASE_AUTHORS_CHAT_TOTAL_COUNT: 'INCREASE_AUTHORS_CHAT_TOTAL_COUNT',
    REMOVE_CHAT_OBJECT: 'REMOVE_CHAT_OBJECT',
    MODIFY_CHAT_OBJECT: 'MODIFY_CHAT_OBJECT'
}

export const ActionCreatorsForUserOffers = 
{
    setAuthorOffers: (payload: any) => ({type: ActionTypes.SET_AUTHOR_OFFERS, payload}),
    setAuthorBids: (payload: any) => ({type: ActionTypes.SET_AUTHOR_BIDS, payload}),
    setAuthorsLastBids: (payload: any) => ({type: ActionTypes.SET_AUTHORS_LAST_BIDS, payload}),
    setAuthorsLastOffer: (payload: any) => ({type: ActionTypes.SET_AUTHORS_LAST_OFFER, payload}),
    setAuthorOffersTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHOR_OFFERS_TOTAL_COUNT, payload}),
    setAuthorBidsTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHOR_BIDS_TOTAL_COUNT, payload}),
    acceptOfferAuthor: (payload: any) => ({type: ActionTypes.ACCEPT_OFFER_AUTHOR, payload}),
    declineOffer: (payload: any) => ({type: ActionTypes.DECLINE_OFFER, payload}),
    declineBid: (payload: any) => ({type: ActionTypes.DECLINE_BID, payload}),
    setLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE, payload}),
    setLoadingValueBids: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE_BIDS, payload}),
    setAuthorBookingChat: (payload: any) => ({type: ActionTypes.SET_AUTHOR_BOOKING_CHAT, payload}),
    setAuthorBookingChatLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_AUTHOR_BOOKING_CHAT_LOADING_VALUE, payload}),
    setAuthorBookingChatTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHOR_BOOKING_CHAT_TOTAL_COUNT, payload}),
    receiveAuthorsChatObject: (payload: any) => ({type: ActionTypes.RECEIVE_AUTHOR_CHAT_OBJECT, payload}),
    removeChatObject: (payload: any) => ({type: ActionTypes.REMOVE_CHAT_OBJECT, payload}),
    modifyChatObject: (payload: any) => ({type: ActionTypes.MODIFY_CHAT_OBJECT, payload})
}

export default function authorOffersReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_AUTHOR_OFFERS:
            return {...state, authorOffers: [...action.payload]};
        case ActionTypes.SET_AUTHOR_BIDS:
            return {...state, authorBids: [...action.payload]};
        case ActionTypes.SET_AUTHORS_LAST_BIDS:
            return {...state, authorsLastBids: {...action.payload}};
        case ActionTypes.SET_AUTHORS_LAST_OFFER:
            return {...state, authorsLastOffer: {...action.payload}};
        case ActionTypes.ACCEPT_OFFER_AUTHOR:
            return {...state, authorOffers: state.authorOffers.filter(item => item.id !== action.payload)};
        case ActionTypes.DECLINE_OFFER:
            return {...state, authorOffers: state.authorOffers.filter(item => item.id !== action.payload)};
        case ActionTypes.DECLINE_BID:
            return {...state, authorBids: state.authorBids.filter(item => item.id !== action.payload)};
        case ActionTypes.SET_LOADING_VALUE:
            return {...state, loadingValue: action.payload}
        case ActionTypes.SET_LOADING_VALUE_BIDS:
            return {...state, loadingValueBids: action.payload}
        case ActionTypes.SET_AUTHOR_BIDS_TOTAL_COUNT:
            return {...state, authorBidsTotalCount: action.payload}
        case ActionTypes.SET_AUTHOR_OFFERS_TOTAL_COUNT:
            return {...state, authorOffersTotalCount: action.payload}
        case ActionTypes.SET_AUTHOR_BOOKING_CHAT:
            return {...state, authorBookingChat: [...action.payload]};
        case ActionTypes.SET_AUTHOR_BOOKING_CHAT_LOADING_VALUE:
            return {...state, authorBookingChatLoadingValue: action.payload}
        case ActionTypes.SET_AUTHOR_BOOKING_CHAT_TOTAL_COUNT:
            return {...state, authorBookingChatTotalCount: action.payload}
        case ActionTypes.RECEIVE_AUTHOR_CHAT_OBJECT:
            return {...state, authorBookingChatTotalCount: state.authorBookingChatTotalCount + 1, authorBookingChat: [action.payload, ...state.authorBookingChat]}
        case ActionTypes.REMOVE_CHAT_OBJECT:
            return {...state, authorBookingChatTotalCount: state.authorBookingChatTotalCount - 1, authorBookingChat: state.authorBookingChat.filter(item => item.bookingId !== action.payload.bookingId)};
        case ActionTypes.MODIFY_CHAT_OBJECT:
            return {...state, authorBookingChatTotalCount: state.authorBookingChatTotalCount + 1, authorBookingChat: [action.payload, ...state.authorBookingChat]};
        default:
            return state;
    }
}