import React, { useState, useEffect } from 'react';
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { routes } from '../http/routes';
import ListingCard from './ListingCard';

const ListingsPage = () => {
    const [products, setProducts] = useState([]);
    const axiosPrivate = useAxiosPrivate();

    useEffect(() => {
        const controller = new AbortController();

        const fetchProducts = async () => {
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

        fetchProducts();
    }, []);

    return (
        <div class="product-list">
            {
                products.map(product => (
                    <ListingCard key={product.id} product={product} />
                ))}
        </div>
    )
}

export default ListingsPage;