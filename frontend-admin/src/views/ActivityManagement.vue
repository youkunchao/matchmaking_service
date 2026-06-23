<template>
  <div>
    <div class="flex items-center justify-between mb-6">
      <h3 class="text-lg font-semibold">活动列表</h3>
      <div class="flex items-center gap-4">
        <el-input v-model="searchText" placeholder="搜索活动名称" style="width: 200px" />
        <el-select v-model="statusFilter" placeholder="状态筛选">
          <el-option label="全部" value="" />
          <el-option label="预告中" value="预告中" />
          <el-option label="进行中" value="进行中" />
          <el-option label="已结束" value="已结束" />
        </el-select>
        <el-button type="primary">新增活动</el-button>
      </div>
    </div>
    
    <el-table :data="activities" border>
      <el-table-column prop="id" label="ID" width="80" />
      <el-table-column prop="title" label="活动名称" />
      <el-table-column prop="location" label="地点" />
      <el-table-column prop="date" label="活动时间" />
      <el-table-column prop="price" label="费用" width="80" />
      <el-table-column prop="participants" label="报名人数" width="100">
        <template #default="scope">
          {{ scope.row.participants }}/{{ scope.row.maxParticipants }}
        </template>
      </el-table-column>
      <el-table-column prop="status" label="状态">
        <template #default="scope">
          <el-tag :type="getStatusType(scope.row.status)">
            {{ scope.row.status }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="创建时间" />
      <el-table-column label="操作">
        <template #default="scope">
          <el-button size="small" @click="viewActivity(scope.row)">查看</el-button>
          <el-button size="small" type="warning" @click="editActivity(scope.row)">编辑</el-button>
          <el-button size="small" type="danger" @click="deleteActivity(scope.row)">删除</el-button>
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
const currentPage = ref(1)
const pageSize = ref(10)
const total = ref(30)

const activities = ref([
  { id: 1, title: '浪漫下午茶相亲会', location: '深圳南山', date: '2024-02-14', price: '¥199', participants: 32, maxParticipants: 50, status: '预告中', createTime: '2024-01-15' },
  { id: 2, title: '户外徒步交友活动', location: '深圳福田', date: '2024-02-10', price: '¥158', participants: 45, maxParticipants: 60, status: '进行中', createTime: '2024-01-20' },
  { id: 3, title: '情人节特别活动', location: '深圳罗湖', date: '2024-02-14', price: '¥299', participants: 28, maxParticipants: 40, status: '预告中', createTime: '2024-01-25' },
  { id: 4, title: '读书会交友活动', location: '深圳宝安', date: '2024-02-08', price: '¥68', participants: 20, maxParticipants: 30, status: '已结束', createTime: '2024-01-10' },
  { id: 5, title: '厨艺交流派对', location: '深圳龙岗', date: '2024-02-17', price: '¥228', participants: 15, maxParticipants: 25, status: '预告中', createTime: '2024-01-28' }
])

const getStatusType = (status) => {
  switch (status) {
    case '预告中': return 'info'
    case '进行中': return 'success'
    case '已结束': return 'warning'
    default: return 'info'
  }
}

const viewActivity = (activity) => {
  ElMessage.info(`查看活动: ${activity.title}`)
}

const editActivity = (activity) => {
  ElMessage.info(`编辑活动: ${activity.title}`)
}

const deleteActivity = (activity) => {
  ElMessage.confirm('确定删除该活动？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消'
  }).then(() => {
    ElMessage.success('删除成功')
  }).catch(() => {
    ElMessage.info('已取消删除')
  })
}
</script>
