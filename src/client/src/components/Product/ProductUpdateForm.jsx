import React, { useState } from "react";
import { updateProduct } from "../../api/products";
import styles from "./ProductCreationForm.module.css"
import Button from "../UI/Button/Button"
import { trySendAuthorizedRequest } from "../../api/auth";


const ProductCreationForm = ({product}) => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');

    return (
        <form id={styles.form} onSubmit={async e => {
            e.preventDefault();
            if (await trySendAuthorizedRequest(updateProduct, 
                {productId: product.id, productUpdateDto: {name, description, price}}))
                window.location.reload();
        }}>
            <div className={styles.inputs}>
                <input className={styles.input} onChange={e => setName(e.target.value)} name="Name" placeholder="Название" />
                <textarea className={styles.textarea} onChange={e => setDescription(e.target.value)} name="Description" placeholder="Описание" />
                <input className={styles.input} onChange={e => setPrice(e.target.value)} name="Price" placeholder="Цена" />
            </div>
            <Button classNames={styles.button}>
                Изменить товар
            </Button>
        </form>
    )
}

export default ProductCreationForm;