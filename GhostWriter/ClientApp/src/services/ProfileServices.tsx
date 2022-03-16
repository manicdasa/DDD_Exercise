import { axiosInstance } from '../axios/key';
import axios  from 'axios';
import { ActionCreatorsForUser } from '../store/AuthorInformationsReducer';
import { ActionCreatorsForUserOffers } from '../store/AuthorOffersReducer';
import { ActionCreators } from '../store/AuthorActiveProjectsReducer';
import { debug } from 'console';
import { ErrorHandler } from './ErrorServices';

export const GetAuthorPrivateInfo = async (dispatch: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/User/GetAuthorPrivateInfo', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreatorsForUser.setLoadingValue(false)); 
        dispatch(ActionCreatorsForUser.setAuthorInformations(data));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetAuthorPublicInfo = async (dispatch: any, id: any, alert: any) => {
    try 
    {       
        var response = await axiosInstance.get('/User/GetAuthorPublicInfo?authorId=' + `${id}`);       
        return response.data;
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const GetAuthorActiveOffers = async (dispatch: any, alert: any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetAuthorsActiveOffers', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreatorsForUserOffers.setAuthorOffers(data.items));
        dispatch(ActionCreatorsForUserOffers.setAuthorOffersTotalCount(data.totalCount));
        dispatch(ActionCreatorsForUserOffers.setLoadingValue(false)); 
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetAuthorBids = async (dispatch: any, alert: any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetAuthorsBids', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }); 

        dispatch(ActionCreatorsForUserOffers.setAuthorBids(data.items));
        dispatch(ActionCreatorsForUserOffers.setAuthorBidsTotalCount(data.totalCount));
        dispatch(ActionCreatorsForUserOffers.setLoadingValueBids(false))
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetAuthorsLastBids = async (dispatch: any, value: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetAuthorsLastBid?projectId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }); 
        dispatch(ActionCreatorsForUserOffers.setAuthorsLastBids(data));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetAuthorsLastOffers = async (dispatch: any, value: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetAuthorsLastOffer?projectId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} })
        dispatch(ActionCreatorsForUserOffers.setAuthorsLastOffer(data));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const CreateBid = async (dispatch: any, value: any, offer: any, alert:any) =>
{
    try
    {
        await axiosInstance.post('/Proposal/CreateBid?projectId='+`${value}`+'&financialOffer='+`${offer}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        return 'An active bid/offer for this project and author already exists.';
    }
}

export const CancelBid = async (dispatch: any, value: any, alert:any) =>
{
    try
    {
        await axiosInstance.put('/Proposal/DropProposalAuthor?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreatorsForUserOffers.setAuthorsLastBids({}));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        return 'An error occured while trying to cancel this bid. Please try again.';
    }
}

export const AcceptOffer = async (dispatch: any, value: any, alert:any) =>
{
    try
    {
        await axiosInstance.put('/Booking/AcceptOffer?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const DeclineOffer = async (dispatch: any, value: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Proposal/DropProposalAuthor?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const DropProposalAuthor = async (dispatch: any, value: any, alert: any) =>
{

    try
    {
        await axiosInstance.put('/Proposal/DropProposalAuthor?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        alert.success('Successfuly declined customer offer.');
    }
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to decline customers offer.', alert);
    }
}

export const DropBidAuthor = async (dispatch: any, value: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Proposal/DropProposalAuthor?proposalId='+`${value}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });

        alert.success('You successfuly canceled your bid.');
    }
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to accept customers offer.', alert);
    }
}

export const AcceptProposalAuthor = async (dispatch: any, value: any, offer: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Booking/AcceptOffer?proposalId='+`${value}`, offer, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} })

        alert.success('Successfuly accepted customer offer.')
    }
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to accept customers offer.', alert);
    }
}

export const GetBookingDetails = async (dispatch: any, value:any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Booking/GetBookingDetails?bookingId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreators.setProjectDetails(data));
        dispatch(ActionCreators.setProjectDetailsLoadingValue(false));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetProjectDetails = async (dispatch: any, value:any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Project/GetProjectDetails?projectId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreators.setProposalDetails(data));
        dispatch(ActionCreators.setProposalDetailsLoadingValue(false));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetProposalDetails = async (dispatch: any, value:any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Proposal/GetProposalDetails?proposalId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        dispatch(ActionCreators.setProposalDetails(data));
        dispatch(ActionCreators.setProposalDetailsLoadingValue(false));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const UploadDocument = async (value: any, file: any, alert: any) =>
{
    try
    {   
        const formData = new FormData();
        formData.append('FormFile', file);

        await axiosInstance.put('/Booking/UploadProjectDocument?bookingId='+`${value}`, formData, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`, 'content-type': 'multipart/form-data'} })

        alert.success('Successfuly uploaded a document.');
    }   
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to upload document.', alert);
    }
}
export const SubmitFinalVersion = async (value: any, data: any, alert: any) =>
{
    try
    {   
        const formData = new FormData();
        formData.append('FormFile', data);

        await axiosInstance.put('/Booking/SubmitFinalVersion?bookingId='+`${value}`, formData, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`, 'content-type': 'multipart/form-data'} })
        
        alert.success('Successfuly uploaded final version document.');
    }
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to upload final version of document.', alert);
    }
}

export const DownloadDocument = async (value: any, name: any, alert: any) =>
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
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetAllMessages = async (dispatch: any, value: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Chat/GetAllMessagesByBooking?bookingId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }})

        dispatch(ActionCreators.clearMessagesForSpecificChatAuthor(value));
        data.forEach((element: any) => 
        {
            dispatch(ActionCreators.setMessages(element));
        });  
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetAllMessagesForProfileChat = async (dispatch: any, value: any, alert:any) =>
{
    try
    {        
        const { data } = await axiosInstance.get('/Chat/GetAllMessagesByHeadProposal?headProposalId=' + `${value}`, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
        setTimeout(()=> 
        {
            dispatch(ActionCreators.clearMessagesForSpecificChat(value));
            data.forEach((element: any) => {
                dispatch(ActionCreators.receiveMessagesForProfileChat(element));
            });      
        }, 0);
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const SendMessage = async (dispatch: any, id: any, value: any, message: any, alert: any) =>
{
    try
    {
        await axiosInstance.post('/Chat/SendMessage?headProposalId='+`${value}`+'&exactEntityId='+`${id}`+'&message='+`${encodeURIComponent(message)}`,
            {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }});
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetBookingChatInfoAuthor = async (dispatch: any, alert:any) => 
{
    try
    {
        const { data } = await axiosInstance.get('/Booking/GetBookingChatInfoAuthor?Page=0&PageSize=6', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }})
        dispatch(ActionCreatorsForUserOffers.setAuthorBookingChat(data.items));
        dispatch(ActionCreatorsForUserOffers.setAuthorBookingChatTotalCount(data.totalCount));
        dispatch(ActionCreatorsForUserOffers.setAuthorBookingChatLoadingValue(false));
    }
    catch (e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetBookingChatInfoAuthorOnPageChange = async (dispatch: any, value: any, alert: any) => 
{
    try
    {
        const { data } = await axiosInstance.get('/Booking/GetBookingChatInfoAuthor?Page='+`${value}`+'&PageSize=6', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}` }})
        dispatch(ActionCreatorsForUserOffers.setAuthorBookingChatTotalCount(data.totalCount));
        dispatch(ActionCreatorsForUserOffers.setAuthorBookingChatLoadingValue(false));
        dispatch(ActionCreatorsForUserOffers.setAuthorBookingChat(data.items));
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const AnonymizeCustomerCheck = async (dispatch: any, alert: any) => {
    try {
        const { data } = await axiosInstance.get('/User/AnonymizeCustomerCheck', { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });
        return data;
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const AnonymizeAuthorCheck = async (dispatch: any, alert: any) => {
    try {
        const { data } = await axiosInstance.get('/User/AnonymizeAuthorCheck', { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });
        return data;
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return false;
    }
}

export const AnonymizeCustomer = async (alert: any) => {
    try {
        await axiosInstance.put('/User/AnonymizeCustomer', {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then(res => {
            localStorage.clear();
            window.location.href = '/login';
        });
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const AnonymizeAuthor = async (alert: any) => {
    try {
        await axiosInstance.put('/User/AnonymizeAuthor', {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then(res => {
            localStorage.clear();
            window.location.href = '/login';
        });
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

