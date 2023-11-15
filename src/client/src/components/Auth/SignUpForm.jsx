import React, { useState } from "react";
import { signUp } from "../../helpers/auth";
import styles from "./AuthForm.module.css"

const SignUpForm = () => {
    const [form, setForm] = useState({
        email : '',
        password : '',
        firstName : '',
        lastName : '',
        city : '',
        age : ''
    });

    return (
        <form className={styles.form} onSubmit={async (e) => {
            e.preventDefault();
            await signUp(form);
        }}>
            <div className={styles.authInputs}>
                <input className={styles.authInput} name="FirstName" placeholder='Имя' onChange={(e) => setForm({...form, firstName : e.target.value})}/>
                <input className={styles.authInput} name="LastName" placeholder='Фамилия' onChange={(e) => setForm({...form, lastName : e.target.value})}/>
                <input className={styles.authInput} name="City" placeholder='Город' onChange={(e) => setForm({...form, city : e.target.value})}/>
                <input className={styles.authInput} name="Age" placeholder='Возраст' onChange={(e) => setForm({...form, age : e.target.value})}/>
                <input className={styles.authInput} name="Email" placeholder='Почта' onChange={(e) => setForm({...form, email : e.target.value})}/>
                <input className={styles.authInput} name="Password" type="password" placeholder='Пароль' onChange={(e) => setForm({...form, password : e.target.value})}/>
            </div>
            <div className={styles.authButtons}>
                <button className={styles.authButton} type="submit">Создать аккаунт</button>
            </div>
        </form>
    )
}

export default SignUpForm;