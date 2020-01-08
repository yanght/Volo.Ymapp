import axios from '@/libs/api.request'
export const getRoles = () => {
    return axios.request({
        url: '/api/identity/roles',
        method: 'get'
    })
}


