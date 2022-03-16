const initialState = {
    authorParams: {},
    loadingValue: false
}

export const ActionTypes =
{
    SET_AUTHOR_PARAMS: 'SET_AUTHOR_PARAMS',
    SET_LOADING_VALUE: 'SET_LOADING_VALUE'
}

export const ActionCreators = 
{
    setAuthorParams: (payload: Object) => ({type: ActionTypes.SET_AUTHOR_PARAMS, payload}),
    setLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE, payload})
}

export default function searchAuthorReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_AUTHOR_PARAMS:
            return {...state, authorParams: {...action.payload}};
        case ActionTypes.SET_LOADING_VALUE:
            return {...state, loadingValue: action.payload}
        default:
            return state;
    }
}