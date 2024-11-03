import axios from "../http/axios";
import useAuth from "./useAuth";

const useRefreshToken = () => {
    const {auth, setAuth} = useAuth();

    const refresh = async() => {
        const response = await axios.post('http://localhost:5263/Users/RefreshToken', {},
            {
                headers: {
                    'Authorization': `Bearer ${auth.accessToken}`, 
                },
                withCredentials: true,
            })
        

        setAuth(prev => {
            console.log("dsfsdfsdfsdfsdfsdf"+JSON.stringify(prev))
            console.log("aaaaaaaa"+JSON.stringify)
            console.log(response.data.accessToken)

            return {...prev, accessToken: response.data.accessToken }
        })
        return response.data.accessToken;
    }

    return refresh;
}

export default useRefreshToken; 