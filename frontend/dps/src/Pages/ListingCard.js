import { useState } from 'react';
import { Carousel, Embla } from '@mantine/carousel';
import { Image, Modal } from '@mantine/core';
import { Link } from 'react-router-dom';

const ListingCard = ({ product }) => {
  const [opened, setOpened] = useState(false);
  const [embla, setEmbla] = useState<Embla | null>(null);
  const [currentIndex, setCurrentIndex] = useState(0); // Track the current index of the image

  const images = [
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/images/bg-1.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/images/bg-2.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/images/bg-3.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/images/bg-4.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/images/bg-5.png',
  ];

   // Function to scroll to a specific slide using the Embla API
   const scrollToSlide = (index) => {
    if (embla) {
      embla.scrollTo(index); // Scroll to the specified slide
      setCurrentIndex(index); // Update the currentIndex state
    }
  };

  // Function to open the modal and set the clicked image index
  const handleImageClick = (index) => {
    setCurrentIndex(index);  // Set the clicked image index
    setOpened(true); // Open the modal with the full-screen carousel
  };

  // Carousel Component
  const ImageCarousel = ({ images, currentIndex, setCurrentIndex }) => (
    <Carousel
      value={currentIndex}
      onChange={setCurrentIndex} // Update the index when the carousel slides
      withIndicators
      getEmblaApi={setEmbla}  // Store the Embla API
      loop
    >
      {images.map((url, index) => (
        <Carousel.Slide key={url}>
          <Image
            src={url}
            alt={`Product image ${index + 1}`}
            style={{
              width: '100%',
              height: 'auto',
              objectFit: 'contain',
              cursor: 'pointer',
            }}
            onClick={() => handleImageClick(index)} // Open modal when clicking an image
          />
        </Carousel.Slide>
      ))}
    </Carousel>
  );

  return (
    <div className="listing-card" style={{ maxWidth: '400px', margin: '40px', border: '1px solid black' }}>
      <h3>{product.name}</h3>
      <p>{product.description}</p>
      <b>Price: {product.price}</b>

      <ImageCarousel
        images={images}
        setCurrentIndex={setCurrentIndex}
      />

      <i>
        <Link to={`/shop/${product.author.id}`}>
          {product.author.username}
        </Link>
      </i>

      {/* Fullscreen Modal with Carousel */}
      <Modal
        opened={opened}
        onClose={() => setOpened(false)}
        size="full"
        padding={0}
        withCloseButton={true}
      >
        <ImageCarousel
          images={images}
          currentIndex={currentIndex}
          setCurrentIndex={setCurrentIndex} // Pass down the same state to the carousel inside the modal
        />
      </Modal>
    </div>
  );
};

export default ListingCard;
