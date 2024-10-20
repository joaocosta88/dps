import { Outlet } from "react-router-dom";
import useAuth from "../Hooks/useAuth";


const Layout = () => {

    const { auth } = useAuth();
alert(auth?.user+"asdf")

    alert()
    return (
        <main className="App">
            <Outlet />
        </main>
    )
}

export default Layout;