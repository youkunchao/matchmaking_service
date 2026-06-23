<template>
  <div>
    <div class="flex items-center justify-between mb-6">
      <h3 class="text-lg font-semibold">文章管理</h3>
      <div class="flex items-center gap-4">
        <el-input v-model="searchText" placeholder="搜索文章标题" style="width: 200px" />
        <el-select v-model="categoryFilter" placeholder="分类筛选">
          <el-option label="全部" value="" />
          <el-option label="相亲技巧" value="skill" />
          <el-option label="恋爱心理" value="psychology" />
          <el-option label="婚姻经营" value="marriage" />
        </el-select>
        <el-button type="primary">发布文章</el-button>
      </div>
    </div>
    
    <el-table :data="articles" border>
      <el-table-column prop="id" label="ID" width="80" />
      <el-table-column prop="title" label="标题" />
      <el-table-column prop="category" label="分类" />
      <el-table-column prop="views" label="阅读量" width="100" />
      <el-table-column prop="status" label="状态">
        <template #default="scope">
          <el-tag :type="scope.row.status === '发布' ? 'success' : 'warning'">
            {{ scope.row.status }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="创建时间" />
      <el-table-column label="操作">
        <template #default="scope">
          <el-button size="small" @click="viewArticle(scope.row)">预览</el-button>
          <el-button size="small" type="warning" @click="editArticle(scope.row)">编辑</el-button>
          <el-button size="small" type="danger" @click="deleteArticle(scope.row)">删除</el-button>
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
const categoryFilter = ref('')
const currentPage = ref(1)
const pageSize = ref(10)
const total = ref(100)

const articles = ref([
  { id: 1, title: '第一次相亲应该注意什么？这5个技巧帮你留下好印象', category: '相亲技巧', views: 2345, status: '发布', createTime: '2024-02-01' },
  { id: 2, title: '恋爱中的安全感从何而来？心理学家告诉你答案', category: '恋爱心理', views: 1892, status: '发布', createTime: '2024-01-28' },
  { id: 3, title: '婚姻保鲜秘诀：这3个习惯让爱情长久', category: '婚姻经营', views: 3421, status: '发布', createTime: '2024-01-25' },
  { id: 4, title: '如何克服相亲焦虑？专家给出实用建议', category: '相亲技巧', views: 1567, status: '草稿', createTime: '2024-01-20' },
  { id: 5, title: '恋爱中的常见误区，你中了几个？', category: '恋爱心理', views: 2134, status: '发布', createTime: '2024-01-15' }
])

const viewArticle = (article) => {
  ElMessage.info(`预览文章: ${article.title}`)
}

const editArticle = (article) => {
  ElMessage.info(`编辑文章: ${article.title}`)
}

const deleteArticle = (article) => {
  ElMessage.confirm('确定删除该文章？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消'
  }).then(() => {
    ElMessage.success('删除成功')
  }).catch(() => {
    ElMessage.info('已取消删除')
  })
}
</script>
