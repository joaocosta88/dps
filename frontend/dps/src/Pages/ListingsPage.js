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

        return () => controller.abort();
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

    return (
        <div className="product-list">
            {
                products.map(product => (
                    <ListingCard key={product.id} product={product}/>
                ))}
        </div>
    )
}

export default ListingsPage;