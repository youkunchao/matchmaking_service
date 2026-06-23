<template>
  <view class="page">
    <view class="header">
      <text class="header-title">会员套餐</text>
      <text class="header-desc">开通会员，享受更多优质服务</text>
    </view>

    <view class="package-list">
      <view 
        class="package-card" 
        v-for="pkg in packages" 
        :key="pkg.id"
        :class="{ active: selectedPackage === pkg.id, recommend: pkg.recommend }"
        @click="selectPackage(pkg.id)"
      >
        <view class="package-badge" v-if="pkg.recommend">推荐</view>
        <view class="package-header">
          <text class="package-name">{{ pkg.name }}</text>
          <view class="package-price">
            <text class="price-symbol">¥</text>
            <text class="price-value">{{ pkg.price }}</text>
          </view>
        </view>
        <text class="package-duration">有效期 {{ pkg.duration }}个月</text>
        <view class="package-features">
          <view class="feature-item" v-for="feature in pkg.features" :key="feature">
            <text class="feature-icon">✓</text>
            <text class="feature-text">{{ feature }}</text>
          </view>
        </view>
        <view class="package-select" v-if="selectedPackage === pkg.id">
          <text class="select-icon">✓</text>
        </view>
      </view>
    </view>

    <view class="comparison-table">
      <view class="table-header">
        <text class="table-title">套餐对比</text>
      </view>
      <view class="table-content">
        <view class="table-row header-row">
          <text class="table-cell">功能</text>
          <text class="table-cell">基础</text>
          <text class="table-cell">高级</text>
          <text class="table-cell">VIP</text>
        </view>
        <view class="table-row" v-for="item in comparisonData" :key="item.feature">
          <text class="table-cell">{{ item.feature }}</text>
          <text class="table-cell" :class="{ highlight: item.basic }">{{ item.basic ? '✓' : '-' }}</text>
          <text class="table-cell" :class="{ highlight: item.advanced }">{{ item.advanced ? '✓' : '-' }}</text>
          <text class="table-cell" :class="{ highlight: item.vip }">{{ item.vip ? '✓' : '-' }}</text>
        </view>
      </view>
    </view>

    <view class="action-bar">
      <view class="total-info">
        <text class="total-label">已选套餐:</text>
        <text class="total-value">{{ selectedPackageName }}</text>
        <text class="total-price">¥{{ selectedPackagePrice }}</text>
      </view>
      <button class="buy-btn" @click="buyPackage">立即开通</button>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

const packages = ref([
  {
    id: 1,
    name: '基础会员',
    price: 299,
    duration: 3,
    features: ['每日推荐5位匹配对象', '在线聊天', '查看用户资料', '参与线下活动'],
    recommend: false
  },
  {
    id: 2,
    name: '高级会员',
    price: 599,
    duration: 6,
    features: ['每日推荐10位匹配对象', '在线聊天', '查看用户资料', '专属红娘服务', '优先参与线下活动'],
    recommend: true
  },
  {
    id: 3,
    name: 'VIP会员',
    price: 999,
    duration: 12,
    features: ['无限匹配推荐', '在线聊天', '查看用户资料', '专属红娘一对一', '线下活动优先报名', 'VIP专属活动'],
    recommend: false
  },
  {
    id: 4,
    name: '定制猎婚',
    price: 2999,
    duration: 24,
    features: ['专属红娘一对一服务', '精准匹配推荐', '线下见面安排', '全程跟踪服务', '成功配对保障'],
    recommend: false
  }
])

const comparisonData = ref([
  { feature: '每日推荐', basic: '5位', advanced: '10位', vip: '无限' },
  { feature: '在线聊天', basic: true, advanced: true, vip: true },
  { feature: '查看资料', basic: true, advanced: true, vip: true },
  { feature: '专属红娘', basic: false, advanced: true, vip: true },
  { feature: '线下活动', basic: '普通', advanced: '优先', vip: 'VIP专属' },
  { feature: '服务时长', basic: '3个月', advanced: '6个月', vip: '12个月' }
])

const selectedPackage = ref(2)

const selectedPackageName = computed(() => {
  const pkg = packages.value.find(p => p.id === selectedPackage.value)
  return pkg?.name || ''
})

const selectedPackagePrice = computed(() => {
  const pkg = packages.value.find(p => p.id === selectedPackage.value)
  return pkg?.price || 0
})

const selectPackage = (id: number) => {
  selectedPackage.value = id
}

const buyPackage = () => {
  uni.showModal({
    title: '确认购买',
    content: `确定要购买${selectedPackageName.value}（¥${selectedPackagePrice.value}）吗？`,
    success: (res) => {
      if (res.confirm) {
        uni.showToast({ title: '跳转支付...', icon: 'none' })
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

.header {
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  padding: 40rpx 30rpx;
  text-align: center;
}

.header-title {
  font-size: 36rpx;
  font-weight: bold;
  color: #fff;
}

.header-desc {
  font-size: 26rpx;
  color: rgba(255, 255, 255, 0.8);
  margin-top: 12rpx;
}

.package-list {
  padding: 20rpx;
  display: flex;
  flex-wrap: wrap;
  gap: 16rpx;
}

.package-card {
  width: calc(50% - 8rpx);
  background: #fff;
  border-radius: 16rpx;
  padding: 24rpx;
  position: relative;
  border: 2rpx solid transparent;
  
  &.active {
    border-color: #ef4444;
  }
  
  &.recommend {
    .package-badge {
      display: block;
    }
  }
}

.package-badge {
  display: none;
  position: absolute;
  top: -10rpx;
  right: 20rpx;
  background: #ef4444;
  color: #fff;
  font-size: 22rpx;
  padding: 6rpx 16rpx;
  border-radius: 20rpx;
}

.package-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12rpx;
}

.package-name {
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
}

.package-price {
  display: flex;
  align-items: baseline;
}

.price-symbol {
  font-size: 24rpx;
  color: #ef4444;
}

.price-value {
  font-size: 36rpx;
  font-weight: bold;
  color: #ef4444;
}

.package-duration {
  font-size: 24rpx;
  color: #999;
  margin-bottom: 16rpx;
}

.package-features {
  display: flex;
  flex-direction: column;
  gap: 12rpx;
}

.feature-item {
  display: flex;
  align-items: center;
  gap: 8rpx;
}

.feature-icon {
  font-size: 22rpx;
  color: #22c55e;
}

.feature-text {
  font-size: 24rpx;
  color: #666;
}

.package-select {
  position: absolute;
  bottom: 20rpx;
  right: 20rpx;
  width: 40rpx;
  height: 40rpx;
  background: #ef4444;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.select-icon {
  color: #fff;
  font-size: 24rpx;
}

.comparison-table {
  background: #fff;
  margin: 20rpx;
  border-radius: 16rpx;
  padding: 24rpx;
}

.table-header {
  margin-bottom: 20rpx;
}

.table-title {
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
}

.table-content {
  border: 2rpx solid #eee;
  border-radius: 12rpx;
}

.table-row {
  display: flex;
  border-bottom: 2rpx solid #eee;
  
  &:last-child {
    border-bottom: none;
  }
  
  &.header-row {
    background: #f9f9f9;
    
    .table-cell {
      font-weight: bold;
      color: #333;
    }
  }
}

.table-cell {
  flex: 1;
  padding: 16rpx;
  font-size: 24rpx;
  color: #666;
  text-align: center;
  
  &:first-child {
    text-align: left;
  }
  
  &.highlight {
    color: #22c55e;
  }
}

.action-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20rpx 30rpx;
  background: #fff;
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
  box-shadow: 0 -4rpx 20rpx rgba(0, 0, 0, 0.05);
}

.total-info {
  display: flex;
  align-items: center;
  gap: 12rpx;
}

.total-label {
  font-size: 26rpx;
  color: #999;
}

.total-value {
  font-size: 28rpx;
  color: #333;
}

.total-price {
  font-size: 32rpx;
  font-weight: bold;
  color: #ef4444;
}

.buy-btn {
  width: 200rpx;
  height: 80rpx;
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  border-radius: 40rpx;
  font-size: 28rpx;
  color: #fff;
  border: none;
}
</style>