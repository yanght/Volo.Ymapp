<template>
  <div>
    <Form
      ref="productform"
      :model="product"
      :rules="ruleValidate"
      :label-width="80"
      :loading="loading"
    >
      <Row>
        <Col span="18">
          <Card>
            <Row>
              <Col span="24">
                <FormItem label="商品名称" prop="name">
                  <Input v-model="product.title" placeholder="请输入商品名称"></Input>
                </FormItem>
              </Col>
            </Row>
            <Row>
              <Col span="24">
                <FormItem label="商品图片">
                  <div class="demo-upload-list" v-for="item in uploadList">
                    <template v-if="item.status === 'finished'">
                      <img :src="item.url" />
                      <div class="demo-upload-list-cover">
                        <Icon type="ios-eye-outline" @click.native="handleView(item.url)"></Icon>
                        <Icon type="ios-trash-outline" @click.native="handleRemove(item)"></Icon>
                      </div>
                    </template>
                    <template v-else>
                      <Progress v-if="item.showProgress" :percent="item.percentage" hide-info></Progress>
                    </template>
                  </div>
                  <Upload
                    ref="upload"
                    :show-upload-list="false"
                    :default-file-list="defaultList"
                    :on-success="handleSuccess"
                    :on-error="handleError"
                    :format="['jpg','jpeg','png']"
                    :max-size="2048"
                    :on-format-error="handleFormatError"
                    :on-exceeded-size="handleMaxSize"
                    :before-upload="handleBeforeUpload"
                    multiple
                    type="drag"
                    action="/api/pictures"
                    style="display: inline-block;width:58px;"
                  >
                    <div style="width: 58px;height:58px;line-height: 58px;">
                      <Icon type="ios-camera" size="20"></Icon>
                    </div>
                  </Upload>
                  <Modal title="View Image" v-model="visible">
                    <img :src="imgurl" v-if="visible" style="width: 100%" />
                  </Modal>
                </FormItem>
              </Col>
            </Row>
            <FormItem>
              <Button type="primary" @click="saveProduct('productform')">提交</Button>
              <Button @click="handleReset('productform')" style="margin-left: 8px">重置</Button>
            </FormItem>
          </Card>
        </Col>
        <Col span="6">
          <Card>
            <FormItem label="分类" prop="categoryId">
              <treeselect
                v-model="product.categoryId"
                :options="categorys"
                :normalizer="normalizer"
                @select="categorySelect"
              />
            </FormItem>
          </Card>
        </Col>
      </Row>
    </Form>
  </div>
</template>

<script>
// import the component
import Treeselect from "@riophae/vue-treeselect";
// import the styles
import "@riophae/vue-treeselect/dist/vue-treeselect.css";
import { mapMutations } from "vuex";
import { getProductDetail, addOrUpdateProduct } from "@/api/product";
import { getProductCategoryTree } from "@/api/product-category";
import { getPropertyList } from "@/api/property";
export default {
  name: "product_edit",
  components: {
    Treeselect
  },
  data() {
    return {
      loading: false,
      product: {},
      propertys: [],
      categorys: [],
      defaultList: [],
      imgurl: "",
      visible: false,
      uploadList: [],
      ruleValidate: {
        title: [
          {
            required: true,
            message: "请输入角色名称",
            trigger: "blur"
          }
        ]
      },
      normalizer(node) {
        return {
          id: node.id,
          label: node.title,
          children: node.children
        };
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
        getProductDetail(id).then(res => {
          this.product = res.data;
          res.data.productImages.forEach(item => {
            this.defaultList.push({ name: item, url: item });
          });
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
    categorySelect(node, instanceId) {
      this.propertyList(node.id);
    },
    saveProduct(name) {
      this.$refs[name].validate(valid => {
        if (valid) {
          addOrUpdateProduct(this.product).then(res => {
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
    },
    handleView(url) {
      this.imgurl = url;
      this.visible = true;
    },
    handleRemove(file) {
      const fileList = this.$refs.upload.fileList;
      this.$refs.upload.fileList.splice(fileList.indexOf(file), 1);
    },
    handleSuccess(res, file) {
      file.url = res.data[0].path;
      file.name = res.data[0].name;
    },
    handleError(error, file, fileList) {
      console.log(error);
    },
    handleFormatError(file) {
      this.$Notice.warning({
        title: "文件格式错误",
        desc: "文件 " + file.name + " 格式错误 仅支持 jpg,jpeg,png"
      });
    },
    handleMaxSize(file) {
      this.$Notice.warning({
        title: "文件超出大小",
        desc: "文件  " + file.name + "最大为2M."
      });
    },
    handleBeforeUpload() {
      const check = this.uploadList.length < 5;
      if (!check) {
        this.$Notice.warning({
          title: "最多上传5张图片."
        });
      }
      return check;
    }
  },
  mounted() {
    this.getData();
    this.categoryList();
    this.uploadList = this.$refs.upload.fileList;
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
<style>
.demo-upload-list {
  display: inline-block;
  width: 60px;
  height: 60px;
  text-align: center;
  line-height: 60px;
  border: 1px solid transparent;
  border-radius: 4px;
  overflow: hidden;
  background: #fff;
  position: relative;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);
  margin-right: 4px;
}
.demo-upload-list img {
  width: 100%;
  height: 100%;
}
.demo-upload-list-cover {
  display: none;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.6);
}
.demo-upload-list:hover .demo-upload-list-cover {
  display: block;
}
.demo-upload-list-cover i {
  color: #fff;
  font-size: 20px;
  cursor: pointer;
  margin: 0 2px;
}
</style>