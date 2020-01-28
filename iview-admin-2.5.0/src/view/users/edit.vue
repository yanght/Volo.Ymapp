<template>
  <Form ref="formValidate" :model="formValidate" :rules="ruleValidate" :label-width="80">
    <FormItem label="用户名" prop="userName">
      <Input v-model="formValidate.userName" placeholder="请输入"></Input>
    </FormItem>
    <FormItem label="邮箱" prop="email">
      <Input v-model="formValidate.email" placeholder="请输入"></Input>
    </FormItem>
    <FormItem>
      <Button type="primary" @click="handleSubmit('formValidate')">Submit</Button>
      <Button @click="handleReset('formValidate')" style="margin-left: 8px">Reset</Button>
    </FormItem>
  </Form>
</template>
<script>
import { getModel } from "@/api/user";
export default {
  data() {
    return {
      formValidate: {
        userName: "",
        email: ""
      },
      ruleValidate: {
        userName: [
          {
            required: true,
            message: "The name cannot be empty",
            trigger: "blur"
          }
        ],
        email: [
          {
            required: true,
            message: "Mailbox cannot be empty",
            trigger: "blur"
          },
          { type: "email", message: "Incorrect email format", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    getData() {
      const id = this.$route.params.id;
      getModel(id).then(res => {
        this.formValidate = res.data;
      });
    },
    handleSubmit(name) {
      this.$refs[name].validate(valid => {
        if (valid) {
          this.$Message.success("Success!");
        } else {
          this.$Message.error("Fail!");
        }
      });
    },
    handleReset(name) {
      this.$refs[name].resetFields();
    }
  },
  mounted() {
    this.getData();
  }
};
</script>
