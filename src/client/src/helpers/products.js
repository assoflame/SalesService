import { getAccessToken } from "./auth";
import { server } from "./shared";

export const getProducts = async (filter) => {
    let query = createQuery(`${server}/products`, filter);
    console.log(query);
    let response = await fetch(query);

    if(response.ok) {
      let result = await response.json();
      console.log(result);
      return result;
    } else {
      console.log("get products error");
    }

    return [];
}

const createQuery = (url, queryParams) => {
  let query = url + '?';
  Object.entries(queryParams).forEach( entry => {
    const [key, value] = entry;
    query = query.concat(value !== '' ? `${key}=${value}&`: '');
  }
  )

  return query;
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

export const getUserRatings = async (id) => {
  let response = await fetch(`${server}/users/${id}/ratings`);

  if(response.ok) {
    let ratings = await response.json();
    console.log('success user ratings fetch');
    return ratings;
  } else {
    console.log('user ratings fetch error');
  }

  return [];
}
