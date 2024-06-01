import { createQuery, api } from "./shared"

export const getChats = async (queryParams) => {
    let queryStr = createQuery(`${api}/chats`, queryParams);
    let response = await fetch(queryStr, {
        method : 'GET',
        headers : { "Content-Type" : "application/json" },
        credentials: "include"
    });

    if(response.ok)
        return response

    return [];
}

export const getChatById = async (id) => {
    let response = await fetch(`${api}/chats/${id}`, {
        method : 'GET',
        headers : { "Content-Type" : "application/json" },
        credentials: "include"
    });

    if(response.ok) {
        return await response.json();
    }

    return null
}

export const sendMessage = async ({userId, message}) => {
    console.log(userId)
    console.log(message)
    let response = await fetch(`${api}/users/${userId}/messages`, {
        method: 'POST',
        headers : { "Content-Type" : "application/json" },
        body: JSON.stringify({body: message}),
        credentials: "include"
    });
    
    return response.ok;
}

export const getLastMessageTime = (message) => {
    return new Date(message.creationDate)
        .toLocaleString()
        .split(', ')[1]
        .split(':')
        .slice(0, 2)
        .join(':');
}