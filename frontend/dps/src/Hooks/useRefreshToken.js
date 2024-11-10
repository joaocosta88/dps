import axios from "../http/axios";
import { routes } from "../http/routes"

const useRefreshToken = () => {
    const refresh = async() => {
        const response = await axios.post(routes.auth.refreshToken, {},
            {
                withCredentials: true,
            })
        
        return response;
    }

    return refresh;
}

export default useRefreshToken; 