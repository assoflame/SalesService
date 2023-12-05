import React from "react";
import ProductCard from "./ProductCard";
import styles from "./ProductsList.module.css"

const ProductsList = ({ products }) => {

    return (
        <div className={styles.productsList}>
            {products.map(product => 
                <ProductCard key={product.id} product={product} />
            )}
        </div>
    )
}

export default ProductsList;
