import React, { useEffect, useRef, useState } from "react";
import { getChatById, getLastMessageTime, sendMessage } from "../../helpers/chats";
import Button from "../UI/Button/Button"
import styles from "./Chat.module.css"
import { useFetching } from "../../hooks/useFetching";


const Chat = ({ className, chatId }) => {
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);
    const [chat, setChat] = useState({});

    const [fetchChat, isChatLoading, fetchChatError] = useFetching(async () => {
        let freshChat = await getChatById(chatId);
        setChat(freshChat);
        setMessages(freshChat.messages);
    }, () => setChat({}));

    const messageRef = useRef();

    useEffect(() => {
        if (chatId > 0) {
            getChat();
            const interval = setInterval(async () => {
                getChat();
            },
                30 * 1000);

            return () => clearInterval(interval);
        }
    }, [chatId]);

    const getChat = async () => {
        await fetchChat();
        scrollToEnd();
    }

    const scrollToEnd = () => {
        if (messageRef && messageRef.current) {
            const { scrollHeight, clientHeight } = messageRef.current;
            messageRef.current.scrollTo({
                left: 0, top: scrollHeight - clientHeight,
                behavior: 'smooth'
            });
        }
    }

    const getMessageSender = (message) => {
        return message.userId === chat.firstUser.id
            ? chat.firstUser.fullName.split(' ')[0]
            : chat.secondUser.fullName.split(' ')[0];
    }

    const isMainSender = (message) => {
        return String(message.userId) === window.localStorage.getItem('id');
    }

    const getOtherUser = () => {
        return String(chat.firstUser.id) === window.localStorage.getItem('id')
            ? chat.secondUser
            : chat.firstUser
    }

    return (
        <>
            {
                chat?.messages?.length > 0 &&
                <div className={[styles.chat, className].join(' ')}>
                    <div>{getOtherUser().fullName}</div>
                    <div className={styles.messages} ref={messageRef}>
                        {
                            messages?.length > 0 &&
                            messages.map(message =>
                                <div key={message.id}
                                    className={[styles.message, isMainSender(message) ? styles.mainSenderMessage : styles.otherSenderMessage].join(' ')}>
                                    <div className={[styles.messageBody, isMainSender(message) ? styles.mainSenderBody : styles.otherSenderBody].join(' ')}>{message.body}</div>
                                    <div className={[styles.messageSender, isMainSender(message) ? styles.mainSender : styles.otherSender].join(' ')}>{getMessageSender(message)}</div>
                                    <div className={[styles.messageTime, isMainSender(message) ? styles.mainSenderTime : styles.otherSenderTime].join(' ')}>{getLastMessageTime(message)}</div>
                                </div>
                            )
                        }
                    </div>
                    <div className={styles.messageCreation}>
                        <textarea onChange={e => setMessage(e.target.value)} value={message} placeholder="Сообщение..." className={styles.textarea} />
                        <Button callback={() => {
                            sendMessage(getOtherUser().id, { body: message });
                            setMessages([...messages,
                            {
                                body: message,
                                creationDate: Date.now(),
                                id: messages[messages.length - 1].id + 1,
                                userId: Number(localStorage.getItem('id'))
                            }])
                        }} classNames={styles.button}>Отправить</Button>
                    </div>
                </div>
            }
        </>
    )
}

export default Chat;
