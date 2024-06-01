import { api } from "./shared";

export const signIn = async (signInDto) => {
  let response = await fetch(`${api}/auth/signin`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(signInDto),
    credentials: "include"
  });

  if (response.ok) {
    const result = await response.json();
    localStorage.setItem('id', result.userId);
    localStorage.setItem('isAdmin', result.roles.includes('admin'));
    localStorage.setItem('tokenExpired', result.tokenExpired)
    return true;
  }

  return false;
}

export const signUp = async (signUpDto) => {
  let response = await fetch(`${api}/auth/signup`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(signUpDto)
  });

  return response.ok;
}

export const logout = async () => {
  await fetch(`${api}/auth/logout`, {
    method: "POST",
    credentials: 'include'
  });
  localStorage.removeItem('id')
  localStorage.removeItem('isAdmin')
  localStorage.removeItem('tokenExpired')
}

export const trySendAuthorizedRequest = async (request, requestParams) => {
  const expTime = localStorage.getItem('tokenExpired')
  console.log(new Date(expTime).toUTCString())
  console.log(new Date(Date.now()).toUTCString())

  if(new Date(expTime).toUTCString() <= new Date(Date.now()).toUTCString()) {
    await tryRefreshToken();
  }

  return await request(requestParams);
}

export const tryRefreshToken = async () => {
  let refreshResult = await fetch(`${api}/auth/refresh`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include"
  })

  if(refreshResult.ok) {
    const tokenExpiredTime = (await refreshResult.json()).tokenExpired;
    localStorage.setItem('tokenExpired', tokenExpiredTime)
    return true;
  }

  await logout();
  return false;
}

export const loggedIn = () => {
  return localStorage.getItem('id');
}

export const isAdmin = () => {
  return localStorage.getItem('isAdmin') === 'true';
}
