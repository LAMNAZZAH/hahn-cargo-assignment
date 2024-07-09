import AxiosInstance from "../../Utils/AxiosInstance";

export const FetchCoinsReq = async () => {
    return AxiosInstance.get('/api/Coin/Get')
    .then(res => res.data.data)
    .catch(err => {
        console.error('Error fetching coins balance:', err);
        throw err;
    });
}