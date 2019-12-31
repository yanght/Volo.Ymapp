import axios from '@/libs/api.request'

export const login = ({ userName, password }) => {
  var params = new URLSearchParams();
  params.append('userName', userName)
  params.append('password', password)
  params.append('client_id', 'Ymapp_Web')
  params.append('client_secret', '1q2w3e*')
  params.append('grant_type', 'password')
  return axios.request({
    url: 'connect/token',
    data: params,
    method: 'post',
    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
  })
}

export const getUserInfo = () => {
  return axios.request({
    url: 'connect/userinfo',
    method: 'get'
  })
}

export const logout = (token) => {
  return axios.request({
    url: 'logout',
    method: 'post'
  })
}

export const getUnreadCount = () => {
  return axios.request({
    url: 'message/count',
    method: 'get'
  })
}

export const getMessage = () => {
  return axios.request({
    url: 'message/init',
    method: 'get'
  })
}

export const getContentByMsgId = msg_id => {
  return axios.request({
    url: 'message/content',
    method: 'get',
    params: {
      msg_id
    }
  })
}

export const hasRead = msg_id => {
  return axios.request({
    url: 'message/has_read',
    method: 'post',
    data: {
      msg_id
    }
  })
}

export const removeReaded = msg_id => {
  return axios.request({
    url: 'message/remove_readed',
    method: 'post',
    data: {
      msg_id
    }
  })
}

export const restoreTrash = msg_id => {
  return axios.request({
    url: 'message/restore',
    method: 'post',
    data: {
      msg_id
    }
  })
}

export const getUserTableData = (data) => {
  return axios.request({
    url: '/api/identity/users',
    params: data,
    method: 'get'
  })
}
