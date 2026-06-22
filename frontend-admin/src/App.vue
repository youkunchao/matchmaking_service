<template>
  <div class="min-h-screen bg-gray-50">
    <router-view v-if="!isAuthenticated" />
    <template v-else>
      <aside class="fixed left-0 top-0 bottom-0 w-64 bg-white shadow-md z-50">
        <div class="p-6 border-b">
          <div class="flex items-center gap-3">
            <div class="w-10 h-10 bg-red-500 rounded-full flex items-center justify-center">
              <span class="text-white text-xl font-bold">Z</span>
            </div>
            <span class="text-lg font-bold">管理后台</span>
          </div>
        </div>
        <el-menu :default-active="activeMenu" class="mt-4" mode="vertical">
          <el-menu-item index="/" @click="navigate('/')">
            <el-icon><component :is="icons.LayoutDashboard" /></el-icon>
            <span>数据看板</span>
          </el-menu-item>
          <el-menu-item index="/users" @click="navigate('/users')">
            <el-icon><component :is="icons.User" /></el-icon>
            <span>会员管理</span>
          </el-menu-item>
          <el-menu-item index="/hongniang" @click="navigate('/hongniang')">
            <el-icon><component :is="icons.UserFilled" /></el-icon>
            <span>红娘管理</span>
          </el-menu-item>
          <el-menu-item index="/activities" @click="navigate('/activities')">
            <el-icon><component :is="icons.Calendar" /></el-icon>
            <span>活动管理</span>
          </el-menu-item>
          <el-menu-item index="/orders" @click="navigate('/orders')">
            <el-icon><component :is="icons.ShoppingCart" /></el-icon>
            <span>订单管理</span>
          </el-menu-item>
          <el-menu-item index="/packages" @click="navigate('/packages')">
            <el-icon><component :is="icons.Box" /></el-icon>
            <span>套餐管理</span>
          </el-menu-item>
          <el-menu-item index="/articles" @click="navigate('/articles')">
            <el-icon><component :is="icons.Notebook" /></el-icon>
            <span>文章管理</span>
          </el-menu-item>
          <el-menu-item index="/content" @click="navigate('/content')">
            <el-icon><component :is="icons.Document" /></el-icon>
            <span>内容管理</span>
          </el-menu-item>
          <el-menu-item index="/audit" @click="navigate('/audit')">
            <el-icon><component :is="icons.CheckCircle" /></el-icon>
            <span>审核中心</span>
          </el-menu-item>
        </el-menu>
      </aside>
      <main class="ml-64 min-h-screen">
        <header class="h-16 bg-white shadow-sm px-6 flex items-center justify-between">
          <span class="text-gray-600">{{ pageTitle }}</span>
          <div class="flex items-center gap-4">
            <span class="text-sm text-gray-500">{{ currentUser }}</span>
            <button @click="logout" class="text-red-500 hover:text-red-600">退出登录</button>
          </div>
        </header>
        <div class="p-6">
          <router-view />
        </div>
      </main>
    </template>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import * as icons from '@element-plus/icons-vue'

const router = useRouter()
const route = useRoute()

const isAuthenticated = computed(() => localStorage.getItem('token') !== null)
const currentUser = ref('管理员')

const pageTitleMap = {
  '/': '数据看板',
  '/users': '会员管理',
  '/hongniang': '红娘管理',
  '/activities': '活动管理',
  '/orders': '订单管理',
  '/packages': '套餐管理',
  '/articles': '文章管理',
  '/content': '内容管理',
  '/audit': '审核中心'
}

const pageTitle = computed(() => pageTitleMap[route.path] || '管理后台')
const activeMenu = computed(() => route.path)

const navigate = (path) => {
  router.push(path)
}

const logout = () => {
  localStorage.removeItem('token')
  router.push('/login')
}

onMounted(() => {
  if (!isAuthenticated.value && route.path !== '/login') {
    router.push('/login')
  }
})

watch(isAuthenticated, (val) => {
  if (!val && route.path !== '/login') {
    router.push('/login')
  }
})
</script>
