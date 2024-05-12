import { React, useEffect, useState } from "react";
import { usePagination } from "../../hooks/usePagination";
import { getChats } from "../../helpers/chats";
import { useFetching } from "../../hooks/useFetching";
import Loader from "../UI/Loader/Loader";
import { getPagesCount } from "../../helpers/shared";
import PageNumbersList from "../UI/Paging/PageNumbersList/PageNumbersList";
import styles from "./ChatsList.module.css";
import ChatCard from "./ChatCard";
import { trySendAuthorizedRequest } from "../../helpers/auth";


const ChatsList = ({className, setChatId}) => {
    const [chats, setChats] = useState([]);
    const [queryParams, setQueryParams] = useState({
        pageNumber: 1,
        pageSize: 5
    });
    const [totalPages, setTotalPages] = useState(0);
    const pages = usePagination(totalPages);

    const [fetchChats, isChatsLoading, fetchChatsError] = useFetching(async () => {
        const response = await trySendAuthorizedRequest(getChats, queryParams)
        setChats([...await response.json()]);
        const totalCount = JSON.parse(response.headers.get('X-Pagination')).TotalCount;
        setTotalPages(getPagesCount(totalCount, queryParams.pageSize));
    }, () => setChats([]));


    useEffect(() => {
        fetchChats();
        const interval = setInterval(() => fetchChats(), 60 * 1000);

        return () => clearInterval(interval);
    }, [queryParams.pageNumber]);

    return (
        <div className={[className,styles.container].join(' ')}>
            {fetchChatsError && <div>Ошибка загрузки чатов</div>}
            {
                isChatsLoading
                    ? <Loader/>
                    : <div className={styles.chats}>
                        <div className={styles.chatsList}>
                            {chats.map(chat => <ChatCard key={chat.id} chat={chat} setChatId={setChatId}/>)}
                        </div>
                        <PageNumbersList
                            pages={pages}
                            activePage={queryParams.pageNumber}
                            setPage={(page) => setQueryParams({ ...queryParams, pageNumber: page })} />
                    </div>
            }
        </div>
    )
}

export default ChatsList;