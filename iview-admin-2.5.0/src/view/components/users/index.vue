<template>
  <div>
    <Card>
      <Form inline :label-width="80">
        <FormItem label="用户名：">
          <Input v-model="form.userName" placeholder="请输入" style="width:200px;" />
        </FormItem>
        <Button type="primary" @click="getData" style="margin-right:8px;">查询</Button>
        <Button @click="handleReset" style="margin-right:8px;">重置</Button>
        <Button type="primary" icon="md-add" @click="handleCreate">新建</Button>
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
import { getUserTableData, updateUser, getUserRoles } from "@/api/user";
import { getRoles } from "@/api/role";
export default {
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
          title: "用户名",
          key: "userName"
        },
        {
          title: "昵称",
          key: "name"
        },
        {
          title: "邮箱",
          key: "email"
        },
        {
          title: "手机号码",
          key: "phoneNumber"
        },
        {
          title: "状态",
          key: "lockoutEnabled",
          render: (h, { row }) => {
            if (row.lockoutEnabled === false) {
              return h("Badge", {
                props: {
                  status: "processing",
                  text: "正常"
                }
              });
            } else if (row.lockoutEnabled === true) {
              return h("Badge", {
                props: {
                  status: "error",
                  text: "锁定"
                }
              });
            }
          }
        },
        {
          title: "创建时间",
          key: "creationTime"
        },
        {
          title: "Action",
          key: "action",
          width: 150,
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
      getUserTableData(data).then(res => {
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
    handleCreate() {
      this.edituser = true;
      this.userModel = {
        lockoutEnabled: false
      };
      this.userRoles = [];
      getRoles().then(res => {
        this.roles = res.data.items;
      });
    },
    HandleEdit(row) {
      this.edituser = true;
      this.userModel = row;
      getRoles().then(res => {
        this.roles = res.data.items;
      });
      getUserRoles({ id: row.id }).then(res => {
        res.data.items.forEach(item => {
          this.userRoles.push(item.name);
        });
      });
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