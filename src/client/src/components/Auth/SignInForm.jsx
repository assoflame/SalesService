import React, { useState } from "react";
import { signIn } from "../../helpers/auth";
import styles from "./AuthForm.module.css"


const SignInForm = () => {
    const [form, setForm] = useState({
        email : '',
        password : ''
    });

    return (
        <form className={styles.form} onSubmit={async (e) => {
            e.preventDefault();
            await signIn(form);
        }}>
            <div className={styles.authInputs}>
                <input className={styles.authInput} name="Email" placeholder='Почта' onChange={(e) => setForm({...form, email : e.target.value})}/>
                <input className={styles.authInput} name="Password" type="password" placeholder='Пароль' onChange={(e) => setForm({...form, password : e.target.value})}/>
            </div>
            <div className={styles.authButtons}>
                <button className={styles.authButton} type="submit">Войти в аккаунт</button>
            </div>
        </form>
    )
}

export default SignInForm;