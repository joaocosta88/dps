import { Link } from "react-router-dom";

const ListingCard = ({ product }) => {

    return (
        <div className="listing-card" style={{ border: '1px solid black' }}>
            <h3>{product.name}</h3>
            <br />
            <p> {product.description}</p>
            <b>Price: {product.price}</b>
            <br />
            <i>
                <Link to={`/shop/${product.author.id}`}>
                    {product.author.username}
                </Link>
            </i>
            <br />  
        </div>
    )
}

export default ListingCard;