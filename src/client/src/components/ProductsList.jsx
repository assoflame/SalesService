import React, { useEffect, useState } from "react";
import { getProducts } from "../helpers/products";
import ProductCard from "./ProductCard";


const ProductsList = () => {
    const [products, setProducts] = useState([]);
    const [filter, setFilter] = useState({
        searchString : '',
        minPrice : '',
        maxPrice : ''
    });

    const searchProducts = async () => {
        await fetchProducts();
    }

    useEffect(() => {
        fetchProducts();
    }, [])

    const fetchProducts = async () => { 
        setProducts([...await getProducts(filter)]);
    }

    return (
        <>
            <div>
                <div>
                    <input onChange={e => setFilter({ ...filter, searchString: e.target.value})} placeholder="Поиск..."/>
                    <input onChange={e => setFilter({ ...filter, minPrice: e.target.value})} placeholder="Минимальная цена"/>
                    <input onChange={e => setFilter({ ...filter, maxPrice: e.target.value})} placeholder="Максимальная цена"/>
                </div>
                <button onClick={async () => await searchProducts()}>Найти</button>
            </div>
            {
                products.length > 0 &&
                products.map(product => <ProductCard key={product.id} productDto={product}/>)
            }
        </>
    )
}



export default ProductsList;