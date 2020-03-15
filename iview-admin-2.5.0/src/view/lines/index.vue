<template>
  <div>
    <Card>
      <Form inline :label-width="80">
        <FormItem label="用户名：">
          <Input v-model="form.userName" placeholder="请输入" style="width:200px;" />
        </FormItem>
        <Button type="primary" @click="getData" style="margin-right:8px;">查询</Button>
        <Button @click="handleReset" style="margin-right:8px;">重置</Button>
        <Button type="primary" icon="md-add" @click="HandleEdit">新建</Button>
      </Form>
    </Card>
    <Table :data="tableData" :columns="columns" :loading="loading" size="small"></Table>
    <div style="text-align:center;margin:16px 0">
      <Page
        :total="total"
        :current.sync="current"
        :page-size-opts="page_size_array"
        show-sizer
        show-total
        @on-change="getData"
        @on-page-size-change="handleChangeSize"
      ></Page>
    </div>
    <Modal v-model="edituser" title="编辑用户" width="50">
      <Form>
        <Form ref="userform" :model="userModel" :rules="ruleValidate" :label-width="80">
          <Row>
            <Col span="12">
              <FormItem label="用户名" prop="userName">
                <Input v-model="userModel.userName" placeholder="Enter your name"></Input>
              </FormItem>
            </Col>
            <Col span="12">
              <FormItem label="昵称" prop="name">
                <Input v-model="userModel.name" placeholder="Enter your name"></Input>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Col span="12">
              <FormItem label="邮箱" prop="email">
                <Input v-model="userModel.email" placeholder="Enter your e-mail"></Input>
              </FormItem>
            </Col>
            <Col span="12">
              <FormItem label="电话" prop="phoneNumber">
                <Input v-model="userModel.phoneNumber" placeholder="Enter your e-mail"></Input>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Col span="12">
              <FormItem label="真实姓名" prop="surname">
                <Input v-model="userModel.surname" placeholder="Enter your e-mail"></Input>
              </FormItem>
            </Col>
            <Col span="6">
              <FormItem label="是否锁定" prop="lockoutEnabled">
                <i-Switch
                  v-model="userModel.lockoutEnabled"
                  @on-change="HandlelockoutEnabled"
                  size="large"
                >
                  <span slot="open">是</span>
                  <span slot="close">否</span>
                </i-Switch>
              </FormItem>
            </Col>
            <Col span="6">
              <FormItem label="二次验证" prop="twoFactorEnabled">
                <Checkbox v-model="userModel.twoFactorEnabled">是</Checkbox>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Col span="24">
              <Card :bordered="false">
                <p slot="title">角色</p>
                <CheckboxGroup v-model="userRoles">
                  <Checkbox v-for="item in roles" :label="item.name" :key="item.name"></Checkbox>
                </CheckboxGroup>
              </Card>
            </Col>
          </Row>
        </Form>
      </Form>
      <Button slot="footer" type="primary" @click="handleSubmit('userform')">保存</Button>
    </Modal>
  </div>
</template>

<script>
import { getLineList } from "@/api/lines";
export default {
  name: "line_page",
  data() {
    return {
      total: 0,
      current: 1, // 当前页面
      size: 10, // 设置初始每页可以显示的数据条数
      page_size_array: [10, 20, 30, 40, 60, 100],
      tableData: [],
      loading: false,
      columns: [
        {
          title: "编号",
          key: "lineCode",
          width: 120
        },
        {
          title: "名称",
          key: "title",
          width: 280,
          render: (h, { row }) => {
            return h("div", [
              h(
                "Tooltip",
                {
                  props: {
                    placement: "top"
                  }
                },
                [
                  h(
                    "span",
                    {
                      style: {
                        display: "inline-block",
                        overflow: "hidden",
                        textOverflow: "ellipsis",
                        whiteSpace: "nowrap"
                      }
                    },
                    row.title
                  ), // 表格显示文字
                  h(
                    "div",
                    {
                      slot: "content",
                      style: {
                        whiteSpace: "normal"
                      }
                    },
                    row.title // 气泡内的文字
                  )
                ]
              )
            ]);
          }
        },
        {
          title: "洲",
          width: 100,
          key: "continent"
        },
        {
          title: "出发/返回",
          key: "placeLeave",
          width: 100,
          render: (h, { row }) => {
            return h("span", row.placeLeave + "/" + row.placeReturn);
          }
        },
        {
          title: "价格",
          key: "customerPrice",
          width: 100,
          render: (h, { row }) => {
            return h("div", [
              h(
                "Tooltip",
                {
                  props: {
                    placement: "top"
                  }
                },
                [
                  "¥" + row.customerPrice, // 表格显示文字
                  h(
                    "div",
                    {
                      slot: "content",
                      style: {
                        whiteSpace: "normal"
                      }
                    },
                    "成人价:¥" +
                      row.customerPrice +
                      ",儿童价:¥" +
                      row.childPrice +
                      ",单房差:¥" +
                      row.singleRoom // 气泡内的文字
                  )
                ]
              )
            ]);
          }
        },
        {
          title: "天数",
          key: "numDay",
          width: 100,
          render: (h, { row }) => {
            return h("span", row.numDay + "天/" + row.numNight + "晚");
          }
        },
        // {
        //   title: "状态",
        //   key: "lockoutEnabled",
        //   render: (h, { row }) => {
        //     if (row.lockoutEnabled === false) {
        //       return h("Badge", {
        //         props: {
        //           status: "processing",
        //           text: "正常"
        //         }
        //       });
        //     } else if (row.lockoutEnabled === true) {
        //       return h("Badge", {
        //         props: {
        //           status: "error",
        //           text: "锁定"
        //         }
        //       });
        //     }
        //   }
        // },
        {
          title: "创建时间",
          width: 100,
          key: "creationTime"
        },
        {
          title: "操作",
          key: "action",
          width: 200,
          fixed: "right",
          align: "center",
          render: (h, params) => {
            return h("div", [
              h(
                "Button",
                {
                  props: {
                    type: "primary",
                    size: "small"
                  },
                  style: {
                    marginRight: "5px"
                  },
                  on: {
                    click: () => {
                      this.HandleDetail(params.row);
                    }
                  }
                },
                "详细"
              ),
              h(
                "Button",
                {
                  props: {
                    type: "primary",
                    size: "small"
                  },
                  style: {
                    marginRight: "5px"
                  },
                  on: {
                    click: () => {
                      this.HandleEdit(params.row);
                    }
                  }
                },
                "编辑"
              ),
              h(
                "Button",
                {
                  props: {
                    type: "error",
                    size: "small"
                  },
                  on: {
                    click: () => {
                      this.remove(params.index);
                    }
                  }
                },
                "删除"
              )
            ]);
          }
        }
      ],
      form: {
        userName: ""
      },
      edituser: false,
      userModel: {},
      roles: [],
      userRoles: [],
      ruleValidate: {
        userName: [
          {
            required: true,
            message: "The name cannot be empty",
            trigger: "blur"
          }
        ]
      },
      isCreate: false
    };
  },
  methods: {
    getData() {
      this.loading = true;
      let data = {
        SkipCount: (this.current - 1) * this.size,
        MaxResultCount: this.size
      };
      getLineList(data).then(res => {
        this.tableData = res.data.items;
        this.total = res.data.totalCount;
        this.loading = false;
      });
    },
    handleChangeSize(val) {
      this.size = val;
      this.$nextTick(() => {
        this.getData();
      });
    },
    handleReset() {
      this.form.userName = "";
    },
    HandlelockoutEnabled(status) {
      this.userModel.lockoutEnabled = status;
    },

    HandleEdit(row) {
      const route = {
        name: "line_edit",
        query: {
          id: row.id
        },
        meta: {
          title: "编辑线路"
        }
      };
      this.$router.push(route);
    },
    HandleDetail(row) {
      const route = {
        name: "line_detail",
        query: {
          id: row.id
        },
        meta: {
          title: "线路详情"
        }
      };
      this.$router.push(route);
    },
    handleSubmit(name) {
      this.$refs[name].validate(valid => {
        if (valid) {
          this.userModel.roleNames = this.userRoles;
          if (this.userModel.id != "") {
            updateUser(this.userModel).then(res => {
              if (res.status == 200) {
                this.$Message.success("Success!");
                this.edituser = false;
                this.getData();
              }
            });
          }
        } else {
          this.$Message.error("Fail!");
        }
      });
    },
    remove(index) {
      this.tableData.splice(index, 1);
    }
  },
  mounted() {
    this.getData();
  }
};
</script>