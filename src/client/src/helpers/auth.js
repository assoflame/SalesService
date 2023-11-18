import { api } from "./shared";

export const signIn = async (signInDto) => {
  let response = await fetch(`${api}/auth/signin`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(signInDto)
  });

  if (response.ok) {
    let result = await response.json();
    document.cookie = `accessToken=${result.token.accessToken}; path=/; expires=${new Date(Date.now() + 86400 * 1000).toUTCString()}`;
    console.log('success sign in');
  } else {
    console.log("sign in error");
  }
}

export const signUp = async (signUpDto) => {
  let response = await fetch(`${api}/auth/signup`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(signUpDto)
  });

  if (response.ok) {
    console.log('success sign up');
  } else {
    console.log('sign up error');
  }
}

export const getAccessToken = () => {
  let cookies = document.cookie.split('; ');
  for (let i = 0; i < cookies.length; ++i) {
    if (cookies[i].startsWith('accessToken')) {
      return cookies[i].split('=')[1];
    }
  }

  return '';
}

export const logout = () => {
  document.cookie = `accessToken=; path=/; expires=${new Date(Date.now()).toUTCString()}`;
}

export const loggedIn = () => {
  return getAccessToken() !== '';
}