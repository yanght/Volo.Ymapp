<template>
  <div>
    <Card>
      <Form
        ref="lineform"
        :model="lineModel"
        :rules="ruleValidate"
        :label-width="80"
        :loading="loading"
      >
        <Row>
          <Col span="18">
            <FormItem label="线路名称" prop="title">
              <Input v-model="lineModel.title" placeholder="请输入线路名称"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem>
              <i-col span="11">
                <Form-item prop="numDay">
                  <Input v-model="lineModel.numDay" placeholder="天">
                    <span slot="append">天</span>
                  </Input>
                </Form-item>
              </i-col>
              <i-col span="2" style="text-align: center">-</i-col>
              <i-col span="11">
                <Form-item prop="numNight">
                  <Input v-model="lineModel.numNight" placeholder="晚">
                    <span slot="append">晚</span>
                  </Input>
                </Form-item>
              </i-col>
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span="6">
            <FormItem label="成人价" prop="customerPrice">
              <Input v-model="lineModel.customerPrice" placeholder="请输入成人价"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="儿童价" prop="childPrice">
              <Input v-model="lineModel.childPrice" placeholder="请输入儿童价"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="单房差" prop="singleRoom">
              <Input v-model="lineModel.singleRoom" placeholder="请输入单房差"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="海外参团价" prop="overseasJoinPrice">
              <Input v-model="lineModel.overseasJoinPrice" placeholder="请输入海外参团价"></Input>
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span="6">
            <FormItem label="洲" prop="continent">
              <Input v-model="lineModel.continent" placeholder="请输入洲"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="国家" prop="country">
              <Input v-model="lineModel.country" placeholder="请输入国家"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="出发城市" prop="placeLeave">
              <Input v-model="lineModel.placeLeave" placeholder="请输入出发城市"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="返回城市" prop="placeReturn">
              <Input v-model="lineModel.placeReturn" placeholder="请输入返回城市"></Input>
            </FormItem>
          </Col>
        </Row>
        <!-- <Row>
          <FormItem label="封面图片" prop="firstLineImg">
            <Upload action="//jsonplaceholder.typicode.com/posts/">
              <i-button icon="ios-cloud-upload-outline">上传文件</i-button>
            </Upload>
          </FormItem>
        </Row>-->
        <FormItem>
          <Button type="primary" @click="handleSubmit('lineform')">提交</Button>
          <Button @click="handleReset('lineform')" style="margin-left: 8px">重置</Button>
        </FormItem>
      </Form>
    </Card>
    <Card>
      <Form ref="lineTeams" :model="lineModel" :label-width="80" style="width: 300px">
        <FormItem
          v-for="(item, index) in lineModel.lineTeams"
          :key="index"
          :rules="{required: true, message: 'Item ' + item.index +' can not be empty', trigger: 'blur'}"
        >
          <Row>
            <Col span="18">
              <Input type="text" v-model="item.productName" placeholder="Enter something..."></Input>
            </Col>
            <Col span="4" offset="1">
              <Button @click="handleRemove(index)">Delete</Button>
            </Col>
          </Row>
        </FormItem>
        <FormItem>
          <Row>
            <Col span="12">
              <Button type="dashed" long @click="handleAddTeam" icon="md-add">Add item</Button>
            </Col>
          </Row>
        </FormItem>
      </Form>
    </Card>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
import { getLineByLineId } from "@/api/lines";
export default {
  name: "line_edit",
  data() {
    return {
      loading: false,
      lineModel: {},
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
        name: "line_edit",
        query: {
          id: this.$route.query.id
        }
      });
    },
    getData() {
      this.loading = true;
      const id = this.$route.query.id;
      getLineByLineId(id).then(res => {
        this.lineModel = res.data;
        //this.loading = false;
      });
    },
    handleReset(name) {
      this.$refs[name].resetFields();
    },
    handleAddTeam() {
      this.lineModel.lineTeams.push({
        productName: ""
      });
    },
    handleRemove(index) {
      this.lineMode.LineTeams[index].status = 0;
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