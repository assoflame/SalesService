import React, { useState } from "react";
import { signUp } from "../helpers/auth";

const SignUpForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [city, setCity] = useState('');
    const [age, setAge] = useState('');

    return (
        <form onSubmit={async (e) => {
            e.preventDefault();
            await signUp({
                email,
                password,
                firstName,
                lastName,
                city,
                age
            });
        }}>
            <div className="authInputs">
                <input placeholder='Email' onChange={(e) => setEmail(e.target.value)}/>
                <input type="password" placeholder='Password' onChange={(e) => setPassword(e.target.value)}/>
                <input placeholder='First Name' onChange={(e) => setFirstName(e.target.value)}/>
                <input placeholder='Last Name' onChange={(e) => setLastName(e.target.value)}/>
                <input placeholder='City' onChange={(e) => setCity(e.target.value)}/>
                <input placeholder='Age' onChange={(e) => setAge(e.target.value)}/>
            </div>
            <div className="authButton">
                <button type="submit">Sign Up</button>
            </div>
        </form>
    )
}

export default SignUpForm;