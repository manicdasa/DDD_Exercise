const initialState = {
    requestBookingInputs: {}
}

export const ActionTypes =
{
    SET_INPUTS_REQUEST_BOOKING: 'SET_INPUTS_REQUEST_BOOKING'
}

export const ActionCreatorsForCreateProject = 
{
    setInputsRequestBooking: (payload: Object) => ({type: ActionTypes.SET_INPUTS_REQUEST_BOOKING, payload})
}

export default function requestBookingReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_INPUTS_REQUEST_BOOKING:
            return {...state, requestBookingInputs: {...action.payload}};
        default:
            return state;
    }
}