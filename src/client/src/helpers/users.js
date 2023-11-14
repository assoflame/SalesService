import { getAccessToken } from "./auth";
import { server } from "./shared";

export const getUser = async (id) => {
    let response = await fetch(`${server}/admin/users/${id}`, {
        method : 'GET',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${getAccessToken()}`
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
    let response = await fetch(`${server}/admin/users`, {
        method : 'GET',
        headers : {
            "Content-Type" : "application/json",
            'Authorization' : `Bearer ${getAccessToken()}`
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