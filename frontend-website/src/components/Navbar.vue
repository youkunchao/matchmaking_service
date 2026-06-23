<template>
  <nav class="fixed top-0 left-0 right-0 bg-white/95 backdrop-blur-sm shadow-sm z-50">
    <div class="container mx-auto px-4">
      <div class="flex items-center justify-between h-16">
        <div class="flex items-center space-x-2">
          <div class="w-10 h-10 bg-primary-500 rounded-full flex items-center justify-center">
            <span class="text-white text-xl font-bold">Z</span>
          </div>
          <span class="text-xl font-bold text-gray-800">郑好遇见你</span>
        </div>
        
        <div class="hidden md:flex items-center space-x-8">
          <a v-for="item in navItems" :key="item.path" :href="item.path" 
             class="text-gray-600 hover:text-primary-500 transition-colors">{{ item.label }}</a>
        </div>
        
        <div class="hidden md:flex items-center space-x-4">
          <button class="text-primary-500 px-4 py-2 rounded-lg border border-primary-500 hover:bg-primary-50 transition-colors">
            登录
          </button>
          <button class="bg-primary-500 text-white px-4 py-2 rounded-lg hover:bg-primary-600 transition-colors">
            免费注册
          </button>
        </div>
        
        <button @click="toggleMenu" class="md:hidden p-2">
          <svg class="w-6 h-6 text-gray-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
          </svg>
        </button>
      </div>
    </div>
    
    <div v-if="isMenuOpen" class="md:hidden bg-white border-t">
      <div class="container mx-auto px-4 py-4 space-y-2">
        <a v-for="item in navItems" :key="item.path" :href="item.path" 
           class="block py-2 text-gray-600 hover:text-primary-500" @click="isMenuOpen = false">{{ item.label }}</a>
        <div class="pt-4 border-t space-y-2">
          <button class="w-full text-primary-500 px-4 py-2 rounded-lg border border-primary-500">登录</button>
          <button class="w-full bg-primary-500 text-white px-4 py-2 rounded-lg">免费注册</button>
        </div>
      </div>
    </div>
  </nav>
</template>

<script setup>
import { ref } from 'vue'

const isMenuOpen = ref(false)

const navItems = [
  { label: '首页', path: '/' },
  { label: '服务套餐', path: '/packages' },
  { label: '红娘团队', path: '/hongniang' },
  { label: '线下活动', path: '/activities' },
  { label: '情感资讯', path: '/articles' },
  { label: '关于我们', path: '/about' }
]

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value
}
</script>
