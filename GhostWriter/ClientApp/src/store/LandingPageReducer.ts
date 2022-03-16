const initialState = {
    value: false
}

export const ActionTypes =
{
    SET_VALUE: 'SET_VALUE'
}

export const ActionCreators = 
{
    setValue: (payload: boolean) => ({type: ActionTypes.SET_VALUE, payload})
}

export default function landingPageReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_VALUE:
            return {...state, value: action.payload};
        default:
            return state;
    }
}