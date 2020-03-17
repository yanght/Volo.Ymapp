import axios from '@/libs/api.request'

export const addPropertyName = (data) => {
    return axios.request({
        url: '/api/app/productProperty/propertyName',
        data: data,
        method: 'post'
    })
}

export const addPropertyValue = (data) => {
    return axios.request({
        url: '/api/app/productProperty/propertyValue',
        data: data,
        method: 'post'
    })
}

export const getPropertyList = (data) => {
    return axios.request({
        url: '/api/app/productProperty/propertyList',
        params: data,
        method: 'get'
    })
}

export const deletePropertyName = (id) => {
    return axios.request({
        url: `/api/app/productProperty/${id}/propertyName`,
        method: 'delete'
    })
}


export const deletePropertyValue = (id) => {
    return axios.request({
        url: `/api/app/productProperty/${id}/propertyValue`,
        method: 'delete'
    })
}

