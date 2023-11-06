import { getAccessToken } from "./auth";
import { server } from "./shared";

export const getProducts = async () => {
    let response = await fetch(`${server}/products`);

    if(response.ok) {
      let result = await response.json();
      console.log(result);
      return result;
    } else {
      console.log("get products error");
    }
}

export const getProductById = async (productId) => {
  let response = await fetch(`${server}/products/${productId}`);

  if(response.ok) {
    let product = await response.json();
    console.log(product);
    return product;
  } else {
    console.log("get product by id error");
  }
}

export const createProduct = async (product) => {
  let response = await fetch(`${server}/products`, {
    method: "POST",
    headers: {
      "Content-Type" : "application/json",
      'Authorization' : `Bearer ${getAccessToken()}`
    },
    body: JSON.stringify(product)
  });

  if(response.ok) {
    console.log('success product creation');
  } else {
    console.log('product creation error');
  }
}

