const initialState = {
    loadingValue: false,
    authorInformations: 
    {   id: 0,
        username: "",
        firstName: "",
        lastName: "",
        birthDate: "",
        profileIntroduction: "",
        directBooking: true,
        pricePerPage: 0,
        pagesPerDay: "",
        reviewRating: 0,
        picturePath: "",
        picture: {
            id: 0,
            mimeType: "",
            seoFilename: "",
            localPath: "",
            pictureFileName: "",
            dateCreated: ""
        },
        highestDegree: {
            id: 0,
            value: "",
            description: ""
        },
        expertiseAreas: [
        {
            id: 0,
            value: "",
            description: ""
            }],
        languages: [
            {
                id: 0,
                value: "",
                description: ""
            }],
        kindOfWorks: [
        {
            id: 0,
            value: "",
            description: ""
        }
    ]},
    authorPublicInformations: 
    {
            id: 0,
            username: "",
            directBooking: true,
            pricePerPage: 0,
            reviewRating: 0,
            picturePath: "",
            picture: {
              id: 0,
              mimeType: "",
              seoFilename: "",
              localPath: "",
              pictureFileName: "",
              dateCreated: ""
            },
            highestDegree: {
              id: 0,
              value: "",
              description: ""
            },
            expertiseAreas: [
              {
                id: 0,
                value: "",
                description: ""
              }
            ],
            kindOfWorks: [
              {
                id: 0,
                value: "",
                description: ""
              }
            ],
            languages: [
              {
                id: 0,
                value: ""
              }
            ],
            avgPricePerPage: 0,
            pagesPerDay: 0,
            profileIntroduction: "",
            description: "",
            ratings: [
              {
                id: 0,
                starRating: 0,
                comment: "",
                rateWriter: 0
              }
            ]
    },
}

export const ActionTypes =
{
    SET_AUTHOR_INFO: 'SET_AUTHOR_INFO',
    SET_AUTHOR_PUBLIC_INFO: 'SET_AUTHOR_PUBLIC_INFO',
    SET_LOADING_VALUE: 'SET_LOADING_VALUE',
}

export const ActionCreatorsForUser = 
{
    setAuthorInformations: (payload: Object) => ({type: ActionTypes.SET_AUTHOR_INFO, payload}),
    setAuthorPublicInfo: (payload: Object) => ({type: ActionTypes.SET_AUTHOR_PUBLIC_INFO, payload}),
    setLoadingValue: (payload: boolean) => ({type: ActionTypes.SET_LOADING_VALUE, payload})
}

export default function authorInformationsReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_LOADING_VALUE:
            return {...state, loadingValue: action.payload}
        case ActionTypes.SET_AUTHOR_INFO:
            return {...state, authorInformations: {...action.payload}};
        case ActionTypes.SET_AUTHOR_PUBLIC_INFO:
            return {...state, authorPublicInformations: {...action.payload}};
        default:
            return state;
    }
}