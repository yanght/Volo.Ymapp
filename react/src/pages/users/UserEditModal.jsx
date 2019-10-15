import React, { Component } from 'react';
import { Form, Row, Col, Button, Spin, Checkbox, Card } from 'antd';
import _ from 'lodash';
import { FormElement } from '@/library/antd';
import PageContent from '@/layouts/page-content';
import config from '@/commons/config-hoc';
import validator from '@/library/utils/validation-rule';
import modal from '@/components/modal-hoc';

@config({ ajax: true })
@Form.create()
@modal(props => props.id === null ? '添加用户' : '修改用户')

export default class EditModal extends Component {
    state = {
        loading: false,
        data: {}, // 表单回显数据
        roles: [],
        selectedRoles: [],
    };

    componentDidMount() {
        const { id } = this.props;

        const isEdit = id !== null;

        if (isEdit) {
            this.setState({ loading: true });
            this.props.ajax.get(`/api/identity/users/${id}`)
                .then(res => {
                    this.setState({ data: res || {} });
                })
                .finally(() => this.setState({ loading: false }));

            this.props.ajax.get(`/api/identity/users/${id}/roles`).then(res => {
                let selectedRoles = [];
                res.items.forEach(item => {
                    selectedRoles.push(item.name);
                })
                this.setState({ selectedRoles: selectedRoles });
            })
        }
        this.props.ajax.get('/api/identity/roles')
            .then(res => {
                let roles = [];
                res.items.forEach(item => {
                    roles.push({ label: item.name, value: item.name });
                })
                this.setState({ roles: roles || {} });
            });
    }


    handleOk = () => {
        if (this.state.loading) return; // 防止重复提交

        this.props.form.validateFieldsAndScroll((err, values) => {
            if (err) return;

            const { id } = this.props;
            const isEdit = id !== null;

            if (isEdit) {
                this.setState({ loading: true });
                this.props.ajax.put(`/api/identity/users/${id}`, values, { successTip: '修改成功！' })
                    .then(() => {
                        const { onOk } = this.props;
                        onOk && onOk();
                    })
                    .finally(() => this.setState({ loading: false }));
            } else {
                this.props.ajax.post('/api/identity/users', values, { successTip: '添加成功！' })
                    .then(() => {
                        const { onOk } = this.props;
                        onOk && onOk();
                    })
                    .finally(() => this.setState({ loading: false }));
            }
        });
    };


    // 节流校验写法
    userNameExist = _.debounce((rule, value, callback) => {
        console.log('节流发请求');
    }, 500);

    handleCancel = () => {
        const { onCancel } = this.props;
        onCancel && onCancel();
    };

    handleReset = () => {
        this.props.form.resetFields();
    };

    // 这样可以保证每次render时，FormElement不是每次都创建，这里可以进行一些共用属性的设置
    FormElement = (props) => <FormElement form={this.props.form} labelWidth={100} disabled={this.props.isDetail} {...props} />;

    render() {
        const { id } = this.props;
        const { getFieldDecorator } = this.props.form;
        const isEdit = id !== null;
        const { loading, data, roles, selectedRoles } = this.state;
        const span = 8;
        //const selectedRoles = ['admin'];
        const FormElement = this.FormElement;
        function onChange(checkedValues) {
            console.log('checked = ', checkedValues);
        }
        return (
            <Spin spinning={loading}>
                <PageContent footer={false}>
                    <Form onSubmit={this.handleSubmit}>
                        {isEdit ? <FormElement type="hidden" field="id" initialValue={data.id} /> : null}
                        <FormElement type="hidden" field="concurrencyStamp" initialValue={data.concurrencyStamp} />
                        <Row>
                            <Col span={span}>
                                <FormElement
                                    label="用户名"
                                    field="userName"
                                    initialValue={data.userName}
                                    required
                                // rules={[
                                //     validator.noSpace(),
                                //     validator.userNameExist(),
                                //     { validator: this.userNameExist }
                                // ]}
                                />
                            </Col>
                            <Col span={span}>
                                <FormElement
                                    label="昵称"
                                    field="name"
                                    initialValue={data.name}
                                    required
                                />
                            </Col>
                        </Row>
                        <Row>
                            <Col span={span}>
                                <FormElement
                                    label="邮箱"
                                    field="email"
                                    initialValue={data.email}
                                    required
                                />
                            </Col>
                        </Row>
                        <Card bordered={false} title="选择角色">
                            <Form.Item >
                                {getFieldDecorator('roleNames', { initialValue: selectedRoles })(
                                    <Checkbox.Group
                                        options={roles}
                                        onChange={onChange}
                                    />
                                )}

                            </Form.Item>
                        </Card>
                    </Form>
                </PageContent>
                <div className="ant-modal-footer">
                    <Button onClick={this.handleOk} type="primary">保存</Button>
                    <Button onClick={this.handleReset}>重置</Button>
                    <Button onClick={this.handleCancel}>取消</Button>
                </div>
            </Spin>
        );
    }
}

