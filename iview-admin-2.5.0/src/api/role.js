import axios from '@/libs/api.request'


export const addRole = (data) => {
  //console.log(data)
  return axios.request({
    url: '/api/identity/roles',
    data: data,
    method: 'post'
  })
}

export const updateRole = (data) => {
  return axios.request({
    url: `/api/identity/roles/${data.id}`,
    data: data,
    method: 'put'
  })
}


export const deleteRole = (id) => {
  return axios.request({
    url: `/api/identity/roles/${id}`,
    method: 'delete'
  })
}

export const addOrUpdateRole = (data) => {
  if (data.id != '' && data.id != undefined) {
    return updateRole(data);
  } else {
    return addRole(data);
  }
}

export const getRole = (id) => {
  return axios.request({
    url: `/api/identity/roles/${id}`,
    method: 'get'
  })
}

export const getRoles = (data) => {
  return axios.request({
    url: '/api/identity/roles',
    params: data,
    method: 'get'
  })
}

export const getPermissions = (providerName, providerKey) => {
  return axios.request({
    url: `/api/abp/permissions?providerName=${providerName}&providerKey=${providerKey}`,
    method: 'get'
  })
}

export const grantPermission = (providerName, providerKey, data) => {
  return axios.request({
    url: `/api/abp/permissions?providerName=${providerName}&providerKey=${providerKey}`,
    data: data,
    method: 'put'
  })
}
