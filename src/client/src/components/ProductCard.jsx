import React from "react";



const ProductCard = ({productDto}) => {
    return (
        <div>
            {/* <div>
                <img/>
            </div> */}
            <div>
                <div>{productDto.name}</div>
                <div>{productDto.description.slice(0, Math.min(productDto.description.length, 25))}</div>
                <div>Цена: {productDto.price + ' руб'}</div>
            </div>
        </div>
    )
}

export default ProductCard;