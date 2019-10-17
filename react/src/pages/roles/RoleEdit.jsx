import React, { Component } from 'react';
import { Modal, Form, Spin, Icon, Row, Col } from 'antd';
import config from '@/commons/config-hoc';
import { FormElement } from '@/library/antd';

@config({
    ajax: true,
})
@Form.create()
export default class RoleEdit extends Component {
    state = {
        loading: false,
        data: {},
        isDefault: false,
        isPublic: false
    };

    columns = [
        {
            title: '名称', dataIndex: 'text', key: 'text', width: 250,
            render: (value, record) => {
                const { icon } = record;

                if (icon) return <span><Icon type={icon} /> {value}</span>;

                return value;
            }
        },
        {
            title: '类型', dataIndex: 'type', key: 'type', width: 80,
            render: value => {
                if (value === '1') return '菜单';
                if (value === '2') return '功能';
                // 默认都为菜单
                return '菜单';
            }
        },
        { title: 'path', dataIndex: 'path', key: 'path', width: 150 },
        { title: 'url', dataIndex: 'url', key: 'url' },
        { title: 'target', dataIndex: 'target', key: 'target', width: 100 },
    ];

    componentDidMount() {
        //this.fetchMenus();
        this.windowHeight = document.body.clientHeight;
    }

    componentDidUpdate(prevProps) {
        const { visible, form: { resetFields } } = this.props;

        // 打开弹框
        if (!prevProps.visible && visible) {
            // 重置表单，接下来填充新的数据
            resetFields();

            // 重新获取数据
            this.fetchData();
        }
    }


    fetchData() {
        const { roleId } = this.props;
        if (!roleId) {
            // 添加操作
            this.setState({ data: {} });

        } else {
            // 修改操作

            // TODO 根据id 发送ajax请求获取数据
            this.setState({ loading: true });


            this.props.ajax.get(`/api/identity/roles/${roleId}`).then(res => {
                this.setState({ data: res || {}, isDefault: res.isDefault, isPublic: res.isPublic });
            }).finally(() => this.setState({ loading: false }));

        }
    }




    handleOk = () => {
        const { loading } = this.state;
        if (loading) return;
        const { onOk, form: { validateFieldsAndScroll } } = this.props;

        validateFieldsAndScroll((err, values) => {
            if (!err) {
                // 半选、全选都要提交给后端保存
                const params = { ...values };
                const { id } = values;

                // TODO ajax 提交数据

                // id存在未修改，不存在未添加
                const ajax = id ? this.props.ajax.put : this.props.ajax.post;
                const url = id ? `/api/identity/roles/${id}` : '/api/identity/roles';
                this.setState({ loading: true });
                ajax(url, params)
                    .then(() => {
                        if (onOk) onOk();
                    })
                    .finally(() => this.setState({ loading: false }));
            }
        });
    };

    handleCancel = () => {
        const { onCancel } = this.props;
        if (onCancel) onCancel();
    };
    onChange = (e) => {
        if (e.target.id == 'isDefault') {
            this.setState({
                isDefault: e.target.checked
            });
        }
        if (e.target.id == 'isPublic') {
            this.setState({
                isPublic: e.target.checked
            });
        }
    }

    FormElement = (props) => <FormElement form={this.props.form} labelWidth={100} {...props} />;

    render() {
        const { visible } = this.props;
        const { loading, data } = this.state;
        const FormElement = this.FormElement;
        return (
            <Modal
                destroyOnClose
                width="70%"
                confirmLoading={loading}
                visible={visible}
                title={data.id ? '编辑角色' : '添加角色'}
                onOk={this.handleOk}
                onCancel={this.handleCancel}
            >
                <Spin spinning={loading}>
                    <Form>
                        {data.id ? (<FormElement type="hidden" field="id" decorator={{ initialValue: data.id }} />) : null}
                        <FormElement type="hidden" field="concurrencyStamp" decorator={{ initialValue: data.concurrencyStamp }} />
                        <Row>
                            <Col span={10}>
                                <FormElement
                                    label="角色名称"
                                    field="name"
                                    decorator={{
                                        initialValue: data.name,
                                        rules: [
                                            { required: true, message: '请输入角色名称！' }
                                        ],
                                    }}
                                />
                            </Col>
                            <Col span={4}>
                                <FormElement
                                    label="是否默认"
                                    field="isDefault"
                                    type="checkbox"
                                    onChange={this.onChange}
                                    checked={this.state.isDefault}
                                />
                            </Col>

                            <Col span={4}>
                                <FormElement
                                    label="是否公共"
                                    field="isPublic"
                                    type="checkbox"
                                    onChange={this.onChange}
                                    checked={this.state.isPublic}
                                />
                            </Col>
                        </Row>
                    </Form>
                    {/* <Table
                        size="small"
                        defaultExpandAllRows
                        columns={this.columns}
                        rowSelection={{
                            selectedRowKeys,
                            onChange: this.setSelectedRowKeys,
                            getCheckboxProps: this.getCheckboxProps,
                            onSelect: this.onSelect
                        }}
                        dataSource={menuTreeData}
                        pagination={false}
                        scroll={{ y: this.windowHeight ? this.windowHeight - 390 : 400 }}
                    /> */}
                </Spin>
            </Modal>
        );
    }
}
