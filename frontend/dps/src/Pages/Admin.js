import { Link } from "react-router-dom";
import Users from "./Users";

const Admin = () => {

    return (
        <section>
            <br />
            <h1>Admins page</h1>
            <Users />
            <br />
            <Link to="#">Retry</Link>
        </section>
    );
}

export default Admin;