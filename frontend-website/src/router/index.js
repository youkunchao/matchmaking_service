import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Packages from '../views/Packages.vue'
import Hongniang from '../views/Hongniang.vue'
import Activities from '../views/Activities.vue'
import Articles from '../views/Articles.vue'
import About from '../views/About.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/packages',
    name: 'Packages',
    component: Packages
  },
  {
    path: '/hongniang',
    name: 'Hongniang',
    component: Hongniang
  },
  {
    path: '/activities',
    name: 'Activities',
    component: Activities
  },
  {
    path: '/articles',
    name: 'Articles',
    component: Articles
  },
  {
    path: '/about',
    name: 'About',
    component: About
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    } else {
      return { top: 0 }
    }
  }
})

export default router
