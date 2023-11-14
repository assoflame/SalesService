import React from "react";



const ProductCard = ({product}) => {
    return (
        <div>
            <div>
                <div>
                    <img src="" alt="" />
                </div>
                <div>{product.name}</div>
                <div>{product.description.slice(0, Math.min(product.description.length, 25))}</div>
                <div>Цена: {product.price + ' руб'}</div>
                <div>Дата создания: {product.creationDate}</div>
            </div>
        </div>
    )
}

export default ProductCard;
