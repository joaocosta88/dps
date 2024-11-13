export const routes = {
    auth: {
        register: "/auth/register",
        login: "/auth/login",
        refreshToken: "/auth/refreshtoken",
        logout: "/auth/logout",
    },
    users: {
        me: "/users/profile",
        all: "/users/all"
    },
    listing: {
        add: "/api/listing",
        search: "/api/listing",
    },
};