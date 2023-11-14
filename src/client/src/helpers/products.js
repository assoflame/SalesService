import { getAccessToken } from "./auth";
import { createQuery, server } from "./shared";

export const getProducts = async (filter) => {
    let query = createQuery(`${server}/products`, filter);
    console.log(query);
    let response = await fetch(query);

    if(response.ok) {
      return response;
    } else {
      console.log("get products error");
    }

    return [];
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

export const getUserRatings = async (id, queryParams) => {
  let query = createQuery(`${server}/users/${id}/ratings`, queryParams);
  let response = await fetch(query);

  if(response.ok) {
    console.log('success user ratings fetch');
    return response;
  } else {
    console.log('user ratings fetch error');
  }

  return [];
}
