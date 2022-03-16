const initialState = {
    notifications: [{}],
    notificationsLength: 0,
    activeProjectsLength: 0,
    newOffersLength: 0,
    myBidsLength: 0,
    liveBroadcastsLength: 0
}

export const ActionTypes = 
{
    GET_NOTIFICATIONS: 'GET_NOTIFICATIONS',
    SET_NOTIFICATIONS_LENGTH: 'SET_NOTIFICATIONS_LENGTH',
    REDUCE_NOTIFICATIONS_LENGTH: 'REDUCE_NOTIFICATIONS_LENGTH',
    RECEIVE_NOTIFICATION: 'RECEIVE_NOTIFICATION',
    SET_ACTIVE_PROJECTS_LENGTH: 'SET_ACTIVE_PROJECTS_LENGTH',
    SET_NEW_OFFERS_LENGTH: 'SET_NEW_OFFERS_LENGTH',
    SET_MY_BIDS_LENGTH: 'SET_MY_BIDS_LENGTH',
    SET_LIVE_BROADCAST_LENGTH: 'SET_LIVE_BROADCAST_LENGTH'
}

export const ActionCreatorsForNotifications = 
{
    getNotifications: () => ({ type: ActionTypes.GET_NOTIFICATIONS }),
    setNotificationsLength: (payload: any) => ({type: ActionTypes.SET_NOTIFICATIONS_LENGTH, payload}),
    reduceNotificationsLength: (payload: any) => ({type: ActionTypes.REDUCE_NOTIFICATIONS_LENGTH, payload}),
    receiveNotifications: (payload: any) => ({type: ActionTypes.RECEIVE_NOTIFICATION, payload}),
    setNumberOfActiveProjects: (payload: any) => ({ type: ActionTypes.SET_ACTIVE_PROJECTS_LENGTH, payload}),
    setNumberOfNewOffers: (payload: any) => ({ type: ActionTypes.SET_NEW_OFFERS_LENGTH, payload}),
    setNumberOfMyBids: (payload: any) => ({ type: ActionTypes.SET_MY_BIDS_LENGTH, payload }),
    setNumberOfLiveBroadcast: (payload: any) => ({ type: ActionTypes.SET_LIVE_BROADCAST_LENGTH, payload })
}

export default function notificationsReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.GET_NOTIFICATIONS:
            return {...state, notifications: []};
        case ActionTypes.SET_NOTIFICATIONS_LENGTH:
            return { ...state, notificationsLength: action.payload }
        case ActionTypes.RECEIVE_NOTIFICATION:
            return { ...state, notifications: [...state.notifications, action.payload] }
        case ActionTypes.SET_ACTIVE_PROJECTS_LENGTH:
            return { ...state, activeProjectsLength: action.payload }
        case ActionTypes.SET_NEW_OFFERS_LENGTH:
            return { ...state, newOffersLength: action.payload }
        case ActionTypes.SET_MY_BIDS_LENGTH:
            return { ...state, myBidsLength: action.payload }
        case ActionTypes.SET_LIVE_BROADCAST_LENGTH:
            return { ...state, liveBroadcastsLength: action.payload }
        case ActionTypes.REDUCE_NOTIFICATIONS_LENGTH:
            return { ...state, notificationsLength: state.notificationsLength - action.payload }
        default:
            return state;
    }
}