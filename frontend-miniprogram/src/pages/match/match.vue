<template>
  <view class="page">
    <view class="filter-bar">
      <view class="filter-item" :class="{ active: filter.gender === '' }" @click="setFilter('gender', '')">全部</view>
      <view class="filter-item" :class="{ active: filter.gender === 'male' }" @click="setFilter('gender', 'male')">男士</view>
      <view class="filter-item" :class="{ active: filter.gender === 'female' }" @click="setFilter('gender', 'female')">女士</view>
    </view>

    <view class="filter-section">
      <view class="filter-row">
        <text class="filter-label">年龄范围</text>
        <view class="filter-range">
          <input class="filter-input" type="number" v-model="filter.minAge" placeholder="最小" />
          <text class="filter-separator">-</text>
          <input class="filter-input" type="number" v-model="filter.maxAge" placeholder="最大" />
        </view>
      </view>
      <view class="filter-row">
        <text class="filter-label">学历</text>
        <view class="filter-options">
          <view 
            class="filter-option" 
            v-for="edu in educationOptions" 
            :key="edu.value"
            :class="{ active: filter.education === edu.value }"
            @click="setFilter('education', edu.value)"
          >{{ edu.label }}</view>
        </view>
      </view>
      <view class="filter-row">
        <text class="filter-label">城市</text>
        <view class="filter-options">
          <view 
            class="filter-option" 
            v-for="city in cityOptions" 
            :key="city"
            :class="{ active: filter.city === city }"
            @click="setFilter('city', city)"
          >{{ city }}</view>
        </view>
      </view>
    </view>

    <view class="match-result">
      <text class="result-count">共找到 {{ matchedUsers.length }} 位匹配用户</text>
      <view class="user-list">
        <view class="user-card" v-for="user in matchedUsers" :key="user.id" @click="goToUserDetail(user.id)">
          <image class="user-avatar" :src="user.avatar" mode="aspectFill" />
          <view class="user-info">
            <view class="user-header">
              <text class="user-name">{{ user.name }}</text>
              <text class="user-age">{{ user.age }}岁</text>
              <view class="user-gender" :class="user.gender">{{ user.gender === 'male' ? '男' : '女' }}</view>
            </view>
            <view class="user-details">
              <text class="detail-item">{{ user.city }}</text>
              <text class="detail-divider">|</text>
              <text class="detail-item">{{ user.education }}</text>
              <text class="detail-divider">|</text>
              <text class="detail-item">{{ user.occupation }}</text>
            </view>
            <view class="user-tags">
              <text class="user-tag" v-for="tag in user.tags" :key="tag">{{ tag }}</text>
            </view>
          </view>
          <view class="match-indicator">
            <text class="match-percent">{{ user.matchScore }}%</text>
            <text class="match-label">匹配度</text>
          </view>
        </view>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, reactive, computed } from 'vue'

const filter = reactive({
  gender: '',
  minAge: '',
  maxAge: '',
  education: '',
  city: ''
})

const educationOptions = [
  { label: '不限', value: '' },
  { label: '大专', value: '大专' },
  { label: '本科', value: '本科' },
  { label: '硕士', value: '硕士' },
  { label: '博士', value: '博士' }
]

const cityOptions = ['不限', '深圳', '广州', '东莞', '惠州']

const allUsers = ref([
  { id: 1, name: '小芳', avatar: 'https://via.placeholder.com/100', age: 28, gender: 'female', city: '深圳', education: '本科', occupation: '程序员', tags: ['爱旅行', '美食', '阅读'], matchScore: 92 },
  { id: 2, name: '小明', avatar: 'https://via.placeholder.com/100', age: 30, gender: 'male', city: '广州', education: '硕士', occupation: '设计师', tags: ['摄影', '健身', '音乐'], matchScore: 88 },
  { id: 3, name: '小红', avatar: 'https://via.placeholder.com/100', age: 26, gender: 'female', city: '深圳', education: '本科', occupation: '教师', tags: ['运动', '电影', '烹饪'], matchScore: 95 },
  { id: 4, name: '小李', avatar: 'https://via.placeholder.com/100', age: 32, gender: 'male', city: '东莞', education: '博士', occupation: '医生', tags: ['阅读', '旅行', '书法'], matchScore: 85 },
  { id: 5, name: '小王', avatar: 'https://via.placeholder.com/100', age: 29, gender: 'male', city: '深圳', education: '本科', occupation: '工程师', tags: ['游戏', '篮球', '音乐'], matchScore: 89 },
  { id: 6, name: '小张', avatar: 'https://via.placeholder.com/100', age: 27, gender: 'female', city: '广州', education: '硕士', occupation: '产品经理', tags: ['旅行', '瑜伽', '美食'], matchScore: 91 }
])

const matchedUsers = computed(() => {
  return allUsers.value.filter(user => {
    if (filter.gender && user.gender !== filter.gender) return false
    if (filter.minAge && user.age < parseInt(filter.minAge)) return false
    if (filter.maxAge && user.age > parseInt(filter.maxAge)) return false
    if (filter.education && user.education !== filter.education) return false
    if (filter.city && filter.city !== '不限' && user.city !== filter.city) return false
    return true
  })
})

const setFilter = (key: string, value: string) => {
  (filter as Record<string, string>)[key] = value
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

.filter-bar {
  display: flex;
  background: #fff;
  padding: 20rpx 30rpx;
  gap: 20rpx;
}

.filter-item {
  flex: 1;
  text-align: center;
  padding: 20rpx;
  font-size: 28rpx;
  color: #666;
  border-radius: 20rpx;
  background: #f5f5f5;
  
  &.active {
    background: #ef4444;
    color: #fff;
  }
}

.filter-section {
  background: #fff;
  margin-top: 20rpx;
  padding: 30rpx;
}

.filter-row {
  margin-bottom: 24rpx;
  
  &:last-child {
    margin-bottom: 0;
  }
}

.filter-label {
  display: block;
  font-size: 26rpx;
  color: #333;
  margin-bottom: 16rpx;
}

.filter-range {
  display: flex;
  align-items: center;
  gap: 16rpx;
}

.filter-input {
  width: 160rpx;
  height: 72rpx;
  border: 2rpx solid #eee;
  border-radius: 12rpx;
  padding: 0 20rpx;
  font-size: 26rpx;
}

.filter-separator {
  font-size: 28rpx;
  color: #999;
}

.filter-options {
  display: flex;
  flex-wrap: wrap;
  gap: 16rpx;
}

.filter-option {
  padding: 16rpx 32rpx;
  font-size: 26rpx;
  color: #666;
  background: #f5f5f5;
  border-radius: 20rpx;
  
  &.active {
    background: #fef2f2;
    color: #ef4444;
  }
}

.match-result {
  padding: 30rpx;
}

.result-count {
  display: block;
  font-size: 26rpx;
  color: #999;
  margin-bottom: 20rpx;
}

.user-list {
  display: flex;
  flex-direction: column;
  gap: 20rpx;
}

.user-card {
  display: flex;
  background: #fff;
  border-radius: 20rpx;
  padding: 24rpx;
}

.user-avatar {
  width: 140rpx;
  height: 140rpx;
  border-radius: 50%;
  flex-shrink: 0;
}

.user-info {
  flex: 1;
  margin-left: 20rpx;
  display: flex;
  flex-direction: column;
}

.user-header {
  display: flex;
  align-items: center;
  gap: 12rpx;
  margin-bottom: 12rpx;
}

.user-name {
  font-size: 30rpx;
  font-weight: bold;
  color: #333;
}

.user-age {
  font-size: 26rpx;
  color: #666;
}

.user-gender {
  font-size: 22rpx;
  padding: 6rpx 16rpx;
  border-radius: 20rpx;
  
  &.male {
    background: #dbeafe;
    color: #3b82f6;
  }
  
  &.female {
    background: #fce7f3;
    color: #ec4899;
  }
}

.user-details {
  display: flex;
  align-items: center;
  gap: 12rpx;
  margin-bottom: 16rpx;
}

.detail-item {
  font-size: 24rpx;
  color: #999;
}

.detail-divider {
  color: #eee;
}

.user-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 12rpx;
}

.user-tag {
  font-size: 22rpx;
  color: #ef4444;
  background: #fef2f2;
  padding: 8rpx 16rpx;
  border-radius: 16rpx;
}

.match-indicator {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 0 24rpx;
}

.match-percent {
  font-size: 36rpx;
  font-weight: bold;
  color: #ef4444;
}

.match-label {
  font-size: 22rpx;
  color: #999;
}
</style>
