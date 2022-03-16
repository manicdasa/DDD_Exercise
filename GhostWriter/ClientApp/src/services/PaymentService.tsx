import { axiosInstance } from '../axios/key';
import { ErrorHandler } from './ErrorServices';

export const GeneratePaymentToken = async (alert: any): Promise<string> => {
    try 
    {
        var token = await axiosInstance.get('/Payment/GeneratePaymentToken', { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });

        return token.data;
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return "Error occurred !";
    }
};

export const MakePayment = async (headProposalId: number, payment_method_nonce: string , alert: any) =>
{
    try {
        var response = await axiosInstance.post('/Payment/MakePayment?' + 'headProposalId=' + `${headProposalId}` + '&PaymentMethodNonce=' + `${encodeURIComponent( payment_method_nonce )}`, {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });

        return response.data;
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return "Error occurred !";
    }
}
