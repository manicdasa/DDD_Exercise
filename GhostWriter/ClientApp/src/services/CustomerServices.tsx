import { axiosInstance } from '../axios/key';
import { ActionCreators } from '../store/CustomerReducer';
import axios from 'axios';
import { ErrorHandler } from './ErrorServices';

export const GetCustomerInformations = async (dispatch: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/User/GetCustomerPrivateInfo', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreators.setCustomerInformations(data));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetCustomerNewOffers = async (dispatch: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetCustomersNewOffers', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        dispatch(ActionCreators.setCustomerOffers(data.items));
        dispatch(ActionCreators.setCustomerOffersTotalCount(data.totalCount));
        dispatch(ActionCreators.setLoadingValue(false));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}




export const DropProposalCustomer = async (dispatch: any, value: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Proposal/DropProposalCustomer?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        alert.success('Successfuly declined author offer.')
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to decline author offer.', alert);
    }
}

export const DropProposalCustomerOnProfile = async (dispatch: any, value: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Proposal/DropProposalCustomer?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        alert.success('Successfuly declined author offer.');
        setTimeout(()=>{window.location.reload();}, 1000);
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to decline author offer.', alert);
    }
}

export const AcceptProposalCustomer = async (dispatch: any, value: any, offer: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Booking/AcceptBid?proposalId='+`${value}`, offer, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        alert.success('Successfuly accepted author offer.')

    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to accept author offer.', alert);
    }
}


export const AcceptProposalCustomerOnProfile = async (dispatch: any, value: any, offer: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Booking/AcceptBid?proposalId='+`${value}`, offer, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        alert.success('Successfuly accepted author offer.')
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to accept author offer.', alert);
    }
}

export const GetProjectDetails = async (dispatch: any, value:any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Project/GetProjectDetails?projectId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        dispatch(ActionCreators.setProposalDetailsCustomer(data));
        dispatch(ActionCreators.setProposalDetailsCustomerLoadingValue(false));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const DownloadDocument = async (value: any, name: any, alert:any) =>
{
    try
    {   
        await axios({
            url: '/Booking/DownloadProjectDocument?documentId='+`${value}`,
            method: 'GET',
            responseType: 'blob', 
            headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} 
        }).then((response: any) => {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', name);
            document.body.appendChild(link);
            link.click();
          });   
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetBookingDetailsCustomer = async (dispatch: any, value:any, alert: any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Booking/GetBookingDetails?bookingId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreators.setProjectDetailsCustomer(data));
        dispatch(ActionCreators.setProjectDetailsLoadingValueCustomer(false));
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetAllMessages = async (dispatch: any, value: any, alert: any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Chat/GetAllMessagesByBooking?bookingId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }})

        dispatch(ActionCreators.clearMessagesForChatCustomer(value));

        data.forEach((element: any) => {
            dispatch(ActionCreators.setMessagesCustomer(element));
        });  
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const SendMessage = async (dispatch: any, id: any, value: any, message: any, alert:any) =>
{
    try
    {
        await axiosInstance.post('/Chat/SendMessage?headProposalId='+`${value}`+'&exactEntityId='+`${id}`+'&message='+`${encodeURIComponent(message)}`,
             {}, 
             { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }})
        //const messageObj = { messageText: message, dateTimeSent: Date.now().toString() }; 
        //dispatch(ActionCreators.sendMessageCustomer(messageObj));         
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetBookingChatInfoCustomer = async (dispatch: any, alert:any) => 
{
    try
    {
        const { data } = await axiosInstance.get('/Booking/GetBookingChatInfoCustomer?Page=0&PageSize=10', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }})
        dispatch(ActionCreators.setCustomerBookingChat(data.items));
        dispatch(ActionCreators.setCustomerBookingChatTotalCount(data.totalCount));
        dispatch(ActionCreators.setCustomerBookingChatLoadingValue(false));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}


export const GetBookingChatInfoCustomerOnPageChange = async (dispatch: any, value: any, alert:any) => 
{
    try
    {
        const { data } = await axiosInstance.get('/Booking/GetBookingChatInfoCustomer?Page='+`${value}`+'&PageSize=6', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }})
        dispatch(ActionCreators.setCustomerBookingChatTotalCount(data.totalCount));
        dispatch(ActionCreators.setCustomerBookingChatLoadingValue(false));
        dispatch(ActionCreators.setCustomerBookingChat(data.items));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetCustomerMyOffers = async (dispatch: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetCustomersGeneratedOffers', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        dispatch(ActionCreators.setCustomerMyOffers(data.items));
        dispatch(ActionCreators.setCustomerMyOffersTotalCount(data.totalCount));
        dispatch(ActionCreators.setCustomerMyOffersLoadingValue(false));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const CancelOfferCustomer = async (dispatch: any, value: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Proposal/DropProposalCustomer?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        alert.success('Successfuly canceled author offer.')

        setTimeout(()=>dispatch(ActionCreators.declineOfferCustomer(value)), 5000);
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to cancel author offer.', alert);
    }
}


export const GetCustomersProjectBids = async (dispatch: any, value: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetCustomersProjectBids?projectId='+`${value}`, 
                            { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} })
        dispatch(ActionCreators.setCustomersProjectBids(data));

    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const ConfirmProject = async (bookingId: any, alert: any) => 
{
    try
    {
        await axiosInstance.put('/Booking/ConfirmProject?bookingId=' + `${bookingId}`, {},
                                    { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} })
        alert.success('You have successfuly confirmed authors project.')
    }
    catch (e) {
        ErrorHandler(e, 'There was an error while trying to accept authors project', alert);
    }
}

export const CreateDispute = async (bookingId: any, message: any, alert: any) => 
{
    try
    {
        await axiosInstance.post('/Booking/CreateDispute?bookingId=' + `${bookingId}` + '&message=' + `${encodeURIComponent(message)}`, {},
                                    { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} })
        alert.success('Successfully opened a dispute for author project.');
    }
    catch (e) {
        ErrorHandler(e, 'There was an error while trying to create a dispute.', alert);
    }
}

export const CancelProject = async (bookingId: any, alert: any, history: any, closePopupCancelProject: any) =>
{
    try
    {
        await axiosInstance.put('/Booking/CancelProject?bookingId='+`${bookingId}`, {},  { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} })
        closePopupCancelProject();
        alert.success('Successfuly canceled the project.')
    }
    catch (e) {
        closePopupCancelProject();
        ErrorHandler(e, 'An error occured while trying to cancel the project.', alert);
    }
}