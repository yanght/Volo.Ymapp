import React, { Component } from 'react';
import { Table, Icon, Modal, Form, Row, Col } from 'antd';
import config from '@/commons/config-hoc';
import PageContent from '@/layouts/page-content';
import { convertToTree, disebleTreeeNode } from "@/library/utils/tree-utils";
import { ToolBar, Operator, FormElement } from '@/library/antd';

@config({
    path: '/categorys',
    title: { local: 'categorys', text: '分类列表', icon: 'lock' },
    ajax: true,
})
@Form.create()
export default class index extends Component {
    state = {
        loading: false,
        treeData: [],
        visible: false,
        record: {},
        disabled: false,
        categorys: [],
    };

    columns = [
        // key 与parentKey自动生成了，不需要展示和编辑
        // {title: 'key', dataIndex: 'key', key: 'key'},
        // { title: 'parentKey', dataIndex: 'parentKey', key: 'parentKey' },
        {
            title: '名称', dataIndex: 'text', key: 'text', width: 200,
            render: (value, record) => {
                const { icon } = record;

                if (icon) return <span><Icon type={icon} /> {value}</span>;

                return value;
            }
        },
        {
            title: '类型', dataIndex: 'type', key: 'type', width: 60,
            render: value => {
                if (value === 1) return '商品';
                if (value === 2) return '文章';
                // 默认都为菜单
                return '未知';
            }
        },
        { title: '排序', dataIndex: 'order', key: 'order', width: 60 },
        {
            title: '操作', dataIndex: 'operator', key: 'operator', width: 150,
            render: (value, record) => {
                const items = [
                    {
                        label: '编辑',
                        icon: 'form',
                        onClick: () => this.handleEditNode(record),
                    },
                    {
                        label: '删除',
                        icon: 'delete',
                        color: 'red',
                        confirm: {
                            title: '您请确定要删除此节点及其子节点吗？',
                            onConfirm: () => this.handleDeleteNode(record),
                        }
                    },
                    {
                        label: '添加子菜单',
                        icon: 'folder-add',
                        onClick: () => this.handleAddSubMenu(record),
                    },

                ];
                return <Operator items={items} />
            },
        },
    ];

    componentDidMount() {
        this.fetchCategorys();
    }

    fetchCategorys() {

        this.props.ajax.get('/api/app/category').then(res => {
            let categorys = [];
            res.items.forEach(item => {
                const node = { concurrencyStamp: item.concurrencyStamp, key: item.id, text: item.name, type: item.type, order: item.sort, icon: 'align-left' };
                if (item.parentId != '00000000-0000-0000-0000-000000000000') {
                    node.parentKey = item.parentId
                }
                categorys.push(node);
            })
            const menuTreeData = convertToTree(categorys);

            this.setState({ categorys: menuTreeData });
        });
    }

    handleAddTopMenu = () => {
        this.props.form.resetFields();
        this.setState({ visible: true, disabled: false, loading: true, });
        // 获取树形分类
        this.props.ajax
            .get('/api/app/category/categoryTree')
            .then(res => {
                this.setState({ treeData: res });
            })
            .finally(() => this.setState({ loading: false }));
    };

    handleEditNode = (record) => {
        const { resetFields, setFieldsValue } = this.props.form;

        resetFields();
        const {
            concurrencyStamp,
            key,
            parentKey,
            text,
            icon,
            type,
            sort,
        } = record;

        setTimeout(() => {
            setFieldsValue({
                concurrencyStamp,
                key,
                parentKey,
                text,
                icon,
                type,
                sort,
            })
        });

        this.setState({ visible: true, record, disabled: false, });
        // 获取树形分类
        this.setState({ loading: true });
        this.props.ajax
            .get('/api/app/category/categoryTree')
            .then(res => {
                disebleTreeeNode(res, key);
                this.setState({ treeData: res });
            })
            .finally(() => this.setState({ loading: false }));
    };

    handleAddSubMenu = (record) => {
        const { resetFields, setFieldsValue } = this.props.form;

        resetFields();
        const parentKey = record.key;
        const type = record.type;
        setTimeout(() => setFieldsValue({ parentKey, type }));

        this.setState({ visible: true, disabled: true, record });
    };

    handleDeleteNode = (record) => {
        const { key } = record;

        // TODO
        this.setState({ loading: true });
        this.props.ajax
            .del(`/api/app/category/${key}`)
            .then(() => {
                this.setState({ visible: false });
                this.fetchCategorys();
            })
            .finally(() => this.setState({ loading: false }));
    };

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.form.validateFieldsAndScroll((err, values) => {
            if (!err) {
                const params = {
                    concurrencyStamp: values.concurrencyStamp,
                    id: values.key,
                    name: values.text,
                    parentId: values.parentKey,
                    type: values.type,
                    sort: values.order
                }
                // 如果key存在视为修改，其他为添加
                const ajax = params.id ? this.props.ajax.put : this.props.ajax.post;
                const url = params.id ? `/api/app/category/${params.id}` : '/api/app/category';
                // TODO
                this.setState({ loading: true });
                ajax(url, params)
                    .then(() => {
                        this.setState({ visible: false });
                        this.fetchCategorys();
                    })
                    .finally(() => this.setState({ loading: false }));
            }
        });
    };
    handleIconClick = () => {
        this.setState({ iconVisible: true });
    };

    FormElement = (props) => <FormElement form={this.props.form} labelWidth={70} {...props} />;

    render() {
        const {
            categorys,
            visible,
            loading,
            iconVisible,
            treeData,
            disabled,
        } = this.state;
        const { form, form: { getFieldValue, setFieldsValue } } = this.props;
        const { getFieldDecorator } = this.props.form;
        const FormElement = this.FormElement;

        return (
            <PageContent >
                <ToolBar items={[{ type: 'primary', text: '添加顶级', onClick: this.handleAddTopMenu }]} />
                <Table
                    loading={loading}
                    columns={this.columns}
                    dataSource={categorys}
                    pagination={false}
                />
                <Modal
                    id="menu-modal"
                    title="分类编辑"
                    visible={visible}
                    onOk={this.handleSubmit}
                    onCancel={() => this.setState({ visible: false })}
                >
                    <Form onSubmit={this.handleSubmit}>
                        <FormElement type="hidden" field="key" />
                        <FormElement type="hidden" field="concurrencyStamp" />
                        <Row>
                            <Col span={12}>
                                <FormElement
                                    label="名称"
                                    field="text"
                                    decorator={{
                                        rules: [
                                            { required: true, message: '请输入名称！' },
                                        ],
                                    }}
                                />
                            </Col>
                            <Col span={12}>

                                <FormElement
                                    type="select-tree"
                                    label="上级分类"
                                    field="parentKey"
                                    allowClear={true}
                                    showSearch={true}
                                    treeNodeFilterProp="title"
                                    dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
                                    options={treeData}
                                    disabled={disabled}
                                    placeholder="请选择"
                                    treeDefaultExpandAll
                                />

                            </Col>
                        </Row>
                        <Row>
                            <Col span={12}>
                                <FormElement
                                    label="类型"
                                    type="select"
                                    options={[
                                        { value: 1, label: '商品' },
                                        { value: 2, label: '文章' },
                                    ]}
                                    field="type"
                                    disabled={disabled}
                                    decorator={{
                                        rules: [
                                            { required: true, message: '请选择类型' },
                                        ],
                                    }}
                                // getPopupContainer={() => document.querySelector('.ant-modal-wrap')}
                                />
                            </Col>
                            <Col span={12}>
                                <FormElement
                                    label="排序"
                                    field="order"
                                    type='number'
                                />
                            </Col>

                        </Row>
                    </Form>
                </Modal>
            </PageContent>
        );
    }
}

