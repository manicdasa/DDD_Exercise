import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface AuthState {
    isLoading: boolean;
    authData?: AuthData
}

export interface AuthData {
    user: string;
    token: string;
    expiration: string;
}


// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestLogInAction {
    type: 'REQUEST_LOGIN';
    username: string;
}

interface RequestLogOutAction {
    type: 'REQUEST_LOGOUT';
}

interface ResponseLogInSuccessAction {
    type: 'RESPONSE_LOGIN_SUCCESS';
    user: string;
    token: string;
    expiration: string;
}

interface ResponseLogInFailedAction {
    type: 'RESPONSE_LOGIN_FAILED';
}

interface RequestRegisterAuthorAction {
    type: 'REQUEST_REGISTER_AUTHOR';
    username: string;
}

interface RequestRegisterBuyerAction {
    type: 'REQUEST_REGISTER_BUYER';
    username: string;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestLogInAction | RequestLogOutAction | ResponseLogInSuccessAction | ResponseLogInFailedAction | RequestRegisterAuthorAction | RequestRegisterBuyerAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

interface AuthorRegisterModel {
    firstName: string,
    lastName: string,
    username: string,
    email: string,
    password: string,
    highestDegree: number,
    languages: number[],
    areaOfExpertise: number[],
    pricePerPage: number,
    directBooking: boolean,
    pagesPerDay: number,
}

export const actionCreators = {
    requestLogIn: (username: string, password: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })
        } as RequestInit;

        fetch(`Authenticate/login`, requestOptions)
            .then(
                response => {
                    if (response.status === 200)
                        return response.json() as Promise<AuthData>;
                    else
                        throw new Error('Wrong username and/or password');
                })
            .then(data => {
                dispatch({ type: 'RESPONSE_LOGIN_SUCCESS', ...data });
            })
            .catch(error => {
                dispatch({ type: 'RESPONSE_LOGIN_FAILED' });
            });

        dispatch({ type: 'REQUEST_LOGIN', username: username });
    },
    requestLogOut: () => ({ type: 'REQUEST_LOGOUT' } as RequestLogOutAction),
    requestRegisterAuthor: (model: AuthorRegisterModel): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(model)
        } as RequestInit;

        fetch(`Authenticate/registerAuthor`, requestOptions)
            .then(
                response => {
                    if (response.status === 200)
                        return response.json() as Promise<AuthData>;
                    else
                        throw new Error(response.status === 400 ? response.json() as unknown as string : 'Wrong username and/or password');
                })
            .then(data => {
                dispatch({ type: 'RESPONSE_LOGIN_SUCCESS', ...data });
            })
            .catch(error => {
                dispatch({ type: 'RESPONSE_LOGIN_FAILED' });
            });

        dispatch({ type: 'REQUEST_REGISTER_AUTHOR', username: model.username });
    },
    //requestRegisterBuyer: (username: string, password: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
    //    const requestOptions = {
    //        method: 'POST',
    //        headers: { 'Content-Type': 'application/json' },
    //        body: JSON.stringify({ username, password })
    //    } as RequestInit;

    //    fetch(`Authenticate/login`, requestOptions)
    //        .then(
    //            response => {
    //                if (response.status === 200)
    //                    return response.json() as Promise<AuthData>;
    //                else
    //                    throw new Error('Wrong username and/or password');
    //            })
    //        .then(data => {
    //            dispatch({ type: 'RESPONSE_LOGIN_SUCCESS', ...data });
    //        })
    //        .catch(error => {
    //            dispatch({ type: 'RESPONSE_LOGIN_FAILED' });
    //        });

    //    dispatch({ type: 'REQUEST_LOGIN', username: username });
    //}
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: AuthState = { isLoading: false };

export const reducer: Reducer<AuthState> = (state: AuthState | undefined, incomingAction: Action): AuthState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_LOGIN':
            return {
                isLoading: true
            };
        case 'REQUEST_LOGOUT':
            return {
                isLoading: false
            };
        case 'REQUEST_REGISTER_AUTHOR':
            return {
                isLoading: true
            };
        case 'RESPONSE_LOGIN_SUCCESS':
            return {
                isLoading: false,
                authData: {
                    user: action.user,
                    token: action.token,
                    expiration: action.expiration
                }
            };
        case 'RESPONSE_LOGIN_FAILED':
            return {
                isLoading: false
            };
    }

    return state;
};
