import axios from '@/libs/api.request'

export const getLineList = (data) => {
  return axios.request({
    url: '/api/app/line/lineList',
    params: data,
    method: 'get'
  })
}

export const getLineByLineId = (lineId) => {
  return axios.request({
    url: `/api/app/line/lineByLineId/${lineId}`,
    method: 'get'
  })
}
