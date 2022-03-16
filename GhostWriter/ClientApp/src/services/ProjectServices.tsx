import { axiosInstance } from '../axios/key';
import { ErrorHandler } from './ErrorServices';

export const GetProjectInfo = async (values: any, baseUrl: string, alert: any) => {
    try 
    {
        var kindOfWorks = values.kindOfWork?.map((v: any) => v.value).join('&KindOfWorkIds=');
        var areaOfExpertise = values.areaOfExpertise?.map((v: any) => v.value).join('&AreaOfExpertiseIds=');
        var language = values.language?.map((v: any) => v.value).join('&LanguageIds=');

        return await axiosInstance.get(baseUrl + '?NoPagesFromRange=' + `${values.pagesFrom}`
            + '&NoPagesToRange=' + `${values.pagesTo}`
            +  (kindOfWorks != "" ? '&KindOfWorkIds=' + `${kindOfWorks}` : "")
            +  (areaOfExpertise != "" ? '&AreaOfExpertiseIds=' + `${areaOfExpertise}` : "")
            + (language != "" ? '&LanguageIds=' + `${language}` : "")
            + (values.highestDegree != "" ? '&MinimumDegreeId=' + `${values.highestDegree.value}` : "")
            + (values.deadline != "" ? '&Deadline=' + `${values.deadline}` : "")
            + '&Page=' + `${values.currentPage}`
                + '&PageSize=5', { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then((res) => { return res.data });

    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const PrepopulateProjectInfo = async (values: any, baseUrl: string, alert:any) => {
    try 
    {
        var kindOfWorks = values.kindOfWork?.map((v: any) => v).join('&KindOfWorkIds=');
        var areaOfExpertise = values.areaOfExpertise?.map((v: any) => v).join('&AreaOfExpertiseIds=');
        var language = values.language?.map((v: any) => v).join('&LanguageIds=');

        return await axiosInstance.get(baseUrl + '?NoPagesFromRange=' + `${values.pagesFrom}`
            + '&NoPagesToRange=' + `${values.pagesTo}`
            +  (kindOfWorks != "" ? '&KindOfWorkIds=' + `${kindOfWorks}` : "")
            +  (areaOfExpertise != "" ? '&AreaOfExpertiseIds=' + `${areaOfExpertise}` : "")
            + (language != "" ? '&LanguageIds=' + `${language}` : "")
            + (values.highestDegree != "" ? '&MinimumDegreeId=' + `${values.highestDegree.value}` : "")
            + (values.deadline != "" ? '&Deadline=' + `${values.deadline}` : "")
            + '&Page=' + `${values.currentPage}`
                + '&PageSize=5', { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then((res) => { return res.data });

    }
    catch (e) {
        ErrorHandler(e, "Error occurred !", alert);
    }
}

export const EditProject = async (project: any, alert: any, history: any) =>
{
    try
    {
        await axiosInstance.put('/Project/EditProjectDetails', project, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } });
        alert.success('You have successfuly changed your project.');
        setTimeout(()=>{ history.goBack()}, 1000);
    }
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to modify your project.', alert);
        setTimeout(()=>{ history.goBack()}, 1000);
    }
}


export const DeleteProject = async (projectId: any, alert: any, history: any, closeDeletePopup: any) =>
{
    try
    {
        await axiosInstance.put('/Project/DeleteProject?projectId='+`${projectId}`, {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })  
        closeDeletePopup();
        alert.success('Successfuly deleted this project.')
        setTimeout(()=> { history.push('/customer-profile') }, 1000);
    }
    catch(e)
    {        
        closeDeletePopup();
        ErrorHandler(e, 'An error occured while trying to delete a project.', alert);
    }
}

export const ChangeBroadcastInfo = async (projectId: any, isPublished: any, setBroadcasted: any, alert: any) =>
{
    try
    {
        await axiosInstance.put('/Project/ChangeBroadcastInfo?projectId=' + `${projectId}` + '&isPublished=' + `${isPublished}`, {}, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
        setBroadcasted(isPublished);
    }
    catch(e)
    {
        ErrorHandler(e, 'An error occured while trying to publish the project.', alert);
        setBroadcasted(!isPublished);
    }
}


