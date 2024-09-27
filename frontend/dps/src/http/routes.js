import config from '../Config';

export const routes = {
    auth: {
        register: config.API_URL+"/user/register",
        login: config.API_URL+"/user/login",
        refreshToken: config.API_URL+"/user/refreshtoken",
        me: config.API_URL+"/user/profile",
    },
    listing: {
        add: config.API_URL+"/api/listing",
        search: config.API_URL+"/api/listing",
    },
};