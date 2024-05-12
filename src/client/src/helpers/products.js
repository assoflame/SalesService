import { createQuery, api } from "./shared";

export const getProducts = async (filter) => {
  let query = createQuery(`${api}/products`, filter);
  let response = await fetch(query, {
    credentials: 'include'
  });

  if (response.ok)
    return response;

  return [];
}

export const getProductById = async (productId) => {
  let response = await fetch(`${api}/products/${productId}`, {
    credentials: "include"
  });

  if (response.ok)
    return await response.json();

  return null
}

export const createProduct = async (product) => {
  let formData = new FormData();
  formData.append('Name', product.name);
  formData.append('Description', product.description);
  formData.append('Price', product.price);
  product.images.forEach(image => formData.append('Images', image));

  let response = await fetch(`${api}/products`, {
    method: "POST",
    body: formData,
    credentials: 'include'
  });

  return response.ok
}

export const getUserReviews = async ({id, queryParams}) => {
  let query = createQuery(`${api}/users/${id}/reviews`, queryParams);
  let response = await fetch(query, {
    credentials: 'include'
  });

  if (response.ok)
    return response;

  return [];
}

export const deleteProduct = async (productId) => {
  await fetch(`${api}/products/${productId}`, {
    method: "DELETE",
    headers: { "Content-Type" : "application/json" },
    credentials: 'include'
  });
}

export const updateProduct = async ({productId, productUpdateDto}) => {
  const response = await fetch(`${api}/products/${productId}`, {
    method: "PUT",
    headers: {  "Content-Type" : "application/json" },
    body: JSON.stringify(productUpdateDto),
    credentials: 'include'
  })

  return response.ok
}
