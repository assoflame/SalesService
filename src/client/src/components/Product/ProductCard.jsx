import React, { useEffect } from "react";
import styles from "./ProductCard.module.css"
import { server } from "../../helpers/shared"



const ProductCard = ({ product }) => {

    useEffect(() => {
        console.log(product);

    }, [])

    const getImagePath = () => {
        return product.imagePaths.length > 0
            ? `${server}${product.imagePaths[0]}`
            : `${server}/images/default/default_product.png`;
    }

    const getDescription = () => {
        const shortDescriptionLegth = 100;
        return product.description.length > shortDescriptionLegth
            ? product.description.slice(0, shortDescriptionLegth).concat('...')
            : product.description;
    }

    return (
        <div className={styles.card}>
                <img className={styles.image} src={getImagePath()} alt="" />
            <div className={styles.info}>
                <div className={styles.firstInfo}>
                    <div className={styles.name}>{product.name}</div>
                    <div className={styles.desctiption}>{getDescription()}</div>
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
