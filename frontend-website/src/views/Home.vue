<template>
  <div class="pt-16">
    <section class="relative bg-gradient-to-r from-primary-500 to-primary-600 text-white overflow-hidden">
      <div class="absolute inset-0 opacity-10">
        <div class="absolute top-20 left-10 w-32 h-32 bg-white rounded-full"></div>
        <div class="absolute bottom-20 right-20 w-48 h-48 bg-white rounded-full"></div>
        <div class="absolute top-1/2 left-1/3 w-24 h-24 bg-white rounded-full"></div>
      </div>
      
      <div class="container mx-auto px-4 py-20 md:py-32 relative">
        <div class="max-w-2xl mx-auto text-center">
          <h1 class="text-4xl md:text-6xl font-bold mb-6">
            郑好遇见你
          </h1>
          <p class="text-xl md:text-2xl mb-8 opacity-90">
            正好遇见对的人
          </p>
          <p class="text-lg mb-10 opacity-80">
            专业红娘一对一服务，实名认证保障，同城精准匹配
          </p>
          
          <div class="flex flex-col sm:flex-row gap-4 justify-center mb-12">
            <button class="bg-white text-primary-500 px-8 py-4 rounded-full font-semibold hover:bg-gray-50 transition-colors">
              免费注册
            </button>
            <button class="border-2 border-white text-white px-8 py-4 rounded-full font-semibold hover:bg-white/10 transition-colors">
              了解更多
            </button>
          </div>
          
          <div class="inline-block bg-white/10 backdrop-blur-sm rounded-xl p-6">
            <form @submit.prevent="handleSubmit" class="flex flex-col sm:flex-row gap-4">
              <input type="text" v-model="form.phone" placeholder="请输入手机号码" 
                     class="px-4 py-3 rounded-lg bg-white/90 text-gray-800 placeholder-gray-500 focus:outline-none w-full sm:w-64" />
              <select v-model="form.gender" class="px-4 py-3 rounded-lg bg-white/90 text-gray-800 focus:outline-none">
                <option value="">请选择性别</option>
                <option value="male">男</option>
                <option value="female">女</option>
              </select>
              <button type="submit" class="bg-primary-500 text-white px-6 py-3 rounded-lg hover:bg-primary-600 transition-colors">
                立即预约
              </button>
            </form>
            <p class="text-sm opacity-80 mt-3">提交后将有专属红娘联系您</p>
          </div>
        </div>
        
        <div class="mt-16 grid grid-cols-3 gap-8 max-w-3xl mx-auto">
          <div class="text-center">
            <div class="text-3xl md:text-4xl font-bold">10000+</div>
            <div class="text-sm opacity-80">成功配对</div>
          </div>
          <div class="text-center">
            <div class="text-3xl md:text-4xl font-bold">500+</div>
            <div class="text-sm opacity-80">专业红娘</div>
          </div>
          <div class="text-center">
            <div class="text-3xl md:text-4xl font-bold">98%</div>
            <div class="text-sm opacity-80">实名认证率</div>
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <h2 class="section-title">核心服务</h2>
        <p class="section-subtitle">我们提供全方位的婚恋服务，帮助您找到心仪的另一半</p>
        
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="service in services" :key="service.title" class="card text-center">
            <div class="w-16 h-16 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <span class="text-2xl">{{ service.icon }}</span>
            </div>
            <h3 class="text-xl font-semibold mb-2">{{ service.title }}</h3>
            <p class="text-gray-500">{{ service.desc }}</p>
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <h2 class="section-title">成功案例</h2>
        <p class="section-subtitle">真实的幸福故事，见证爱情的美好</p>
        
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="caseItem in successCases" :key="caseItem.name" class="card">
            <div class="flex items-center gap-4 mb-4">
              <img :src="caseItem.avatar" :alt="caseItem.name" class="w-16 h-16 rounded-full" />
              <div>
                <h3 class="font-semibold">{{ caseItem.name }}</h3>
                <p class="text-sm text-gray-500">{{ caseItem.date }}</p>
              </div>
            </div>
            <p class="text-gray-600">{{ caseItem.story }}</p>
            <div class="mt-4 flex items-center gap-1">
              <span v-for="i in 5" :key="i" class="text-yellow-400">★</span>
            </div>
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <h2 class="section-title">热门活动</h2>
        <p class="section-subtitle">丰富多彩的线下活动，让您有更多机会结识新朋友</p>
        
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="activity in activities" :key="activity.title" class="card overflow-hidden">
            <img :src="activity.image" :alt="activity.title" class="w-full h-40 object-cover" />
            <div class="p-4">
              <div class="flex items-center gap-2 mb-2">
                <span class="px-2 py-1 bg-primary-100 text-primary-600 text-xs rounded-full">{{ activity.tag }}</span>
                <span class="text-sm text-gray-500">{{ activity.date }}</span>
              </div>
              <h3 class="font-semibold mb-2">{{ activity.title }}</h3>
              <p class="text-sm text-gray-500 mb-3">{{ activity.location }}</p>
              <div class="flex items-center justify-between">
                <span class="text-primary-500 font-semibold">{{ activity.price }}</span>
                <button class="text-sm text-secondary-500 hover:text-secondary-600">立即报名</button>
              </div>
            </div>
          </div>
        </div>
        
        <div class="text-center mt-8">
          <button class="border-2 border-primary-500 text-primary-500 px-8 py-3 rounded-full hover:bg-primary-50 transition-colors">
            查看更多活动
          </button>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gradient-to-r from-secondary-500 to-secondary-600 text-white">
      <div class="container mx-auto px-4 text-center">
        <h2 class="text-3xl font-bold mb-4">开启您的寻爱之旅</h2>
        <p class="mb-8 opacity-90">立即注册，专属红娘将为您提供一对一服务</p>
        <div class="flex flex-col sm:flex-row gap-4 justify-center">
          <button class="bg-white text-secondary-500 px-8 py-4 rounded-full font-semibold hover:bg-gray-50 transition-colors">
            免费注册
          </button>
          <button class="border-2 border-white text-white px-8 py-4 rounded-full font-semibold hover:bg-white/10 transition-colors">
            联系客服
          </button>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue'

const form = reactive({
  phone: '',
  gender: ''
})

const services = [
  { icon: '💑', title: '精准匹配', desc: '基于AI算法，结合人工筛选，为您推荐最合适的对象' },
  { icon: '👩‍❤️‍👨', title: '红娘服务', desc: '专业红娘一对一指导，全程陪伴您的婚恋之旅' },
  { icon: '✅', title: '实名认证', desc: '严格的身份认证体系，确保每位会员信息真实可靠' }
]

const successCases = [
  { name: '张先生 & 李女士', date: '2024年1月', avatar: 'https://via.placeholder.com/64', story: '通过平台认识三个月后成功牵手，现在已经订婚了。非常感谢郑好遇见你的红娘团队！' },
  { name: '王先生 & 刘女士', date: '2024年2月', avatar: 'https://via.placeholder.com/64', story: '在这里找到了人生的另一半，红娘老师非常专业，帮我们安排了很多活动。' },
  { name: '赵先生 & 陈女士', date: '2024年3月', avatar: 'https://via.placeholder.com/64', story: '本来对相亲不抱希望，没想到真的找到了合适的人。感谢平台！' }
]

const activities = [
  { title: '浪漫下午茶相亲会', image: 'https://via.placeholder.com/400x200', tag: '预告中', date: '2024-02-14', location: '深圳市南山区', price: '¥199' },
  { title: '户外徒步交友活动', image: 'https://via.placeholder.com/400x200', tag: '进行中', date: '2024-02-10', location: '深圳市福田区', price: '¥158' },
  { title: '情人节特别活动', image: 'https://via.placeholder.com/400x200', tag: '预告中', date: '2024-02-14', location: '深圳市罗湖区', price: '¥299' }
]

const handleSubmit = () => {
  if (!form.phone) {
    alert('请输入手机号码')
    return
  }
  if (!form.gender) {
    alert('请选择性别')
    return
  }
  alert('提交成功！我们的红娘将尽快联系您')
}
</script>
