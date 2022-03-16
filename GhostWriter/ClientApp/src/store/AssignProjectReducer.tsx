const initialState = {
    assignProjectParams: 
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
    },
}

export const ActionTypes = 
{
    SET_ASSIGN_PARAMS: 'SET_ASSIGN_PARAMS',
}

export const ActionCreatorsForAssign = 
{
    setAssignParams: (payload: any) => ({type: ActionTypes.SET_ASSIGN_PARAMS, payload})
}

export default function assignProjectReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_ASSIGN_PARAMS:
            return {...state, assignProjectParams: action.payload};
        default:
            return state;
    }
}