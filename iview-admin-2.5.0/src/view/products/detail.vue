<template>
  <div>
    <Card>
      <h2>ID: {{ $route.query.id }}</h2>
      <Button @click="close">调用closeTag方法关闭本页</Button>
    </Card>
    <Tabs active-key="key1">
      <Tab-pane label="团队" key="key1">
        <Table
          :data="this.line.lineTeams"
          :columns="lineTeamsColumns"
          :loading="loading"
          size="small"
        ></Table>
      </Tab-pane>
      <Tab-pane label="行程" key="key2">
        <Table
          :data="this.line.lineDays"
          :columns="lineDaysColumns"
          :loading="loading"
          size="small"
        ></Table>
      </Tab-pane>
      <Tab-pane label="介绍" key="key3">
        <Collapse active-key="1" accordion>
          <Panel v-for="item in this.line.lineDays" :key="item.id">
            {{item.channelType}}
            <p slot="content">{{item.describe}}</p>
          </Panel>
        </Collapse>
      </Tab-pane>
    </Tabs>
  </div>
</template>

<script>
import { mapMutations } from "vuex";
import { getLineByLineId } from "@/api/lines";
export default {
  name: "line_page",
  data() {
    return {
      loading: false,
      lineTeamsColumns: [
        {
          title: "发团时间",
          key: "dateStart",
          width: 100
        },
        {
          title: "名称",
          key: "productName",
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
                    row.productName
                  ), // 表格显示文字
                  h(
                    "div",
                    {
                      slot: "content",
                      style: {
                        whiteSpace: "normal"
                      }
                    },
                    row.productName // 气泡内的文字
                  )
                ]
              )
            ]);
          }
        },
        {
          title: "航空公司",
          key: "airCompany",
          width: 150
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
          title: "天数",
          key: "dayNum",
          width: 80,
          render: (h, { row }) => {
            return h("span", row.dayNum + "/天");
          }
        },
        {
          title: "计划/剩余",
          key: "planNum",
          width: 100,
          render: (h, { row }) => {
            return h("span", row.planNum + "/" + row.freeNum + "人");
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
          title: "上线时间",
          key: "dateOnline",
          width: 100
        },
        {
          title: "下线时间",
          key: "dateOffline",
          width: 100
        }
      ],
      lineIntrosClumns: [],
      lineDayImagesColumns: [],
      lineDaysColumns: [
        {
          title: "编号",
          key: "dayNumber",
          width: 80
        },
        {
          title: "酒店",
          key: "dayHotel"
        },
        {
          title: "早/中/晚",
          key: "dayHotel",
          render: (h, { row }) => {
            return h(
              "span",
              row.breakfast + "/" + row.lunch + "/" + row.dinner
            );
          }
        },
        {
          title: "交通",
          key: "dayTraffic"
        }
      ],
      lineRouteDatesColumns: [],
      line: {}
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
        name: "line_detail",
        query: {
          id: this.$route.query.id
        }
      });
    },
    getData() {
      this.loading = true;
      const id = this.$route.query.id;
      getLineByLineId(id).then(res => {
        this.line = res.data;
        this.loading = false;
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

<style>
</style>
