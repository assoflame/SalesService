import React, { useState } from 'react';
import { sendMessage } from '../../helpers/chats';


const ModalMessage = ({ sellerId }) => {

    const [message, setMessage] = useState('');

    return (
        <div>
            <textarea onChange={e => setMessage(e.target.value)}>
                {message}
            </textarea>
            <button onClick={() => sendMessage(sellerId)}>Отправить</button>
        </div>
    )
}

export default ModalMessage;
