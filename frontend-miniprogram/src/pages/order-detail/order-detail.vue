<template>
  <view class="page">
    <view class="order-header">
      <view class="order-status" :class="order.statusClass">{{ order.statusText }}</view>
      <text class="order-no">订单号: {{ order.orderNo }}</text>
    </view>

    <view class="section">
      <view class="section-title">订单信息</view>
      <view class="info-list">
        <view class="info-item">
          <text class="info-label">服务类型</text>
          <text class="info-value">{{ order.serviceType }}</text>
        </view>
        <view class="info-item">
          <text class="info-label">服务内容</text>
          <text class="info-value">{{ order.serviceName }}</text>
        </view>
        <view class="info-item">
          <text class="info-label">订单金额</text>
          <text class="info-value price">¥{{ order.amount }}</text>
        </view>
        <view class="info-item">
          <text class="info-label">创建时间</text>
          <text class="info-value">{{ order.createTime }}</text>
        </view>
        <view class="info-item" v-if="order.payTime">
          <text class="info-label">支付时间</text>
          <text class="info-value">{{ order.payTime }}</text>
        </view>
      </view>
    </view>

    <view class="section" v-if="order.hongniang">
      <view class="section-title">红娘信息</view>
      <view class="hongniang-card">
        <image class="hongniang-avatar" :src="order.hongniang.avatar" mode="aspectFill" />
        <view class="hongniang-info">
          <text class="hongniang-name">{{ order.hongniang.name }}</text>
          <text class="hongniang-title">{{ order.hongniang.title }}</text>
          <view class="hongniang-stats">
            <text class="stat-item">服务{{ order.hongniang.serviceCount }}人</text>
            <text class="stat-item">成功率{{ order.hongniang.successRate }}%</text>
          </view>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="section-title">服务进度</view>
      <view class="progress-list">
        <view class="progress-item" v-for="(step, index) in order.progress" :key="index" :class="{ active: step.completed }">
          <view class="progress-dot"></view>
          <view class="progress-content">
            <text class="progress-title">{{ step.title }}</text>
            <text class="progress-time">{{ step.time }}</text>
          </view>
        </view>
      </view>
    </view>

    <view class="action-bar" v-if="order.status === 'pending'">
      <button class="cancel-btn" @click="cancelOrder">取消订单</button>
      <button class="pay-btn" @click="payOrder">立即支付</button>
    </view>

    <view class="action-bar" v-if="order.status === 'paid'">
      <button class="contact-btn" @click="contactHongniang">联系红娘</button>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'

const order = reactive({
  orderNo: 'ORD20240210001',
  status: 'paid',
  statusClass: 'paid',
  statusText: '已支付',
  serviceType: '红娘服务',
  serviceName: 'VIP专属红娘服务（12个月）',
  amount: 999,
  createTime: '2024-02-10 10:30:00',
  payTime: '2024-02-10 10:35:00',
  hongniang: {
    avatar: 'https://via.placeholder.com/100',
    name: '红娘小李',
    title: '金牌红娘',
    serviceCount: 156,
    successRate: 92
  },
  progress: [
    { title: '订单创建', time: '2024-02-10 10:30', completed: true },
    { title: '支付完成', time: '2024-02-10 10:35', completed: true },
    { title: '红娘接单', time: '2024-02-10 11:00', completed: true },
    { title: '资料审核', time: '预计1-2天', completed: false },
    { title: '开始匹配', time: '待进行', completed: false },
    { title: '服务完成', time: '待进行', completed: false }
  ]
})

const cancelOrder = () => {
  uni.showModal({
    title: '取消订单',
    content: '确定要取消该订单吗？',
    success: (res) => {
      if (res.confirm) {
        uni.showToast({ title: '订单已取消', icon: 'success' })
      }
    }
  })
}

const payOrder = () => {
  uni.showToast({ title: '跳转支付...', icon: 'none' })
}

const contactHongniang = () => {
  uni.navigateTo({ url: `/pages/chat-detail/chat-detail?id=${order.hongniang.id || 1}` })
}
</script>

<style lang="scss" scoped>
.page {
  min-height: 100vh;
  background: #f5f5f5;
  padding-bottom: 120rpx;
}

.order-header {
  background: #fff;
  padding: 30rpx;
  text-align: center;
}

.order-status {
  font-size: 36rpx;
  font-weight: bold;
  margin-bottom: 16rpx;
  
  &.pending {
    color: #f59e0b;
  }
  
  &.paid {
    color: #22c55e;
  }
  
  &.completed {
    color: #3b82f6;
  }
  
  &.cancelled {
    color: #999;
  }
}

.order-no {
  font-size: 24rpx;
  color: #999;
}

.section {
  background: #fff;
  margin: 20rpx 0;
  padding: 24rpx 30rpx;
}

.section-title {
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
  margin-bottom: 20rpx;
}

.info-list {
  display: flex;
  flex-direction: column;
  gap: 16rpx;
}

.info-item {
  display: flex;
  justify-content: space-between;
}

.info-label {
  font-size: 26rpx;
  color: #999;
}

.info-value {
  font-size: 26rpx;
  color: #333;
  
  &.price {
    color: #ef4444;
    font-weight: bold;
  }
}

.hongniang-card {
  display: flex;
  gap: 20rpx;
  padding: 20rpx;
  background: #f9f9f9;
  border-radius: 12rpx;
}

.hongniang-avatar {
  width: 80rpx;
  height: 80rpx;
  border-radius: 50%;
}

.hongniang-info {
  flex: 1;
}

.hongniang-name {
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
}

.hongniang-title {
  font-size: 24rpx;
  color: #ef4444;
  margin-top: 8rpx;
}

.hongniang-stats {
  display: flex;
  gap: 20rpx;
  margin-top: 12rpx;
}

.stat-item {
  font-size: 22rpx;
  color: #999;
}

.progress-list {
  padding-left: 30rpx;
}

.progress-item {
  display: flex;
  gap: 20rpx;
  padding-bottom: 30rpx;
  position: relative;
  
  &:last-child {
    padding-bottom: 0;
  }
  
  &::before {
    content: '';
    position: absolute;
    left: 10rpx;
    top: 30rpx;
    bottom: 0;
    width: 2rpx;
    background: #eee;
  }
  
  &:last-child::before {
    display: none;
  }
  
  &.active {
    .progress-dot {
      background: #ef4444;
    }
    
    .progress-title {
      color: #333;
    }
  }
}

.progress-dot {
  width: 20rpx;
  height: 20rpx;
  border-radius: 50%;
  background: #eee;
  flex-shrink: 0;
  position: relative;
  z-index: 1;
}

.progress-content {
  flex: 1;
}

.progress-title {
  font-size: 26rpx;
  color: #999;
}

.progress-time {
  font-size: 22rpx;
  color: #999;
  margin-top: 8rpx;
}

.action-bar {
  display: flex;
  gap: 20rpx;
  padding: 20rpx 30rpx;
  background: #fff;
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
}

.cancel-btn, .contact-btn {
  flex: 1;
  height: 88rpx;
  background: #fff;
  border: 2rpx solid #ddd;
  border-radius: 44rpx;
  font-size: 28rpx;
  color: #666;
}

.pay-btn {
  flex: 1;
  height: 88rpx;
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  border-radius: 44rpx;
  font-size: 28rpx;
  color: #fff;
  border: none;
}
</style>