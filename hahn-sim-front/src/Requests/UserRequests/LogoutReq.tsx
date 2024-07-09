import AxiosInstance from "../../Utils/AxiosInstance";

export const LogoutReq = async () => {
    return AxiosInstance.get('/api/user/logout')
    .then(res => res.data)
    .catch(err => {
        console.error('Logout Failed:', err);
        throw err;
    });
}