import { React, useEffect, useState } from "react";
import { usePagination } from "../hooks/usePagination";
import { getChats } from "../helpers/chats";
import { useFetching } from "../hooks/useFetching";
import Loader from "./UI/Loader/Loader";
import { getPagesCount } from "../helpers/shared";
import Modal from "./UI/Modal/Modal";
import Chat from "./Chat/Chat";


const ChatsList = () => {
    const [chats, setChats] = useState([]);
    const [queryParams, setQueryParams] = useState({
        pageNumber: 1,
        pageSize: 10
    });
    const [totalPages, setTotalPages] = useState(0);
    const pages = usePagination(totalPages);

    const [fetchChats, isChatsLoading, fetchChatsError] = useFetching(async () => {
        const response = await getChats(queryParams);
        setChats([...await response.json()]);
        const totalCount = JSON.parse(response.headers.get('X-Pagination')).TotalCount;
        setTotalPages(getPagesCount(totalCount, queryParams.pageSize));
    }, () => setChats([]));

    // const [chatModal, setChatModal] = useState(true);
    const [chatId, setChatId] = useState(-1);

    useEffect(() => {
        fetchChats();
    }, []);

    return (
        <div>
            {/* <Modal visible={chatModal} setVisible={setChatModal}><Chat id={chatId}/></Modal> */}
            {fetchChatsError && <div>Ошибка загрузки чатов</div>}
            {
                isChatsLoading
                    ? <Loader />
                    : <div>
                        <div>
                            {pages?.length > 0 && pages.map(page => <span key={page}
                                onClick={() => setQueryParams({ ...queryParams, pageNumber: page })}>{page}</span>)}
                        </div>
                        <div>
                            {chats?.map(chat => <div onClick={() => setChatId(chat.id)}>{chat.firstUserId}, {chat.secondUserId}</div>)}
                        </div>

                        <Chat id={chatId}/>
                    </div>
            }
        </div>
    )
}

export default ChatsList;