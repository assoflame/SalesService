import React, { useEffect } from "react";
import styles from "./ProductCard.module.css"


const ProductCard = ({ product }) => {

    return (
        <div className={styles.card}>
                <img className={styles.image} src="http://localhost:5090/images/avatars/default_avatar.jpg" alt="" />
            <div className={styles.info}>
                <div className={styles.firstInfo}>
                    <div className={styles.name}>{product.name}</div>
                    <div className={styles.desctiption}>{product.description.slice(0, Math.min(product.description.length, 25))}</div>
                </div>
                <div className={styles.secondInfo}>
                    <div className={styles.price}>Цена: {product.price + ' руб.'}</div>
                    <div className={styles.datetime}>{new Date(product.creationDate).toLocaleString()}</div>
                </div>
            </div>
        </div>
    )
}

export default ProductCard;
