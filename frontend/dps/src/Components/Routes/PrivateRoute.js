import {useContext} from "react";
import { Navigate, Outlet } from "react-router-dom";
import AuthContext from '../../Providers/AuthContext';

const PrivateRoute = () => {
  const user = useContext(AuthContext);
  if (!user.token) return <Navigate to="/login" />;
  return <Outlet />;
};

export default PrivateRoute;
