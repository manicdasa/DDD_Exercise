import { axiosInstance } from '../axios/key';
import { ActionCreators } from '../store/AdminDashboard';
import { ErrorHandler } from './ErrorServices';


export const AdminLoginMethod = async (history:any, values: any, alert:any) => 
{
    localStorage.clear();
    try
    {
        await axiosInstance.post('/Authenticate/login', values)
            .then((response)=>
            {
                response.data.userRoles.map((x: any) => 
                {
                    if(x==='Admin')
                    {
                        response.data.userRoles.map((x: any)=> localStorage.setItem(`role=${x}`, x));
                        localStorage.setItem('token', response.data.token);
                        localStorage.setItem('expiration', response.data.expiration);
                        localStorage.setItem('user', response.data.user);
                
                        history.push('/dashboard')
                    }
                    else
                    {
                        const doc = document.getElementById('signin-error')!;
                        doc.style.display='block';
                        doc.innerHTML='You cannot log in here.'
                    }
                })})
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const GetStats = async (baseURL: any, alert: any) => 
{
    try
    {
        return await axiosInstance.get(baseURL, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }).then((response)=> { return response.data; })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetNewDataWithPagination = async (baseURL: any, alert:any) =>
{
    try
    {
        return await axiosInstance.get(baseURL+'?Page=0&PageSize=10',
            { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }).then((response) => { return response.data });
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}


export const GetNewDataFromPageChange = async (baseURL: any, page: any, rows: any, searchParams: string, orderColumn: string, asc: string ,alert: any) => {
    try {
        if (searchParams === '') {
            return await axiosInstance.get(baseURL + '?Page=' + `${page}` + '&PageSize=' + `${rows}`+'&OrderColumn.OrderByColumn=' + `${encodeURIComponent(orderColumn)}`
                + '&OrderColumn.OrderByAsc=' + `${asc==="asc"}`,
                { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then((response) => { return response.data });
        }
        else {
            return await axiosInstance.get(baseURL + '?Search=' + `${encodeURIComponent(searchParams)}` + '&Page=' + `${page}` + '&PageSize=' + `${rows}`,
                { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then((response) => { return response.data });
        }
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const SortFunction = async (baseURL: any, page: any, rows: any, searchParams: string, orderColumn: any, orderColumnBool: any, alert: any) =>
{
    try
    {
        var boolValue = false;
        
        if(orderColumnBool === 'asc') { boolValue = true; }
        if(searchParams === '')
        {
            return await axiosInstance.get(baseURL+'?OrderColumn.OrderByColumn='+`${encodeURIComponent(orderColumn)}`
                                        +'&OrderColumn.OrderByAsc='+`${boolValue}`
                                        +'&Page='+`${page}`
                                        +'&PageSize='+`${rows}`
                    ,{ headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }).then((response) => { return response.data });
        }
        else
        {
            return await axiosInstance.get('/Admin/GetNewProjects?Search='+`${encodeURIComponent(searchParams)}`
                                        +'&OrderColumn.OrderByColumn='+`${encodeURIComponent(orderColumn)}`
                                        +'&OrderColumn.OrderByAsc='+`${boolValue}`
                                        +'&Page='+`${page}`
                                        +'&PageSize='+`${rows}`
                    ,{ headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }).then((response) => { return response.data });
        }
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetNewAuthors = async (dispatch: any, alert:any) =>
{
    try
    {
        const { data } = await axiosInstance.get('/Admin/GetNewAuthors?Page=0&PageSize=10', { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }) 
        dispatch(ActionCreators.setNewAuthors(data.items));
        dispatch(ActionCreators.setNewAuthorsTotalCount(data.totalCount));
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const GetProjectDetails = async (dispatch: any, value:any, alert: any) =>
{
    try
    {
        return await axiosInstance.get('/Booking/GetBookingDetails?bookingId='+`${value}`, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }).then((res) => { return res.data });
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const PayAuthor = async (bookingid: any, paymentAmount: any, alert: any, authorusername: any) =>
{
    try {
        await axiosInstance.post('/Admin/PayAuthor?bookingId=' + `${bookingid}` + '&paymentAmount=' + `${paymentAmount}`, {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })

        alert.success('Successfuly made payment to ' + `${authorusername}`);
        setTimeout(() => { window.location.reload() }, 3000);
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to make payment to ' + `${authorusername}`, alert);
    }
}


export const MarkAsPaidAuthor = async (bookingid: any, paymentAmount: any, alert: any, authorusername: any) =>
{
    try {
        await axiosInstance.post('/Admin/MarkAsPaidAuthor?bookingId=' + `${bookingid}` + '&paymentAmount=' + `${paymentAmount}`, {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })

        alert.success('Successfuly made payment to ' + `${authorusername}`);
        setTimeout(() => { window.location.reload() }, 3000);
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to make payment to ' + `${authorusername}`, alert);
    }
}

export const ProcessCustomerKindOfWork = async (id: any, approved: any, alert: any) => 
{
    try
    {
        await axiosInstance.put('/Lookup/ProcessCustomKindOfWork?id='+`${id}`+'&approved='+`${approved}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        alert.success('Successfuly updated kind of work status.')
        window.location.reload();
    }
    catch (e) {
            ErrorHandler(e, 'An error occured while trying to update kind of work status.', alert);
    }
}

export const ProcessCustomerExpertiseArea = async (id: any, approved: any, alert: any) => 
{
    try
    {
        await axiosInstance.put('/Lookup/ProcessCustomExpertiseArea?id='+`${id}`+'&approved='+`${approved}`, {}, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        alert.success('Successfuly updated expertise area status.')
        window.location.reload();
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to update expertise area status.', alert);
    }
}