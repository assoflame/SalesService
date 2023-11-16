import React from "react";
import Button from "../UI/Button/Button";
import { logout } from "../../helpers/auth";
import { useNavigate } from "react-router-dom";
import styles from './Logout.module.css'

const Logout = () => {
    const navigate = useNavigate();
    const signInPage = '/signin';

    return (
        <Button classNames={styles.button} callback={() => {
            logout();
            navigate(signInPage, { replace: true })
        }}>Выйти</Button>
    )
}

export default Logout;