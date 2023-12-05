import { getAccessToken } from "./auth";
import { createQuery, api } from "./shared";

export const getProducts = async (filter) => {
  let query = createQuery(`${api}/products`, filter);
  let response = await fetch(query);

  if (response.ok) {
    return response;
  } else {
    console.log("get products error");
  }

  return [];
}

export const getProductById = async (productId) => {
  let response = await fetch(`${api}/products/${productId}`);

  if (response.ok) {
    let product = await response.json();
    console.log(product);
    return product;
  } else {
    console.log("get product by id error");
  }
}

export const createProduct = async (product) => {
  let formData = new FormData();
  formData.append('Name', product.name);
  formData.append('Description', product.description);
  formData.append('Price', product.price);
  product.images.forEach(image => formData.append('Images', image));

  console.log([...formData]);

  let response = await fetch(`${api}/products`, {
    method: "POST",
    headers : {
      'Authorization' : `Bearer ${await getAccessToken()}`
    },
    body: formData
  });

  if (response.ok) {
    console.log('success product creation');
    return true;
  }
  console.log('product creation error');
  return false;
}

export const getUserReviews = async (id, queryParams) => {
  let query = createQuery(`${api}/users/${id}/reviews`, queryParams);
  let response = await fetch(query);

  if (response.ok) {
    console.log('success user ratings fetch');
    return response;
  } else {
    console.log('user ratings fetch error');
  }

  return [];
}

export const deleteProduct = async (productId) => {
  const response = await fetch(`${api}/products/${productId}`, {
    method: "DELETE",
    headers: {
      "Content-Type" : "application/json",
      'Authorization' : `Bearer ${await getAccessToken()}`
    },
  });

  if (response.ok) {
    console.log('delete product by admin success');
  } else {
    console.log('delete product by admin error');
  }
}

export const updateProduct = async (productId, productUpdateDto) => {
  const response = await fetch(`${api}/products/${productId}`, {
    method: "PUT",
    headers: {
      "Content-Type" : "application/json",
      'Authorization' : `Bearer ${await getAccessToken()}`
    },
    body: JSON.stringify(productUpdateDto)
  });

  if (response.ok) {
    console.log('update product success');
    return true;
  }
  console.log('update product error');

  return false;
}
