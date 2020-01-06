import axios from '@/libs/api.request'

export const getCategoryTableData = (data) => {
    return axios.request({
        url: '/api/identity/users',
        params: data,
        method: 'get'
    })
}
