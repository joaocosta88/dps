export const buildQueryParams = (queryParams) => {
  let queryString = "";
  queryParams.forEach((queryParam) => {
    if (queryString) {
      queryString += `&${queryParam.key}=${queryParam.value}`;
    } else {
      queryString = `?${queryParam.key}=${queryParam.value}`;
    }
  });

  return queryString;
};
