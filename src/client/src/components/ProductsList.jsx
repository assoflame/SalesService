import React from "react";
import ProductCard from "./ProductCard";


const ProductsList = ({ products }) => {
    return (
        <div>
            {products.map(product => <div key={product.id}><ProductCard product={product} /><br /></div>)}
        </div>
    )
}

export default ProductsList;
