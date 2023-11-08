import React, { useState } from "react";
import { signIn } from "../helpers/auth";


const SignInForm = () => {
    const [form, setForm] = useState({
        email : '',
        password : ''
    });

    return (
        <form onSubmit={async (e) => {
            e.preventDefault();
            await signIn(form);
        }}>
            <div className="authInputs">
                <input name="Email" placeholder='Email' onChange={(e) => setForm({...form, email : e.target.value})}/>
                <input name="Password" type="password" placeholder='Password' onChange={(e) => setForm({...form, password : e.target.value})}/>
            </div>
            <div className="authButton">
                <button type="submit">Sign In</button>
            </div>
        </form>
    )
}

export default SignInForm;