<template>
  <div>
    <Card shadow>
      树状表格组件tree-table-vue，基于
      <a
        href="https://github.com/MisterTaki/vue-table-with-tree-grid"
      >vue-table-with-tree-grid</a>进行开发，修复了一些bug，添加了一些新属性
      <p>
        <b>支持使用slot-scope进行自定义列渲染内容</b>
      </p>
      <p>
        文档请看
        <a
          href="https://github.com/lison16/tree-table-vue"
        >https://github.com/lison16/tree-table-vue</a>
      </p>
      <tree-table
        expand-key="title"
        :selectable="false"
        :expand-type="false"
        :columns="columns"
        :data="data"
      >
        <template slot="likes" slot-scope="scope">
          <Button type="primary" size="small" style="margin-right: 5px" @click="show(scope)">添加子节点</Button>
          <Button type="error" size="small" @click="remove(scope)">删除</Button>
        </template>
      </tree-table>
    </Card>
  </div>
</template>

<script>
import { getProductCategoryTree } from "@/api/product-category";
export default {
  name: "productcategory",
  data() {
    return {
      columns: [
        {
          title: "名称",
          key: "title",
          width: "400px"
        },
        {
          title: "操作",
          key: "id",
          minWidth: "200px",
          type: "template",
          template: "likes"
        }
      ],
      data: []
    };
  },
  methods: {
    getData() {
      this.loading = true;
      getProductCategoryTree().then(res => {
        console.log(res);
        this.data = res.data;
        this.loading = false;
      });
    },
    handle(scope) {
      console.log(scope);
    },
    show(scope) {
      console.log(scope);
      this.$Modal.info({
        title: "User Info",
        content: `Name：${this.data[scope.rowIndex].title}<br>`
      });
    },
    remove(scope) {
      this.data.splice(scope.rowIndex, 1);
    }
  },
  mounted() {
    this.getData();
  }
};
</script>

<style>
</style>
