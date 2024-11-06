import React from "react";

import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth"

const RequireAuth = ({ allowedRoles }) => {
    console.log("roles"+JSON.stringify(allowedRoles))
    const { auth } = useAuth();
    const location = useLocation();

    return (
        auth?.user
            ? <Outlet/>
            : <Navigate to="/login" state={{ from: location }} replace />
    )
}

export default RequireAuth;