import request from '@/utils/request';

export async function queryRule() {
    return request('/api/identity/users', {
        method: 'GET',
        data: params,
    });
}