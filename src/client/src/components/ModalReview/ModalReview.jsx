import React, { useState } from "react";
import styles from "./ModalReview.module.css"
import { sendReview } from "../../helpers/users";
import Button from "../UI/Button/Button"


const ModalReview = ({ sellerId }) => {
    const [review, setReview] = useState('');
    const [starsCount, setStarsCount] = useState(0);

    return (
        <form className={styles.form}>
            <div className={styles.textContainer}>
                <textarea className={styles.starsCount} placeholder="Количество звёзд (1-5)" onChange={e => setStarsCount(e.target.value)} />
                <textarea className={styles.comment} placeholder="Комментарий" onChange={e => setReview(e.target.value)} />
            </div>
            <Button classNames={styles.button} callback={() => sendReview(sellerId, { starsCount, comment: review })}>Отправить</Button>
        </form>
    )
}

export default ModalReview;