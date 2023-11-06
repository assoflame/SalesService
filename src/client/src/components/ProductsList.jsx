import React, { useEffect, useState } from "react";
import { getProducts } from "../helpers/products";
import ProductCard from "./ProductCard";



const ProductsList = () => {
    const [products, setProducts] = useState([]);
    // const [searchString, setSearchString] = useState('');
    // const [minPrice, setMinPrice] = useState('');
    // const [maxPrice, setMaxPrice] = useState('');

    useEffect(() => {
        fetchProducts();
    }, [])

    const fetchProducts = async () => {
        setProducts([...await getProducts()]);
    }

    return (
        <>
            {/* <div>
                <input onChange={e => setSearchString(e.target.value)} placeholder="Поиск..."/>
                <input onChange={e => setMinPrice(e.target.value)} placeholder="Минимальная цена"/>
                <input onChange={e => setMaxPrice(e.target.value)} placeholder="Максимальная цена"/>
            </div> */}
            {
                products.length > 0 &&
                products.map(product => <ProductCard key={product.id} productDto={product}/>)
            }
        </>
    )
}



export default ProductsList;