import React, { useEffect, useState } from "react";
import {useParams} from "react-router-dom";
import { getUser } from "../helpers/users";


const User = () => {
    const {id} = useParams();
    const [user, setUser] = useState({fullName: ''});

    useEffect(() => {
        fetchUser();
    }, []);

    const fetchUser = async () => {
        let u = await getUser(id);
        console.log(u);
        setUser(u);
    }

    return (
        <>
            {
                <div>
                    {user.fullName}
                </div>
            }
        </>
    )
}

export default User;