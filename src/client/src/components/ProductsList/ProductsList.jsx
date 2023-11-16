import React, { useEffect } from "react";
import ProductCard from "../ProductCard/ProductCard";
import styles from "./ProductsList.module.css"
import { Link } from "react-router-dom";

const ProductsList = ({ products }) => {

    return (
        <div className={styles.productsList}>
            {products.map(product => <Link className={styles.link} to={`/products/${product.id}`} key={product.id}>
                <ProductCard product={product} />
            </Link>)}
        </div>
    )
}

export default ProductsList;
