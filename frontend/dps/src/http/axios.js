import axios from "axios";
import config from "../Config";

export default axios.create({
    baseURL: config.API_URL
})

export const axiosPrivate = axios.create({
    baseURL: config.API_URL,
    headers: {'Content-Type': 'application/json'},
    withCredentials: true
})