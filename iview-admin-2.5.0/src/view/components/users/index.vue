<template>
  <div>
    <Card>
      <Form inline :label-width="80">
        <FormItem label="用户名：">
          <Input v-model="form.userName" placeholder="请输入" style="width:200px;" />
        </FormItem>
        <Button type="primary" @click="getData" style="margin-right:8px;">查询</Button>
        <Button @click="handleReset" style="margin-right:8px;">重置</Button>
        <Button type="primary" icon="md-add" @click="edituser=true">新建</Button>
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
        <Form ref="formValidate" :model="formValidate" :rules="ruleValidate" :label-width="80">
          <Row>
            <Col span="12">
              <FormItem label="用户名" prop="userName">
                <Input v-model="formValidate.userName" placeholder="Enter your name"></Input>
              </FormItem>
            </Col>
            <Col span="12">
              <FormItem label="昵称" prop="name">
                <Input v-model="formValidate.name" placeholder="Enter your name"></Input>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Col span="12">
              <FormItem label="邮箱" prop="email">
                <Input v-model="formValidate.email" placeholder="Enter your e-mail"></Input>
              </FormItem>
            </Col>
            <Col span="12">
              <FormItem label="电话" prop="phone">
                <Input v-model="formValidate.phone" placeholder="Enter your e-mail"></Input>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Col span="12">
              <FormItem label="真实姓名" prop="surname">
                <Input v-model="formValidate.surname" placeholder="Enter your e-mail"></Input>
              </FormItem>
            </Col>
            <Col span="12">
              <FormItem label="账号状态" prop="lockoutEnabled">
                <i-Switch v-model="formValidate.lockoutEnabled" size="large">
                  <span slot="open">正常</span>
                  <span slot="close">锁定</span>
                </i-Switch>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Col span="8">
              <FormItem label="二次验证" prop="twoFactorEnabled">
                <Checkbox v-model="formValidate.twoFactorEnabled">是</Checkbox>
              </FormItem>
            </Col>
            <Col span="8">
              <FormItem label="电话验证" prop="phoneNumberConfirmed">
                <Checkbox v-model="formValidate.phoneNumberConfirmed">是</Checkbox>
              </FormItem>
            </Col>
            <Col span="8">
              <FormItem label="邮箱验证" prop="emailConfirmed">
                <Checkbox v-model="formValidate.emailConfirmed">是</Checkbox>
              </FormItem>
            </Col>
          </Row>
        </Form>
      </Form>
      <Button slot="footer" type="primary" @click="handleCreate">创建</Button>
    </Modal>
  </div>
</template>

<script>
import { getUserTableData } from "@/api/user";
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
                      this.show(params.index);
                    }
                  }
                },
                "View"
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
                "Delete"
              )
            ]);
          }
        }
      ],
      form: {
        userName: ""
      },
      edituser: false,
      formValidate: {
        userName: "",
        lockoutEnabled: true
      },
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
    handleCreate() {
      const userName = this.formValidate.userName;
    },
    show(index) {
      this.edituser = true;
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