import React, { useState } from 'react';
import { sendMessage } from '../../helpers/chats';
import styles from "./ModalMessage.module.css"
import Button from "../UI/Button/Button"


const ModalMessage = ({ sellerId }) => {

    const [message, setMessage] = useState('');

    return (
        <form className={styles.form}
            onSubmit={(e) => {
                e.preventDefault();
                sendMessage(sellerId, {body: message});
            }}>
            <textarea placeholder='Сообщение...' className={styles.textarea}
                onChange={(e) => setMessage(e.target.value)}/>
            <Button classNames={styles.button}>Отправить</Button>
        </form>
    )
}

export default ModalMessage;
