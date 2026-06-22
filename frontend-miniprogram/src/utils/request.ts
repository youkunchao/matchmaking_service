const baseURL = 'http://localhost:5000/api'

interface RequestOptions {
  url: string
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE'
  data?: any
  params?: any
  header?: any
}

export const request = (options: RequestOptions) => {
  const { url, method = 'GET', data = {}, params = {}, header = {} } = options
  
  const token = uni.getStorageSync('token')
  if (token) {
    header.Authorization = `Bearer ${token}`
  }
  
  return new Promise((resolve, reject) => {
    uni.request({
      url: baseURL + url,
      method,
      data,
      params,
      header: {
        'Content-Type': 'application/json',
        ...header
      },
      success: (res) => {
        if (res.statusCode === 200) {
          resolve(res.data)
        } else if (res.statusCode === 401) {
          uni.removeStorageSync('token')
          uni.redirectTo({ url: '/pages/login/login' })
          reject(new Error('登录过期'))
        } else {
          reject(new Error(res.data?.message || '请求失败'))
        }
      },
      fail: (err) => {
        reject(err)
      }
    })
  })
}

export const userApi = {
  login: (data: { phone: string; code: string }) => request({ url: '/users/login', method: 'POST', data }),
  register: (data: any) => request({ url: '/users/register', method: 'POST', data }),
  getProfile: () => request({ url: '/users/profile' }),
  updateProfile: (data: any) => request({ url: '/users/profile', method: 'PUT', data }),
  getRecommendations: (params?: any) => request({ url: '/users/recommendations', params }),
  getUserDetail: (id: number) => request({ url: `/users/${id}` }),
  likeUser: (id: number) => request({ url: `/users/${id}/like`, method: 'POST' }),
  match: (id: number) => request({ url: `/users/${id}/match`, method: 'POST' })
}

export const activityApi = {
  getList: (params?: any) => request({ url: '/activities', params }),
  getDetail: (id: number) => request({ url: `/activities/${id}` }),
  participate: (id: number) => request({ url: `/activities/${id}/participate`, method: 'POST' })
}

export const chatApi = {
  getConversations: () => request({ url: '/chat/conversations' }),
  getMessages: (userId: number) => request({ url: `/chat/messages/${userId}` }),
  sendMessage: (data: { toUserId: number; content: string }) => request({ url: '/chat/messages', method: 'POST', data }),
  markRead: (userId: number) => request({ url: `/chat/mark-read/${userId}`, method: 'POST' })
}

export const orderApi = {
  getList: (params?: any) => request({ url: '/orders', params }),
  getDetail: (id: number) => request({ url: `/orders/${id}` }),
  create: (data: any) => request({ url: '/orders', method: 'POST', data }),
  pay: (id: number) => request({ url: `/orders/${id}/pay`, method: 'POST' })
}

export const packageApi = {
  getList: () => request({ url: '/packages' }),
  getDetail: (id: number) => request({ url: `/packages/${id}` })
}

export default request
