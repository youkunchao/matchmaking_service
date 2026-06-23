<template>
  <view class="page">
    <view class="profile-header">
      <view class="header-bg"></view>
      <view class="header-content">
        <image class="user-avatar" :src="user.avatar" mode="aspectFill" />
        <view class="user-info">
          <text class="user-name">{{ user.name }}</text>
          <text class="user-title">{{ user.title }}</text>
        </view>
        <view class="edit-btn" @click="goToEdit">编辑资料</view>
      </view>
      <view class="stats-row">
        <view class="stat-item">
          <text class="stat-value">{{ user.matchCount }}</text>
          <text class="stat-label">匹配次数</text>
        </view>
        <view class="stat-item">
          <text class="stat-value">{{ user.chatCount }}</text>
          <text class="stat-label">聊天人数</text>
        </view>
        <view class="stat-item">
          <text class="stat-value">{{ user.visitedCount }}</text>
          <text class="stat-label">被访问</text>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="section-title">我的服务</view>
      <view class="service-list">
        <view class="service-item" v-for="service in services" :key="service.title" @click="handleService(service.key)">
          <text class="service-icon">{{ service.icon }}</text>
          <text class="service-name">{{ service.title }}</text>
          <text class="service-arrow">›</text>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="section-title">我的订单</view>
      <view class="order-list">
        <view class="order-item" v-for="order in orders" :key="order.key" @click="handleOrder(order.key)">
          <text class="order-icon">{{ order.icon }}</text>
          <text class="order-name">{{ order.title }}</text>
          <view class="order-badge" v-if="order.count > 0">{{ order.count }}</view>
          <text class="order-arrow">›</text>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="setting-list">
        <view class="setting-item" v-for="setting in settings" :key="setting.title" @click="handleSetting(setting.key)">
          <text class="setting-name">{{ setting.title }}</text>
          <text class="setting-desc">{{ setting.desc }}</text>
          <text class="setting-arrow">›</text>
        </view>
      </view>
    </view>

    <view class="logout-btn" @click="handleLogout">退出登录</view>
  </view>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const user = ref({
  avatar: 'https://via.placeholder.com/100',
  name: '用户昵称',
  title: 'VIP会员',
  matchCount: 88,
  chatCount: 23,
  visitedCount: 156
})

const services = ref([
  { key: 'hongniang', icon: '👩‍❤️‍👨', title: '专属红娘' },
  { key: 'vip', icon: '💎', title: 'VIP服务' },
  { key: 'activity', icon: '📅', title: '活动报名' },
  { key: 'match', icon: '💑', title: '精准匹配' }
])

const orders = ref([
  { key: 'pending', icon: '⏳', title: '待付款', count: 2 },
  { key: 'confirmed', icon: '✅', title: '已确认', count: 0 },
  { key: 'completed', icon: '🎉', title: '已完成', count: 5 },
  { key: 'refund', icon: '💰', title: '退款/售后', count: 1 }
])

const settings = ref([
  { key: 'profile', title: '个人资料', desc: '完善您的信息' },
  { key: 'privacy', title: '隐私设置', desc: '谁可以看到我' },
  { key: 'notification', title: '通知设置', desc: '消息提醒开关' },
  { key: 'help', title: '帮助中心', desc: '常见问题解答' },
  { key: 'about', title: '关于我们', desc: '版本 1.0.0' }
])

const goToEdit = () => {
  uni.navigateTo({ url: '/pages/profile-edit/profile-edit' })
}

const handleService = (key: string) => {
  uni.showToast({ title: `${key}服务`, icon: 'none' })
}

const handleOrder = (key: string) => {
  uni.showToast({ title: `${key}订单`, icon: 'none' })
}

const handleSetting = (key: string) => {
  uni.showToast({ title: `${key}设置`, icon: 'none' })
}

const handleLogout = () => {
  uni.showModal({
    title: '确认退出',
    content: '确定要退出登录吗？',
    success: (res) => {
      if (res.confirm) {
        uni.redirectTo({ url: '/pages/login/login' })
      }
    }
  })
}
</script>

<style lang="scss" scoped>
.page {
  min-height: 100vh;
  background: #f5f5f5;
  padding-bottom: 120rpx;
}

.profile-header {
  position: relative;
  padding-bottom: 30rpx;
}

.header-bg {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  height: 400rpx;
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  border-radius: 0 0 40rpx 40rpx;
}

.header-content {
  position: relative;
  z-index: 1;
  display: flex;
  align-items: center;
  padding: 120rpx 30rpx 30rpx;
}

.user-avatar {
  width: 160rpx;
  height: 160rpx;
  border-radius: 50%;
  border: 6rpx solid rgba(255, 255, 255, 0.5);
}

.user-info {
  flex: 1;
  margin-left: 24rpx;
}

.user-name {
  display: block;
  font-size: 36rpx;
  font-weight: bold;
  color: #fff;
  margin-bottom: 8rpx;
}

.user-title {
  display: block;
  font-size: 24rpx;
  color: rgba(255, 255, 255, 0.8);
}

.edit-btn {
  background: rgba(255, 255, 255, 0.2);
  padding: 16rpx 32rpx;
  border-radius: 32rpx;
  font-size: 26rpx;
  color: #fff;
}

.stats-row {
  position: relative;
  z-index: 1;
  display: flex;
  justify-content: space-around;
  background: #fff;
  margin: 0 30rpx;
  border-radius: 20rpx;
  padding: 30rpx 0;
  box-shadow: 0 8rpx 30rpx rgba(0, 0, 0, 0.1);
}

.stat-item {
  text-align: center;
}

.stat-value {
  display: block;
  font-size: 40rpx;
  font-weight: bold;
  color: #ef4444;
}

.stat-label {
  display: block;
  font-size: 24rpx;
  color: #999;
  margin-top: 8rpx;
}

.section {
  background: #fff;
  margin: 20rpx 0;
}

.section-title {
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
  padding: 24rpx 30rpx 16rpx;
}

.service-list, .order-list {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
}

.service-item, .order-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 30rpx 0;
  position: relative;
}

.service-icon, .order-icon {
  font-size: 48rpx;
  margin-bottom: 12rpx;
}

.service-name, .order-name {
  font-size: 24rpx;
  color: #333;
}

.service-arrow, .order-arrow {
  display: none;
}

.order-badge {
  position: absolute;
  top: 16rpx;
  right: 20rpx;
  background: #ef4444;
  color: #fff;
  font-size: 20rpx;
  padding: 4rpx 12rpx;
  border-radius: 20rpx;
}

.setting-list {
  padding: 0 30rpx;
}

.setting-item {
  display: flex;
  align-items: center;
  padding: 30rpx 0;
  border-bottom: 2rpx solid #f5f5f5;
  
  &:last-child {
    border-bottom: none;
  }
}

.setting-name {
  flex: 1;
  font-size: 28rpx;
  color: #333;
}

.setting-desc {
  font-size: 24rpx;
  color: #999;
  margin-right: 16rpx;
}

.setting-arrow {
  font-size: 32rpx;
  color: #ccc;
}

.logout-btn {
  margin: 30rpx;
  text-align: center;
  padding: 24rpx;
  background: #fff;
  border-radius: 12rpx;
  font-size: 28rpx;
  color: #999;
}
</style>
