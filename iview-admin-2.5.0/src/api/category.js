import axios from '@/libs/api.request'

export const getCategoryTableData = (params) => {
    return axios.request({
        url: '/api/identity/users',
        params: params,
        method: 'get'
    })
}
