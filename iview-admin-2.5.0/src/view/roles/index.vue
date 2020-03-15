<template>
  <div>
    <Button type="primary" icon="md-add" @click="handleCreate">新建</Button>
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
    <Modal v-model="editrole" title="编辑角色" width="50">
      <Form>
        <Form ref="roleform" :model="roleModel" :rules="ruleValidate" :label-width="80">
          <Row>
            <Col span="12">
              <FormItem label="角色名称" prop="name">
                <Input v-model="roleModel.name" placeholder="请输入角色名称"></Input>
              </FormItem>
            </Col>
            <Col span="6">
              <FormItem label prop="isDefault">
                <Checkbox v-model="roleModel.isDefault">默认</Checkbox>
              </FormItem>
            </Col>
            <Col span="6">
              <FormItem label prop="isPublic">
                <Checkbox v-model="roleModel.isPublic">公共</Checkbox>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Card v-for="item in permissions" v-bind:key="item.name">
              <p slot="title">{{item.displayName}}</p>
              <CheckboxGroup v-model="selectedPermissions">
                <Checkbox
                  :label="perm.name"
                  v-for="perm in item.permissions"
                  v-bind:key="perm.name"
                  :value="perm.name"
                >{{perm.displayName}}</Checkbox>
              </CheckboxGroup>
            </Card>
          </Row>
        </Form>
      </Form>
      <Button slot="footer" type="primary" @click="handleSubmit('roleform')">保存</Button>
    </Modal>
  </div>
</template>

<script>
import {
  getRoles,
  addRole,
  updateRole,
  getRole,
  deleteRole,
  addOrUpdateRole,
  getPermissions,
  grantPermission
} from "@/api/role";
export default {
  data() {
    return {
      total: 0,
      current: 1, // 当前页面
      size: 10, // 设置初始每页可以显示的数据条数
      page_size_array: [10, 20, 30, 40, 60, 100],
      tableData: [],
      permissions: [],
      selectedPermissions: [],
      loading: false,
      columns: [
        {
          title: "名称",
          key: "name"
        },
        {
          title: "是否默认",
          key: "isDefault",
          render: (h, { row }) => {
            if (row.isDefault) {
              return h("Badge", {
                props: {
                  status: "processing",
                  text: "是"
                }
              });
            } else {
              return h("Badge", {
                props: {
                  status: "error",
                  text: "否"
                }
              });
            }
          }
        },
        {
          title: "是否静态",
          key: "isStatic",
          render: (h, { row }) => {
            if (row.isStatic) {
              return h("Badge", {
                props: {
                  status: "processing",
                  text: "是"
                }
              });
            } else {
              return h("Badge", {
                props: {
                  status: "error",
                  text: "否"
                }
              });
            }
          }
        },
        {
          title: "是否公共",
          key: "isPublic",
          render: (h, { row }) => {
            if (row.isPublic) {
              return h("Badge", {
                props: {
                  status: "processing",
                  text: "是"
                }
              });
            } else {
              return h("Badge", {
                props: {
                  status: "error",
                  text: "否"
                }
              });
            }
          }
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
                      this.HandleDelete(params.row);
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
      editrole: false,
      roleModel: {},
      ruleValidate: {
        name: [
          {
            required: true,
            message: "请输入角色名称",
            trigger: "blur"
          }
        ]
      },
      isCreate: false
    };
  },
  methods: {
    /* 获取角色列表 */
    getData() {
      this.loading = true;
      let data = {
        SkipCount: (this.current - 1) * this.size,
        MaxResultCount: this.size
      };
      getRoles(data).then(res => {
        this.tableData = res.data.items;
        this.total = res.data.totalCount;
        this.loading = false;
      });
    },
    /**获取权限列表 */
    getPermissions(roleName) {
      this.selectedPermissions = [];
      getPermissions("Role", roleName).then(res => {
        this.permissions = res.data.groups;
        res.data.groups.forEach(element => {
          element.permissions.forEach(item => {
            if (item.isGranted == true) {
              this.selectedPermissions.push(item.name);
            }
          });
        });
        //console.log(this.selectedPermissions);
        this.loading = false;
      });
    },
    /**分页改变 */
    handleChangeSize(val) {
      this.size = val;
      this.$nextTick(() => {
        this.getData();
      });
    },
    /**创建新角色 */
    handleCreate() {
      this.roleModel = {};
      this.getPermissions("");
      this.loading = false;
      this.editrole = true;
    },
    /**编辑角色 */
    HandleEdit(row) {
      this.loading = true;
      getRole(row.id).then(res => {
        this.roleModel = res.data;
        this.getPermissions(row.name);
        this.loading = false;
        this.editrole = true;
      });
    },
    HandleDelete(row) {
      this.loading = true;
      deleteRole(row.id).then(res => {
        this.loading = false;
        this.$Message.success("Success!");
        this.getData();
      });
    },
    /**提交保存 */
    handleSubmit(name) {
      this.$refs[name].validate(valid => {
        if (valid) {
          console.log(this.roleModel);
          addOrUpdateRole(this.roleModel).then(res => {
            if (res.status == 200) {
              const allPermissions = this.filterPermissions(
                this.permissions,
                this.selectedPermissions
              );
              grantPermission("Role", this.roleModel.name, {
                permissions: allPermissions
              });
              this.$Message.success("Success!");
              this.editrole = false;
              this.getData();
            }
          });
        }
      });
    },
    /**重组最新权限选择列表 */
    filterPermissions(allPermissions, selectedPermissions) {
      let permissions = [];
      allPermissions.forEach(item => {
        item.permissions.forEach(permission => {
          let isGranted = selectedPermissions.indexOf(permission.name) != -1;
          permissions.push({ name: permission.name, isGranted: isGranted });
        });
      });
      return permissions;
    }
  },
  mounted() {
    this.getData();
  }
};
</script>