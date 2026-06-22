<template>
  <div>
    <div class="mb-6">
      <el-button type="primary" @click="showAddDialog">添加套餐</el-button>
    </div>
    
    <el-table :data="packages" border>
      <el-table-column prop="name" label="套餐名称" />
      <el-table-column prop="durationMonths" label="有效期(月)" />
      <el-table-column prop="price" label="价格">
        <template #default="scope">
          ¥{{ scope.row.price }}
        </template>
      </el-table-column>
      <el-table-column prop="description" label="描述" />
      <el-table-column prop="status" label="状态">
        <template #default="scope">
          <el-tag :type="scope.row.status === 1 ? 'success' : 'danger'">
            {{ scope.row.status === 1 ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="sortOrder" label="排序" />
      <el-table-column label="操作" width="200">
        <template #default="scope">
          <el-button size="small" @click="editPackage(scope.row)">编辑</el-button>
          <el-button size="small" type="danger" @click="deletePackage(scope.row.id)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    
    <el-dialog v-model="dialogVisible" title="套餐管理" width="500">
      <el-form :model="form" label-width="100">
        <el-form-item label="套餐名称">
          <el-input v-model="form.name" />
        </el-form-item>
        <el-form-item label="有效期(月)">
          <el-input-number v-model="form.durationMonths" :min="1" :max="36" />
        </el-form-item>
        <el-form-item label="价格">
          <el-input-number v-model="form.price" :min="0" :precision="2" />
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="form.description" type="textarea" />
        </el-form-item>
        <el-form-item label="功能特性">
          <el-input v-model="form.features" type="textarea" placeholder="多个功能用逗号分隔" />
        </el-form-item>
        <el-form-item label="排序">
          <el-input-number v-model="form.sortOrder" :min="0" />
        </el-form-item>
        <el-form-item label="状态">
          <el-switch v-model="form.status" :active-value="1" :inactive-value="0" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="savePackage">保存</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

const packages = ref([
  { id: '1', name: '基础会员', durationMonths: 3, price: 299, description: '基础匹配服务', features: '每日推荐5位匹配对象,在线聊天', status: 1, sortOrder: 1 },
  { id: '2', name: '高级会员', durationMonths: 6, price: 599, description: '高级匹配服务', features: '每日推荐10位匹配对象,在线聊天,专属红娘', status: 1, sortOrder: 2 },
  { id: '3', name: 'VIP会员', durationMonths: 12, price: 999, description: 'VIP专属服务', features: '无限匹配推荐,在线聊天,专属红娘,线下活动优先', status: 1, sortOrder: 3 },
  { id: '4', name: '定制猎婚', durationMonths: 24, price: 2999, description: '一对一定制服务', features: '专属红娘一对一服务,精准匹配,线下见面安排', status: 1, sortOrder: 4 }
])

const dialogVisible = ref(false)
const isEdit = ref(false)
const form = reactive({
  id: '',
  name: '',
  durationMonths: 3,
  price: 0,
  description: '',
  features: '',
  sortOrder: 0,
  status: 1
})

const showAddDialog = () => {
  isEdit.value = false
  Object.assign(form, {
    id: '',
    name: '',
    durationMonths: 3,
    price: 0,
    description: '',
    features: '',
    sortOrder: 0,
    status: 1
  })
  dialogVisible.value = true
}

const editPackage = (row) => {
  isEdit.value = true
  Object.assign(form, row)
  dialogVisible.value = true
}

const savePackage = () => {
  if (!form.name) {
    ElMessage.warning('请输入套餐名称')
    return
  }
  
  if (isEdit.value) {
    const index = packages.value.findIndex(p => p.id === form.id)
    if (index > -1) {
      packages.value[index] = { ...form }
    }
    ElMessage.success('更新成功')
  } else {
    packages.value.push({ ...form, id: Date.now().toString() })
    ElMessage.success('添加成功')
  }
  
  dialogVisible.value = false
}

const deletePackage = (id) => {
  ElMessageBox.confirm('确定删除该套餐吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(() => {
    packages.value = packages.value.filter(p => p.id !== id)
    ElMessage.success('删除成功')
  }).catch(() => {})
}

onMounted(() => {
  // 加载套餐数据
})
</script>