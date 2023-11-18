import React, { useState } from "react";
import styles from "./ModalReview.module.css"
import { sendReview } from "../../helpers/users";


const ModalReview = ({ sellerId }) => {
    const [review, setReview] = useState('');
    const [starsCount, setStarsCount] = useState(0);

    return (
        <form>
            <div>
                <input placeholder="Оценка" onChange={e => setStarsCount(e.target.value)}/>
            </div>
            <div>
                <textarea placeholder="Комментарий" onChange={e => setReview(e.target.value)}/>
            </div>
            <button onClick={() => sendReview(sellerId)}>Отправить</button>
        </form>
    )
}

export default ModalReview;