import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getProductById } from "../helpers/products";


const Product = () => {
    const {id} = useParams();
    const [product, setProduct] = useState({});

    useEffect(() => {
        fetchProduct();
    });
    
    const fetchProduct = async () => {
        setProduct(await getProductById(id));
    }

    return (
        <div>
            <div>
                <div>
                    {product?.imagePaths?.length > 0 
                        && product.imagePaths.map((imagePath, index) => <div key={index}>{imagePath}</div>/*<img  alt='product' src={imagePath}/>*/)}
                </div>
                <div>
                    <div>{product?.name}</div>
                    <div>Цена: {product?.price}</div>
                    <div>Описание: {product?.description}</div>
                </div>
            </div>
            <div>
                <div>Отзывы продавца:</div>
                {
                    product?.user?.ratings?.length > 0
                    ? product.user.ratings.map((rating, index) => <div key={index}>Кол-во звёзд: {rating.starsCount}<br/>Отзыв: {rating.comment}</div>)
                    : <div>У продавца нет отзывов</div>
                }
            </div>
        </div>
    )
}

export default Product;
