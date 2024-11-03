import config from '../Config';

export const routes = {
    auth: {
        register: config.API_URL+"/users/register",
        login: config.API_URL+"/users/login",
        refreshToken: config.API_URL+"/users/refreshtoken",
        me: config.API_URL+"/users/profile",
    },
    listing: {
        add: config.API_URL+"/api/listing",
        search: config.API_URL+"/api/listing",
    },
};