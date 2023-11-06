import { server } from "./shared";

export const signIn = async (signInDto) => {
  console.log(document.cookie);
    let response = await fetch(`${server}/auth/signin`, {
      method: "POST",
      headers: {
        "Content-Type" : "application/json"
      },
      body: JSON.stringify(signInDto)
    });

    if(response.ok){
      let result = await response.json();
      document.cookie = `accessToken=${result.token.accessToken}; expires=${new Date(Date.now() + 86400 * 1000).toUTCString()}`;
      console.log('success sign in');
      console.log(document.cookie);
    } else {
      console.log("sign in error");
    }
}

export const signUp = async (signUpDto) => {
    let response = await fetch(`${server}/auth/signup`, {
        method: "POST",
        headers: {
            "Content-Type" : "application/json"
        },
        body: JSON.stringify(signUpDto)
    });

    if(response.ok) {
        console.log('success sign up');
    } else {
        console.log('sign up error');
    }
}

export const getAccessToken = () => {
  let cookies = document.cookie.split('; ');
    for (let i = 0; i < cookies.length; ++i)
    {
      if(cookies[i].startsWith('accessToken')) {
        return cookies[i].split('=')[1];
      }
    }

    return '';
}
