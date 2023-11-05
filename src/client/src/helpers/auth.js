export const signIn = async (signInDto) => {
    let response = await fetch("http://localhost:5090/api/auth/signin", {
      method: "POST",
      headers: {
        "Content-Type" : "application/json"
      },
      body: JSON.stringify(signInDto)
    });

    if(response.ok){
      let result = await response.json();
    //   document.cookie = `accessToken=${result.accessToken}; expires=${new Date(Date.now() + 86400 * 1000).toUTCString()}`;

      console.log('success sign in');
      console.log(result.token);
    } else {
      console.log("sign in error");
    }
}

export const signUp = async (signUpDto) => {
    let response = await fetch("http://localhost:5090/api/auth/signup", {
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