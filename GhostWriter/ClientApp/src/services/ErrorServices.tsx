
export const ErrorHandler = (e: any, text: any, alert: any) => {
    if (e != undefined && e.response != undefined && e.response.data != undefined && e.response.data.message != undefined) {
        alert.error(e.response.data.message);
    }
    else
        alert.error(text);
}