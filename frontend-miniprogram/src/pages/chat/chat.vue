<template>
  <view class="page">
    <view class="chat-list">
      <view class="chat-item" v-for="chat in chats" :key="chat.id" @click="goToChatDetail(chat.id)">
        <image class="chat-avatar" :src="chat.avatar" mode="aspectFill" />
        <view class="chat-content">
          <view class="chat-header">
            <text class="chat-name">{{ chat.name }}</text>
            <text class="chat-time">{{ chat.time }}</text>
          </view>
          <view class="chat-footer">
            <text class="chat-message">{{ chat.message }}</text>
            <view class="chat-badge" v-if="chat.unread > 0">{{ chat.unread }}</view>
          </view>
        </view>
      </view>
    </view>

    <view class="empty-state" v-if="chats.length === 0">
      <view class="empty-icon">💬</view>
      <text class="empty-text">暂无消息</text>
      <text class="empty-hint">快去匹配心仪的人吧</text>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const chats = ref([
  { id: 1, name: '小芳', avatar: 'https://via.placeholder.com/100', message: '周末有空一起喝咖啡吗？', time: '10:30', unread: 3 },
  { id: 2, name: '小红', avatar: 'https://via.placeholder.com/100', message: '好的，明天见~', time: '昨天', unread: 0 },
  { id: 3, name: '红娘小李', avatar: 'https://via.placeholder.com/100', message: '您好，我是您的专属红娘...', time: '09:00', unread: 1 },
  { id: 4, name: '小明', avatar: 'https://via.placeholder.com/100', message: '[图片]', time: '周一', unread: 0 }
])

const goToChatDetail = (id: number) => {
  uni.navigateTo({ url: `/pages/chat-detail/chat-detail?id=${id}` })
}
</script>

<style lang="scss" scoped>
.page {
  min-height: 100vh;
  background: #f5f5f5;
  padding-bottom: 120rpx;
}

.chat-list {
  padding: 20rpx;
}

.chat-item {
  display: flex;
  background: #fff;
  border-radius: 20rpx;
  padding: 24rpx;
  margin-bottom: 16rpx;
}

.chat-avatar {
  width: 100rpx;
  height: 100rpx;
  border-radius: 50%;
  flex-shrink: 0;
}

.chat-content {
  flex: 1;
  margin-left: 20rpx;
  display: flex;
  flex-direction: column;
}

.chat-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12rpx;
}

.chat-name {
  font-size: 30rpx;
  font-weight: bold;
  color: #333;
}

.chat-time {
  font-size: 22rpx;
  color: #999;
}

.chat-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.chat-message {
  font-size: 26rpx;
  color: #666;
  flex: 1;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.chat-badge {
  background: #ef4444;
  color: #fff;
  font-size: 22rpx;
  padding: 4rpx 16rpx;
  border-radius: 20rpx;
  min-width: 40rpx;
  text-align: center;
}

.empty-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 120rpx 40rpx;
}

.empty-icon {
  font-size: 120rpx;
  margin-bottom: 30rpx;
}

.empty-text {
  font-size: 32rpx;
  color: #333;
  margin-bottom: 16rpx;
}

.empty-hint {
  font-size: 26rpx;
  color: #999;
}
</style>
