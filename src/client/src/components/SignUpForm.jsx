import React, { useState } from "react";
import { signUp } from "../helpers/auth";

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
        <form onSubmit={async (e) => {
            e.preventDefault();
            await signUp(form);
        }}>
            <div className="authInputs">
                <input name="Email" placeholder='Email' onChange={(e) => setForm({...form, email : e.target.value})}/>
                <input name="Password" type="password" placeholder='Password' onChange={(e) => setForm({...form, password : e.target.value})}/>
                <input name="FirstName" placeholder='First Name' onChange={(e) => setForm({...form, firstName : e.target.value})}/>
                <input name="LastName" placeholder='Last Name' onChange={(e) => setForm({...form, lastName : e.target.value})}/>
                <input name="City" placeholder='City' onChange={(e) => setForm({...form, city : e.target.value})}/>
                <input name="Age" placeholder='Age' onChange={(e) => setForm({...form, age : e.target.value})}/>
            </div>
            <div className="authButton">
                <button type="submit">Sign Up</button>
            </div>
        </form>
    )
}

export default SignUpForm;