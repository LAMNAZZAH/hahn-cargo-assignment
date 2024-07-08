import axios from 'axios';
import { API_BASE_URL} from '../config.js'

const AxiosInstance = axios.create({
    baseURL: API_BASE_URL,
    timeout: 1000,
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json',
    }
})

export default AxiosInstance;