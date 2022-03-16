import { axiosInstance } from '../axios/key';
import { ErrorHandler } from './ErrorServices';

export const AcceptDispute = async (bookingId: number, refundAmount: any, paymentAmountAuthor: any, message: string, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Booking/AcceptDispute?bookingId=' + `${bookingId}` + '&refundAmount=' + `${refundAmount}` + '&paymentAmountAuthor=' + `${paymentAmountAuthor}`+'&message='+`${encodeURIComponent(message)}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        window.location.reload();
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to accept dispute.', alert);
    }
}

export const DeclineDispute = async (bookingId: number, message: string, alert: any) => 
{
    try
    {
        await axiosInstance.put('/Booking/DeclineDispute?bookingId='+`${bookingId}`+'&message='+`${encodeURIComponent(message)}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} })
        window.location.reload();
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to decline dispute.', alert);
    }
}