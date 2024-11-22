export const routes = {
    auth: {
        register: "/auth/register",
        login: "/auth/login",
        refreshToken: "/auth/refreshtoken",
        logout: "/auth/logout",
    },
    users: {
        me: "/users/profile",
        all: "/users/all",
        byUsername: "/users/getbyusername/"
    },
    listings: {
        add: "/listings/createlisting",
        get: "/listings/getlistings",
        delete: "/listings/deletelisting"
    },
};