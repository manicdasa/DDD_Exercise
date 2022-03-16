const initialState = {
    authorsOffers: 
    [
        {
          id: 0,
          headProposalId: 0,
          dateCreated: "",
          financialOffer: 0,
          serviceCharges: 0,
          deadline: "",
          projectId: 0,
          projectTopic: "",
          pagesNo: 0,
          customerUsername: "",
          authorUsername: "",
          authorId: "",
          languageDTO: 
          {
            id: 0,
            value: ""
          },
          kindOfWorkDTO: {
            id: 0,
            value: "",
            description: "",
            fieldStatusDTO: {
              id: 0,
              value: ""
            }
          },
          expertiseAreaListDTOs: [
            {
              id: 0,
              value: "",
              description: "",
              fieldStatusDTO: {
                id: 0,
                value: ""
              }
            }
          ]
        }],
    authorsOffersTotalCount: 0,
    authorsMyBids: 
    [
        {
          id: 0,
          headProposalId: 0,
          dateCreated: "",
          financialOffer: 0,
          serviceCharges: 0,
          deadline: "",
          projectId: 0,
          projectTopic: "",
          pagesNo: 0,
          customerUsername: "",
          authorUsername: "",
          authorId: "",
          languageDTO: 
          {
            id: 0,
            value: ""
          },
          kindOfWorkDTO: {
            id: 0,
            value: "",
            description: "",
            fieldStatusDTO: {
              id: 0,
              value: ""
            }
          },
          expertiseAreaListDTOs: [
            {
              id: 0,
              value: "",
              description: "",
              fieldStatusDTO: {
                id: 0,
                value: ""
              }
            }
          ]
        }
    ],
    authorsMyBidsTotalCount: 0
}

export const ActionTypes = 
{
    SET_AUTHORS_OFFERS: 'SET_AUTHORS_OFFERS',
    RECEIVE_AUTHOR_OFFER: 'RECEIVE_AUTHOR_OFFER',
    REMOVE_OFFER: 'REMOVE_OFFER',
    SET_AUTHORS_OFFERS_TOTAL_COUNT: 'SET_AUTHORS_OFFERS_TOTAL_COUNT',
    INCREASE_TOTAL_COUNT_AUTHORS_OFFERS: 'INCREASE_TOTAL_COUNT_AUTHORS_OFFERS',
    MODIFY_AUTHORS_OFFERS: 'MODIFY_AUTHORS_OFFERS',
    CHANGE_NEGOTIATE_NEW_STATE: 'CHANGE_NEGOTIATE_NEW_STATE',

    SET_AUTHORS_MY_BIDS: 'SET_AUTHORS_MY_BIDS',
    RECEIVE_AUTHORS_MY_BIDS: 'RECEIVE_AUTHORS_MY_BIDS',
    REMOVE_BID: 'REMOVE_BID',
    SET_AUTHORS_MY_BIDS_TOTAL_COUNT: 'SET_AUTHORS_MY_BIDS_TOTAL_COUNT',
    INCREASE_TOTAL_COUNT_AUTHORS_MY_BIDS: 'INCREASE_TOTAL_COUNT_AUTHORS_MY_BIDS',
    MODIFY_AUTHORS_BIDS: 'MODIFY_AUTHORS_BIDS',
    CHANGE_NEGOTIATE_NEW_STATE_BID: 'CHANGE_NEGOTIATE_NEW_STATE_BID'

}

export const ActionCreatorsForSidePanel = 
{
    setAuthorsOffers: (payload: any) => ({type: ActionTypes.SET_AUTHORS_OFFERS, payload}),
    receiveAuthorOffer: (payload: any) => ({type: ActionTypes.RECEIVE_AUTHOR_OFFER, payload}),
    removeOffer: (payload: any) => ({type: ActionTypes.REMOVE_OFFER, payload}),
    setAuthorsOffersTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHORS_OFFERS_TOTAL_COUNT, payload }),
    increaseTotalCountAuthorsOffers: () => ({type: ActionTypes.INCREASE_TOTAL_COUNT_AUTHORS_OFFERS}),
    modifyAuthorsOffers: (payload: any) => ({type: ActionTypes.MODIFY_AUTHORS_OFFERS, payload }),
    changeNegotiateNewState: (payload: any) => ({type: ActionTypes.CHANGE_NEGOTIATE_NEW_STATE, payload}),

    setAuthorsMyBids: (payload: any) => ({type: ActionTypes.SET_AUTHORS_MY_BIDS, payload}),
    receiveAuthorsMyBids: (payload: any) => ({type: ActionTypes.RECEIVE_AUTHORS_MY_BIDS, payload}),
    removeBid: (payload: any) => ({type: ActionTypes.REMOVE_BID, payload}),
    setAuthorsMyBidsTotalCount: (payload: any) => ({type: ActionTypes.SET_AUTHORS_MY_BIDS_TOTAL_COUNT, payload }),
    increaseTotalCountAuthorsMyBids: () => ({type: ActionTypes.INCREASE_TOTAL_COUNT_AUTHORS_MY_BIDS}),
    modifyAuthorsBids: (payload: any) => ({type: ActionTypes.MODIFY_AUTHORS_BIDS, payload }),
    changeNegotiateNewStateBid: (payload: any) => ({type: ActionTypes.CHANGE_NEGOTIATE_NEW_STATE_BID, payload})
}

export default function sidePanelReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.SET_AUTHORS_OFFERS:
            return {...state, authorsOffers: [...action.payload]};
        case ActionTypes.RECEIVE_AUTHOR_OFFER:
            return { ...state, authorsOffersTotalCount: state.authorsOffersTotalCount + 1, authorsOffers: [action.payload, ...state.authorsOffers] }
        case ActionTypes.REMOVE_OFFER:
            return {...state, authorsOffersTotalCount: state.authorsOffersTotalCount - 1, authorsOffers: state.authorsOffers.filter(item => item.id !== action.payload.id)};
        case ActionTypes.SET_AUTHORS_OFFERS_TOTAL_COUNT:
            return { ...state, authorsOffersTotalCount: action.payload }
        case ActionTypes.INCREASE_TOTAL_COUNT_AUTHORS_OFFERS:
            return { ...state, authorsOffersTotalCount: state.authorsOffersTotalCount + 1}
        case ActionTypes.MODIFY_AUTHORS_OFFERS:
            return {...state, authorsOffersTotalCount: state.authorsOffersTotalCount + 1, authorsOffers: [action.payload, ...state.authorsOffers]};
        case ActionTypes.CHANGE_NEGOTIATE_NEW_STATE:
          return { ...state, authorsOffers: state.authorsOffers.map((content) => content.projectId === action.payload.projectId ? {...content, isNewReceiveOffer: false} : content)}

        case ActionTypes.SET_AUTHORS_MY_BIDS:
            return {...state, authorsMyBids: [...action.payload]};
        case ActionTypes.RECEIVE_AUTHORS_MY_BIDS:
            return { ...state, authorsMyBidsTotalCount: state.authorsMyBidsTotalCount + 1, authorsMyBids: [action.payload, ...state.authorsMyBids] }
        case ActionTypes.REMOVE_BID:
            return {...state, authorsMyBidsTotalCount: state.authorsMyBidsTotalCount - 1, authorsMyBids: state.authorsMyBids.filter(item => item.id !== action.payload.id)};
        case ActionTypes.SET_AUTHORS_MY_BIDS_TOTAL_COUNT:
            return { ...state, authorsMyBidsTotalCount: action.payload }
        case ActionTypes.INCREASE_TOTAL_COUNT_AUTHORS_MY_BIDS:
            return { ...state, authorsMyBidsTotalCount: state.authorsMyBidsTotalCount + 1}
        case ActionTypes.MODIFY_AUTHORS_BIDS:
            return {...state, authorsMyBidsTotalCount: state.authorsMyBidsTotalCount + 1, authorsMyBids: [action.payload, ...state.authorsMyBids]};
        case ActionTypes.CHANGE_NEGOTIATE_NEW_STATE_BID:
          return { ...state, authorsMyBids: state.authorsMyBids.map((content) => content.projectId === action.payload.projectId ? {...content, isNewReceiveBid: false} : content)}
        default:
            return state;
    }
}