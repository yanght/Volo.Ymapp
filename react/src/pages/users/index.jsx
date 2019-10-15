import React, { Component } from 'react';
import { Button, Table, Form, Tag } from 'antd';
import PageContent from '@/layouts/page-content';
import {
    QueryBar,
    Pagination,
    Operator,
    ToolBar,
    FormRow,
    FormElement,
} from "@/library/antd";
import config from '@/commons/config-hoc';
import UserEditModal from './UserEditModal';

@config({
    path: '/users',
    ajax: true,
    pageHead: false
})
@Form.create()
export default class UserCenter extends Component {
    state = {
        dataSource: [],     // 表格数据
        total: 0,           // 分页中条数
        pageSize: 10,       // 分页每页显示条数
        pageNum: 1,         // 分页当前页
        collapsed: true,    // 是否收起
        visible: false,     // 添加、修改弹框
        id: null,           // 需要修改的数据id
    };

    columns = [
        { title: '用户名', dataIndex: 'userName', key: 'userName' },
        { title: '昵称', dataIndex: 'name', key: 'name' },
        { title: '邮箱', dataIndex: 'email', key: 'email' },
        // {
        //     title: '是否公共', dataIndex: 'isPublic', key: 'isPublic',
        //     render: (value, record) => {
        //         let color = record.isPublic ? 'green' : 'geekblue';
        //         let text = record.isPublic ? '是' : '否';
        //         if (record.isPublic)
        //             return (
        //                 <Tag color={color} key={text}>
        //                     {text}
        //                 </Tag>
        //             )
        //     }
        // },
        // {
        //     title: '角色', dataIndex: 'roleNames', key: 'roleNames', render: roleNames => (
        //         <span>
        //             {
        //                 roleNames.map(tag => {
        //                     return (
        //                         <Tag color='volcano' key={tag}>{tag}</Tag>
        //                     )
        //                 })
        //             }
        //         </span>
        //     )
        // },
        {
            title: '操作', dataIndex: 'operator', key: 'operator',
            render: (value, record) => {
                const { id, name } = record;
                const items = [
                    {
                        label: '编辑',
                        onClick: () => this.setState({ visible: true, id }),
                    },
                    {
                        label: '删除',
                        color: 'red',
                        confirm: {
                            title: `您确定删除"${name}"?`,
                            onConfirm: () => this.handleSearch(),
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
        this.props.form.validateFieldsAndScroll((err, values) => {
            if (err) return;
            //const { pageNum, pageSize } = this.state;
            const skipCount = (this.state.pageNum - 1) * this.state.pageSize;
            const maxResultCount = this.state.pageSize;
            const params = {
                ...values,
                skipCount: skipCount,
                maxResultCount: maxResultCount,
            };

            this.props.ajax.get('/api/identity/users', params)
                .then(res => {
                    const dataSource = res.items || [];
                    const total = res.totalCount || 0;

                    this.setState({ dataSource, total });
                });
        });

    };

    FormElement = (props) => <FormElement form={this.props.form} labelWidth={62} width={300} style={{ paddingLeft: 16 }} {...props} />;

    render() {
        const {
            total,
            pageNum,
            pageSize,
            collapsed,
            dataSource,
            visible,
            id,
        } = this.state;

        const FormElement = this.FormElement;
        return (
            <PageContent>
                <QueryBar
                    showCollapsed
                    collapsed={collapsed}
                    onCollapsedChange={collapsed => this.setState({ collapsed })}
                >
                    <FormRow>
                        <FormElement
                            label="用户名"
                            field="userName"
                        />
                        <FormElement
                            type="select"
                            label="是否激活"
                            field="isActive"
                            options={[
                                { value: 'true', label: '是' },
                                { value: 'false', label: '否' },
                            ]}
                        />
                        <FormElement layout width="auto">
                            <Button type="primary" onClick={this.handleSearch}>提交</Button>
                            <Button onClick={() => this.props.form.resetFields()}>重置</Button>
                        </FormElement>
                    </FormRow>
                    {/* <FormRow>
                        <FormElement
                            type="date"
                            label="入职时间"
                            field="time"
                        />
                        <FormElement
                            label="年龄"
                            field="age"
                        />
                       
                    </FormRow> */}
                </QueryBar>

                <ToolBar
                    items={[
                        { type: 'primary', text: '添加用户', icon: 'user-add', onClick: () => this.setState({ visible: true, id: null }) }
                    ]}
                />

                <Table
                    columns={this.columns}
                    dataSource={dataSource}
                    rowKey="id"
                    pagination={false}
                />

                <Pagination
                    total={total}
                    pageNum={pageNum}
                    pageSize={pageSize}
                    onPageNumChange={pageNum => this.setState({ pageNum }, this.handleSearch)}
                    onPageSizeChange={pageSize => this.setState({ pageSize, pageNum: 1 }, this.handleSearch)}
                />
                <UserEditModal
                    visible={visible}
                    id={id}
                    onOk={() => this.setState({ visible: false }, this.handleSearch)}
                    onCancel={() => this.setState({ visible: false })}
                />
            </PageContent>
        );
    }
}
