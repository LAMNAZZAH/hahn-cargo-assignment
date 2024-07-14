import AxiosInstance from "../Utils/AxiosInstance";

export const FetchAvailableOrdersReq = async () => {
    return AxiosInstance.get('/api/Order/GetAllAvailable')
    .then(res => res.data?.data)
    .catch(err => {
        console.log('error fetching available orders');
        throw err;
    })
}