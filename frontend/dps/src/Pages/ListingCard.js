import { Button } from '@mantine/core';

const ListingCard = ({ product, onDelete }) => {

    const handleDelete = () => {
        if (window.confirm(`Are you sure you want to delete ${product.name}?`)) {
          onDelete(product.id);
        }
      };

    return (
        <div class="listing-card">
            <h3>{product.name}</h3>
            <br />
            <p> {product.description}</p>
            <b>Price: {product.price}</b>
            <br />
            <i>{product.author.username} ({product.author.id})</i>
            <br />
            <Button variant="filled"
                onClick={handleDelete}
            >Delete</Button>
        </div>
    )
}

export default ListingCard;