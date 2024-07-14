import AxiosInstance from "../Utils/AxiosInstance";

export const FetchAcceptedOrdersReq = async () => {
    return AxiosInstance.get('/api/Order/GetAllAccepted')
    .then(res => res.data?.data)
    .catch(err => {
        console.error('Error Fetching Accepted Orders:', err);
        throw err;
    });
}