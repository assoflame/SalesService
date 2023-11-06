import React, { useState } from "react";
import { signIn } from "../helpers/auth";


const SignInForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    return (
        <form onSubmit={async (e) => {
            e.preventDefault();
            await signIn({email, password});
        }}>
            <div className="authInputs">
                <input name="Email" placeholder='Email' onChange={(e) => setEmail(e.target.value)}/>
                <input name="Password" type="password" placeholder='Password' onChange={(e) => setPassword(e.target.value)}/>
            </div>
            <div className="authButton">
                <button type="submit">Sign In</button>
            </div>
        </form>
    )
}

export default SignInForm;