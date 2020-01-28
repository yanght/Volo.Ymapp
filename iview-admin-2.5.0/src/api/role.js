import axios from '@/libs/api.request'
export const getRoles = () => {
    return axios.request({
        url: '/api/identity/roles',
        method: 'get'
    })
}

export const updateRole = (data) => {
    return axios.request({
        url: `/api/identity/roles/${data.id}`,
        data: data,
        method: 'put'
    })
}



