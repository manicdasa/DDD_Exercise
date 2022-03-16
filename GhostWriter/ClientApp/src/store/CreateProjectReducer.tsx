const initialState = {
    createProjectInputs: 
    {
        deadline: '',
        pricePerPage: 0,
        projectTopic: '',
        description: '',
        pagesNo: 0,
        minimumDegreeId: '',
        languageId: '',
        kindOfWorkId: '',
        expertiseAreaIds: [
        ]
    }
}

export const ActionTypes =
{
    SET_INPUTS_CREATE_PROJECT: 'SET_INPUTS_CREATE_PROJECT'
}

export const ActionCreatorsForCreateProject = 
{
    setInputsCreateProject: (payload: Object) => ({type: ActionTypes.SET_INPUTS_CREATE_PROJECT, payload})
}

export default function requestBookingReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_INPUTS_CREATE_PROJECT:
            return {...state, createProjectInputs: {...action.payload}};
        default:
            return state;
    }
}