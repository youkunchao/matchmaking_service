<template>
  <div class="pt-16">
    <section class="bg-gradient-to-r from-secondary-500 to-secondary-600 text-white py-16">
      <div class="container mx-auto px-4 text-center">
        <h1 class="text-3xl md:text-4xl font-bold mb-4">情感资讯</h1>
        <p class="opacity-90">分享恋爱技巧，探讨婚姻经营，陪伴您的情感成长</p>
      </div>
    </section>
    
    <section class="py-20 bg-white">
      <div class="container mx-auto px-4">
        <div class="flex flex-wrap gap-4 mb-8 justify-center">
          <button v-for="category in categories" :key="category.value" 
                  @click="activeCategory = category.value"
                  class="px-6 py-2 rounded-full transition-colors"
                  :class="activeCategory === category.value ? 'bg-secondary-500 text-white' : 'bg-gray-100 text-gray-600 hover:bg-gray-200'">
            {{ category.label }}
          </button>
        </div>
        
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="article in filteredArticles" :key="article.id" class="card cursor-pointer hover:shadow-xl transition-shadow" @click="showDetail(article)">
            <img :src="article.image" :alt="article.title" class="w-full h-40 object-cover mb-4" />
            <h3 class="font-semibold mb-2 line-clamp-2">{{ article.title }}</h3>
            <p class="text-gray-500 text-sm mb-4 line-clamp-2">{{ article.summary }}</p>
            <div class="flex items-center justify-between text-sm text-gray-400">
              <span>{{ article.category }}</span>
              <span>{{ article.date }}</span>
            </div>
          </div>
        </div>
        
        <div class="text-center mt-8">
          <button class="border-2 border-secondary-500 text-secondary-500 px-8 py-3 rounded-full hover:bg-secondary-50 transition-colors">
            加载更多
          </button>
        </div>
      </div>
    </section>
    
    <section class="py-20 bg-gray-50">
      <div class="container mx-auto px-4">
        <h2 class="section-title">热门文章</h2>
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
          <div v-for="article in popularArticles" :key="article.id" class="flex gap-4 bg-white rounded-lg p-4 cursor-pointer hover:shadow-md transition-shadow">
            <img :src="article.image" :alt="article.title" class="w-32 h-24 object-cover rounded-lg" />
            <div class="flex-1">
              <h3 class="font-semibold mb-1 line-clamp-2">{{ article.title }}</h3>
              <div class="flex items-center gap-4 text-sm text-gray-400">
                <span>{{ article.date }}</span>
                <span>{{ article.views }}阅读</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    
    <div v-if="selectedArticle" class="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
      <div class="bg-white rounded-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
        <div class="relative">
          <img :src="selectedArticle.image" :alt="selectedArticle.title" class="w-full h-64 object-cover rounded-t-xl" />
          <button @click="selectedArticle = null" class="absolute top-4 right-4 w-8 h-8 bg-white/80 rounded-full flex items-center justify-center hover:bg-white">
            ×
          </button>
        </div>
        <div class="p-6">
          <div class="flex items-center gap-4 mb-4">
            <span class="px-3 py-1 bg-secondary-100 text-secondary-600 text-sm rounded-full">{{ selectedArticle.category }}</span>
            <span class="text-gray-400">{{ selectedArticle.date }}</span>
            <span class="text-gray-400">{{ selectedArticle.views }}阅读</span>
          </div>
          <h1 class="text-2xl font-bold mb-6">{{ selectedArticle.title }}</h1>
          <div class="prose prose-gray max-w-none">
            <p v-for="(paragraph, index) in selectedArticle.content" :key="index" class="mb-4 text-gray-600 leading-relaxed">
              {{ paragraph }}
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'

const activeCategory = ref('all')
const selectedArticle = ref(null)

const categories = [
  { label: '全部', value: 'all' },
  { label: '相亲技巧', value: 'skill' },
  { label: '恋爱心理', value: 'psychology' },
  { label: '婚姻经营', value: 'marriage' }
]

const articles = [
  {
    id: 1,
    title: '第一次相亲应该注意什么？这5个技巧帮你留下好印象',
    image: 'https://via.placeholder.com/400x200',
    summary: '第一次相亲是认识新朋友的重要机会，如何在短时间内给对方留下好印象？本文分享5个实用技巧...',
    category: '相亲技巧',
    categoryValue: 'skill',
    date: '2024-02-01',
    views: 2345,
    content: [
      '相亲是一种传统而有效的交友方式，但很多人会感到紧张和不安。其实，只要做好充分准备，就能轻松应对。',
      '首先，穿着打扮要得体。不需要过于正式，但一定要整洁干净。选择适合自己的服装，展现自信的一面。',
      '其次，提前了解对方的基本情况。通过红娘或介绍人了解对方的职业、爱好等信息，准备一些共同话题。',
      '第三，保持良好的心态。不要把相亲当成一种压力，而是看作认识新朋友的机会。即使这次不成功，也是一次宝贵的经验。',
      '第四，注意交流技巧。学会倾听，不要只顾自己说话。适当提问，表现出对对方的兴趣。',
      '最后，注意细节。准时到达、礼貌用语、适当的肢体语言，这些细节都能给对方留下好印象。'
    ]
  },
  {
    id: 2,
    title: '恋爱中的安全感从何而来？心理学家告诉你答案',
    image: 'https://via.placeholder.com/400x200',
    summary: '安全感是恋爱关系中非常重要的因素，它影响着双方的信任和亲密程度。本文从心理学角度分析安全感的来源...',
    category: '恋爱心理',
    categoryValue: 'psychology',
    date: '2024-01-28',
    views: 1892,
    content: [
      '在恋爱关系中，安全感是建立健康亲密关系的基石。缺乏安全感会导致焦虑、猜忌和不信任。',
      '安全感首先来自于自我认同。一个自信的人更容易在关系中感到安全，因为他们不需要通过他人来证明自己的价值。',
      '其次，安全感来自于清晰的边界。明确的沟通和相互尊重是建立安全感的关键。',
      '第三，一致性和可靠性也是安全感的重要来源。言行一致的伴侣更容易让人信任。',
      '最后，安全感也来自于共同的未来规划。当双方对未来有共同的期待时，会感到更加安心。'
    ]
  },
  {
    id: 3,
    title: '婚姻保鲜秘诀：这3个习惯让爱情长久',
    image: 'https://via.placeholder.com/400x200',
    summary: '婚姻需要用心经营，本文分享3个简单而有效的习惯，帮助您的婚姻保持新鲜和活力...',
    category: '婚姻经营',
    categoryValue: 'marriage',
    date: '2024-01-25',
    views: 3421,
    content: [
      '婚姻不是爱情的终点，而是新的开始。如何让婚姻保持长久的活力？这里有几个简单但有效的习惯。',
      '第一个习惯是定期沟通。每天花15分钟时间，和伴侣聊聊一天的经历和感受，可以大大增进彼此的了解。',
      '第二个习惯是保持约会。即使结婚多年，也要保持约会的习惯。每周或每月安排一次专属的二人时光。',
      '第三个习惯是学会欣赏和感恩。不要把对方的付出视为理所当然，学会表达感谢和欣赏。'
    ]
  },
  {
    id: 4,
    title: '如何克服相亲焦虑？专家给出实用建议',
    image: 'https://via.placeholder.com/400x200',
    summary: '很多人面对相亲会感到焦虑和紧张，这是很常见的现象。本文分享专家的建议，帮助您克服相亲焦虑...',
    category: '相亲技巧',
    categoryValue: 'skill',
    date: '2024-01-20',
    views: 1567,
    content: [
      '相亲焦虑是很多人都会遇到的问题。其实，适当的紧张是正常的，但过度焦虑会影响表现。',
      '首先，正确看待相亲。把它看作认识新朋友的机会，而不是必须成功的任务。',
      '其次，做好充分准备。准备几个话题、了解对方的基本信息，可以增加信心。',
      '第三，练习放松技巧。深呼吸、冥想等方法可以帮助缓解紧张情绪。',
      '最后，记住，对方也可能很紧张。大家都是普通人，不必过于在意结果。'
    ]
  },
  {
    id: 5,
    title: '恋爱中的常见误区，你中了几个？',
    image: 'https://via.placeholder.com/400x200',
    summary: '在恋爱中，我们很容易陷入一些误区，影响关系的发展。本文列举几个常见的误区，帮助您避免犯错...',
    category: '恋爱心理',
    categoryValue: 'psychology',
    date: '2024-01-15',
    views: 2134,
    content: [
      '恋爱是一门学问，很多人在不知不觉中会陷入一些误区。了解这些误区，可以帮助我们更好地经营感情。',
      '第一个误区是过度依赖。把所有的情感需求都寄托在伴侣身上，会给对方造成很大压力。',
      '第二个误区是试图改变对方。每个人都有自己的个性和习惯，学会接受比试图改变更重要。',
      '第三个误区是忽视自我成长。在恋爱中也要保持独立，不断提升自己。',
      '第四个误区是缺乏沟通。很多问题都是因为沟通不畅导致的，学会表达和倾听是关键。'
    ]
  },
  {
    id: 6,
    title: '夫妻沟通的艺术：如何有效解决矛盾',
    image: 'https://via.placeholder.com/400x200',
    summary: '夫妻之间难免会有矛盾，关键是如何有效沟通和解决问题。本文分享沟通的艺术...',
    category: '婚姻经营',
    categoryValue: 'marriage',
    date: '2024-01-10',
    views: 2789,
    content: [
      '夫妻之间的矛盾是不可避免的，但如何处理这些矛盾，会直接影响婚姻的质量。',
      '首先，保持冷静。在情绪激动时不要做出决定，先冷静下来再沟通。',
      '其次，使用"我"语句。用"我感到..."代替"你总是..."，可以减少对方的防御心理。',
      '第三，学会倾听。真正理解对方的感受，而不是急于反驳。',
      '最后，寻求共同点。关注问题的解决，而不是谁对谁错。'
    ]
  }
]

const filteredArticles = computed(() => {
  if (activeCategory.value === 'all') return articles
  return articles.filter(a => a.categoryValue === activeCategory.value)
})

const popularArticles = computed(() => {
  return [...articles].sort((a, b) => b.views - a.views).slice(0, 4)
})

const showDetail = (article) => {
  selectedArticle.value = article
}
</script>
