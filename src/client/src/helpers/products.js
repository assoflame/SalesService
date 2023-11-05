

export const getProducts = async () => {
    let response = await fetch("http://localhost:5090/api/products");

    if(response.ok){
      let result = await response.json();
      console.log(result);
      return result;
    } else {
      console.log("get products error");
    }
}