import { Button } from '@mantine/core';
import { Link } from "react-router-dom";

const ListingCard = ({ product, onDelete }) => {

    const handleDelete = () => {
        if (window.confirm(`Are you sure you want to delete ${product.name}?`)) {
            onDelete(product.id);
        }
    };

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
            <Button variant="filled"
                onClick={handleDelete}
            >Delete</Button>
        </div>
    )
}

export default ListingCard;