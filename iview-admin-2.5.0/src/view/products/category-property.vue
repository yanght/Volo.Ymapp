<template>
  <div>
    <Card shadow>
      <Button type="primary" icon="md-add" @click="handleCreate">新建</Button>
      <tree-table
        expand-key="title"
        :selectable="false"
        :expand-type="false"
        :columns="columns"
        :data="data"
      >
        <template slot="opt" slot-scope="scope">
          <Button type="primary" size="small" style="margin-right: 5px" @click="modify(scope)">修改</Button>
          <Button
            type="primary"
            size="small"
            style="margin-right: 5px"
            @click="handleCreate(scope)"
          >添加子节点</Button>
          <Button type="error" size="small" @click="remove(scope)">删除</Button>
        </template>
      </tree-table>
    </Card>
    <Modal v-model="edit" title="编辑分类" width="30">
      <Form
        ref="productCategoryForm"
        :model="productCategory"
        :rules="ruleValidate"
        :label-width="80"
      >
        <Row>
          <FormItem label="名称" prop="title">
            <Input v-model="productCategory.title" placeholder="Enter your name"></Input>
          </FormItem>
        </Row>
      </Form>
      <Button slot="footer" type="primary" @click="handleSubmit('productCategoryForm')">保存</Button>
    </Modal>
  </div>
</template>

<script>
import {
  getProductCategoryTree,
  updateProductCategory,
  addProductCategory,
  deleProductCategory
} from "@/api/product-category";
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
          template: "opt"
        }
      ],
      data: [],
      edit: false,
      productCategory: {},
      ruleValidate: {
        title: [
          {
            required: true,
            message: "请输入名称",
            trigger: "blur"
          }
        ]
      }
    };
  },
  methods: {
    getData() {
      this.loading = true;
      getProductCategoryTree().then(res => {
        this.data = res.data;
        this.loading = false;
      });
    },
    modify(scope) {
      this.productCategory = scope.row;
      this.edit = true;
    },
    handleCreate(scope) {
      this.productCategory = {};
      this.productCategory.parentId = scope.row == undefined ? 0 : scope.row.id;
      this.edit = true;
    },
    remove(scope) {
      console.log(scope);
      deleProductCategory(scope.row.id).then(res => {
        if (res.status == 200) {
          this.$Message.success("Success!");
          this.edit = false;
          this.getData();
        }
      });
    },
    /**提交保存 */
    handleSubmit(name) {
      this.$refs[name].validate(valid => {
        if (valid) {
          if (this.productCategory.id == 0) {
            console.log(this.productCategory);
            updateProductCategory(this.productCategory).then(res => {
              if (res.status == 200) {
                this.$Message.success("Success!");
                this.edit = false;
                this.getData();
              }
            });
          } else {
            addProductCategory(this.productCategory).then(res => {
              if (res.status == 200) {
                this.$Message.success("Success!");
                this.edit = false;
                this.getData();
              }
            });
          }
        }
      });
    }
  },
  mounted() {
    this.getData();
  }
};
</script>

<style>
</style>
