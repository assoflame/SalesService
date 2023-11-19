import React, { useEffect } from "react";
import styles from "./ChatCard.module.css"
import { getLastMessageTime } from "../../helpers/chats";



const ChatCard = ({ chat, setChatId }) => {

    const getUserName = () => {
        return window.localStorage.getItem('id') === String(chat.firstUser.id)
            ? chat.secondUser.fullName
            : chat.firstUser.fullName;
    }

    return (
        <div className={styles.card} onClick={() => setChatId(chat.id)}>
            <h1 className={styles.userName}>{getUserName()}</h1>
            <div className={styles.lastMessage}>
                <div>{chat.messages[chat.messages.length - 1].body}</div>
                <div>{getLastMessageTime(chat.messages[0])}</div>
            </div>
        </div>
    )
}


export default ChatCard;