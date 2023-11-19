import { getAccessToken } from "./auth";
import { createQuery, api } from "./shared"

export const getChats = async (queryParams) => {
    console.log('get chats');
    let queryStr = createQuery(`${api}/chats`, queryParams);

    console.log(queryStr);
    let response = await fetch(queryStr, {
        method : 'GET',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${getAccessToken()}`
        }
    });

    if(response.ok) {
        console.log('get chats success');
        return response;
    } else {
        console.log('get chats error');
    }

    return [];
}

export const getChatById = async (id) => {
    let response = await fetch(`${api}/chats/${id}`, {
        method : 'GET',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${getAccessToken()}`
        }
    });

    if(response.ok) {
        console.log('get chat by id success');
        let chat = await response.json();
        return chat;
    } else {
        console.log('get chat by id error');
    }
}

export const sendMessage = async (userId, message) => {
    let response = await fetch(`${api}/users/${userId}/messages`, {
        method: 'POST',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${getAccessToken()}`
        },
        body: JSON.stringify(message)
    });

    if(response.ok) {
        console.log('send message success');
    } else {
        console.log('send message error');
    }
}

export const getLastMessageTime = (message) => {
    return new Date(message.creationDate)
        .toLocaleString()
        .split(', ')[1]
        .split(':')
        .slice(0, 2)
        .join(':');
}