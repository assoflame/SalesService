import React, { useState } from "react";
import { signIn } from "../../api/auth";
import styles from "./AuthForm.module.css"
import { NavLink, useNavigate } from "react-router-dom";


const SignInForm = () => {
    const [form, setForm] = useState({
        email : '',
        password : ''
    });

    const navigate = useNavigate();
    const productsPage = '/products';

    return (
        <form className={styles.form} onSubmit={async (e) => {
            e.preventDefault();
            if(await signIn(form)){
                navigate(productsPage, {replace : true});
            }
        }}>
            <div className={styles.authInputs}>
                <input className={styles.authInput} name="Email" placeholder='Почта'
                 onChange={(e) => setForm({...form, email : e.target.value})}/>
                <input className={styles.authInput} name="Password" type="password" 
                placeholder='Пароль' onChange={(e) => setForm({...form, password : e.target.value})}/>
            </div>
            <div className={styles.authButtons}>
                <button className={styles.authButton} type="submit">Войти в аккаунт</button>
                <NavLink className={styles.link} to="/signup">Создать аккаунт</NavLink>
            </div>
        </form>
    )
}

export default SignInForm;

