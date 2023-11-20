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
    const result = await response.json();
    const payload = JSON.parse(atob(result.token.accessToken.split('.')[1]));

    console.log(payload);
    const exp = new Date(payload.exp * 1000).toUTCString();

    localStorage.setItem('id', payload.Id);
    localStorage.setItem('isAdmin', payload.role.includes('admin'));

    document.cookie = `accessToken=${result.token.accessToken}; path=/; expires=${exp}`;
    console.log('success sign in');
    return true;
  }
  
  console.log(response.error);
  console.log("sign in error");
  return false;

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

export const isAdmin = () => {
  return localStorage.getItem('isAdmin') === 'true';
}