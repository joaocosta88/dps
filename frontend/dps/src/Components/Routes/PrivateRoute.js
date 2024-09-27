import {useContext} from "react";
import { Navigate, Outlet } from "react-router-dom";
import { AuthContext } from '../../Providers/AuthProvider';

const PrivateRoute = () => {
  const user = useContext(AuthContext);
  if (!user.token) return <Navigate to="/login" />;
  return <Outlet />;
};

export default PrivateRoute;
