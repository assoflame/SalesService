export const server = 'http://localhost:5090/api';

export const createQuery = (url, queryParams = {}) => {
    let query = url + '?';
    Object.entries(queryParams).forEach( entry => {
      const [key, value] = entry;
      query = query.concat(value !== '' ? `${key}=${value}&`: '');
    }
    )
  
    return query;
  }

  export const getPagesCount = (totalCount, limit) => {
    return Math.ceil(totalCount / limit);
  }