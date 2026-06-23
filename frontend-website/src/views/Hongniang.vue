<template>
  <div class="pt-16">
    <section class="bg-gradient-to-r from-secondary-500 to-secondary-600 text-white py-16">
      <div class="container mx-auto px-4 text-center">
        <h1 class="text-3xl md:text-4xl font-bold mb-4">红娘团队</h1>
        <p class="opacity-90">专业红娘团队，为您提供一对一婚恋服务</p>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="hongniang in hongniangList" :key="hongniang.name" class="card text-center cursor-pointer" @click="showDetail(hongniang)">
            <img :src="hongniang.avatar" :alt="hongniang.name" class="w-24 h-24 rounded-full mx-auto mb-4 object-cover" />
            <h3 class="text-xl font-semibold mb-1">{{ hongniang.name }}</h3>
            <p class="text-gray-500 text-sm mb-3">{{ hongniang.title }}</p>
            <div class="flex items-center justify-center gap-1 mb-3">
              <span v-for="i in 5" :key="i" class="text-yellow-400">★</span>
              <span class="text-gray-500 text-sm">({{ hongniang.rating }})</span>
            </div>
            <p class="text-sm text-gray-500">从业{{ hongniang.experience }}年 | {{ hongniang.successCount }}对成功配对</p>
          </div>
        </div>
        
        <div class="text-center mt-8">
          <button class="border-2 border-secondary-500 text-secondary-500 px-8 py-3 rounded-full hover:bg-secondary-50 transition-colors">
            查看全部红娘
          </button>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <h2 class="section-title">红娘服务流程</h2>
        <div class="grid grid-cols-1 md:grid-cols-4 gap-8">
          <div v-for="(step, index) in serviceSteps" :key="step.title" class="text-center">
            <div class="w-16 h-16 bg-secondary-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <span class="text-2xl font-bold text-secondary-500">{{ index + 1 }}</span>
            </div>
            <h3 class="font-semibold mb-2">{{ step.title }}</h3>
            <p class="text-sm text-gray-500">{{ step.desc }}</p>
          </div>
        </div>
      </div>
    </section>
    
    <div v-if="selectedHongniang" class="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
      <div class="bg-white rounded-xl max-w-lg w-full max-h-[90vh] overflow-y-auto">
        <div class="relative">
          <img :src="selectedHongniang.avatar" :alt="selectedHongniang.name" class="w-full h-48 object-cover rounded-t-xl" />
          <button @click="selectedHongniang = null" class="absolute top-4 right-4 w-8 h-8 bg-white/80 rounded-full flex items-center justify-center hover:bg-white">
            ×
          </button>
        </div>
        <div class="p-6">
          <div class="flex items-center gap-4 mb-4">
            <img :src="selectedHongniang.avatar" :alt="selectedHongniang.name" class="w-16 h-16 rounded-full" />
            <div>
              <h3 class="text-xl font-semibold">{{ selectedHongniang.name }}</h3>
              <p class="text-gray-500">{{ selectedHongniang.title }}</p>
            </div>
          </div>
          
          <div class="grid grid-cols-3 gap-4 mb-6 text-center">
            <div class="bg-gray-50 rounded-lg p-3">
              <div class="text-lg font-bold text-primary-500">{{ selectedHongniang.experience }}</div>
              <div class="text-sm text-gray-500">从业年限</div>
            </div>
            <div class="bg-gray-50 rounded-lg p-3">
              <div class="text-lg font-bold text-primary-500">{{ selectedHongniang.successCount }}</div>
              <div class="text-sm text-gray-500">成功配对</div>
            </div>
            <div class="bg-gray-50 rounded-lg p-3">
              <div class="text-lg font-bold text-primary-500">{{ selectedHongniang.rating }}</div>
              <div class="text-sm text-gray-500">服务评分</div>
            </div>
          </div>
          
          <div class="mb-6">
            <h4 class="font-semibold mb-2">擅长人群</h4>
            <div class="flex flex-wrap gap-2">
              <span v-for="tag in selectedHongniang.specialty" :key="tag" class="px-3 py-1 bg-secondary-100 text-secondary-600 text-sm rounded-full">
                {{ tag }}
              </span>
            </div>
          </div>
          
          <div class="mb-6">
            <h4 class="font-semibold mb-2">服务理念</h4>
            <p class="text-gray-500">{{ selectedHongniang.philosophy }}</p>
          </div>
          
          <button class="w-full bg-primary-500 text-white py-3 rounded-lg font-semibold hover:bg-primary-600 transition-colors">
            预约咨询
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const selectedHongniang = ref(null)

const hongniangList = [
  {
    name: '王红娘',
    title: '高级婚恋顾问',
    avatar: 'https://via.placeholder.com/100',
    experience: 8,
    successCount: 128,
    rating: 4.9,
    specialty: ['白领精英', '海归人士', '30+未婚'],
    philosophy: '用心倾听每一位会员的需求，用专业和真诚帮助他们找到幸福。'
  },
  {
    name: '李红娘',
    title: '资深婚恋专家',
    avatar: 'https://via.placeholder.com/100',
    experience: 12,
    successCount: 206,
    rating: 4.8,
    specialty: ['离异再婚', '同城交友', '高收入人群'],
    philosophy: '相信每一个人都值得被爱，我会尽全力为您找到最合适的另一半。'
  },
  {
    name: '张红娘',
    title: '金牌婚恋顾问',
    avatar: 'https://via.placeholder.com/100',
    experience: 6,
    successCount: 98,
    rating: 4.9,
    specialty: ['年轻群体', '校园恋情', '异地恋'],
    philosophy: '用热情和专业为年轻人搭建爱的桥梁，见证每一段美好的爱情。'
  }
]

const serviceSteps = [
  { title: '初次沟通', desc: '了解您的需求和期望' },
  { title: '资料分析', desc: '分析您的个人资料和择偶标准' },
  { title: '精准匹配', desc: '为您推荐合适的对象' },
  { title: '全程跟进', desc: '陪伴您的整个婚恋过程' }
]

const showDetail = (hongniang) => {
  selectedHongniang.value = hongniang
}
</script>
