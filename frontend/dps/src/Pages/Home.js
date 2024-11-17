import { Link } from "react-router-dom";
import ListingsPage from "./ListingsPage"
import useAuth from "../hooks/useAuth";
import AddListing from "./AddListing";

const Home = () => {
    const { auth } = useAuth();

    return (
        <section>
            <h1>Home</h1>
            <br />
            {
                auth?.accessToken ?
                    <p>You are logged in!</p>
                    : <p></p>}
            <br />
            <br />
            <ListingsPage />
            <Link to="/addlisting">Add listing</Link>
<br />
            <Link to="/admin">Go to the admin page</Link>
        </section>
    );
};

export default Home;