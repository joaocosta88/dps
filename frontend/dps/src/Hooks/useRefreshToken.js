import axios from "../http/axios";
import useAuth from "./useAuth";
import { routes } from "../http/routes"

const useRefreshToken = () => {
    const {auth} = useAuth();

    const refresh = async() => {
        const response = await axios.post(routes.auth.refreshToken, {},
            {
                headers: {
                    'Authorization': `Bearer ${auth.accessToken}`, 
                },
                withCredentials: true,
            })
        
        return response;
    }

    return refresh;
}

export default useRefreshToken; 