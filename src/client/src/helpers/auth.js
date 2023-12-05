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
    const payload = JSON.parse(atob(result.accessToken.split('.')[1]));

    console.log(new Date(payload.exp * 1000).toUTCString());
    localStorage.setItem('id', payload.Id);
    localStorage.setItem('isAdmin', payload.role.includes('admin'));

    document.cookie = `token=${JSON.stringify(result)}; path=/;`;

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

export const logout = () => {
  document.cookie = `token=; path=/; expires=${new Date(Date.now()).toUTCString()}`;
  localStorage.removeItem('id');
}

export const loggedIn = () => {
  return localStorage.getItem('id');
}

export const isAdmin = () => {
  return localStorage.getItem('isAdmin') === 'true';
}

export const getAccessToken = async () => {
  let cookies = document.cookie.split('; ');
  for (let i = 0; i < cookies.length; ++i) {
    if (cookies[i].startsWith('token')) {
      const token = JSON.parse(cookies[i].slice('token'.length + 1));
      const payload = JSON.parse(atob(token.accessToken.split('.')[1]));
      const expTime = new Date(payload.exp * 1000).toUTCString();

      if (new Date(Date.now()).toUTCString() >= expTime) {
        return await getNewToken(token);
      }

      return token.accessToken;
    }

    return '';
  }
}

const getNewToken = async (token) => {
  const response = await fetch(`${api}/auth/refresh`, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(token)
  });

  if (response.ok) {
    const result = await response.json();
    document.cookie = `token=${JSON.stringify(result)}; path=/;`;
    return result.accessToken;
  }

  return '';
}