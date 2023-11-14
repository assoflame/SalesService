import React, { useEffect, useState } from "react";
import styles from "./Chat.module.css"
import { useFetching } from "../../hooks/useFetching";
import { getChatById } from "../../helpers/chats";
import Loader from "../UI/Loader/Loader";


const Chat = ({id}) => {
    const [chat, setChat] = useState({});

    const [fecthChat, isChatLoading, fetchChatError] = useFetching(async () => {
        setChat(await getChatById(id));
    }, () => setChat({}));

    useEffect(() => {
        fecthChat();
    }, [id])

    return (
        <div>
            {fetchChatError && <div>Ошибка загрузки чата</div>}
            {
                isChatLoading
                ? <Loader/>
                : 
                <div>
                    <div>
                        {chat?.messages?.map(message => <div>{message}</div>)}
                    </div>
                    <div>
                        <textarea></textarea>
                        <button>Отправить сообщение</button>
                    </div>
                </div>
            }
        </div>
    )
}


export default Chat;
