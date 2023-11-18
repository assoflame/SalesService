import { getAccessToken } from "./auth";
import { createQuery, api } from "./shared";

export const getProducts = async (filter) => {
    let query = createQuery(`${api}/products`, filter);
    let response = await fetch(query);

    if(response.ok) {
      return response;
    } else {
      console.log("get products error");
    }

    return [];
}

export const getProductById = async (productId) => {
  let response = await fetch(`${api}/products/${productId}`);

  if(response.ok) {
    let product = await response.json();
    console.log(product);
    return product;
  } else {
    console.log("get product by id error");
  }
}

export const createProduct = async (product) => {
  console.log(getAccessToken());
  console.log(product);
  let formData = new FormData();
  formData.append('Name', product.name);
  formData.append('Description', product.description);
  formData.append('Price', product.price);
  product.images.forEach(image => formData.append('Images', image));

  console.log([...formData]);

  let response = await fetch(`${api}/products`, {
    method: "POST",
    headers: {
      'Authorization' : `Bearer ${getAccessToken()}`
    },
    body: formData
  });

  if(response.ok) {
    console.log('success product creation');
  } else {
    console.log('product creation error');
  }
}

export const getUserRatings = async (id, queryParams) => {
  let query = createQuery(`${api}/users/${id}/ratings`, queryParams);
  let response = await fetch(query);

  if(response.ok) {
    console.log('success user ratings fetch');
    return response;
  } else {
    console.log('user ratings fetch error');
  }

  return [];
}
