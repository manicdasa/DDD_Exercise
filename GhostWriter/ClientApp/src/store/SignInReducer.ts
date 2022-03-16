const initialState = {
    loggedIn: false
}

export const ActionTypes =
{
    SET_LOGGED_IN: 'SET_LOGGED_IN'
}

export const ActionCreators = 
{
    setLoggedIn: (payload: Object) => ({type: ActionTypes.SET_LOGGED_IN, payload}),
}

export default function signInReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_LOGGED_IN:
            return {...state, loggedIn: action.payload}
        default:
            return state;
    }
}