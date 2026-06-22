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
    const token = localStorage.getItem('admin_token')
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
      localStorage.removeItem('admin_token')
      window.location.href = '/login'
    }
    return Promise.reject(error)
  }
)

export const authApi = {
  login: (data: { username: string; password: string }) => api.post('/auth/login', data),
  logout: () => api.post('/auth/logout')
}

export const userApi = {
  getList: (params?: any) => api.get('/admin/users', { params }),
  getDetail: (id: number) => api.get(`/admin/users/${id}`),
  create: (data: any) => api.post('/admin/users', data),
  update: (id: number, data: any) => api.put(`/admin/users/${id}`, data),
  delete: (id: number) => api.delete(`/admin/users/${id}`),
  approve: (id: number) => api.post(`/admin/users/${id}/approve`),
  reject: (id: number, reason: string) => api.post(`/admin/users/${id}/reject`, { reason })
}

export const activityApi = {
  getList: (params?: any) => api.get('/admin/activities', { params }),
  getDetail: (id: number) => api.get(`/admin/activities/${id}`),
  create: (data: any) => api.post('/admin/activities', data),
  update: (id: number, data: any) => api.put(`/admin/activities/${id}`, data),
  delete: (id: number) => api.delete(`/admin/activities/${id}`),
  publish: (id: number) => api.post(`/admin/activities/${id}/publish`)
}

export const orderApi = {
  getList: (params?: any) => api.get('/admin/orders', { params }),
  getDetail: (id: number) => api.get(`/admin/orders/${id}`),
  update: (id: number, data: any) => api.put(`/admin/orders/${id}`, data),
  delete: (id: number) => api.delete(`/admin/orders/${id}`),
  confirm: (id: number) => api.post(`/admin/orders/${id}/confirm`),
  complete: (id: number) => api.post(`/admin/orders/${id}/complete`)
}

export const articleApi = {
  getList: (params?: any) => api.get('/admin/articles', { params }),
  getDetail: (id: number) => api.get(`/admin/articles/${id}`),
  create: (data: any) => api.post('/admin/articles', data),
  update: (id: number, data: any) => api.put(`/admin/articles/${id}`, data),
  delete: (id: number) => api.delete(`/admin/articles/${id}`),
  publish: (id: number) => api.post(`/admin/articles/${id}/publish`)
}

export const packageApi = {
  getList: () => api.get('/admin/packages'),
  getDetail: (id: number) => api.get(`/admin/packages/${id}`),
  create: (data: any) => api.post('/admin/packages', data),
  update: (id: number, data: any) => api.put(`/admin/packages/${id}`, data),
  delete: (id: number) => api.delete(`/admin/packages/${id}`)
}

export const hongniangApi = {
  getList: (params?: any) => api.get('/admin/hongniang', { params }),
  getDetail: (id: number) => api.get(`/admin/hongniang/${id}`),
  create: (data: any) => api.post('/admin/hongniang', data),
  update: (id: number, data: any) => api.put(`/admin/hongniang/${id}`, data),
  delete: (id: number) => api.delete(`/admin/hongniang/${id}`),
  approve: (id: number) => api.post(`/admin/hongniang/${id}/approve`),
  reject: (id: number, reason: string) => api.post(`/admin/hongniang/${id}/reject`, { reason })
}

export const statisticsApi = {
  dashboard: () => api.get('/admin/statistics/dashboard'),
  userGrowth: (params?: any) => api.get('/admin/statistics/user-growth', { params }),
  orderStatistics: (params?: any) => api.get('/admin/statistics/order-statistics', { params }),
  activityStatistics: (params?: any) => api.get('/admin/statistics/activity-statistics', { params })
}

export const auditApi = {
  getUserAudits: (params?: any) => api.get('/admin/audit/users', { params }),
  getHongniangAudits: (params?: any) => api.get('/admin/audit/hongniang', { params }),
  approveUser: (id: number) => api.post(`/admin/audit/users/${id}/approve`),
  rejectUser: (id: number, reason: string) => api.post(`/admin/audit/users/${id}/reject`, { reason }),
  approveHongniang: (id: number) => api.post(`/admin/audit/hongniang/${id}/approve`),
  rejectHongniang: (id: number, reason: string) => api.post(`/admin/audit/hongniang/${id}/reject`, { reason })
}

export default api
