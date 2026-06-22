<template>
  <view class="page">
    <view class="tabs">
      <view class="tab-item" :class="{ active: activeTab === 'all' }" @click="activeTab = 'all'">全部</view>
      <view class="tab-item" :class="{ active: activeTab === 'upcoming' }" @click="activeTab = 'upcoming'">预告</view>
      <view class="tab-item" :class="{ active: activeTab === 'ongoing' }" @click="activeTab = 'ongoing'">进行中</view>
    </view>

    <view class="activity-list">
      <view class="activity-card" v-for="activity in filteredActivities" :key="activity.id">
        <image class="activity-image" :src="activity.image" mode="aspectFill" />
        <view class="activity-info">
          <view class="activity-tag" :class="activity.status">{{ activity.statusText }}</view>
          <text class="activity-title">{{ activity.title }}</text>
          <view class="activity-meta">
            <view class="meta-item">
              <text class="meta-icon">📅</text>
              <text class="meta-text">{{ activity.date }}</text>
            </view>
            <view class="meta-item">
              <text class="meta-icon">📍</text>
              <text class="meta-text">{{ activity.location }}</text>
            </view>
          </view>
          <view class="activity-desc">{{ activity.description }}</view>
          <view class="activity-footer">
            <view class="activity-price">
              <text class="price-symbol">¥</text>
              <text class="price-value">{{ activity.price }}</text>
              <text class="price-unit">/人</text>
            </view>
            <button class="activity-btn" @click="handleSignup(activity.id)">
              {{ activity.status === 'upcoming' ? '立即报名' : '查看详情' }}
            </button>
          </view>
        </view>
      </view>
    </view>

    <view class="empty-state" v-if="filteredActivities.length === 0">
      <view class="empty-icon">📅</view>
      <text class="empty-text">暂无活动</text>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

const activeTab = ref('all')

const activities = ref([
  { 
    id: 1, 
    title: '浪漫下午茶相亲会', 
    image: 'https://via.placeholder.com/600x400', 
    status: 'upcoming', 
    statusText: '预告中',
    date: '2024年2月14日 14:00',
    location: '深圳南山区某咖啡厅',
    description: '情人节特别活动，与心仪的TA共度浪漫下午茶时光',
    price: '199'
  },
  { 
    id: 2, 
    title: '户外徒步交友活动', 
    image: 'https://via.placeholder.com/600x400', 
    status: 'ongoing', 
    statusText: '进行中',
    date: '2024年2月10日 09:00',
    location: '深圳福田莲花山公园',
    description: '春暖花开，一起徒步踏青，结识新朋友',
    price: '158'
  },
  { 
    id: 3, 
    title: '高端精英酒会', 
    image: 'https://via.placeholder.com/600x400', 
    status: 'upcoming', 
    statusText: '预告中',
    date: '2024年2月18日 19:00',
    location: '深圳福田CBD某酒店',
    description: '汇聚各行业精英，拓展人脉，邂逅缘分',
    price: '399'
  },
  { 
    id: 4, 
    title: '亲子互动体验日', 
    image: 'https://via.placeholder.com/600x400', 
    status: 'upcoming', 
    statusText: '预告中',
    date: '2024年2月25日 10:00',
    location: '深圳宝安区亲子乐园',
    description: '专为有孩子的单身父母打造的交友活动',
    price: '258'
  }
])

const filteredActivities = computed(() => {
  if (activeTab.value === 'all') return activities.value
  return activities.value.filter(a => a.status === activeTab.value)
})

const handleSignup = (id: number) => {
  uni.showToast({
    title: '报名成功',
    icon: 'success'
  })
}
</script>

<style lang="scss" scoped>
.page {
  min-height: 100vh;
  background: #f5f5f5;
  padding-bottom: 120rpx;
}

.tabs {
  display: flex;
  background: #fff;
  padding: 0 30rpx;
  border-bottom: 2rpx solid #f0f0f0;
}

.tab-item {
  flex: 1;
  text-align: center;
  padding: 30rpx 0;
  font-size: 28rpx;
  color: #666;
  position: relative;
  
  &.active {
    color: #ef4444;
    
    &::after {
      content: '';
      position: absolute;
      bottom: 0;
      left: 50%;
      transform: translateX(-50%);
      width: 60rpx;
      height: 6rpx;
      background: #ef4444;
      border-radius: 6rpx;
    }
  }
}

.activity-list {
  padding: 20rpx;
}

.activity-card {
  background: #fff;
  border-radius: 20rpx;
  overflow: hidden;
  margin-bottom: 20rpx;
}

.activity-image {
  width: 100%;
  height: 360rpx;
}

.activity-info {
  padding: 24rpx;
}

.activity-tag {
  display: inline-block;
  font-size: 22rpx;
  padding: 8rpx 20rpx;
  border-radius: 20rpx;
  margin-bottom: 16rpx;
  
  &.upcoming {
    background: #fef2f2;
    color: #ef4444;
  }
  
  &.ongoing {
    background: #dcfce7;
    color: #22c55e;
  }
}

.activity-title {
  display: block;
  font-size: 32rpx;
  font-weight: bold;
  color: #333;
  margin-bottom: 16rpx;
}

.activity-meta {
  display: flex;
  gap: 30rpx;
  margin-bottom: 16rpx;
}

.meta-item {
  display: flex;
  align-items: center;
  gap: 8rpx;
}

.meta-icon {
  font-size: 28rpx;
}

.meta-text {
  font-size: 24rpx;
  color: #999;
}

.activity-desc {
  font-size: 26rpx;
  color: #666;
  line-height: 1.6;
  margin-bottom: 20rpx;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 2;
  overflow: hidden;
}

.activity-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.activity-price {
  display: flex;
  align-items: baseline;
}

.price-symbol {
  font-size: 26rpx;
  color: #ef4444;
}

.price-value {
  font-size: 40rpx;
  font-weight: bold;
  color: #ef4444;
}

.price-unit {
  font-size: 24rpx;
  color: #999;
  margin-left: 8rpx;
}

.activity-btn {
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  color: #fff;
  border: none;
  border-radius: 32rpx;
  padding: 20rpx 48rpx;
  font-size: 28rpx;
}
</style>
