import axios from 'axios';
export const axiosInstance = axios.create({
    baseURL: ''
});

axiosInstance.interceptors.response.use(function (response) 
    {
        return response
    }, function (error) {
    switch (error.response.status) 
    {
        case 403: 
          localStorage.clear();
          window.location.href = '/login';
          break;
        case 405: 
          localStorage.clear();
          window.location.href = '/login';
          break;
    }
    return Promise.reject(error)
})

axiosInstance.interceptors.request.use(request => {

    var tempDate = new Date();
    var expDateString = localStorage.getItem('expiration') || "";
    var expDate = new Date(expDateString);

    if (tempDate > expDate)
    {
        var isAdmin = localStorage.getItem('role=Admin') != null ? true : false;
        localStorage.clear();
        window.location.href = isAdmin ? '/dashboard/login' :'/login';
    }

    return request;
})