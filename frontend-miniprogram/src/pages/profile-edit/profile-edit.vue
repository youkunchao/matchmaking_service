<template>
  <view class="page">
    <view class="section">
      <view class="section-title">基本信息</view>
      <view class="form-item">
        <text class="form-label">昵称</text>
        <input class="form-input" v-model="form.nickname" placeholder="请输入昵称" />
      </view>
      <view class="form-item">
        <text class="form-label">性别</text>
        <view class="gender-options">
          <view 
            class="gender-option" 
            :class="{ active: form.gender === 'male' }"
            @click="form.gender = 'male'"
          >男</view>
          <view 
            class="gender-option" 
            :class="{ active: form.gender === 'female' }"
            @click="form.gender = 'female'"
          >女</view>
        </view>
      </view>
      <view class="form-item">
        <text class="form-label">年龄</text>
        <input class="form-input" type="number" v-model="form.age" placeholder="请输入年龄" />
      </view>
      <view class="form-item">
        <text class="form-label">身高</text>
        <input class="form-input" type="number" v-model="form.height" placeholder="请输入身高(cm)" />
      </view>
      <view class="form-item">
        <text class="form-label">学历</text>
        <view class="picker-wrap" @click="showEducationPicker = true">
          <text class="picker-value">{{ form.education || '请选择学历' }}</text>
          <text class="picker-arrow">›</text>
        </view>
      </view>
    </view>

    <view class="section">
      <view class="section-title">详细资料</view>
      <view class="form-item">
        <text class="form-label">职业</text>
        <input class="form-input" v-model="form.occupation" placeholder="请输入职业" />
      </view>
      <view class="form-item">
        <text class="form-label">月收入</text>
        <view class="picker-wrap" @click="showIncomePicker = true">
          <text class="picker-value">{{ form.income || '请选择月收入' }}</text>
          <text class="picker-arrow">›</text>
        </view>
      </view>
      <view class="form-item">
        <text class="form-label">城市</text>
        <input class="form-input" v-model="form.city" placeholder="请输入所在城市" />
      </view>
      <view class="form-item">
        <text class="form-label">兴趣爱好</text>
        <view class="hobby-options">
          <view 
            class="hobby-tag" 
            v-for="hobby in hobbies" 
            :key="hobby"
            :class="{ active: form.hobbies.includes(hobby) }"
            @click="toggleHobby(hobby)"
          >{{ hobby }}</view>
        </view>
      </view>
      <view class="form-item">
        <text class="form-label">自我介绍</text>
        <textarea class="form-textarea" v-model="form.intro" placeholder="请介绍一下自己..." />
      </view>
    </view>

    <view class="submit-btn" @click="handleSubmit">保存</view>

    <picker mode="selector" :range="educationOptions" @change="onEducationChange" :visible="showEducationPicker">
      <view v-if="showEducationPicker" class="picker-mask" @click="showEducationPicker = false"></view>
    </picker>

    <picker mode="selector" :range="incomeOptions" @change="onIncomeChange" :visible="showIncomePicker">
      <view v-if="showIncomePicker" class="picker-mask" @click="showIncomePicker = false"></view>
    </picker>
  </view>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'

const form = reactive({
  nickname: '',
  gender: '',
  age: '',
  height: '',
  education: '',
  occupation: '',
  income: '',
  city: '',
  hobbies: [] as string[],
  intro: ''
})

const hobbies = ['阅读', '旅行', '美食', '运动', '音乐', '电影', '摄影', '游戏', '烹饪', '书法']
const educationOptions = ['大专', '本科', '硕士', '博士']
const incomeOptions = ['3000以下', '3000-5000', '5000-8000', '8000-12000', '12000-20000', '20000以上']

const showEducationPicker = ref(false)
const showIncomePicker = ref(false)

const toggleHobby = (hobby: string) => {
  const index = form.hobbies.indexOf(hobby)
  if (index > -1) {
    form.hobbies.splice(index, 1)
  } else {
    form.hobbies.push(hobby)
  }
}

const onEducationChange = (e: any) => {
  form.education = educationOptions[e.detail.value]
  showEducationPicker.value = false
}

const onIncomeChange = (e: any) => {
  form.income = incomeOptions[e.detail.value]
  showIncomePicker.value = false
}

const handleSubmit = () => {
  uni.showToast({ title: '保存成功', icon: 'success' })
  setTimeout(() => {
    uni.navigateBack()
  }, 1500)
}
</script>

<style lang="scss" scoped>
.page {
  min-height: 100vh;
  background: #f5f5f5;
  padding-bottom: 120rpx;
}

.section {
  background: #fff;
  margin: 20rpx 0;
  padding: 0 30rpx;
}

.section-title {
  font-size: 28rpx;
  font-weight: bold;
  color: #333;
  padding: 24rpx 0 16rpx;
}

.form-item {
  padding: 24rpx 0;
  border-bottom: 2rpx solid #f5f5f5;
  
  &:last-child {
    border-bottom: none;
  }
}

.form-label {
  display: block;
  font-size: 26rpx;
  color: #666;
  margin-bottom: 16rpx;
}

.form-input {
  width: 100%;
  height: 80rpx;
  font-size: 30rpx;
  color: #333;
}

.form-textarea {
  width: 100%;
  height: 200rpx;
  font-size: 28rpx;
  color: #333;
  line-height: 1.6;
}

.gender-options {
  display: flex;
  gap: 30rpx;
}

.gender-option {
  padding: 20rpx 60rpx;
  background: #f5f5f5;
  border-radius: 40rpx;
  font-size: 28rpx;
  color: #666;
  
  &.active {
    background: #fef2f2;
    color: #ef4444;
  }
}

.picker-wrap {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 80rpx;
}

.picker-value {
  font-size: 30rpx;
  color: #333;
}

.picker-arrow {
  font-size: 32rpx;
  color: #ccc;
}

.hobby-options {
  display: flex;
  flex-wrap: wrap;
  gap: 16rpx;
}

.hobby-tag {
  padding: 16rpx 32rpx;
  background: #f5f5f5;
  border-radius: 32rpx;
  font-size: 26rpx;
  color: #666;
  
  &.active {
    background: #fef2f2;
    color: #ef4444;
  }
}

.submit-btn {
  margin: 30rpx;
  padding: 28rpx;
  background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%);
  border-radius: 44rpx;
  text-align: center;
  font-size: 32rpx;
  color: #fff;
}

.picker-mask {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
}
</style>
