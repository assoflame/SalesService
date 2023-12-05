import { getAccessToken } from "./auth";
import { api } from "./shared";

export const getUser = async (id) => {
    let response = await fetch(`${api}/admin/users/${id}`, {
        method : 'GET',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${await getAccessToken()}`
        }
    });

    if(response.ok) {
        console.log('get user by id success');
        let user = await response.json();
        return user;
    } else {
        console.log('get user by id error');
    }
}

export const getUsers = async () => {
    let response = await fetch(`${api}/admin/users`, {
        method : 'GET',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${await getAccessToken()}`
        }
    });

    if(response.ok) {
        console.log('get users success');
        let users = await response.json();
        console.log(users);
        return users;
    } else {
        console.log('get users error');
    }
}

export const sendReview = async (userId, review) => {
    let response = await fetch(`${api}/users/${userId}/reviews`, {
        method : 'POST',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${await getAccessToken()}`
        },
        body: JSON.stringify(review)
    });

    if(response.ok) {
        console.log('rate user success');
    } else {
        console.log('rate user error');
    }
}