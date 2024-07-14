import AxiosInstance from "../Utils/AxiosInstance";

export const CreateOrderReq = async () => {
    return AxiosInstance.post('/api/Order/CreateOrder')
    .then(res => res.data?.success)
    .catch(err => {
        console.error('Error Creating Order:', err);
        throw err;
    });
}