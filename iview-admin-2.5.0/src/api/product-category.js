import axios from '@/libs/api.request'

export const getProductCategory = (id) => {
  return axios.request({
    url: `/api/app/productCategory/${id}`,
    method: 'get'
  })
}

export const getProductCategoryTree = () => {
  return axios.request({
    url: '/api/app/productCategory/categoryTree',
    method: 'get'
  })
}

export const updateProductCategory = (data) => {
  return axios.request({
    url: `/api/app/productCategory/${data.id}`,
    data: data,
    method: 'put'
  })
}

export const addProductCategory = (data) => {
  return axios.request({
    url: '/api/app/productCategory',
    data: data,
    method: 'post'
  })
}

export const deleteRole = (id) => {
  return axios.request({
    url: `/api/app/productCategory/${id}`,
    method: 'delete'
  })
}

