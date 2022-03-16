import { axiosInstance } from '../axios/key';
import { ErrorHandler } from './ErrorServices';

export const Languages = async (value: any, alert:any) =>
{
    try
    {
        return await axiosInstance.get('/Lookup/Language?Search='+`${value}`+'&Page='+`${0}`+'&PageSize='+`${10}`).then((res) => { return res.data.searchResult });
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        console.log('Error.');
    }
}

export const Expertise = async (value: any, alert:any) =>
{
    try
    {
        return await axiosInstance.get('/Lookup/AreaOfExpertise?Search='+`${value}`+'&Page='+`${0}`+'&PageSize='+`${10}`).then((res) => { return res.data.searchResult });
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        console.log('Error.');
    }
}

export const KindOfWork = async (value: any, alert:any) =>
{
    try
    {   
        return await axiosInstance.get('/Lookup/KindOfWork?Search='+`${value}`+'&Page='+`${0}`+'&PageSize='+`${10}`).then((res) => { return res.data.searchResult });
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        console.log('Error');
    }
}

export const HighestDegree = async (value: any, alert:any) =>
{
    try
    {
        return await axiosInstance.get('/Lookup/Degree?Search='+`${value}`+'&Page='+`${0}`+'&PageSize='+`${10}`).then((res) => { return res.data.searchResult });
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        console.log('Error');
    }
}

export const AddCustomKindOfWork = async (value: any, alert: any) =>
{
    try
    {
        await axiosInstance.post('/Lookup/AddCustomKindOfWork?value='+`${value}`, {},  { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });
        alert.success('Successfully added a new kind of work value ' + `${value}`+ '. Waiting for admin approval...')
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to add new kind of work value.', alert);
    }
}

export const AddCustomExpertiseArea = async (value: any, alert: any) =>
{
    try
    {
        await axiosInstance.post('/Lookup/AddCustomExpertiseArea?value='+`${value}`, {},  { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
        alert.success('Successfully added a new expertise area value ' + `${value}`+ '. Waiting for admin approval...')
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to add new area of expertise value.', alert);
    }
}