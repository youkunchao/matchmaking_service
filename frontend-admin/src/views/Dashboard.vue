<template>
  <div>
    <div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-gray-500 text-sm">今日新增会员</p>
            <p class="text-3xl font-bold text-gray-800 mt-2">{{ stats.newUsers }}</p>
          </div>
          <div class="w-12 h-12 bg-green-100 rounded-full flex items-center justify-center">
            <span class="text-green-500 text-xl">👥</span>
          </div>
        </div>
      </div>
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-gray-500 text-sm">本月订单金额</p>
            <p class="text-3xl font-bold text-gray-800 mt-2">{{ stats.orderAmount }}</p>
          </div>
          <div class="w-12 h-12 bg-blue-100 rounded-full flex items-center justify-center">
            <span class="text-blue-500 text-xl">💰</span>
          </div>
        </div>
      </div>
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-gray-500 text-sm">本周活动报名</p>
            <p class="text-3xl font-bold text-gray-800 mt-2">{{ stats.activitySignups }}</p>
          </div>
          <div class="w-12 h-12 bg-orange-100 rounded-full flex items-center justify-center">
            <span class="text-orange-500 text-xl">📅</span>
          </div>
        </div>
      </div>
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-gray-500 text-sm">匹配成功率</p>
            <p class="text-3xl font-bold text-gray-800 mt-2">{{ stats.matchRate }}</p>
          </div>
          <div class="w-12 h-12 bg-purple-100 rounded-full flex items-center justify-center">
            <span class="text-purple-500 text-xl">💑</span>
          </div>
        </div>
      </div>
    </div>
    
    <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <h3 class="font-semibold mb-4">会员增长趋势</h3>
        <div ref="userChartRef" class="h-64"></div>
      </div>
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <h3 class="font-semibold mb-4">订单分类统计</h3>
        <div ref="orderChartRef" class="h-64"></div>
      </div>
    </div>
    
    <div class="mt-6 bg-white rounded-xl p-6 shadow-sm">
      <h3 class="font-semibold mb-4">最新会员列表</h3>
      <el-table :data="recentUsers" border>
        <el-table-column prop="name" label="姓名" />
        <el-table-column prop="phone" label="手机号" />
        <el-table-column prop="city" label="城市" />
        <el-table-column prop="age" label="年龄" />
        <el-table-column prop="status" label="状态">
          <template #default="scope">
            <el-tag :type="scope.row.status === '已认证' ? 'success' : 'warning'">
              {{ scope.row.status }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="registerTime" label="注册时间" />
      </el-table>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, markRaw } from 'vue'
import * as echarts from 'echarts'

const stats = ref({
  newUsers: 128,
  orderAmount: '¥128,000',
  activitySignups: 256,
  matchRate: '86.5%'
})

const recentUsers = ref([
  { name: '张三', phone: '138****1234', city: '深圳', age: 28, status: '已认证', registerTime: '2024-02-14 15:30' },
  { name: '李四', phone: '139****5678', city: '广州', age: 32, status: '已认证', registerTime: '2024-02-14 14:20' },
  { name: '王五', phone: '137****9012', city: '深圳', age: 26, status: '待审核', registerTime: '2024-02-14 13:15' },
  { name: '赵六', phone: '136****3456', city: '东莞', age: 30, status: '已认证', registerTime: '2024-02-14 12:00' },
  { name: '钱七', phone: '135****7890', city: '深圳', age: 29, status: '已认证', registerTime: '2024-02-14 11:30' }
])

const userChartRef = ref(null)
const orderChartRef = ref(null)

onMounted(() => {
  initUserChart()
  initOrderChart()
})

const initUserChart = () => {
  const chart = echarts.init(userChartRef.value)
  chart.setOption({
    xAxis: {
      type: 'category',
      data: ['1月', '2月', '3月', '4月', '5月', '6月']
    },
    yAxis: {
      type: 'value'
    },
    series: [{
      data: [1200, 1320, 1010, 1340, 1900, 2300],
      type: 'line',
      smooth: true,
      areaStyle: {
        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
          { offset: 0, color: 'rgba(239, 68, 68, 0.3)' },
          { offset: 1, color: 'rgba(239, 68, 68, 0.05)' }
        ])
      },
      lineStyle: {
        color: '#ef4444'
      }
    }]
  })
}

const initOrderChart = () => {
  const chart = echarts.init(orderChartRef.value)
  chart.setOption({
    series: [{
      type: 'pie',
      radius: ['40%', '70%'],
      data: [
        { value: 45, name: '基础会员' },
        { value: 30, name: '高级会员' },
        { value: 15, name: '定制猎婚' },
        { value: 10, name: '活动报名' }
      ],
      itemStyle: {
        colors: ['#ef4444', '#f97316', '#eab308', '#22c55e']
      }
    }]
  })
}
</script>
