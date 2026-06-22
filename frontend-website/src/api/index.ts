import axios from 'axios'

const baseURL = 'http://localhost:5000/api'

const api = axios.create({
  baseURL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json'
  }
})

api.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  error => {
    return Promise.reject(error)
  }
)

api.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token')
      window.location.href = '/login'
    }
    return Promise.reject(error)
  }
)

export const userApi = {
  login: (data: { phone: string; code: string }) => api.post('/users/login', data),
  register: (data: any) => api.post('/users/register', data),
  getProfile: () => api.get('/users/profile'),
  updateProfile: (data: any) => api.put('/users/profile', data),
  uploadAvatar: (data: FormData) => api.post('/users/avatar', data, { headers: { 'Content-Type': 'multipart/form-data' } }),
  getRecommendations: (params?: any) => api.get('/users/recommendations', { params }),
  getUserDetail: (id: number) => api.get(`/users/${id}`),
  likeUser: (id: number) => api.post(`/users/${id}/like`),
  match: (id: number) => api.post(`/users/${id}/match`)
}

export const activityApi = {
  getList: (params?: any) => api.get('/activities', { params }),
  getDetail: (id: number) => api.get(`/activities/${id}`),
  create: (data: any) => api.post('/activities', data),
  update: (id: number, data: any) => api.put(`/activities/${id}`, data),
  delete: (id: number) => api.delete(`/activities/${id}`),
  participate: (id: number) => api.post(`/activities/${id}/participate`),
  getParticipants: (id: number) => api.get(`/activities/${id}/participants`)
}

export const chatApi = {
  getConversations: () => api.get('/chat/conversations'),
  getMessages: (userId: number) => api.get(`/chat/messages/${userId}`),
  sendMessage: (data: { toUserId: number; content: string }) => api.post('/chat/messages', data),
  markRead: (userId: number) => api.post(`/chat/mark-read/${userId}`)
}

export const orderApi = {
  getList: (params?: any) => api.get('/orders', { params }),
  getDetail: (id: number) => api.get(`/orders/${id}`),
  create: (data: any) => api.post('/orders', data),
  pay: (id: number) => api.post(`/orders/${id}/pay`),
  cancel: (id: number) => api.post(`/orders/${id}/cancel`)
}

export const articleApi = {
  getList: (params?: any) => api.get('/articles', { params }),
  getDetail: (id: number) => api.get(`/articles/${id}`),
  create: (data: any) => api.post('/articles', data),
  update: (id: number, data: any) => api.put(`/articles/${id}`, data),
  delete: (id: number) => api.delete(`/articles/${id}`)
}

export const packageApi = {
  getList: () => api.get('/packages'),
  getDetail: (id: number) => api.get(`/packages/${id}`),
  create: (data: any) => api.post('/packages', data),
  update: (id: number, data: any) => api.put(`/packages/${id}`, data),
  delete: (id: number) => api.delete(`/packages/${id}`)
}

export const hongniangApi = {
  getList: (params?: any) => api.get('/hongniang', { params }),
  getDetail: (id: number) => api.get(`/hongniang/${id}`),
  create: (data: any) => api.post('/hongniang', data),
  update: (id: number, data: any) => api.put(`/hongniang/${id}`, data),
  delete: (id: number) => api.delete(`/hongniang/${id}`),
  assign: (userId: number, hongniangId: number) => api.post(`/hongniang/${hongniangId}/assign/${userId}`)
}

export default api
