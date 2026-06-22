<script setup lang="ts">
import { onLaunch, onShow, onHide } from '@dcloudio/uni-app'

onLaunch(() => {
  console.log('App Launch')
})

onShow(() => {
  console.log('App Show')
})

onHide(() => {
  console.log('App Hide')
})
</script>

<style lang="scss">
page {
  font-family: 'PingFang SC', 'Microsoft YaHei', sans-serif;
  background-color: #f5f5f5;
}

.container {
  padding: 0 20rpx;
}

.card {
  background: #fff;
  border-radius: 20rpx;
  padding: 30rpx;
  margin-bottom: 20rpx;
  box-shadow: 0 4rpx 20rpx rgba(0, 0, 0, 0.05);
}

.btn-primary {
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  color: #fff;
  border: none;
  border-radius: 40rpx;
  padding: 24rpx 60rpx;
  font-size: 28rpx;
  font-weight: 500;
}

.btn-secondary {
  background: #fff;
  color: #ef4444;
  border: 2rpx solid #ef4444;
  border-radius: 40rpx;
  padding: 24rpx 60rpx;
  font-size: 28rpx;
  font-weight: 500;
}

.text-primary {
  color: #ef4444;
}

.text-gray {
  color: #999;
}

.text-sm {
  font-size: 24rpx;
}

.text-xs {
  font-size: 22rpx;
}
</style>
