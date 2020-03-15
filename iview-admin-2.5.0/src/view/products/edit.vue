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
              <Input v-model="product.name" placeholder="请输入商品名称"></Input>
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span="8">
            <FormItem label="出发地" prop="placeLeave">
              <Input v-model="product.placeLeave" placeholder="请输入出发地"></Input>
            </FormItem>
          </Col>
          <Col span="8">
            <FormItem label="返回地" prop="placeReturn">
              <Input v-model="product.placeReturn" placeholder="请输入返回地"></Input>
            </FormItem>
          </Col>
          <Col span="8">
            <FormItem>
              <i-col span="11">
                <Form-item prop="dayNumber">
                  <Input v-model="product.dayNumber" number="true" placeholder="天">
                    <span slot="append">天</span>
                  </Input>
                </Form-item>
              </i-col>
              <i-col span="2" style="text-align: center">-</i-col>
              <i-col span="11">
                <Form-item prop="nightNumber">
                  <Input v-model="product.nightNumber" number="true" placeholder="晚">
                    <span slot="append">晚</span>
                  </Input>
                </Form-item>
              </i-col>
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span="8">
            <FormItem label="原价" prop="orignalPrice">
              <Input v-model="product.orignalPrice" number="true" placeholder="请输入原价"></Input>
            </FormItem>
          </Col>
          <Col span="8">
            <FormItem label="现价" prop="price">
              <Input v-model="product.price" number="true" placeholder="请输入现价"></Input>
            </FormItem>
          </Col>
          <Col span="8">
            <FormItem label="儿童价" prop="childrenPrice">
              <Input v-model="product.childrenPrice" number="true" placeholder="请输入儿童价"></Input>
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span="8">
            <Tree :data="treeData" ref="tree" :render="renderContent"></Tree>
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
export default {
  name: "product_edit",
  data() {
    return {
      loading: false,
      product: {},
      ruleValidate: {
        name: [
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
    handleReset(name) {
      this.$refs[name].resetFields();
    },
    // 子节点的option
    renderContent(h, { root, node, data }) {
      return h(
        "Option",
        {
          style: {
            display: "inline-block",
            margin: "0"
          },
          props: {
            value: data.value
          }
        },
        data.title
      );
    },
    saveProduct(name) {
      this.$refs[name].validate(valid => {
        if (valid) {
          this.product.categoryId = "43C0FCCE-4BCA-34B7-CD0A-39F20592944F";
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