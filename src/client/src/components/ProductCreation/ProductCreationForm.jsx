import React, { useState } from "react";
import { createProduct } from "../../helpers/products";
import styles from "./ProductCreationForm.module.css"
import Button from "../UI/Button/Button"


const ProductCreationForm = () => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');

    return (
        <form className={styles.form} onSubmit={async e => {
            e.preventDefault();
            await createProduct({
                name,
                description,
                price
            });
        }}>
            <div className={styles.inputs}>
                <input className={styles.input} onChange={e => setName(e.target.value)} name="Name" placeholder="Название товара"/>
                <textarea className={styles.textarea} onChange={e => setDescription(e.target.value)} name="Description" placeholder="Описание товара"/>
                <input className={styles.input} onChange={e => setPrice(e.target.value)} name="Price" placeholder="Цена"/>
            </div>
            <input type="file" accept="image/*" multiple />
            <Button classNames={styles.button}>Создать товар</Button>
        </form>
    )
}

export default ProductCreationForm;