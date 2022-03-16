import { axiosInstance } from '../axios/key';
import { ErrorHandler } from './ErrorServices';

export const GetProposalInfo = async (baseUrl: string, alert:any) => {
    try 
    {
        return await axiosInstance.get(baseUrl, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then((res) => { return res.data });
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const CreateOffer = async (projectId: any, authorId: any, alert: any) =>
{
    try
    {
        await axiosInstance.post('/Proposal/CreateOffer?projectId=' + `${projectId}` + '&authorId=' + `${authorId}`, {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then((res) => { return res.data })
        alert.success('Successfuly assigned project to author.')
    }
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to offer project to author.', alert);
    }
}