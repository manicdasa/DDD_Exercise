import { axiosInstance } from '../axios/key';
import { ActionCreatorsForNotifications } from '../store/NotificationReducer';
import { ErrorHandler } from './ErrorServices';

export const GetAllNotification = async (dispatch: any, alert: any) =>
{
    try
    {
        //const { data } = await axiosInstance.get('', { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
        //dispatch(ActionCreatorsForNotifications.getNotifications(data));
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const MarkNotificationsAsSeen = async (id: any, alert: any) =>
{
    try
    {
        await axiosInstance.get('/Notification/MarkNotificationsAsSeen?notificationType=' + `${id}`,  { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }});
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetUserNotifications = async (alert: any) => 
{
    try
    {
        return await axiosInstance.get('/Notification/GetUserNotifications',  { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` }})
            .then((response: any) => { return response.data })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}