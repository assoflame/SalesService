import React, { useState } from "react";
import styles from "./ProductCard.module.css"
import { server } from "../../api/shared"
import { isAdmin, trySendAuthorizedRequest } from "../../api/auth";
import Button from "../UI/Button/Button"
import { deleteProduct } from "../../api/products";
import { Link } from "react-router-dom";
import Modal from "../UI/Modal/Modal";
import ProductUpdateForm from "./ProductUpdateForm"


const ProductCard = ({ product }) => {
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

    const [productUpdateVisible, setProductUpdateVisible] = useState(false);

    return (
        <div className={styles.card}>
            <Link className={styles.link} to={`/products/${product.id}`}>
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
            </Link>
            {localStorage["id"] == product.userId &&
                <Button classNames={styles.updateButton} callback={() => setProductUpdateVisible(true)} >Изменить продукт</Button>}

            {(isAdmin() || localStorage["id"] == product.userId) && <Button classNames={styles.deleteButton}
                callback={async () => { await trySendAuthorizedRequest(deleteProduct, product.id); window.location.reload(); }}>Удалить продукт</Button>}

            <Modal visible={productUpdateVisible} setVisible={setProductUpdateVisible}>
                <ProductUpdateForm product={product}/>
            </Modal>
        </div>
    )
}

export default ProductCard;
