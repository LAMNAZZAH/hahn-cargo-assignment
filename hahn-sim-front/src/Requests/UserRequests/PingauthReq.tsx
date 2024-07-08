import AxiosInstance from "../../Utils/AxiosInstance";

export const PingAuthReq = async () => {
    return AxiosInstance.get('/api/user/pingauth')
    .then(res => res.data?.email)
    .catch(err => {
        console.error('Error Getting User Email:', err);
        throw err;
    });
}