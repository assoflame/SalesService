import React, { useState } from "react";
import styles from "./ModalReview.module.css"


const ModalReview = ({ sellerId }) => {
    const [review, setReview] = useState('');
    const [starsCount, setStarsCount] = useState(5);

    return (
        <div>
            <div>
                <label htmlFor="">Оценка</label>
                <textarea onChange={e => setStarsCount(e.target.value)}>{starsCount}</textarea>
            </div>
            <div>
                <label>Комментарий</label>
                <textarea onChange={e => setReview(e.target.value)}>
                    {review}
                </textarea>
            </div>
            <button onClick={() => sendMessage(sellerId)}>Отправить</button>
        </div>
    )
}

export default ModalReview;