import React, { useState } from 'react';
import { sendMessage } from '../../helpers/chats';
import styles from "./ModalMessage.module.css"
import Button from "../UI/Button/Button"
import { trySendAuthorizedRequest } from '../../helpers/auth';


const ModalMessage = ({ sellerId }) => {

    const [message, setMessage] = useState('');

    return (
        <form className={styles.form}
            onSubmit={async (e) => {
                e.preventDefault();
                if(await trySendAuthorizedRequest(sendMessage, {userId: sellerId, message}))
                    window.location.reload();
            }}>
            <textarea placeholder='Сообщение...' className={styles.textarea}
                onChange={(e) => setMessage(e.target.value)}/>
            <Button classNames={styles.button}>Отправить</Button>
        </form>
    )
}

export default ModalMessage;
