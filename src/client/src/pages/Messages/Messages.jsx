import React, { useEffect, useState } from "react";
import Header from "../../components/UI/Header/Header";
import Menu from "../../components/UI/Menu/Menu";
import Logout from "../../components/Auth/Logout";
import ChatsList from "../../components/Messages/ChatsList";
import styles from "./Messages.module.css"
import Chat from "../../components/Chat/Chat"



const Messages = () => {
    const [chatId, setChatId] = useState(0);

    return (
        <>
            <Header><Logout/></Header>
            <Menu/>
            <div className={styles.container}>
                <ChatsList className={styles.chats} setChatId={setChatId}/>
                <Chat className={styles.chat} chatId={chatId}/>
            </div>
        </>
    )
}

export default Messages;