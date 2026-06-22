import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import UserManagement from '../views/UserManagement.vue'
import HongniangManagement from '../views/HongniangManagement.vue'
import ActivityManagement from '../views/ActivityManagement.vue'
import OrderManagement from '../views/OrderManagement.vue'
import ContentManagement from '../views/ContentManagement.vue'
import AuditCenter from '../views/AuditCenter.vue'

const routes = [
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/',
    name: 'Dashboard',
    component: Dashboard,
    meta: { requiresAuth: true }
  },
  {
    path: '/users',
    name: 'UserManagement',
    component: UserManagement,
    meta: { requiresAuth: true }
  },
  {
    path: '/hongniang',
    name: 'HongniangManagement',
    component: HongniangManagement,
    meta: { requiresAuth: true }
  },
  {
    path: '/activities',
    name: 'ActivityManagement',
    component: ActivityManagement,
    meta: { requiresAuth: true }
  },
  {
    path: '/orders',
    name: 'OrderManagement',
    component: OrderManagement,
    meta: { requiresAuth: true }
  },
  {
    path: '/content',
    name: 'ContentManagement',
    component: ContentManagement,
    meta: { requiresAuth: true }
  },
  {
    path: '/audit',
    name: 'AuditCenter',
    component: AuditCenter,
    meta: { requiresAuth: true }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const isLoggedIn = localStorage.getItem('token')
  if (to.meta.requiresAuth && !isLoggedIn) {
    next('/login')
  } else if (to.path === '/login' && isLoggedIn) {
    next('/')
  } else {
    next()
  }
})

export default router
