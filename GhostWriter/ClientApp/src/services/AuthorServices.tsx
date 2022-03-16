import { axiosInstance } from '../axios/key';
import { ActionCreators } from '../store/SearchAuthorReducer';
import { ErrorHandler } from './ErrorServices';

export interface AuthorSearchParams {
    KindOfWorkId: number,
    AreaOfExpertiseId: number,
    LanguageId: number,
    HighestDegreeId: number,
    NumberOfPages: number,
    Deadline: Date,
    PlannedBudget: number,
    Page: number,
    PageSize: number,
};

export interface AuthorSearchResult {
    name: string,
    value: number
};

export const SearchAuthor = async (search: AuthorSearchParams, dispatch: any, alert:any): Promise<AuthorSearchResult[]> => {
    try 
    {
        var filteredAuthors = await axiosInstance.get('/User/AuthorsSearch?KindOfWordId=' + `${search.KindOfWorkId}` + '&AreaOfExpertiseIds=' + `${search.AreaOfExpertiseId}` 
            + '&LanguageId=' + `${search.LanguageId}` + '&HighestDegreeId=' + `${search.HighestDegreeId}` 
            + '&NumberOfPages=' + `${search.NumberOfPages}` + '&Deadline=' + `${search.Deadline}` 
            + (search.PlannedBudget != undefined ? '&PlannedBudget=' + `${search.PlannedBudget}` : "") 
            + '&Page=' + `${ search.Page }` 
            + '&PageSize=' + `${search.PageSize}`).then((res) => res.data as AuthorSearchResult[]);
        dispatch(ActionCreators.setLoadingValue(false));
        return filteredAuthors;
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        return [];
    }
};

export const SearchAuthorForAssign = async (search: any, dispatch: any, alert:any) => {
    try 
    {
        var areaOfExpertise = search.AreaOfExpertiseId?.map((v: any) => v).join('&AreaOfExpertiseIds=');
        var filteredAuthors = await axiosInstance.get('/User/AuthorsSearch?ProjectId=' + `${search.ProjectId}` + '&KindOfWordId=' + `${search.KindOfWorkId}` + '&AreaOfExpertiseIds=' + areaOfExpertise + '&LanguageId=' + `${search.LanguageId}` + '&HighestDegreeId=' + `${search.HighestDegreeId}` +
            '&NumberOfPages=' + `${search.NumberOfPages}` + '&Deadline=' + `${search.Deadline}` + '&PlannedBudget=' + `${search.PlannedBudget}` + '&Page=' + `${ search.Page }` + '&PageSize=' + `${search.PageSize}`).then((res) => res.data as AuthorSearchResult[]);
        dispatch(ActionCreators.setLoadingValue(false));
        return filteredAuthors;
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        return [];
    }
};

export const CreateProjectAndOfferToAuthor = async (history: any, authorId: any, values: any, alert:any) =>
{
    delete values.terms;
    try
    {
        await axiosInstance.post('/Project/CreateProjectAndOfferToAuthor?authorId='+ `${authorId}`, values, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        history.push('/created-project');
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
        console.log('Error');
    }
}

export const CreateProjectMethod = async (history: any, values: any, alert:any) =>
{
    delete values.terms;
    try
    {
        await axiosInstance.post('/Project/CreateProject', values,  { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        history.push('/created-project');
    }
    catch(e)
    {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const EditAuthor = async (history: any, user: any, alert:any) => {
    try 
    {
        await axiosInstance.put('/User/EditAuthorPrivateInfo', user, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} }).then(() => { history.push('/profile'); })
    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
        return e.response.data;
    }
}

export const RateAuthor = async (body: any, history: any, alert: any) =>
{
    try
    {
        await axiosInstance.post('/Booking/AddProjectReview', body, { headers: {"Authorization" : `Bearer ${localStorage.getItem('token')}`} });
        history.push('/customer-profile/refresh');
    }
    catch (e) {
        ErrorHandler(e, 'An error occured while trying to rate the author.', alert);
    }
}