const ListingCard =  ({ product })  => {
    return (
       <div class="listing-card">
       <h3>{product.name}</h3>
       <br />
       <p> {product.description}</p>
       <b>Price: {product.price}</b>
       <br />
       <i>{product.author.username} ({product.author.id})</i>
       </div>
    )
}

export default ListingCard;