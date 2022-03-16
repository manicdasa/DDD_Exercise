const initialState = {
    connection: false
}


export const ActionTypes =
{
    SET_CONNECTION: 'SET_CONNECTION'
}

export const ActionCreatorsForConnection = 
{
    setConnection: (payload: boolean) => ({type: ActionTypes.SET_CONNECTION, payload})
}


export default function connectionReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_CONNECTION:
            return {...state, connection: action.payload}
        default:
            return state;
    }
}