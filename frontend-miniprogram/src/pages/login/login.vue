<template>
  <view class="page">
    <view class="header">
      <view class="logo">
        <view class="logo-icon">Z</view>
        <text class="logo-text">郑好遇见你</text>
      </view>
      <text class="slogan">正好遇见对的人</text>
    </view>

    <view class="form-container">
      <view class="form-item">
        <view class="form-label">手机号码</view>
        <input 
          class="form-input" 
          type="number" 
          v-model="phone" 
          placeholder="请输入手机号码"
          maxlength="11"
        />
      </view>
      <view class="form-item">
        <view class="form-label">验证码</view>
        <view class="code-input-wrap">
          <input 
            class="form-input code-input" 
            type="number" 
            v-model="code" 
            placeholder="请输入验证码"
            maxlength="6"
          />
          <view class="code-btn" :class="{ active: canGetCode }" @click="getCode">
            {{ codeBtnText }}
          </view>
        </view>
      </view>
      <view class="form-item">
        <view class="agree-wrap">
          <view class="agree-checkbox" :class="{ checked: agreed }" @click="agreed = !agreed"></view>
          <text class="agree-text">我已阅读并同意</text>
          <text class="agree-link">《用户协议》</text>
          <text class="agree-text">和</text>
          <text class="agree-link">《隐私政策》</text>
        </view>
      </view>
      <button class="submit-btn" :class="{ active: canSubmit }" @click="handleSubmit">
        登录
      </button>
    </view>

    <view class="other-login">
      <text class="other-title">其他登录方式</text>
      <view class="other-options">
        <view class="other-item" @click="handleWechat">
          <view class="other-icon">💚</view>
          <text class="other-name">微信登录</text>
        </view>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

const phone = ref('')
const code = ref('')
const agreed = ref(true)
const canGetCode = ref(true)
const codeBtnText = ref('获取验证码')

const canSubmit = computed(() => {
  return phone.value.length === 11 && code.value.length === 6 && agreed.value
})

const getCode = () => {
  if (!canGetCode.value) return
  if (phone.value.length !== 11) {
    uni.showToast({ title: '请输入正确的手机号', icon: 'none' })
    return
  }
  
  canGetCode.value = false
  codeBtnText.value = '60s'
  let count = 60
  const timer = setInterval(() => {
    count--
    if (count <= 0) {
      clearInterval(timer)
      canGetCode.value = true
      codeBtnText.value = '获取验证码'
    } else {
      codeBtnText.value = `${count}s`
    }
  }, 1000)
  
  uni.showToast({ title: '验证码已发送', icon: 'success' })
}

const handleSubmit = () => {
  if (!canSubmit.value) return
  uni.showLoading({ title: '登录中...' })
  setTimeout(() => {
    uni.hideLoading()
    uni.switchTab({ url: '/pages/index/index' })
  }, 1000)
}

const handleWechat = () => {
  uni.showToast({ title: '微信登录', icon: 'none' })
}
</script>

<style lang="scss" scoped>
.page {
  min-height: 100vh;
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  padding: 0 30rpx;
}

.header {
  padding-top: 200rpx;
  text-align: center;
}

.logo {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 20rpx;
  margin-bottom: 24rpx;
}

.logo-icon {
  width: 100rpx;
  height: 100rpx;
  background: #fff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 50rpx;
  font-weight: bold;
  color: #ef4444;
}

.logo-text {
  font-size: 50rpx;
  font-weight: bold;
  color: #fff;
}

.slogan {
  font-size: 28rpx;
  color: rgba(255, 255, 255, 0.8);
}

.form-container {
  margin-top: 80rpx;
}

.form-item {
  margin-bottom: 24rpx;
}

.form-label {
  font-size: 26rpx;
  color: rgba(255, 255, 255, 0.8);
  margin-bottom: 12rpx;
}

.form-input {
  width: 100%;
  height: 88rpx;
  background: rgba(255, 255, 255, 0.9);
  border-radius: 12rpx;
  padding: 0 24rpx;
  font-size: 30rpx;
  color: #333;
}

.code-input-wrap {
  display: flex;
  gap: 20rpx;
}

.code-input {
  flex: 1;
}

.code-btn {
  width: 180rpx;
  height: 88rpx;
  background: rgba(255, 255, 255, 0.3);
  border-radius: 12rpx;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 26rpx;
  color: #fff;
  
  &.active {
    background: rgba(255, 255, 255, 0.9);
    color: #ef4444;
  }
}

.agree-wrap {
  display: flex;
  align-items: center;
  gap: 8rpx;
}

.agree-checkbox {
  width: 36rpx;
  height: 36rpx;
  border: 2rpx solid rgba(255, 255, 255, 0.5);
  border-radius: 8rpx;
  
  &.checked {
    background: #fff;
    position: relative;
    
    &::after {
      content: '✓';
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: #ef4444;
      font-size: 24rpx;
    }
  }
}

.agree-text {
  font-size: 24rpx;
  color: rgba(255, 255, 255, 0.8);
}

.agree-link {
  font-size: 24rpx;
  color: #fff;
}

.submit-btn {
  width: 100%;
  height: 96rpx;
  background: rgba(255, 255, 255, 0.3);
  border-radius: 48rpx;
  font-size: 32rpx;
  color: #fff;
  margin-top: 30rpx;
  border: none;
  
  &.active {
    background: #fff;
    color: #ef4444;
  }
}

.other-login {
  margin-top: 100rpx;
  text-align: center;
}

.other-title {
  font-size: 24rpx;
  color: rgba(255, 255, 255, 0.6);
  margin-bottom: 30rpx;
}

.other-options {
  display: flex;
  justify-content: center;
}

.other-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20rpx 40rpx;
  background: rgba(255, 255, 255, 0.1);
  border-radius: 20rpx;
}

.other-icon {
  font-size: 56rpx;
  margin-bottom: 12rpx;
}

.other-name {
  font-size: 24rpx;
  color: #fff;
}
</style>
