<template>
  <div>
    <Card>
      <Form
        ref="productform"
        :model="product"
        :rules="ruleValidate"
        :label-width="80"
        :loading="loading"
      >
        <Row>
          <Col span="24">
            <FormItem label="商品名称" prop="name">
              <Input v-model="product.title" placeholder="请输入商品名称"></Input>
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span="8">
            <FormItem label="出发地" prop="placeLeave">
              <i-select placeholder="请选择" style="width:176px">
                <i-option
                  v-for="item of list"
                  :value="item.id"
                  :key="item.id"
                  style="display: none;"
                >{{ item.title }}</i-option>
                <Tree :data="categorys"></Tree>
              </i-select>
            </FormItem>
          </Col>
        </Row>
        <FormItem>
          <Button type="primary" @click="saveProduct('productform')">提交</Button>
          <Button @click="handleReset('productform')" style="margin-left: 8px">重置</Button>
        </FormItem>
      </Form>
    </Card>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
import { getProduct, addProduct } from "@/api/product";
import { getProductCategoryTree } from "@/api/product-category";
import { getPropertyList } from "@/api/property";
export default {
  name: "product_edit",
  data() {
    return {
      loading: false,
      product: {},
      propertys: [],
      categorys: [],
      list: [],
      data: [
        {
          title: "parent 1",
          value: "1",
          children: [
            {
              title: "parent 1-1",
              value: "11",
              children: [
                {
                  title: "leaf 1-1-1",
                  value: "111"
                },
                {
                  title: "leaf 1-1-2",
                  value: "112"
                }
              ]
            },
            {
              title: "parent 1-2",
              value: "12",
              children: [
                {
                  title: "leaf 1-2-1",
                  value: "121"
                }
              ]
            }
          ]
        }
      ],
      model2: "",
      ruleValidate: {
        title: [
          {
            required: true,
            message: "请输入角色名称",
            trigger: "blur"
          }
        ]
      }
    };
  },
  methods: {
    ...mapMutations(["closeTag"]),
    close() {
      /**
       * 如果是调用closeTag方法，普通的页面传入的对象参数只需要写name字段即可
       * 如果是动态路由和带参路由，需要传入query或params字段，用来区别关闭的是参数为多少的页面
       */
      this.closeTag({
        name: "product_edit",
        query: {
          id: this.$route.query.id
        }
      });
    },
    getData() {
      this.loading = true;
      const id = this.$route.query.id;
      if (id != "" && id != undefined) {
        getProduct(id).then(res => {
          this.product = res.data;
          //this.loading = false;
        });
      }
    },
    /**分类下的属性列表 */
    propertyList(categoryId) {
      getPropertyList({ categoryId: categoryId }).then(res => {
        this.propertys = res.data;
      });
    },
    categoryList() {
      getProductCategoryTree().then(res => {
        this.categorys = res.data;
      });
    },
    handleReset(name) {
      this.$refs[name].resetFields();
    },

    saveProduct(name) {
      this.$refs[name].validate(valid => {
        if (valid) {
          this.product.categoryId = 11;
          addProduct(this.product).then(res => {
            if (res.status == 200) {
              this.$Message.success("Success!");
              this.edituser = false;
              this.getData();
            }
          });
        } else {
          this.$Message.error("Fail!");
        }
      });
    }
  },
  mounted() {
    this.getData();
    this.categoryList();
    this.propertyList(11);
  },
  computed: {
    // 计算属性的 getter
    onQuery: function() {
      // `this` 指向 vm 实例
      return this.$route.params;
    }
  },
  watch: {
    onQuery(old, newValue) {
      // 对路由变化作出响应...
      if (newValue) {
        this.getData();
      }
    }
  }
};
</script>