<template>
  <div>
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-6">
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-gray-500 text-sm">待审核会员</p>
            <p class="text-3xl font-bold text-orange-500 mt-2">{{ pendingUsers }}</p>
          </div>
          <div class="w-12 h-12 bg-orange-100 rounded-full flex items-center justify-center">
            <span class="text-orange-500 text-xl">👥</span>
          </div>
        </div>
      </div>
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-gray-500 text-sm">待审核资料</p>
            <p class="text-3xl font-bold text-yellow-500 mt-2">{{ pendingProfiles }}</p>
          </div>
          <div class="w-12 h-12 bg-yellow-100 rounded-full flex items-center justify-center">
            <span class="text-yellow-500 text-xl">📝</span>
          </div>
        </div>
      </div>
      <div class="bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-gray-500 text-sm">待处理投诉</p>
            <p class="text-3xl font-bold text-red-500 mt-2">{{ pendingComplaints }}</p>
          </div>
          <div class="w-12 h-12 bg-red-100 rounded-full flex items-center justify-center">
            <span class="text-red-500 text-xl">⚠️</span>
          </div>
        </div>
      </div>
    </div>
    
    <div class="flex gap-6">
      <div class="flex-1 bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between mb-4">
          <h3 class="font-semibold">待审核会员</h3>
          <el-button type="primary" size="small">批量通过</el-button>
        </div>
        <el-table :data="pendingUserList" border>
          <el-table-column prop="id" label="ID" width="80" />
          <el-table-column prop="name" label="姓名" />
          <el-table-column prop="phone" label="手机号" />
          <el-table-column prop="city" label="城市" />
          <el-table-column prop="applyTime" label="申请时间" />
          <el-table-column label="操作">
            <template #default="scope">
              <el-button size="small" type="success" @click="approveUser(scope.row)">通过</el-button>
              <el-button size="small" type="danger" @click="rejectUser(scope.row)">拒绝</el-button>
            </template>
          </el-table-column>
        </el-table>
      </div>
      
      <div class="flex-1 bg-white rounded-xl p-6 shadow-sm">
        <div class="flex items-center justify-between mb-4">
          <h3 class="font-semibold">待处理投诉</h3>
        </div>
        <el-table :data="complaintList" border>
          <el-table-column prop="id" label="ID" width="80" />
          <el-table-column prop="complainant" label="投诉人" />
          <el-table-column prop="target" label="被投诉人" />
          <el-table-column prop="type" label="投诉类型" />
          <el-table-column prop="createTime" label="投诉时间" />
          <el-table-column label="操作">
            <template #default="scope">
              <el-button size="small" @click="handleComplaint(scope.row)">处理</el-button>
            </template>
          </el-table-column>
        </el-table>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { ElMessage } from 'element-plus'

const pendingUsers = ref(12)
const pendingProfiles = ref(8)
const pendingComplaints = ref(3)

const pendingUserList = ref([
  { id: 1, name: '张三', phone: '138****1234', city: '深圳', applyTime: '2024-02-14 15:30' },
  { id: 2, name: '李四', phone: '139****5678', city: '广州', applyTime: '2024-02-14 14:20' },
  { id: 3, name: '王五', phone: '137****9012', city: '深圳', applyTime: '2024-02-14 13:15' },
  { id: 4, name: '赵六', phone: '136****3456', city: '东莞', applyTime: '2024-02-14 12:00' },
  { id: 5, name: '钱七', phone: '135****7890', city: '深圳', applyTime: '2024-02-14 11:30' }
])

const complaintList = ref([
  { id: 1, complainant: '张三', target: '李四', type: '骚扰', createTime: '2024-02-14 10:30' },
  { id: 2, complainant: '王五', target: '赵六', type: '虚假信息', createTime: '2024-02-13 15:20' },
  { id: 3, complainant: '孙八', target: '周九', type: '言语不当', createTime: '2024-02-13 09:15' }
])

const approveUser = (user) => {
  ElMessage.success(`会员 ${user.name} 审核通过`)
}

const rejectUser = (user) => {
  ElMessage.confirm('确定拒绝该会员？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消'
  }).then(() => {
    ElMessage.success(`会员 ${user.name} 已拒绝`)
  }).catch(() => {
    ElMessage.info('已取消')
  })
}

const handleComplaint = (complaint) => {
  ElMessage.info(`处理投诉: ${complaint.complainant} 投诉 ${complaint.target}`)
}
</script>
