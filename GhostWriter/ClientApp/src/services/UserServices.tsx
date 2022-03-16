import { axiosInstance } from '../axios/key';
import { ActionCreators } from '../store/SignInReducer';
import { setupSignalRConnection } from '../components/Helpers/SignalRMiddleware';
import { ErrorHandler } from './ErrorServices';

const setupEventsHub = setupSignalRConnection(() => localStorage.getItem('token'));

export const LoginUser = async (history: any, dispatch: any, user: any, alert: any) =>
{
    localStorage.clear();
    try
    {
        await axiosInstance.post('/Authenticate/login', user)
            .then((response)=>
            {
                response.data.userRoles.map((x: any) => 
                {
                    if(x==='Customer')
                    {
                        response.data.userRoles.map((x: any)=> localStorage.setItem(`role=${x}`, x));
                        localStorage.setItem('token', response.data.token);
                        localStorage.setItem('expiration', response.data.expiration);
                        localStorage.setItem('user', response.data.user);
                        dispatch(ActionCreators.setLoggedIn(true))

                        dispatch(setupEventsHub);

                        history.push('/customer-profile');
                    }
                    else
                    {
                        const doc = document.getElementById('signin-error')!;
                        doc.style.display='block';
                        doc.innerHTML='You must have customer account to sign in here.'
                    }
                });
            })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const LoginUserOnCreateProject = async (history: any, dispatch: any, user: any, onSuccessLogin: any, alert: any) =>
{
    localStorage.clear();
    try
    {
        await axiosInstance.post('/Authenticate/login', user)
            .then((response)=>
            { 
                response.data.userRoles.map((x: any) => 
                {
                    if(x==='Customer')
                    {
                        response.data.userRoles.map((x: any)=> localStorage.setItem(`role=${x}`, x));
                        localStorage.setItem('token', response.data.token);
                        localStorage.setItem('expiration', response.data.expiration);
                        localStorage.setItem('user', response.data.user);
                        dispatch(ActionCreators.setLoggedIn(true));

                        dispatch(setupEventsHub);

                        onSuccessLogin();
                    }
                    else
                    {
                        const doc = document.getElementById('signin-error')!;
                        doc.style.display='block';
                        doc.innerHTML='You must have customer account to sign in here.'

                    }
            });
    })}
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const RegisterOnCreateProject = async (values: any, onRegisterSuccess: any, alert: any) =>
{
    try {

        delete values.passwordConfirm;

        await axiosInstance.post('/Authenticate/registerUser', values)
            .then((response) => 
            {
                onRegisterSuccess();
                return response;
            })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const LoginAuthor = async (history: any, dispatch: any, user: any, alert: any) =>
{
    localStorage.clear();
    try
    {
        await axiosInstance.post('/Authenticate/login', user)
            .then((response)=>
            {   
                response.data.userRoles.map((x: any) => 
                {
                    if(x==='Ghostwriter')
                    {
                        response.data.userRoles.map((x: any)=> localStorage.setItem(`role=${x}`, x));

                        localStorage.setItem('token', response.data.token);
                        localStorage.setItem('expiration', response.data.expiration);
                        localStorage.setItem('user', response.data.user);
                        dispatch(ActionCreators.setLoggedIn(true))

                        dispatch(setupEventsHub);

                        history.push('/profile');
                    }
                    else
                    {
                        const doc = document.getElementById('signin-errorauthor')!;
                        doc.style.display='block';
                        doc.innerHTML='You must have author account to sign in here.'
                    }
                });
            })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const ForgotPassword = async (history: any, email: string, alert: any) =>
{
    try {
        await axiosInstance.post('/Authenticate/requestPasswordChange?username=' + `${encodeURIComponent(email)}`)
            .then((response) => {
                return response;
            })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const PasswordChange = async ( oldPassword: string, newPassword: string, showChangePasswordPopup: any, alert: any) => {
    try 
    {
        await axiosInstance.post('/Authenticate/changePassword', { oldPassword: oldPassword, newPassword: newPassword }, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });
        showChangePasswordPopup();
        alert.success('You have successfully changed your password.');
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const EmailConfirmation = async (history: any, user: any, alert: any) => {
    try {
        await axiosInstance.post('/Authenticate/resendEmailConfirmation', user)
            .then((response) => {
                return response;
            })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const ResetPassword = async (history: any, user: any, alert: any) => {
    try {
        await axiosInstance.post('/Authenticate/resetPassword', user)
            .then((response) => {
                return response;
            })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const RegisterUserMethod = async (history: any, user: any, alert: any) => {
    try {
        delete user.passwordConfirm;

        await axiosInstance.post('/Authenticate/registerUser', user)
            .then((response) => {
                history.push('/account-created');
            })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const RegisterAuthorMethodPayPal = async (history: any, user: any, alert: any) =>
{
    try
    {
        delete user.passwordConfirm;
        delete user.terms;

        if(user.directBooking == "true")
        {
            user.directBooking=true;
        }
        else if(user.directBooking == "false")
        {
            user.directBooking=false;
        }
        
        await axiosInstance.post('/Authenticate/registerAuthor', user).then((response)=> { history.push('/account-created'); })
    }
    catch(e)
    {
        ErrorHandler(e, 'Make sure you filled all inputs.', alert);
    }
}

export const RegisterAuthorMethodIBan = async (history: any, user: any, alert: any) =>
{
    try
    {
        delete user.passwordConfirm;
        delete user.terms;

        if(user.directBooking == "true")
        {
            user.directBooking=true;
        }
        else if(user.directBooking == "false")
        {
            user.directBooking=false;
        }
        
        await axiosInstance.post('/Authenticate/registerAuthor', user).then((response)=> { history.push('/account-created'); })
    }
    catch(e)
    {
        ErrorHandler(e, 'Make sure you filled all inputs.', alert);
    }
}

export const CheckEmailAvailability = async (email: string, alert: any) =>
{
    try
    {
        return await axiosInstance.post('/Authenticate/CheckEmailAvailability?email=' + `${encodeURIComponent(email)}`).then((response)=> { return response; })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const CheckUsernameAvailability = async (username: string, firstName: string, lastName: string, birthday: any, alert: any) =>
{
    const obj = { "username": username,
                  "firstName": firstName, 
                  "lastName": lastName, 
                  "birthday": birthday
                }
    try
    {
        return await axiosInstance.post('/Authenticate/CheckUsernameAvailability', obj).then((response)=> { return response; })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const ChangeAuthorsPicture = async (picture: string, closeModalProfilePicture: any, alert: any) => {
    try 
    {       
        await axiosInstance.put('/User/ChangeAuthorsProfilePicture', { picture: picture }, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });
        closeModalProfilePicture();
        alert.success('You have successfully changed your profile picture.')
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to change your profile picture.', alert);
    }
}

export const ConfirmEmail = async (username: any, token: any, alert: any) =>
{
    try
    {
        return await axiosInstance.get('/Authenticate/confirmEmail?username=' + `${username}` + '&token=' + `${encodeURIComponent(token)}`).then((response: any) => { return response.data });
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}