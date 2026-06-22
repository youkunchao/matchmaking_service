<template>
  <div class="min-h-screen bg-gradient-to-r from-red-500 to-red-600 flex items-center justify-center p-4">
    <div class="bg-white rounded-xl shadow-xl w-full max-w-md p-8">
      <div class="text-center mb-8">
        <div class="w-16 h-16 bg-red-500 rounded-full flex items-center justify-center mx-auto mb-4">
          <span class="text-white text-3xl font-bold">Z</span>
        </div>
        <h2 class="text-2xl font-bold text-gray-800">管理后台登录</h2>
        <p class="text-gray-500 mt-2">郑好遇见你婚介服务平台</p>
      </div>
      
      <el-form :model="loginForm" ref="loginFormRef" label-width="80px">
        <el-form-item label="用户名" prop="username">
          <el-input v-model="loginForm.username" placeholder="请输入用户名" />
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input v-model="loginForm.password" type="password" placeholder="请输入密码" />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" class="w-full" @click="handleLogin">登录</el-button>
        </el-form-item>
      </el-form>
      
      <div class="mt-6 text-center">
        <p class="text-sm text-gray-500">忘记密码？联系管理员重置</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'

const router = useRouter()

const loginForm = reactive({
  username: '',
  password: ''
})

const loginFormRef = ref(null)

const handleLogin = () => {
  if (!loginForm.username || !loginForm.password) {
    ElMessage.error('请输入用户名和密码')
    return
  }
  
  if (loginForm.username === 'admin' && loginForm.password === 'Admin@123456') {
    localStorage.setItem('token', 'mock-token')
    ElMessage.success('登录成功')
    router.push('/')
  } else {
    ElMessage.error('用户名或密码错误')
  }
}
</script>
