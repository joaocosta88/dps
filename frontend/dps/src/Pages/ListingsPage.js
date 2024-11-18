import React, { useState, useEffect } from 'react';
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { routes } from '../http/routes';
import ListingCard from './ListingCard';

const ListingsPage = () => {
    const [products, setProducts] = useState([]);
    const axiosPrivate = useAxiosPrivate();

    useEffect(() => {
        const controller = new AbortController();
        fetchProducts(controller);
    }, []);

    const fetchProducts = async (controller) => {
        try {
            const response = await axiosPrivate.get(routes.listings.get,
                {
                    signal: controller.signal
                });

            setProducts(response.data.data);
        } catch (error) {
            console.error(error)
        };
    }

    const handleDelete = async (productId) => {
        try {
            const response = await axiosPrivate.delete(routes.listings.delete+"/"+productId);

            console.log("response after deleling "+JSON.stringify(response))
            
            setProducts(products.filter(product => product.id !== productId));
        } catch (err) {
            JSON.stringify("error deleting product "+JSON.stringify(err))
        }
    };

    return (
        <div class="product-list">
            {
                products.map(product => (
                    <ListingCard key={product.id} product={product} onDelete={handleDelete}/>
                ))}
        </div>
    )
}

export default ListingsPage;