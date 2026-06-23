<template>
  <view class="page">
    <view class="header">
      <view class="header-bg"></view>
      <view class="header-content">
        <view class="logo">
          <view class="logo-icon">Z</view>
          <text class="logo-text">郑好遇见你</text>
        </view>
        <text class="slogan">正好遇见对的人</text>
        <view class="stats">
          <view class="stat-item">
            <text class="stat-value">10000+</text>
            <text class="stat-label">成功配对</text>
          </view>
          <view class="stat-divider"></view>
          <view class="stat-item">
            <text class="stat-value">500+</text>
            <text class="stat-label">专业红娘</text>
          </view>
          <view class="stat-divider"></view>
          <view class="stat-item">
            <text class="stat-value">98%</text>
            <text class="stat-label">实名认证率</text>
          </view>
        </view>
        <view class="header-btn">
          <button class="btn-primary" @click="goToMatch">立即匹配</button>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="section-header">
        <text class="section-title">今日推荐</text>
        <text class="section-more" @click="goToMatch">查看更多 →</text>
      </view>
      <scroll-view scroll-x class="match-scroll">
        <view class="match-list">
          <view class="match-card" v-for="user in recommendUsers" :key="user.id" @click="goToUserDetail(user.id)">
            <image class="match-avatar" :src="user.avatar" mode="aspectFill" />
            <text class="match-name">{{ user.name }}</text>
            <text class="match-info">{{ user.age }}岁 | {{ user.city }}</text>
            <view class="match-tags">
              <text class="match-tag" v-for="tag in user.tags" :key="tag">{{ tag }}</text>
            </view>
            <view class="match-score">匹配度 {{ user.matchScore }}%</view>
          </view>
        </view>
      </scroll-view>
    </view>

    <view class="section">
      <view class="section-header">
        <text class="section-title">热门活动</text>
        <text class="section-more" @click="goToActivity">查看更多 →</text>
      </view>
      <view class="activity-list">
        <view class="activity-card" v-for="activity in activities" :key="activity.id">
          <image class="activity-image" :src="activity.image" mode="aspectFill" />
          <view class="activity-content">
            <view class="activity-tag" :class="activity.status">{{ activity.statusText }}</view>
            <text class="activity-title">{{ activity.title }}</text>
            <view class="activity-info">
              <text class="activity-date">{{ activity.date }}</text>
              <text class="activity-location">{{ activity.location }}</text>
            </view>
            <view class="activity-footer">
              <text class="activity-price">{{ activity.price }}</text>
              <text class="activity-participants">{{ activity.participants }}人已报名</text>
            </view>
          </view>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="section-header">
        <text class="section-title">核心服务</text>
      </view>
      <view class="service-grid">
        <view class="service-item" v-for="service in services" :key="service.title">
          <view class="service-icon">{{ service.icon }}</view>
          <text class="service-title">{{ service.title }}</text>
          <text class="service-desc">{{ service.desc }}</text>
        </view>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const recommendUsers = ref([
  { id: 1, name: '小芳', avatar: 'https://via.placeholder.com/100', age: 28, city: '深圳', tags: ['本科', '程序员', '爱旅行'], matchScore: 92 },
  { id: 2, name: '小明', avatar: 'https://via.placeholder.com/100', age: 30, city: '广州', tags: ['硕士', '设计师', '爱美食'], matchScore: 88 },
  { id: 3, name: '小红', avatar: 'https://via.placeholder.com/100', age: 26, city: '深圳', tags: ['本科', '教师', '爱运动'], matchScore: 95 },
  { id: 4, name: '小李', avatar: 'https://via.placeholder.com/100', age: 32, city: '东莞', tags: ['博士', '医生', '爱阅读'], matchScore: 85 }
])

const activities = ref([
  { id: 1, title: '浪漫下午茶相亲会', image: 'https://via.placeholder.com/300x200', status: 'upcoming', statusText: '预告中', date: '2月14日', location: '深圳南山', price: '¥199', participants: 32 },
  { id: 2, title: '户外徒步交友活动', image: 'https://via.placeholder.com/300x200', status: 'ongoing', statusText: '进行中', date: '2月10日', location: '深圳福田', price: '¥158', participants: 45 }
])

const services = ref([
  { icon: '💑', title: '精准匹配', desc: 'AI算法智能推荐' },
  { icon: '👩‍❤️‍👨', title: '红娘服务', desc: '一对一专属指导' },
  { icon: '✅', title: '实名认证', desc: '信息真实可靠' },
  { icon: '📅', title: '线下活动', desc: '丰富多彩社交' }
])

const goToMatch = () => {
  uni.switchTab({ url: '/pages/match/match' })
}

const goToActivity = () => {
  uni.switchTab({ url: '/pages/activity/activity' })
}

const goToUserDetail = (id: number) => {
  uni.navigateTo({ url: `/pages/user-detail/user-detail?id=${id}` })
}
</script>

<style lang="scss" scoped>
.page {
  min-height: 100vh;
  background: #f5f5f5;
  padding-bottom: 120rpx;
}

.header {
  position: relative;
  padding-top: 100rpx;
  padding-bottom: 60rpx;
}

.header-bg {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  height: 600rpx;
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  border-radius: 0 0 60rpx 60rpx;
}

.header-content {
  position: relative;
  z-index: 1;
  padding: 0 30rpx;
  text-align: center;
}

.logo {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 16rpx;
  margin-bottom: 20rpx;
}

.logo-icon {
  width: 80rpx;
  height: 80rpx;
  background: #fff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 40rpx;
  font-weight: bold;
  color: #ef4444;
}

.logo-text {
  font-size: 40rpx;
  font-weight: bold;
  color: #fff;
}

.slogan {
  font-size: 32rpx;
  color: rgba(255, 255, 255, 0.9);
  margin-bottom: 40rpx;
}

.stats {
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(255, 255, 255, 0.2);
  border-radius: 20rpx;
  padding: 30rpx;
  margin-bottom: 40rpx;
}

.stat-item {
  flex: 1;
  text-align: center;
}

.stat-value {
  display: block;
  font-size: 40rpx;
  font-weight: bold;
  color: #fff;
}

.stat-label {
  display: block;
  font-size: 24rpx;
  color: rgba(255, 255, 255, 0.8);
  margin-top: 8rpx;
}

.stat-divider {
  width: 2rpx;
  height: 60rpx;
  background: rgba(255, 255, 255, 0.3);
}

.header-btn {
  margin-top: 20rpx;
}

.btn-primary {
  width: 400rpx;
  height: 88rpx;
  line-height: 88rpx;
  font-size: 32rpx;
  background: #fff;
  color: #ef4444;
  border-radius: 44rpx;
  border: none;
}

.section {
  padding: 30rpx;
}

.section-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20rpx;
}

.section-title {
  font-size: 32rpx;
  font-weight: bold;
  color: #333;
}

.section-more {
  font-size: 26rpx;
  color: #ef4444;
}

.match-scroll {
  white-space: nowrap;
}

.match-list {
  display: inline-flex;
  gap: 20rpx;
  padding-bottom: 20rpx;
}

.match-card {
  width: 260rpx;
  background: #fff;
  border-radius: 20rpx;
  padding: 20rpx;
  display: inline-block;
  vertical-align: top;
}

.match-avatar {
  width: 120rpx;
  height: 120rpx;
  border-radius: 50%;
  margin: 0 auto;
  display: block;
}

.match-name {
  display: block;
  text-align: center;
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
  margin-top: 16rpx;
}

.match-info {
  display: block;
  text-align: center;
  font-size: 24rpx;
  color: #999;
  margin-top: 8rpx;
}

.match-tags {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 8rpx;
  margin-top: 16rpx;
}

.match-tag {
  font-size: 22rpx;
  color: #ef4444;
  background: #fef2f2;
  padding: 6rpx 16rpx;
  border-radius: 20rpx;
}

.match-score {
  text-align: center;
  font-size: 24rpx;
  color: #ef4444;
  margin-top: 16rpx;
}

.activity-list {
  display: flex;
  flex-direction: column;
  gap: 20rpx;
}

.activity-card {
  background: #fff;
  border-radius: 20rpx;
  overflow: hidden;
  display: flex;
}

.activity-image {
  width: 240rpx;
  height: 180rpx;
  flex-shrink: 0;
}

.activity-content {
  flex: 1;
  padding: 20rpx;
  display: flex;
  flex-direction: column;
}

.activity-tag {
  align-self: flex-start;
  font-size: 22rpx;
  padding: 6rpx 16rpx;
  border-radius: 20rpx;
  margin-bottom: 12rpx;
  
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
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
  margin-bottom: 12rpx;
}

.activity-info {
  display: flex;
  gap: 20rpx;
  margin-bottom: 12rpx;
}

.activity-date, .activity-location {
  font-size: 24rpx;
  color: #999;
}

.activity-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: auto;
}

.activity-price {
  font-size: 28rpx;
  font-weight: bold;
  color: #ef4444;
}

.activity-participants {
  font-size: 24rpx;
  color: #999;
}

.service-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20rpx;
}

.service-item {
  background: #fff;
  border-radius: 20rpx;
  padding: 30rpx 20rpx;
  text-align: center;
}

.service-icon {
  font-size: 48rpx;
  margin-bottom: 16rpx;
}

.service-title {
  display: block;
  font-size: 26rpx;
  font-weight: bold;
  color: #333;
  margin-bottom: 8rpx;
}

.service-desc {
  display: block;
  font-size: 22rpx;
  color: #999;
}
</style>
