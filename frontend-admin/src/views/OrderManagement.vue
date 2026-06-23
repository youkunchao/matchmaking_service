<template>
  <div>
    <div class="flex items-center justify-between mb-6">
      <h3 class="text-lg font-semibold">订单列表</h3>
      <div class="flex items-center gap-4">
        <el-input v-model="searchText" placeholder="搜索订单号" style="width: 200px" />
        <el-select v-model="statusFilter" placeholder="状态筛选">
          <el-option label="全部" value="" />
          <el-option label="待支付" value="待支付" />
          <el-option label="已支付" value="已支付" />
          <el-option label="已取消" value="已取消" />
          <el-option label="已退款" value="已退款" />
        </el-select>
        <el-select v-model="typeFilter" placeholder="类型筛选">
          <el-option label="全部" value="" />
          <el-option label="会员套餐" value="package" />
          <el-option label="活动报名" value="activity" />
        </el-select>
      </div>
    </div>
    
    <el-table :data="orders" border>
      <el-table-column prop="orderNo" label="订单号" />
      <el-table-column prop="userName" label="用户" />
      <el-table-column prop="type" label="类型">
        <template #default="scope">
          <el-tag>{{ scope.row.type === 'package' ? '会员套餐' : '活动报名' }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="amount" label="金额" />
      <el-table-column prop="status" label="状态">
        <template #default="scope">
          <el-tag :type="getStatusType(scope.row.status)">
            {{ scope.row.status }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="创建时间" />
      <el-table-column prop="payTime" label="支付时间" />
      <el-table-column label="操作">
        <template #default="scope">
          <el-button size="small" @click="viewOrder(scope.row)">查看</el-button>
          <el-button v-if="scope.row.status === '待支付'" size="small" type="primary" @click="payOrder(scope.row)">支付</el-button>
          <el-button v-if="scope.row.status === '已支付'" size="small" type="warning" @click="refundOrder(scope.row)">退款</el-button>
        </template>
      </el-table-column>
    </el-table>
    
    <el-pagination 
      class="mt-6"
      layout="prev, pager, next, jumper"
      :total="total"
      :page-size="pageSize"
      v-model:current-page="currentPage"
    />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { ElMessage } from 'element-plus'

const searchText = ref('')
const statusFilter = ref('')
const typeFilter = ref('')
const currentPage = ref(1)
const pageSize = ref(10)
const total = ref(200)

const orders = ref([
  { orderNo: 'ORD202402140001', userName: '张三', type: 'package', amount: '¥2999', status: '已支付', createTime: '2024-02-14 15:30', payTime: '2024-02-14 15:35' },
  { orderNo: 'ORD202402140002', userName: '李四', type: 'activity', amount: '¥199', status: '待支付', createTime: '2024-02-14 14:20', payTime: '-' },
  { orderNo: 'ORD202402140003', userName: '王五', type: 'package', amount: '¥999', status: '已支付', createTime: '2024-02-14 13:15', payTime: '2024-02-14 13:20' },
  { orderNo: 'ORD202402140004', userName: '赵六', type: 'activity', amount: '¥158', status: '已取消', createTime: '2024-02-14 12:00', payTime: '-' },
  { orderNo: 'ORD202402140005', userName: '钱七', type: 'package', amount: '¥9999', status: '已支付', createTime: '2024-02-14 11:30', payTime: '2024-02-14 11:35' }
])

const getStatusType = (status) => {
  switch (status) {
    case '待支付': return 'warning'
    case '已支付': return 'success'
    case '已取消': return 'info'
    case '已退款': return 'danger'
    default: return 'info'
  }
}

const viewOrder = (order) => {
  ElMessage.info(`查看订单: ${order.orderNo}`)
}

const payOrder = (order) => {
  ElMessage.success(`订单 ${order.orderNo} 支付成功`)
}

const refundOrder = (order) => {
  ElMessage.confirm('确定退款？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消'
  }).then(() => {
    ElMessage.success('退款成功')
  }).catch(() => {
    ElMessage.info('已取消退款')
  })
}
</script>
