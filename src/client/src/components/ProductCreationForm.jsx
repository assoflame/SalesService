import React, { useState } from "react";
import { createProduct } from "../helpers/products";


const ProductCreationForm = () => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');

    return (
        <form onSubmit={async e => {
            e.preventDefault();
            await createProduct({
                name,
                description,
                price
            });
        }}>
            <div>
                <input onChange={e => setName(e.target.value)} name="Name" placeholder="Название товара"/>
                <input onChange={e => setDescription(e.target.value)} name="Description" placeholder="Описание товара"/>
                <input onChange={e => setPrice(e.target.value)} name="Price" placeholder="Цена"/>
            </div>
            <div>
                <button>Создать товар</button>
            </div>
        </form>
    )
}


export default ProductCreationForm;