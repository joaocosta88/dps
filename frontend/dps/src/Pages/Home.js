import { Link } from "react-router-dom";
import useLogout from "../hooks/useLogout";

const Home = () => {
    const logout = useLogout();

    return (
        <section>
            <h1>Home</h1>
            <br />
            <p>You are logged in!</p>
            <br />
            <Link to="/linkpage">Go to the link page</Link>
            <br />
            <Link to="/admin">Go to the admin page</Link>
            <div className="flexGrow">
                <button onClick={async() => await logout()}>Sign Out</button>
            </div>
        </section>
    );
};

export default Home;