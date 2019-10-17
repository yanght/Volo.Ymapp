import React, { Component } from 'react';
import { Table, Icon, Modal, Form, Spin, Col } from 'antd';
import { convertToTree, getGenerationKeys } from "@/library/utils/tree-utils";
import config from '@/commons/config-hoc';
import { arrayRemove, arrayPush } from '@/library/utils';

@config({
    ajax: true,
})
@Form.create()
export default class PermissionEdit extends Component {
    state = {
        loading: false,
        visible: false,
        data: [],
        selectedRowKeys: [],
        halfSelectedRowKeys: [],
        menuTreeData: [],
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
        this.windowHeight = document.body.clientHeight;
    }
    fetchData() {
        const { providerName, providerKey } = this.props;

        this.getPermissions(providerName, providerKey).then((values) => {

            const menus = values[0];
            // 菜单根据order 排序
            const orderedData = [...menus].sort((a, b) => {
                const aOrder = a.order || 0;
                const bOrder = b.order || 0;

                // 如果order都不存在，根据 text 排序
                if (!aOrder && !bOrder) {
                    return a.text > b.text ? 1 : -1;
                }

                return bOrder - aOrder;
            });

            const menuTreeData = convertToTree(orderedData);
            const selectedRowKeys = values[1];
            const halfSelectedRowKeys = values[2];
            const data = orderedData;
            this.setState({ menuTreeData, selectedRowKeys, halfSelectedRowKeys, data });
        });
    }

    getPermissions(providerName, providerKey) {
        return this.props.ajax.get('/api/abp/permissions', { providerName: providerName, providerKey: providerKey }).then(res => {
            let permissions = [];
            let gtandPermissions = [];
            let harfGranteddPermissions = [];
            const { groups } = res;
            Object.keys(groups).forEach(key => {
                const tempPermissions = groups[key].permissions;
                Object.keys(tempPermissions).forEach(key => {
                    const _key = tempPermissions[key].name;
                    const _text = tempPermissions[key].displayName;
                    const parent = tempPermissions[key].parentName;
                    const permission = { key: _key, text: _text, parentKey: parent, path: '/', local: _key, icon: 'align-left' };
                    permissions.push(permission);
                    if (tempPermissions[key].isGranted) {
                        gtandPermissions.push(tempPermissions[key].name);
                    }
                });
            });
            return Promise.resolve([permissions, gtandPermissions, harfGranteddPermissions]);
        });
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
    handleOk = () => {
        const { loading, selectedRowKeys, halfSelectedRowKeys, data } = this.state;
        if (loading) return;
        const { onOk, form: { validateFieldsAndScroll } } = this.props;
        validateFieldsAndScroll((err, values) => {
            if (!err) {
                const { providerName, providerKey } = this.props;
                // 半选、全选都要提交给后端保存
                const keys = selectedRowKeys.concat(halfSelectedRowKeys);
                let allkeys = [];
                data.forEach(item => {
                    const isGranted = keys.indexOf(item.key) > -1 ? true : false;
                    allkeys = arrayPush(allkeys, { name: item.key, isGranted: isGranted });
                })
                const params = { permissions: allkeys };
                console.log(params);
                this.setState({ loading: true });
                this.props.ajax.put(`/api/abp/permissions?providerName=${providerName}&providerKey=${providerKey}`, params)
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

    // 处理选中状态：区分全选、半选
    setSelectedRowKeys = (srk) => {
        let selectedRowKeys = [...srk];
        let halfSelectedRowKeys = [...this.state.halfSelectedRowKeys];
        const { menuTreeData } = this.state;

        const loop = (dataSource) => {
            dataSource.forEach(item => {
                const { children, key } = item;
                if (children ?.length) {
                    // 所有后代节点
                    const keys = getGenerationKeys(dataSource, key);
                    // 未选中节点
                    const unSelectedKeys = keys.filter(it => !selectedRowKeys.find(sk => sk === it));

                    // 一个也未选中
                    if (unSelectedKeys.length && unSelectedKeys.length === keys.length) {
                        halfSelectedRowKeys = arrayRemove(halfSelectedRowKeys, key);
                        selectedRowKeys = arrayRemove(selectedRowKeys, key);
                    }

                    // 部分选中
                    if (unSelectedKeys.length && unSelectedKeys.length < keys.length) {
                        halfSelectedRowKeys = arrayPush(halfSelectedRowKeys, key);
                        selectedRowKeys = arrayRemove(selectedRowKeys, key);
                    }

                    // 全部选中了
                    if (!unSelectedKeys.length && keys.length) {
                        halfSelectedRowKeys = arrayRemove(halfSelectedRowKeys, key);
                        selectedRowKeys = arrayPush(selectedRowKeys, key);
                    }

                    loop(children);
                }
            });
        };

        loop(menuTreeData);

        this.setState({ halfSelectedRowKeys, selectedRowKeys });
    };

    getCheckboxProps = (record) => {
        const { halfSelectedRowKeys, selectedRowKeys } = this.state;
        const { key } = record;

        //半选
        if (halfSelectedRowKeys.includes(key)) return { checked: false, indeterminate: true };

        // 全选
        if (selectedRowKeys.includes(key)) return { checked: true, indeterminate: false };

        return {};
    };

    onSelect = (record, selected) => {
        const { key } = record;
        let selectedRowKeys = [...this.state.selectedRowKeys];

        // 选中、反选所有的子节点
        const keys = getGenerationKeys(this.state.menuTreeData, key);
        keys.push(key);

        keys.forEach(k => {
            if (selected) {
                selectedRowKeys = arrayPush(selectedRowKeys, k);
            } else {
                selectedRowKeys = arrayRemove(selectedRowKeys, k);
            }
            this.setSelectedRowKeys(selectedRowKeys);
        })

    };

    render() {
        const {
            loading,
            menuTreeData,
            selectedRowKeys
        } = this.state;
        const { visible, providerKey } = this.props;

        return (
            <Modal
                destroyOnClose
                width="70%"
                confirmLoading={loading}
                visible={visible}
                title={providerKey}
                onOk={this.handleOk}
                onCancel={this.handleCancel}
            >
                <Spin spinning={loading}>
                    <Table
                        size="small"
                        defaultExpandAllRows
                        visible={visible}
                        loading={loading}
                        columns={this.columns}
                        rowSelection={{
                            selectedRowKeys,
                            onChange: this.setSelectedRowKeys,
                            getCheckboxProps: this.getCheckboxProps,
                            onSelect: this.onSelect
                        }}
                        dataSource={menuTreeData}
                        pagination={false}
                    />
                </Spin>
            </Modal>
        )
    }
}
