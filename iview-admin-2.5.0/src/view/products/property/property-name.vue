<template>
  <div>
    <Card>
      {{this.$route.query.title}}
      <Button type="primary" icon="md-add" @click="addProperty">新建</Button>
    </Card>
    <Card v-for="item in data" v-bind:key="item.id">
      <p slot="title">
        <Icon type="ios-film-outline"></Icon>
        {{item.title}}
      </p>

      <a href="#" slot="extra" @click.prevent="deleteproperty(item.id)">
        <Icon type="ios-trash" />删除
      </a>
      <div>
        <Tag
          v-for="model in item.propertyValues"
          v-bind:key="model.id"
          type="border"
          closable
          color="primary"
          @on-close="deleteValue(model.id)"
        >{{ model.value }}</Tag>
        <a href="#" slot="extra" @click="addValue(item.id)">
          <Icon type="ios-add-circle" />添加
        </a>
      </div>
    </Card>
  </div>
</template>

<script>
import {
  addPropertyName,
  getPropertyList,
  addPropertyValue,
  deletePropertyValue,
  deletePropertyName
} from "@/api/property";
export default {
  name: "propertyname",
  data() {
    return {
      data: [],
      propertyName: "",
      propertyValue: ""
    };
  },
  methods: {
    getData() {
      const categoryId = this.$route.query.id;
      getPropertyList({ categoryId: categoryId }).then(res => {
        this.data = res.data;
      });
    },
    addProperty() {
      const categoryId = this.$route.query.id;
      this.$Modal.confirm({
        render: h => {
          return h("Input", {
            props: {
              value: this.value,
              autofocus: true,
              placeholder: "请输入属性名称"
            },
            on: {
              input: val => {
                this.propertyName = val;
              }
            }
          });
        },
        onOk: () => {
          const data = { categoryId: categoryId, title: this.propertyName };
          addPropertyName(data).then(res => {
            this.getData();
          });
        }
      });
    },
    addValue(id) {
      this.$Modal.confirm({
        render: h => {
          return h("Input", {
            props: {
              value: this.value,
              autofocus: true,
              placeholder: "请输入属性值"
            },
            on: {
              input: val => {
                this.propertyValue = val;
              }
            }
          });
        },
        onOk: () => {
          const data = { propertyNameId: id, value: this.propertyValue };
          addPropertyValue(data).then(res => {
            this.getData();
          });
        }
      });
    },
    deleteValue(id) {
      this.$Modal.confirm({
        title: "删除提醒",
        content: "确定要删除吗,该操作不可恢复",
        onOk: () => {
          deletePropertyValue(id).then(res => {
            this.getData();
          });
        }
      });
    },
    deleteproperty(id) {
      this.$Modal.confirm({
        title: "删除提醒",
        content: "确定要删除吗,该操作不可恢复",
        onOk: () => {
          deletePropertyName(id).then(res => {
            this.getData();
          });
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