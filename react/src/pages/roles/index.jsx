import React, { Component } from 'react';
import { Table } from 'antd';
import PageContent from '@/layouts/page-content';
import { Operator, ToolBar } from "@/library/antd";
import config from '@/commons/config-hoc';
import RoleEdit from './RoleEdit';
import PermissionEdit from './PermissionEdit';

@config({
    path: '/roles',
    ajax: true
})
export default class RoleList extends Component {
    state = {
        roleId: void 0,
        providerKey: '',
        providerName: 'Role',
        visible: false,
        permissionVisible: false,
        total: 0,           // 分页中条数
        pageSize: 10,       // 分页每页显示条数
        pageNum: 1,         // 分页当前页
        dataSource: []     // 表格数据
    };

    columns = [
        { title: '角色名称', dataIndex: 'name', key: 'name' },
        { title: '角色描述', dataIndex: 'description', key: 'description' },
        {
            title: '操作', dataIndex: 'operator', key: 'operator',
            render: (value, record) => {
                const { id, name } = record;
                const items = [
                    {
                        label: '编辑',
                        onClick: () => this.handleEdit(id),
                    },
                    {
                        label: '权限',
                        onClick: () => this.handlePermissionEdit(name),
                    },
                    {
                        label: '删除',
                        color: 'red',
                        confirm: {
                            title: `您确定删除"${name}"?`,
                            onConfirm: () => this.handleDelete(id),
                        },
                    }
                ];

                return <Operator items={items} />
            },
        }
    ];

    componentDidMount() {
        this.handleSearch();
    }

    handleSearch = () => {
        // const pageNum = 1;
        // const pageSize = 20;
        // const dataSource = [];
        // for (let i = 0; i < pageSize; i++) {
        //     const id = pageSize * (pageNum - 1) + i + 1;
        //     dataSource.push({ id: `${id}`, name: `管理员${id}`, description: '角色描述' });
        // }

        //const { pageNum, pageSize } = this.state;
        const skipCount = (this.state.pageNum - 1) * this.state.pageSize;
        const maxResultCount = this.state.pageSize;
        const params = {
            skipCount: skipCount,
            maxResultCount: maxResultCount,
        };

        this.props.ajax.get('/api/identity/roles', params)
            .then(res => {
                const dataSource = res.items || [];
                const total = res.totalCount || 0;

                this.setState({ dataSource, total });
            });

    };


    handleAdd = () => {
        this.setState({ roleId: void 0, visible: true });
    };

    handleDelete = (id) => {
        // TODO
    };

    handleEdit = (roleId) => {
        this.setState({ roleId, visible: true });
    };
    handlePermissionEdit = (providerKey) => {
        this.setState({ providerKey, permissionVisible: true });
    }
    render() {
        const {
            dataSource,
            visible,
            roleId,
            permissionVisible,
            providerName,
            providerKey
        } = this.state;

        return (
            <PageContent>
                <ToolBar
                    items={[
                        { type: 'primary', text: '添加角色', icon: 'plus', onClick: this.handleAdd }
                    ]}
                />

                <Table
                    columns={this.columns}
                    dataSource={dataSource}
                    rowKey="id"
                    pagination={false}
                />
                <RoleEdit
                    roleId={roleId}
                    visible={visible}
                    onOk={() => this.setState({ visible: false })}
                    onCancel={() => this.setState({ visible: false })}
                />
                <PermissionEdit
                    providerKey={providerKey}
                    providerName={providerName}
                    visible={permissionVisible}
                    onOk={() => this.setState({ permissionVisible: false })}
                    onCancel={() => this.setState({ permissionVisible: false })}
                />
            </PageContent>
        );
    }
}
