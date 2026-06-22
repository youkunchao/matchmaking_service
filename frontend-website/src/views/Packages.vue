<template>
  <div class="pt-16">
    <section class="bg-gradient-to-r from-primary-500 to-primary-600 text-white py-16">
      <div class="container mx-auto px-4 text-center">
        <h1 class="text-3xl md:text-4xl font-bold mb-4">服务套餐</h1>
        <p class="opacity-90">选择适合您的套餐，开启寻爱之旅</p>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="package in packages" :key="package.name" 
               class="card relative overflow-hidden" 
               :class="{ 'border-2 border-primary-500': package.featured }">
            <div v-if="package.featured" class="absolute top-0 right-0 bg-primary-500 text-white text-xs px-3 py-1">
              热门推荐
            </div>
            <div class="text-center mb-6">
              <h3 class="text-xl font-semibold mb-2">{{ package.name }}</h3>
              <div class="flex items-baseline justify-center gap-1">
                <span class="text-4xl font-bold text-primary-500">{{ package.price }}</span>
                <span class="text-gray-500">/{{ package.duration }}</span>
              </div>
            </div>
            
            <ul class="space-y-3 mb-6">
              <li v-for="feature in package.features" :key="feature" class="flex items-start gap-2">
                <span class="text-green-500 mt-1">✓</span>
                <span class="text-gray-600">{{ feature }}</span>
              </li>
            </ul>
            
            <button class="w-full py-3 rounded-lg font-semibold transition-colors"
                    :class="package.featured ? 'bg-primary-500 text-white hover:bg-primary-600' : 'bg-gray-100 text-gray-700 hover:bg-gray-200'">
              立即购买
            </button>
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <h2 class="section-title">套餐对比</h2>
        <div class="overflow-x-auto">
          <table class="w-full border-collapse">
            <thead>
              <tr class="bg-gray-100">
                <th class="border border-gray-300 px-4 py-3 text-left">服务内容</th>
                <th v-for="package in packages" :key="package.name" class="border border-gray-300 px-4 py-3 text-center">
                  {{ package.name }}
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in comparisonItems" :key="item.name">
                <td class="border border-gray-300 px-4 py-3">{{ item.name }}</td>
                <td v-for="package in packages" :key="package.name" class="border border-gray-300 px-4 py-3 text-center">
                  <span :class="item[package.name] ? 'text-green-500' : 'text-gray-400'">
                    {{ item[package.name] ? '✓' : '✗' }}
                  </span>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <h2 class="section-title">常见问题</h2>
        <div class="max-w-2xl mx-auto space-y-4">
          <div v-for="faq in faqs" :key="faq.question" class="bg-white rounded-lg p-6">
            <h3 class="font-semibold mb-2">{{ faq.question }}</h3>
            <p class="text-gray-500">{{ faq.answer }}</p>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
const packages = [
  {
    name: '基础会员',
    price: '¥999',
    duration: '3个月',
    featured: false,
    features: [
      '实名认证服务',
      '每日匹配推荐',
      '无限聊天功能',
      '活动报名权限',
      '基础资料展示'
    ]
  },
  {
    name: '高级会员',
    price: '¥2999',
    duration: '6个月',
    featured: true,
    features: [
      '包含基础会员所有权益',
      '红娘人工推荐',
      '优先曝光展示',
      '约见跟进服务',
      '专属客服支持'
    ]
  },
  {
    name: '定制猎婚',
    price: '¥9999',
    duration: '12个月',
    featured: false,
    features: [
      '包含高级会员所有权益',
      '专属红娘1v1服务',
      '线下金标核验',
      '保底约见12次',
      '全程婚恋指导'
    ]
  }
]

const comparisonItems = [
  { name: '实名认证', '基础会员': true, '高级会员': true, '定制猎婚': true },
  { name: '每日匹配', '基础会员': true, '高级会员': true, '定制猎婚': true },
  { name: '无限聊天', '基础会员': true, '高级会员': true, '定制猎婚': true },
  { name: '活动报名', '基础会员': true, '高级会员': true, '定制猎婚': true },
  { name: '人工推荐', '基础会员': false, '高级会员': true, '定制猎婚': true },
  { name: '优先曝光', '基础会员': false, '高级会员': true, '定制猎婚': true },
  { name: '专属红娘', '基础会员': false, '高级会员': false, '定制猎婚': true },
  { name: '线下核验', '基础会员': false, '高级会员': false, '定制猎婚': true }
]

const faqs = [
  {
    question: '会员套餐可以退款吗？',
    answer: '未使用的会员服务可以申请退款，已使用部分按比例扣除。退款需在购买后7天内申请。'
  },
  {
    question: '如何更换红娘？',
    answer: '高级会员和定制猎婚会员可以申请更换红娘，联系客服即可办理。'
  },
  {
    question: '匹配推荐是如何进行的？',
    answer: '我们采用AI算法结合人工筛选，根据您的资料和偏好，为您推荐最合适的对象。'
  },
  {
    question: '线下活动需要另外付费吗？',
    answer: '活动报名需要单独付费，会员可以享受一定的折扣优惠。'
  }
]
</script>
