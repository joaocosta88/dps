import { useState } from 'react';
import { Link } from "react-router-dom";
import { Carousel } from '@mantine/carousel';
import { Image, Modal } from '@mantine/core';

const ListingCard = ({ product }) => {
    const [currentIndex, setCurrentIndex] = useState(0);
    const [embla, setEmbla] = useState(null);
    const [modalOpened, setModalOpened] = useState(false);

    const handleImageClick = (index) => {
        setCurrentIndex(index);
        setModalOpened(true);
    };

    const handleSlideChange = (index) => {
        setCurrentIndex(index);
    };

    const carousel = (
        <Carousel
            loop
            withIndicators
            getEmblaApi={setEmbla}
            initialSlide={currentIndex}
            onSlideChange={handleSlideChange}
            styles={{
                indicator: {
                    width: 8,
                    height: 8,
                    backgroundColor: 'rgba(0, 0, 0, 0.4)',
                    borderRadius: '50%',
                    '&[data-active]': {
                        backgroundColor: '#4caf50',
                    },
                },
            }}
        >
            {product.map((url, index) => (
                <Carousel.Slide key={url}>
                    <Image
                        src={url}
                        alt={`Product Image ${index + 1}`}
                        style={{
                            cursor: 'pointer',
                            borderRadius: '12px',
                        }}
                        onClick={() => handleImageClick(index)}
                    />
                </Carousel.Slide>
            ))}
        </Carousel>
    );

    return (
        <div
            className="listing-card"
            style={{
                maxWidth: '360px',
                margin: '20px',
                padding: '16px',
                borderRadius: '16px',
                boxShadow: '0 4px 12px rgba(0, 0, 0, 0.1)',
                backgroundColor: '#fff',
                transition: 'transform 0.2s, box-shadow 0.2s',
                cursor: 'pointer',
            }}
            onMouseEnter={(e) => {
                e.currentTarget.style.transform = 'translateY(-5px)';
                e.currentTarget.style.boxShadow = '0 6px 16px rgba(0, 0, 0, 0.15)';
            }}
            onMouseLeave={(e) => {
                e.currentTarget.style.transform = 'translateY(0)';
                e.currentTarget.style.boxShadow = '0 4px 12px rgba(0, 0, 0, 0.1)';
            }}
        >
            {carousel}
            <p
                style={{
                    margin: 0,
                    fontSize: '1.2rem',
                    fontWeight: 'bold',
                    color: '#333',
                    textAlign: 'left',
                }}
            >
                {product.price} €
            </p>
            <p
                style={{
                    margin: '12px 0 4px',
                    fontSize: '1rem',
                    fontWeight: '400',
                    color: '#555',
                    textAlign: 'left',
                }}
            >
                {product.name}
            </p>

            <Modal
                opened={modalOpened}
                onClose={() => setModalOpened(false)}
                size="70%"
            >
                <div>{carousel}</div>
            </Modal>
        </div>
    );
};

export default ListingCard;
