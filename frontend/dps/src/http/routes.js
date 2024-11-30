import ConfirmAccount from "../pages/Auth/ConfirmAccount";

export const routes = {
    auth: {
        register: "/auth/register",
        login: "/auth/login",
        refreshToken: "/auth/refreshtoken",
        logout: "/auth/logout",
        forgotPassword: "/auth/forgotPassword",
        resetPassword: "/auth/resetPassword",
        confirmAccount: "/auth/confirmaccount"
    },
    users: {
        me: "/users/getuserprofile",
        all: "/users/all",
        byUsername: "/users/getbyusername/"
        
    },
    listings: {
        add: "/listings/createlisting",
        get: "/listings/getlistings",
        delete: "/listings/deletelisting"
    },
};