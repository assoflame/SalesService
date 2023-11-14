import React, { useEffect, useState } from "react";
import { getUsers } from "../helpers/users";


const UsersList = () => {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        setUsers([...await getUsers()]);
    }

    return (
        <>
        {
            users.length > 0
            ? users.map(user => <div key={user.id}>{user.fullName}</div>)
            : <div>Список пуст</div>
        }
        </>
    )
}


export default UsersList;