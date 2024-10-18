import axios from "axios";
import config from "../Config";

export default axios.create({
    baseURL: config.API_URL,
})