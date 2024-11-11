import axios from '../http/axios'
import { useNavigate } from 'react-router-dom';
import useAuth from './useAuth'

const useLogout = () => {
    const { setAuth } = useAuth();
    const navigate = useNavigate();

    const logoutAsync = async () => {
        setAuth({});

        try {
            await axios("/logout", {
                withCredentials: true,
            })
        } catch (err) {
            console.error(err);
        }

        navigate('/');
    }

    return logoutAsync;
}

export default useLogout;