const initialState = {
    openChats: [{ headProposalId: 0 }],
    numberOfOpenChats: 3
}

export const ActionTypes = 
{
    INITIAL_STATE_CHAT: 'INITIAL_STATE_CHAT',
    SET_OPEN_CHATS: 'SET_OPEN_CHATS',
    CLOSE_OPEN_CHAT: 'CLOSE_OPEN_CHAT',
    REMOVE_FIRST_ELEMENT: 'REMOVE_FIRST_ELEMENT'
}

export const ActionCreatorsForChatComponent = 
{
    initialChatState: () => ({type: ActionTypes.INITIAL_STATE_CHAT}),
    setOpenChats: (payload: any) => ({type: ActionTypes.SET_OPEN_CHATS, payload}),
    closeOpenChat: (payload: any) => ({type: ActionTypes.CLOSE_OPEN_CHAT, payload}),
    removeFirstElement: () => ({type: ActionTypes.REMOVE_FIRST_ELEMENT})
}

export default function chatComponentsReducer(state=initialState, action: any)
{
    switch(action.type)
    {
        case ActionTypes.INITIAL_STATE_CHAT:
            return { ...state, openChats: [] }
        case ActionTypes.SET_OPEN_CHATS:
            return { ...state, openChats: [...state.openChats, action.payload] }
        case ActionTypes.CLOSE_OPEN_CHAT:
            return { ...state, openChats: [...state.openChats.filter(chat => chat.headProposalId !== action.payload)] }
        case ActionTypes.REMOVE_FIRST_ELEMENT:
            return { ...state, openChats: [...state.openChats.slice(1, state.openChats.length)] }
        default:
            return state;
    }
}