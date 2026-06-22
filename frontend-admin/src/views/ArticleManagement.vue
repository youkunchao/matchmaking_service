<template>
  <div>
    <div class="mb-6 flex gap-4">
      <el-input v-model="searchKeyword" placeholder="搜索文章标题" style="width: 200px" />
      <el-select v-model="filterCategory" placeholder="选择分类" style="width: 150px">
        <el-option label="全部" value="" />
        <el-option label="恋爱技巧" value="恋爱技巧" />
        <el-option label="相亲指南" value="相亲指南" />
        <el-option label="成功案例" value="成功案例" />
        <el-option label="平台动态" value="平台动态" />
      </el-select>
      <el-button type="primary" @click="showAddDialog">发布文章</el-button>
    </div>
    
    <el-table :data="filteredArticles" border>
      <el-table-column prop="title" label="标题" />
      <el-table-column prop="category" label="分类" />
      <el-table-column prop="author" label="作者" />
      <el-table-column prop="views" label="浏览量" />
      <el-table-column prop="likes" label="点赞数" />
      <el-table-column prop="status" label="状态">
        <template #default="scope">
          <el-tag :type="scope.row.status === 1 ? 'success' : 'info'">
            {{ scope.row.status === 1 ? '已发布' : '草稿' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="publishedAt" label="发布时间" />
      <el-table-column label="操作" width="250">
        <template #default="scope">
          <el-button size="small" @click="viewArticle(scope.row)">查看</el-button>
          <el-button size="small" @click="editArticle(scope.row)">编辑</el-button>
          <el-button size="small" type="danger" @click="deleteArticle(scope.row.id)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    
    <el-dialog v-model="dialogVisible" title="文章管理" width="700">
      <el-form :model="form" label-width="80">
        <el-form-item label="标题">
          <el-input v-model="form.title" />
        </el-form-item>
        <el-form-item label="分类">
          <el-select v-model="form.category">
            <el-option label="恋爱技巧" value="恋爱技巧" />
            <el-option label="相亲指南" value="相亲指南" />
            <el-option label="成功案例" value="成功案例" />
            <el-option label="平台动态" value="平台动态" />
          </el-select>
        </el-form-item>
        <el-form-item label="作者">
          <el-input v-model="form.author" />
        </el-form-item>
        <el-form-item label="封面图">
          <el-input v-model="form.coverImage" placeholder="图片URL" />
        </el-form-item>
        <el-form-item label="摘要">
          <el-input v-model="form.summary" type="textarea" :rows="2" />
        </el-form-item>
        <el-form-item label="内容">
          <el-input v-model="form.content" type="textarea" :rows="8" />
        </el-form-item>
        <el-form-item label="状态">
          <el-radio-group v-model="form.status">
            <el-radio :value="0">草稿</el-radio>
            <el-radio :value="1">发布</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="saveArticle">保存</el-button>
      </template>
    </el-dialog>
    
    <el-dialog v-model="viewDialogVisible" title="文章详情" width="600">
      <div v-if="currentArticle">
        <h2 class="text-xl font-bold mb-4">{{ currentArticle.title }}</h2>
        <div class="flex gap-4 mb-4 text-gray-500">
          <span>分类: {{ currentArticle.category }}</span>
          <span>作者: {{ currentArticle.author }}</span>
          <span>浏览: {{ currentArticle.views }}</span>
        </div>
        <div class="prose">
          {{ currentArticle.content }}
        </div>
      </div>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

const articles = ref([
  { id: '1', title: '相亲第一次见面要注意什么', category: '相亲指南', author: '红娘小李', views: 1234, likes: 56, status: 1, summary: '第一次相亲见面注意事项', content: '相亲第一次见面，穿着要得体...', publishedAt: '2024-02-10', coverImage: '' },
  { id: '2', title: '如何提高相亲成功率', category: '恋爱技巧', author: '情感专家', views: 2345, likes: 89, status: 1, summary: '提高相亲成功率的技巧', content: '相亲成功率提升的方法...', publishedAt: '2024-02-08', coverImage: '' },
  { id: '3', title: '张先生和李女士的幸福故事', category: '成功案例', author: '平台小编', views: 567, likes: 23, status: 1, summary: '成功配对案例分享', content: '他们通过平台相识...', publishedAt: '2024-02-05', coverImage: '' },
  { id: '4', title: '平台新功能上线通知', category: '平台动态', author: '官方', views: 890, likes: 12, status: 1, summary: '新功能介绍', content: '我们上线了新的匹配算法...', publishedAt: '2024-02-01', coverImage: '' }
])

const searchKeyword = ref('')
const filterCategory = ref('')
const dialogVisible = ref(false)
const viewDialogVisible = ref(false)
const isEdit = ref(false)
const currentArticle = ref(null)

const form = reactive({
  id: '',
  title: '',
  category: '',
  author: '',
  coverImage: '',
  summary: '',
  content: '',
  status: 0
})

const filteredArticles = computed(() => {
  return articles.value.filter(article => {
    if (searchKeyword.value && !article.title.includes(searchKeyword.value)) return false
    if (filterCategory.value && article.category !== filterCategory.value) return false
    return true
  })
})

const showAddDialog = () => {
  isEdit.value = false
  Object.assign(form, {
    id: '',
    title: '',
    category: '',
    author: '',
    coverImage: '',
    summary: '',
    content: '',
    status: 0
  })
  dialogVisible.value = true
}

const editArticle = (row) => {
  isEdit.value = true
  Object.assign(form, row)
  dialogVisible.value = true
}

const viewArticle = (row) => {
  currentArticle.value = row
  viewDialogVisible.value = true
}

const saveArticle = () => {
  if (!form.title) {
    ElMessage.warning('请输入文章标题')
    return
  }
  
  if (isEdit.value) {
    const index = articles.value.findIndex(a => a.id === form.id)
    if (index > -1) {
      articles.value[index] = { ...form }
    }
    ElMessage.success('更新成功')
  } else {
    const newArticle = {
      ...form,
      id: Date.now().toString(),
      views: 0,
      likes: 0,
      publishedAt: form.status === 1 ? new Date().toISOString().split('T')[0] : ''
    }
    articles.value.push(newArticle)
    ElMessage.success('添加成功')
  }
  
  dialogVisible.value = false
}

const deleteArticle = (id) => {
  ElMessageBox.confirm('确定删除该文章吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(() => {
    articles.value = articles.value.filter(a => a.id !== id)
    ElMessage.success('删除成功')
  }).catch(() => {})
}

onMounted(() => {
  // 加载文章数据
})
</script>