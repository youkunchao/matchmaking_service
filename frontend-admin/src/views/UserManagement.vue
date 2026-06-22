<template>
  <div>
    <div class="flex items-center justify-between mb-6">
      <h3 class="text-lg font-semibold">会员列表</h3>
      <div class="flex items-center gap-4">
        <el-input v-model="searchText" placeholder="搜索用户名/手机号" style="width: 200px" />
        <el-select v-model="statusFilter" placeholder="状态筛选">
          <el-option label="全部" value="" />
          <el-option label="已认证" value="已认证" />
          <el-option label="待审核" value="待审核" />
          <el-option label="已封禁" value="已封禁" />
        </el-select>
        <el-button type="primary">新增会员</el-button>
      </div>
    </div>
    
    <el-table :data="users" border>
      <el-table-column prop="id" label="ID" width="80" />
      <el-table-column prop="name" label="姓名" />
      <el-table-column prop="phone" label="手机号" />
      <el-table-column prop="city" label="城市" />
      <el-table-column prop="age" label="年龄" width="60" />
      <el-table-column prop="memberLevel" label="会员等级" />
      <el-table-column prop="status" label="状态">
        <template #default="scope">
          <el-tag :type="getStatusType(scope.row.status)">
            {{ scope.row.status }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="registerTime" label="注册时间" />
      <el-table-column label="操作">
        <template #default="scope">
          <el-button size="small" @click="viewUser(scope.row)">查看</el-button>
          <el-button size="small" type="warning" @click="editUser(scope.row)">编辑</el-button>
          <el-button size="small" type="danger" @click="deleteUser(scope.row)">删除</el-button>
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
const total = ref(100)

const users = ref([
  { id: 1, name: '张三', phone: '138****1234', city: '深圳', age: 28, memberLevel: '高级会员', status: '已认证', registerTime: '2024-02-14 15:30' },
  { id: 2, name: '李四', phone: '139****5678', city: '广州', age: 32, memberLevel: '基础会员', status: '已认证', registerTime: '2024-02-14 14:20' },
  { id: 3, name: '王五', phone: '137****9012', city: '深圳', age: 26, memberLevel: '普通会员', status: '待审核', registerTime: '2024-02-14 13:15' },
  { id: 4, name: '赵六', phone: '136****3456', city: '东莞', age: 30, memberLevel: '高级会员', status: '已认证', registerTime: '2024-02-14 12:00' },
  { id: 5, name: '钱七', phone: '135****7890', city: '深圳', age: 29, memberLevel: '定制猎婚', status: '已认证', registerTime: '2024-02-14 11:30' }
])

const getStatusType = (status) => {
  switch (status) {
    case '已认证': return 'success'
    case '待审核': return 'warning'
    case '已封禁': return 'danger'
    default: return 'info'
  }
}

const viewUser = (user) => {
  ElMessage.info(`查看会员: ${user.name}`)
}

const editUser = (user) => {
  ElMessage.info(`编辑会员: ${user.name}`)
}

const deleteUser = (user) => {
  ElMessage.confirm('确定删除该会员？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消'
  }).then(() => {
    ElMessage.success('删除成功')
  }).catch(() => {
    ElMessage.info('已取消删除')
  })
}
</script>
