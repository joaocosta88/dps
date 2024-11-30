import { useNavigate, useSearchParams } from 'react-router-dom';
import { routes } from "../../http/routes"
import { notifications } from '@mantine/notifications';
import axios from "../../http/axios";
import { useEffect } from 'react';

const ConfirmAccount = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();

    useEffect(() => {
        const token = searchParams.get("token"); // Extract token from URL
        if (!token) {
            notifications.show({
                color: "red",
                title: 'Error confirming account',
                message: 'Invalid linkkkk',
            })

            navigate("/", { replace: true })
            return;
        }

        const controller = new AbortController();
        const confirmAccount = async () => {
            try {
                await axios.post(
                    routes.auth.confirmAccount,
                    { token }, // Send token in the request body
                    { signal: controller.signal } // Include AbortController signal
                );

                notifications.show({
                    color: "green",
                    title: 'Account confirmed',
                })

            }
            catch (err) {
                console.log(err)
                notifications.show({
                    color: "red",
                    title: 'Error confirming account',
                })
            }

            navigate("/", { replace: true })
        }
        confirmAccount();

        return () => controller.abort();
    }, [searchParams]);

    return null; // No UI needed for this component
}

export default ConfirmAccount;