import React, { useState } from "react";
import { createProduct } from "../../helpers/products";
import styles from "./ProductCreationForm.module.css"
import Button from "../UI/Button/Button"
import FilesInput from "../UI/FilesInput/FilesInput";
import { trySendAuthorizedRequest } from "../../helpers/auth";


const ProductCreationForm = () => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [images, setImages] = useState([]);

    return (
        <form id={styles.form} onSubmit={async e => {
            e.preventDefault();
            if (await trySendAuthorizedRequest(createProduct, {name, description, price, images}))
                window.location.reload();
        }}>
            <div className={styles.inputs}>
                <input className={styles.input} onChange={e => setName(e.target.value)} name="Name" placeholder="Название товара" />
                <textarea className={styles.textarea} onChange={e => setDescription(e.target.value)} name="Description" placeholder="Описание товара" />
                <input className={styles.input} onChange={e => setPrice(e.target.value)} name="Price" placeholder="Цена" />
            </div>
            <FilesInput uploadedFiles={images} setUploadedFiles={setImages} />
            <Button classNames={styles.button}>
                Создать товар
            </Button>
        </form>
    )
}

export default ProductCreationForm;