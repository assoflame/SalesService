import React from "react";
import { loggedIn } from "../../helpers/auth";
import { Navigate } from "react-router-dom";



const RequireAuth = ({children}) => {
    
    if(!loggedIn()) {
        return <Navigate to='/signin'/>
    }

    return children;
}

export default RequireAuth;