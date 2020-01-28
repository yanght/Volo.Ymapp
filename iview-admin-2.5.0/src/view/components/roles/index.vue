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
  </div>
</template>

<script>
import { getRoles, updateRole, getRole } from "@/api/role";
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
          title: "名称",
          key: "name"
        },
        {
          title: "是都默认",
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
      roles: [],
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
      getRoles(data).then(res => {
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
    }
  },
  mounted() {
    this.getData();
  }
};
</script>