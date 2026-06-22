<template>
  <div class="pt-16">
    <section class="bg-gradient-to-r from-primary-500 to-primary-600 text-white py-16">
      <div class="container mx-auto px-4 text-center">
        <h1 class="text-3xl md:text-4xl font-bold mb-4">关于我们</h1>
        <p class="opacity-90">了解郑好遇见你的品牌故事和发展历程</p>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <div class="grid grid-cols-1 md:grid-cols-2 gap-12 items-center">
          <div>
            <h2 class="text-2xl font-bold mb-6">品牌故事</h2>
            <p class="text-gray-600 mb-4 leading-relaxed">
              郑好遇见你成立于2018年，是一家专注于同城婚恋服务的专业机构。我们致力于通过线上线下相结合的方式，为单身人群提供安全、高效、专业的婚恋服务。
            </p>
            <p class="text-gray-600 mb-6 leading-relaxed">
              我们相信，每个人都值得拥有美好的爱情。因此，我们建立了严格的实名认证体系，确保每位会员的信息真实可靠。同时，我们拥有一支专业的红娘团队，为会员提供一对一的婚恋指导服务。
            </p>
            <p class="text-gray-600 leading-relaxed">
              多年来，我们已经成功帮助超过10000对单身男女找到了他们的幸福。我们的目标是成为最值得信赖的婚恋服务平台。
            </p>
          </div>
          <div class="flex justify-center">
            <img src="https://via.placeholder.com/400x300" alt="品牌故事" class="rounded-xl shadow-lg" />
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <h2 class="section-title">核心价值观</h2>
        <div class="grid grid-cols-1 md:grid-cols-4 gap-8">
          <div v-for="value in coreValues" :key="value.title" class="text-center">
            <div class="w-16 h-16 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <span class="text-2xl">{{ value.icon }}</span>
            </div>
            <h3 class="font-semibold mb-2">{{ value.title }}</h3>
            <p class="text-sm text-gray-500">{{ value.desc }}</p>
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <h2 class="section-title">发展历程</h2>
        <div class="relative">
          <div class="absolute left-1/2 transform -translate-x-1/2 h-full w-0.5 bg-gray-200 hidden md:block"></div>
          <div class="space-y-8">
            <div v-for="(milestone, index) in milestones" :key="milestone.year" 
                 class="relative flex items-center"
                 :class="index % 2 === 0 ? 'md:flex-row' : 'md:flex-row-reverse'">
              <div class="md:w-1/2 p-4" :class="index % 2 === 0 ? 'md:text-right' : 'md:text-left'">
                <div class="bg-gray-50 rounded-lg p-6">
                  <div class="text-2xl font-bold text-primary-500 mb-2">{{ milestone.year }}</div>
                  <h3 class="font-semibold mb-2">{{ milestone.title }}</h3>
                  <p class="text-gray-500 text-sm">{{ milestone.desc }}</p>
                </div>
              </div>
              <div class="absolute left-1/2 transform -translate-x-1/2 w-4 h-4 bg-primary-500 rounded-full border-4 border-white hidden md:block"></div>
              <div class="md:w-1/2"></div>
            </div>
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <h2 class="section-title">资质荣誉</h2>
        <div class="grid grid-cols-2 md:grid-cols-4 gap-6">
          <div v-for="cert in certifications" :key="cert.name" class="bg-white rounded-lg p-6 text-center">
            <div class="w-16 h-16 bg-secondary-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <span class="text-2xl">{{ cert.icon }}</span>
            </div>
            <h3 class="font-semibold">{{ cert.name }}</h3>
          </div>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <h2 class="section-title">联系方式</h2>
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div class="text-center">
            <div class="w-12 h-12 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <span class="text-xl">📞</span>
            </div>
            <h3 class="font-semibold mb-2">客服热线</h3>
            <p class="text-gray-600">400-888-8888</p>
            <p class="text-gray-400 text-sm mt-1">工作日 9:00-21:00</p>
          </div>
          <div class="text-center">
            <div class="w-12 h-12 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <span class="text-xl">✉️</span>
            </div>
            <h3 class="font-semibold mb-2">邮箱地址</h3>
            <p class="text-gray-600">service@zhenghao.com</p>
            <p class="text-gray-400 text-sm mt-1">24小时内回复</p>
          </div>
          <div class="text-center">
            <div class="w-12 h-12 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <span class="text-xl">📍</span>
            </div>
            <h3 class="font-semibold mb-2">公司地址</h3>
            <p class="text-gray-600">深圳市南山区科技园</p>
            <p class="text-gray-400 text-sm mt-1">欢迎来访</p>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
const coreValues = [
  { icon: '💎', title: '真诚', desc: '以真诚的态度对待每一位会员' },
  { icon: '🔒', title: '安全', desc: '严格的实名认证保障安全' },
  { icon: '👩‍💼', title: '专业', desc: '专业红娘团队提供优质服务' },
  { icon: '❤️', title: '用心', desc: '用心帮助每一位会员找到幸福' }
]

const milestones = [
  { year: '2018', title: '公司成立', desc: '郑好遇见你在深圳正式成立，开始提供线下婚恋服务' },
  { year: '2019', title: '线上平台上线', desc: '推出线上婚恋平台，实现线上线下相结合的服务模式' },
  { year: '2020', title: '会员突破', desc: '平台注册会员突破10万人，成功配对超过3000对' },
  { year: '2021', title: '红娘团队扩展', desc: '组建专业红娘团队，提供一对一婚恋指导服务' },
  { year: '2022', title: '品牌升级', desc: '品牌形象全面升级，服务质量进一步提升' },
  { year: '2024', title: '新征程', desc: '开启数字化转型，打造更智能的匹配系统' }
]

const certifications = [
  { name: '婚恋行业协会会员', icon: '🏅' },
  { name: '消费者信得过单位', icon: '⭐' },
  { name: '诚信服务示范企业', icon: '✨' },
  { name: 'AAA级信用企业', icon: '🌟' }
]
</script>
