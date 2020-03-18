import axios from '@/libs/api.request'

export const addProduct = (data) => {
  return axios.request({
    url: `/api/app/product`,
    data: data,
    method: 'post'
  })
}

export const updateProduct = (data) => {
  return axios.request({
    url: `/api/app/product/${data.id}`,
    data: data,
    method: 'put'
  })
}

export const deleteProduct = (id) => {
  return axios.request({
    url: `/api/app/product/${id}`,
    method: 'delete'
  })
}


export const addOrUpdateProduct = (data) => {
  if (data.id != '' && data.id != undefined) {
    return updateProduct(data);
  } else {
    return addProduct(data);
  }
}

export const getProduct = (id) => {
  return axios.request({
    url: `/api/app/product/${id}`,
    method: 'get'
  })
}

export const getProductDetail = (id) => {
  return axios.request({
    url: `/api/app/product/${id}/detail`,
    method: 'get'
  })
}

export const productList = (data) => {
  return axios.request({
    url: '/api/app/product',
    params: data,
    method: 'get'
  })
}

