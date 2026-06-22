<template>
  <view class="page">
    <scroll-view 
      class="chat-container" 
      scroll-y 
      :scroll-into-view="scrollToId"
      scroll-with-animation
    >
      <view class="message-list">
        <view 
          class="message-item" 
          v-for="(msg, index) in messages" 
          :key="index"
          :id="`msg-${index}`"
          :class="{ self: msg.isSelf }"
        >
          <image class="message-avatar" :src="msg.isSelf ? myAvatar : chatUser.avatar" mode="aspectFill" />
          <view class="message-content">
            <text class="message-text">{{ msg.content }}</text>
            <text class="message-time">{{ msg.time }}</text>
          </view>
        </view>
      </view>
    </scroll-view>

    <view class="input-bar">
      <input 
        class="input-field" 
        v-model="inputText" 
        placeholder="输入消息..."
        confirm-type="send"
        @confirm="sendMessage"
      />
      <button class="send-btn" :class="{ active: inputText.trim() }" @click="sendMessage">发送</button>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, reactive, nextTick } from 'vue'

const chatUser = reactive({
  id: 1,
  name: '小芳',
  avatar: 'https://via.placeholder.com/100'
})

const myAvatar = 'https://via.placeholder.com/100'

const messages = ref([
  { id: 1, content: '你好呀！', time: '10:00', isSelf: false },
  { id: 2, content: '你好！很高兴认识你~', time: '10:01', isSelf: true },
  { id: 3, content: '周末有空一起喝咖啡吗？', time: '10:02', isSelf: false },
  { id: 4, content: '好呀！在哪里呢？', time: '10:03', isSelf: true },
  { id: 5, content: '就在市中心的那家星巴克吧', time: '10:05', isSelf: false },
  { id: 6, content: '好的，那周六下午2点见~', time: '10:06', isSelf: true }
])

const inputText = ref('')
const scrollToId = ref('')

const sendMessage = () => {
  if (!inputText.value.trim()) return
  
  const newMsg = {
    id: Date.now(),
    content: inputText.value,
    time: new Date().toLocaleTimeString('zh-CN', { hour: '2-digit', minute: '2-digit' }),
    isSelf: true
  }
  
  messages.value.push(newMsg)
  inputText.value = ''
  
  nextTick(() => {
    scrollToId.value = `msg-${messages.value.length - 1}`
  })

  setTimeout(() => {
    const replyMsg = {
      id: Date.now() + 1,
      content: '好的，不见不散！',
      time: new Date().toLocaleTimeString('zh-CN', { hour: '2-digit', minute: '2-digit' }),
      isSelf: false
    }
    messages.value.push(replyMsg)
    
    nextTick(() => {
      scrollToId.value = `msg-${messages.value.length - 1}`
    })
  }, 1500)
}
</script>

<style lang="scss" scoped>
.page {
  height: 100vh;
  display: flex;
  flex-direction: column;
  background: #f5f5f5;
}

.chat-container {
  flex: 1;
  padding: 20rpx;
}

.message-list {
  padding-bottom: 20rpx;
}

.message-item {
  display: flex;
  gap: 16rpx;
  margin-bottom: 24rpx;
  
  &.self {
    flex-direction: row-reverse;
    
    .message-content {
      background: #ef4444;
      
      .message-text {
        color: #fff;
      }
      
      .message-time {
        color: rgba(255, 255, 255, 0.7);
      }
    }
  }
}

.message-avatar {
  width: 72rpx;
  height: 72rpx;
  border-radius: 50%;
  flex-shrink: 0;
}

.message-content {
  max-width: 70%;
  background: #fff;
  border-radius: 24rpx;
  padding: 20rpx 24rpx;
  box-shadow: 0 4rpx 12rpx rgba(0, 0, 0, 0.05);
  
  &:not(:last-child) {
    border-bottom-left-radius: 8rpx;
  }
  
  .self & {
    border-bottom-right-radius: 8rpx;
    border-bottom-left-radius: 24rpx;
  }
}

.message-text {
  display: block;
  font-size: 28rpx;
  color: #333;
  line-height: 1.6;
}

.message-time {
  display: block;
  font-size: 20rpx;
  color: #999;
  text-align: right;
  margin-top: 8rpx;
}

.input-bar {
  display: flex;
  gap: 16rpx;
  padding: 20rpx;
  background: #fff;
  border-top: 2rpx solid #f0f0f0;
}

.input-field {
  flex: 1;
  height: 80rpx;
  background: #f5f5f5;
  border-radius: 40rpx;
  padding: 0 32rpx;
  font-size: 28rpx;
}

.send-btn {
  width: 140rpx;
  height: 80rpx;
  background: #e0e0e0;
  border-radius: 40rpx;
  font-size: 28rpx;
  color: #999;
  border: none;
  
  &.active {
    background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
    color: #fff;
  }
}
</style>
