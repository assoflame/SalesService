import React from "react";
import Button from "../UI/Button/Button";
import { logout } from "../../helpers/auth";
import { useNavigate } from "react-router-dom";

const Logout = ({ classNames }) => {
    const navigate = useNavigate();
    const signInPage = '/signin';

    return (
        <Button classNames={classNames} callback={() => {
            logout();
            navigate(signInPage, { replace: true })
        }}>Выйти</Button>
    )
}

export default Logout;