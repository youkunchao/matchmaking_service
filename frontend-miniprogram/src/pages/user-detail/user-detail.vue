<template>
  <view class="page">
    <view class="profile-header">
      <view class="header-bg"></view>
      <view class="profile-info">
        <image class="profile-avatar" :src="user.avatar" mode="aspectFill" />
        <view class="profile-meta">
          <view class="profile-name-row">
            <text class="profile-name">{{ user.name }}</text>
            <view class="profile-gender" :class="user.gender">{{ user.gender === 'male' ? '男' : '女' }}</view>
            <text class="profile-age">{{ user.age }}岁</text>
          </view>
          <text class="profile-location">{{ user.city }} · {{ user.education }} · {{ user.occupation }}</text>
          <view class="match-badge">匹配度 {{ user.matchScore }}%</view>
        </view>
      </view>
    </view>

    <view class="action-bar">
      <button class="action-btn secondary" @click="goToChat">发消息</button>
      <button class="action-btn primary" @click="handleLike">
        {{ isLiked ? '已喜欢' : '喜欢' }}
      </button>
    </view>

    <view class="section">
      <view class="section-title">基本资料</view>
      <view class="info-list">
        <view class="info-item">
          <text class="info-label">身高</text>
          <text class="info-value">{{ user.height }}cm</text>
        </view>
        <view class="info-item">
          <text class="info-label">体重</text>
          <text class="info-value">{{ user.weight }}kg</text>
        </view>
        <view class="info-item">
          <text class="info-label">月收入</text>
          <text class="info-value">{{ user.income }}</text>
        </view>
        <view class="info-item">
          <text class="info-label">婚姻状况</text>
          <text class="info-value">{{ user.maritalStatus }}</text>
        </view>
        <view class="info-item">
          <text class="info-label">住房情况</text>
          <text class="info-value">{{ user.housing }}</text>
        </view>
        <view class="info-item">
          <text class="info-label">购车情况</text>
          <text class="info-value">{{ user.car }}</text>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="section-title">兴趣爱好</view>
      <view class="hobby-list">
        <text class="hobby-tag" v-for="hobby in user.hobbies" :key="hobby">{{ hobby }}</text>
      </view>
    </view>

    <view class="section">
      <view class="section-title">自我介绍</view>
      <text class="intro-text">{{ user.intro }}</text>
    </view>

    <view class="section">
      <view class="section-title">择偶要求</view>
      <text class="intro-text">{{ user.requirement }}</text>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'

const user = reactive({
  id: 1,
  name: '小芳',
  avatar: 'https://via.placeholder.com/100',
  gender: 'female',
  age: 28,
  city: '深圳',
  education: '本科',
  occupation: '程序员',
  matchScore: 92,
  height: 165,
  weight: 52,
  income: '8000-12000',
  maritalStatus: '未婚',
  housing: '已购房',
  car: '已购车',
  hobbies: ['阅读', '旅行', '美食', '音乐'],
  intro: '性格开朗，喜欢旅行和美食，希望能找到一个志同道合的人一起探索世界。工作认真负责，生活中喜欢尝试新鲜事物。',
  requirement: '希望对方年龄在28-35岁之间，本科及以上学历，有稳定工作，性格温和，有共同的兴趣爱好。'
})

const isLiked = ref(false)

const goToChat = () => {
  uni.navigateTo({ url: `/pages/chat-detail/chat-detail?id=${user.id}` })
}

const handleLike = () => {
  isLiked.value = !isLiked.value
  uni.showToast({ 
    title: isLiked.value ? '喜欢成功' : '已取消喜欢', 
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

.profile-header {
  position: relative;
  padding-bottom: 30rpx;
}

.header-bg {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  height: 450rpx;
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
}

.profile-info {
  position: relative;
  z-index: 1;
  padding: 100rpx 30rpx 0;
  display: flex;
  gap: 24rpx;
}

.profile-avatar {
  width: 180rpx;
  height: 180rpx;
  border-radius: 50%;
  border: 6rpx solid rgba(255, 255, 255, 0.8);
}

.profile-meta {
  flex: 1;
  padding-top: 30rpx;
}

.profile-name-row {
  display: flex;
  align-items: center;
  gap: 12rpx;
  margin-bottom: 12rpx;
}

.profile-name {
  font-size: 36rpx;
  font-weight: bold;
  color: #fff;
}

.profile-gender {
  font-size: 24rpx;
  padding: 6rpx 16rpx;
  border-radius: 20rpx;
  
  &.male {
    background: rgba(59, 130, 246, 0.8);
    color: #fff;
  }
  
  &.female {
    background: rgba(236, 72, 153, 0.8);
    color: #fff;
  }
}

.profile-age {
  font-size: 28rpx;
  color: rgba(255, 255, 255, 0.8);
}

.profile-location {
  display: block;
  font-size: 26rpx;
  color: rgba(255, 255, 255, 0.8);
  margin-bottom: 16rpx;
}

.match-badge {
  display: inline-block;
  background: rgba(255, 255, 255, 0.2);
  padding: 8rpx 20rpx;
  border-radius: 20rpx;
  font-size: 24rpx;
  color: #fff;
}

.action-bar {
  display: flex;
  gap: 20rpx;
  padding: 30rpx;
  position: relative;
  z-index: 1;
  margin-top: -20rpx;
}

.action-btn {
  flex: 1;
  height: 88rpx;
  border-radius: 44rpx;
  font-size: 30rpx;
  border: none;
  
  &.primary {
    background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
    color: #fff;
  }
  
  &.secondary {
    background: #fff;
    color: #ef4444;
    border: 2rpx solid #ef4444;
  }
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
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 20rpx;
}

.info-item {
  padding: 16rpx;
  background: #f9f9f9;
  border-radius: 12rpx;
}

.info-label {
  display: block;
  font-size: 24rpx;
  color: #999;
  margin-bottom: 8rpx;
}

.info-value {
  font-size: 26rpx;
  color: #333;
}

.hobby-list {
  display: flex;
  flex-wrap: wrap;
  gap: 12rpx;
}

.hobby-tag {
  padding: 12rpx 24rpx;
  background: #fef2f2;
  color: #ef4444;
  font-size: 24rpx;
  border-radius: 24rpx;
}

.intro-text {
  font-size: 28rpx;
  color: #666;
  line-height: 1.8;
}
</style>
