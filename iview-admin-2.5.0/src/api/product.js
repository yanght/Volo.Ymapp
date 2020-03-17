import axios from '@/libs/api.request'

export const productList = (data) => {
  return axios.request({
    url: '/api/app/product',
    params: data,
    method: 'get'
  })
}

export const getProduct = (id) => {
  return axios.request({
    url: `/api/app/product/${id}`,
    method: 'get'
  })
}

export const addProduct = (data) => {
  return axios.request({
    url: `/api/app/product`,
    data: data,
    method: 'post'
  })
}
