import { Link } from "react-router-dom";
import Users from "./Users";

const Admin = () => {

    return (
        <section>
            <br />
            <h1>Admins page</h1>
            <Users />
            <br />
            <div className="flexGrow">
                <Link to="/">Home</Link>
            </div>
        </section>
    );
}

export default Admin;