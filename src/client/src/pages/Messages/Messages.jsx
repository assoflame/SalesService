import React from "react";
import Header from "../../components/UI/Header/Header";
import Menu from "../../components/UI/Menu/Menu";
import Logout from "../../components/Auth/Logout";



const Messages = () => {
    return (
        <>
            <Header><Logout/></Header>
            <Menu/>
            
        </>
    )
}

export default Messages;