import React, { useEffect, useState } from "react";
import { getProducts } from "../helpers/products";
import ProductCard from "./ProductCard";



const ProductsList = () => {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        fetchProducts();
    }, [])

    const fetchProducts = async () => {
        setProducts([...await getProducts()]);
    }

    return (
        <>
            {
                products.map(product => <ProductCard key={product.id} productDto={product}/>)
            }
        </>
    )
}



export default ProductsList;