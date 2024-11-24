import React, { useState, useEffect } from 'react';
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { routes } from '../http/routes';
import useAuth from '../hooks/useAuth';
import ListingCard from './ListingCard';
import { Button } from '@mantine/core';

const UsersListingPage = ({ user }) => {
    const [products, setProducts] = useState([]);
    const { auth } = useAuth();
    const axiosPrivate = useAxiosPrivate();

    useEffect(() => {
        const controller = new AbortController();
        fetchProducts(controller);

        return () => controller.abort();
    }, [user]);

    const fetchProducts = async (controller) => {
        try {
            const response = await axiosPrivate.get(routes.listings.get + "?userId=" + user.userId,
                {
                    signal: controller.signal
                });

            setProducts(response.data.data);
        } catch (error) {
            console.error(error)
        };
    }

    const handleDelete = async (product) => {
        try {
            if (window.confirm(`Are you sure you want to delete ${product.name}?`)) {
                await axiosPrivate.delete(routes.listings.delete + "/" + product.id);
                setProducts(products.filter(p => p.id !== product.id));
            }
        } catch (err) {
            JSON.stringify("error deleting product " + JSON.stringify(err))
        }
    };

    return (
        <div className="product-list">
            {
                products.map(product => (
                    <div key={product.id}>
                        <ListingCard product={product} />

                        {auth.email === user.username ?
                            <Button variant="filled" onClick={() => handleDelete(product)}>
                                Delete
                            </Button>
                            : null}
                    </div>
                ))}
        </div>
    )
}

export default UsersListingPage;