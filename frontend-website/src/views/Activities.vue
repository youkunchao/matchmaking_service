<template>
  <div class="pt-16">
    <section class="bg-gradient-to-r from-primary-500 to-primary-600 text-white py-16">
      <div class="container mx-auto px-4 text-center">
        <h1 class="text-3xl md:text-4xl font-bold mb-4">线下活动</h1>
        <p class="opacity-90">丰富多彩的线下活动，让您有更多机会结识新朋友</p>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <div class="flex flex-wrap gap-4 mb-8 justify-center">
          <button v-for="tab in tabs" :key="tab.value" 
                  @click="activeTab = tab.value"
                  class="px-6 py-2 rounded-full transition-colors"
                  :class="activeTab === tab.value ? 'bg-primary-500 text-white' : 'bg-gray-100 text-gray-600 hover:bg-gray-200'">
            {{ tab.label }}
          </button>
        </div>
        
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="activity in filteredActivities" :key="activity.id" class="card overflow-hidden" @click="showDetail(activity)">
            <img :src="activity.image" :alt="activity.title" class="w-full h-48 object-cover" />
            <div class="p-4">
              <div class="flex items-center gap-2 mb-2">
                <span class="px-2 py-1 rounded-full text-xs" :class="getStatusClass(activity.status)">
                  {{ getStatusText(activity.status) }}
                </span>
                <span class="text-sm text-gray-500">{{ formatDate(activity.date) }}</span>
              </div>
              <h3 class="font-semibold mb-2">{{ activity.title }}</h3>
              <p class="text-sm text-gray-500 mb-3">{{ activity.location }}</p>
              <div class="flex items-center justify-between">
                <span class="text-primary-500 font-semibold">{{ activity.price }}</span>
                <span class="text-gray-400 text-sm">{{ activity.participants }}/{{ activity.maxParticipants }}人</span>
              </div>
            </div>
          </div>
        </div>
        
        <div v-if="filteredActivities.length === 0" class="text-center py-16">
          <p class="text-gray-500">暂无相关活动</p>
        </div>
        
        <div class="text-center mt-8">
          <button class="border-2 border-primary-500 text-primary-500 px-8 py-3 rounded-full hover:bg-primary-50 transition-colors">
            加载更多
          </button>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <h2 class="section-title">往期活动回顾</h2>
        <div class="grid grid-cols-2 md:grid-cols-4 gap-4">
          <img v-for="(img, index) in galleryImages" :key="index" 
               :src="img" alt="活动照片" class="w-full h-32 object-cover rounded-lg hover:opacity-80 transition-opacity cursor-pointer" />
        </div>
      </div>
    </section>
    
    <div v-if="selectedActivity" class="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
      <div class="bg-white rounded-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
        <div class="relative">
          <img :src="selectedActivity.image" :alt="selectedActivity.title" class="w-full h-64 object-cover rounded-t-xl" />
          <button @click="selectedActivity = null" class="absolute top-4 right-4 w-8 h-8 bg-white/80 rounded-full flex items-center justify-center hover:bg-white">
            ×
          </button>
          <div class="absolute bottom-4 left-4 px-3 py-1 bg-black/60 text-white text-sm rounded-full">
            {{ getStatusText(selectedActivity.status) }}
          </div>
        </div>
        <div class="p-6">
          <div class="flex items-center gap-2 mb-4">
            <span class="text-gray-500">{{ formatDate(selectedActivity.date) }}</span>
            <span class="text-gray-300">|</span>
            <span class="text-gray-500">{{ selectedActivity.location }}</span>
          </div>
          <h2 class="text-2xl font-bold mb-4">{{ selectedActivity.title }}</h2>
          <p class="text-gray-600 mb-6">{{ selectedActivity.description }}</p>
          
          <div class="grid grid-cols-3 gap-4 mb-6 text-center bg-gray-50 rounded-lg p-4">
            <div>
              <div class="text-lg font-bold text-primary-500">{{ selectedActivity.participants }}/{{ selectedActivity.maxParticipants }}</div>
              <div class="text-sm text-gray-500">已报名</div>
            </div>
            <div>
              <div class="text-lg font-bold text-primary-500">{{ selectedActivity.price }}</div>
              <div class="text-sm text-gray-500">活动费用</div>
            </div>
            <div>
              <div class="text-lg font-bold text-primary-500">{{ selectedActivity.minAge }}-{{ selectedActivity.maxAge }}岁</div>
              <div class="text-sm text-gray-500">年龄要求</div>
            </div>
          </div>
          
          <div class="mb-6">
            <h4 class="font-semibold mb-2">活动要求</h4>
            <ul class="space-y-1 text-sm text-gray-500">
              <li v-for="req in selectedActivity.requirements" :key="req">• {{ req }}</li>
            </ul>
          </div>
          
          <button class="w-full bg-primary-500 text-white py-3 rounded-lg font-semibold hover:bg-primary-600 transition-colors">
            立即报名
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'

const activeTab = ref('all')
const selectedActivity = ref(null)

const tabs = [
  { label: '全部', value: 'all' },
  { label: '预告中', value: 'upcoming' },
  { label: '进行中', value: 'ongoing' },
  { label: '已结束', value: 'ended' }
]

const activities = [
  {
    id: 1,
    title: '浪漫下午茶相亲会',
    image: 'https://via.placeholder.com/400x200',
    date: '2024-02-14',
    location: '深圳市南山区科技园',
    price: '¥199',
    participants: 32,
    maxParticipants: 50,
    status: 'upcoming',
    minAge: 25,
    maxAge: 35,
    description: '在温馨优雅的环境中，与心仪的TA共度一个浪漫的下午茶时光。我们精心安排了各种互动游戏，让您轻松愉快地结识新朋友。',
    requirements: ['年龄25-35岁', '大专及以上学历', '有稳定工作', '真诚交友态度']
  },
  {
    id: 2,
    title: '户外徒步交友活动',
    image: 'https://via.placeholder.com/400x200',
    date: '2024-02-10',
    location: '深圳市福田区莲花山公园',
    price: '¥158',
    participants: 45,
    maxParticipants: 60,
    status: 'ongoing',
    minAge: 22,
    maxAge: 40,
    description: '亲近大自然，在徒步中结识志同道合的朋友。专业领队带领，沿途风景优美，是放松身心、拓展人脉的绝佳机会。',
    requirements: ['身体健康', '热爱户外运动', '穿着舒适运动鞋']
  },
  {
    id: 3,
    title: '情人节特别活动',
    image: 'https://via.placeholder.com/400x200',
    date: '2024-02-14',
    location: '深圳市罗湖区国贸大厦',
    price: '¥299',
    participants: 28,
    maxParticipants: 40,
    status: 'upcoming',
    minAge: 25,
    maxAge: 45,
    description: '情人节专属活动，为单身的您准备了一场浪漫的邂逅。精美晚宴、互动游戏、心动告白，让这个情人节不再孤单。',
    requirements: ['年龄25-45岁', '有稳定收入', '真诚寻找另一半']
  },
  {
    id: 4,
    title: '读书会交友活动',
    image: 'https://via.placeholder.com/400x200',
    date: '2024-02-08',
    location: '深圳市宝安区图书馆',
    price: '¥68',
    participants: 20,
    maxParticipants: 30,
    status: 'ended',
    minAge: 20,
    maxAge: 45,
    description: '在书香中寻找灵魂伴侣，分享阅读心得，交流人生感悟。适合热爱阅读、注重精神交流的朋友。',
    requirements: ['热爱阅读', '善于交流', '有自己的见解']
  },
  {
    id: 5,
    title: '厨艺交流派对',
    image: 'https://via.placeholder.com/400x200',
    date: '2024-02-17',
    location: '深圳市龙岗区创意园',
    price: '¥228',
    participants: 15,
    maxParticipants: 25,
    status: 'upcoming',
    minAge: 22,
    maxAge: 40,
    description: '展示您的厨艺才华，品尝他人的拿手好菜。在美食的香气中，拉近彼此的距离。',
    requirements: ['会做至少一道拿手菜', '愿意分享美食', '注重卫生']
  },
  {
    id: 6,
    title: '羽毛球友谊赛',
    image: 'https://via.placeholder.com/400x200',
    date: '2024-02-11',
    location: '深圳市龙华区体育馆',
    price: '¥98',
    participants: 30,
    maxParticipants: 40,
    status: 'ongoing',
    minAge: 18,
    maxAge: 50,
    description: '在运动中释放活力，在竞技中增进友谊。无论水平高低，只要热爱运动，都欢迎参加。',
    requirements: ['身体健康', '热爱羽毛球运动', '自带球拍']
  }
]

const galleryImages = [
  'https://via.placeholder.com/400x300',
  'https://via.placeholder.com/400x300',
  'https://via.placeholder.com/400x300',
  'https://via.placeholder.com/400x300'
]

const filteredActivities = computed(() => {
  if (activeTab.value === 'all') return activities
  return activities.filter(a => a.status === activeTab.value)
})

const getStatusText = (status) => {
  const map = { upcoming: '预告中', ongoing: '进行中', ended: '已结束' }
  return map[status] || status
}

const getStatusClass = (status) => {
  const map = {
    upcoming: 'bg-primary-100 text-primary-600',
    ongoing: 'bg-green-100 text-green-600',
    ended: 'bg-gray-100 text-gray-500'
  }
  return map[status] || 'bg-gray-100 text-gray-500'
}

const formatDate = (date) => {
  const d = new Date(date)
  return `${d.getMonth() + 1}月${d.getDate()}日`
}

const showDetail = (activity) => {
  selectedActivity.value = activity
}
</script>
