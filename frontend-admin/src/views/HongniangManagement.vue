<template>
  <div>
    <div class="flex items-center justify-between mb-6">
      <h3 class="text-lg font-semibold">红娘列表</h3>
      <div class="flex items-center gap-4">
        <el-input v-model="searchText" placeholder="搜索姓名/手机号" style="width: 200px" />
        <el-select v-model="statusFilter" placeholder="状态筛选">
          <el-option label="全部" value="" />
          <el-option label="在职" value="在职" />
          <el-option label="离职" value="离职" />
        </el-select>
        <el-button type="primary">新增红娘</el-button>
      </div>
    </div>
    
    <el-table :data="hongniangList" border>
      <el-table-column prop="id" label="ID" width="80" />
      <el-table-column prop="name" label="姓名" />
      <el-table-column prop="phone" label="手机号" />
      <el-table-column prop="experience" label="从业年限" width="100" />
      <el-table-column prop="successCount" label="成功配对" width="100" />
      <el-table-column prop="rating" label="服务评分" width="100" />
      <el-table-column prop="status" label="状态">
        <template #default="scope">
          <el-tag :type="scope.row.status === '在职' ? 'success' : 'danger'">
            {{ scope.row.status }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="入职时间" />
      <el-table-column label="操作">
        <template #default="scope">
          <el-button size="small" @click="viewHongniang(scope.row)">查看</el-button>
          <el-button size="small" type="warning" @click="editHongniang(scope.row)">编辑</el-button>
          <el-button size="small" type="danger" @click="deleteHongniang(scope.row)">删除</el-button>
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
const total = ref(50)

const hongniangList = ref([
  { id: 1, name: '王红娘', phone: '138****1234', experience: 8, successCount: 128, rating: 4.9, status: '在职', createTime: '2022-01-15' },
  { id: 2, name: '李红娘', phone: '139****5678', experience: 12, successCount: 206, rating: 4.8, status: '在职', createTime: '2020-06-20' },
  { id: 3, name: '张红娘', phone: '137****9012', experience: 6, successCount: 98, rating: 4.9, status: '在职', createTime: '2023-03-10' },
  { id: 4, name: '刘红娘', phone: '136****3456', experience: 10, successCount: 156, rating: 4.7, status: '离职', createTime: '2021-02-28' },
  { id: 5, name: '陈红娘', phone: '135****7890', experience: 5, successCount: 78, rating: 4.8, status: '在职', createTime: '2023-08-15' }
])

const viewHongniang = (hongniang) => {
  ElMessage.info(`查看红娘: ${hongniang.name}`)
}

const editHongniang = (hongniang) => {
  ElMessage.info(`编辑红娘: ${hongniang.name}`)
}

const deleteHongniang = (hongniang) => {
  ElMessage.confirm('确定删除该红娘？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消'
  }).then(() => {
    ElMessage.success('删除成功')
  }).catch(() => {
    ElMessage.info('已取消删除')
  })
}
</script>
