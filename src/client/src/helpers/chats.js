import { getAccessToken } from "./auth";
import { createQuery, server } from "./shared"

export const getChats = async (queryParams) => {
    let queryStr = createQuery(`${server}/chats`, queryParams);
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
    let response = await fetch(`${server}/chats/${id}`, {
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

export const sendMessage = async (userId) => {
    
}