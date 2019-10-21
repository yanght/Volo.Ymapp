import React, { Component, Fragment } from 'react';
import PageContent from '@/layouts/page-content';
import config from '@/commons/config-hoc';
import { Button, Form, Table } from 'antd';
import { ToolBar, QueryBar, Operator, FormRow, Pagination, FormElement } from '@/library/antd';
import ArticleEditModal from './ArticleEditModal';

@config({
    path: '/articles',
    title: { local: 'articles', text: '文章列表', icon: 'lock' },
    ajax: true,
})
@Form.create()
export default class Index extends Component {
    state = {
        collapsed: true,
        visible: false,
        dataSource: [],
        categorys: [],
        total: 0,
        pageNum: 1,
        pageSize: 10,
        id: null,
        startTime: null,
        endTime: null,
    };
    columns = [
        { title: '标题', dataIndex: 'title', key: 'title' },
        {
            title: '分类', dataIndex: 'categoryId', key: 'categoryId', render: (value, record) => {
                return record.category.name;
            }
        },
        { title: '作者', dataIndex: 'author', key: 'author' },
        { title: '来源', dataIndex: 'source', key: 'source' },
        {
            title: '是否推荐', dataIndex: 'recommend', key: 'recommend', render: (value, record) => {
                return value ? '是' : '否';
            }
        },
        { title: '创建时间', dataIndex: 'creationTime', key: 'creationTime' },
        {
            title: '操作', dataIndex: 'operator', key: 'operator',
            render: (value, record) => {
                const { id, title } = record;
                const items = [
                    {
                        label: '编辑',
                        onClick: () => this.setState({ visible: true, id }),
                    },
                    {
                        label: '删除',
                        color: 'red',
                        confirm: {
                            title: `您确定删除"${title}"?`,
                            onConfirm: () => this.handleDelete(record),
                        },
                    }
                ];

                return <Operator items={items} />
            },
        }
    ]

    componentDidMount() {
        // 获取树形分类
        this.props.ajax
            .get('/api/app/category/categoryTree')
            .then(res => {
                this.setState({ categorys: res });
            })
            .finally(() => this.setState({ loading: false }));
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
                startTime: this.state.startTime,
                endTime: this.state.endTime,
                skipCount: skipCount,
                maxResultCount: maxResultCount,
            };

            this.props.ajax.get('/api/app/article/articlelist', params)
                .then(res => {
                    const dataSource = res.items || [];
                    const total = res.totalCount || 0;
                    this.setState({ dataSource, total });
                });
        });
    }

    handleDelete = (record) => {
        const { id } = record;
        this.setState({ loading: true });
        this.props.ajax
            .del(`/api/app/article/${id}`)
            .then(() => {
                this.setState({ visible: false });
                this.handleSearch();
            })
            .finally(() => this.setState({ loading: false }));
    }

    onChange = (field, value) => {
        this.setState({
            [field]: value,
        });
    };

    onStartChange = (value, dateString) => {
        this.onChange('startTime', dateString);
    };

    onEndChange = (value, dateString) => {
        this.onChange('endTime', dateString);
    };
    FormElement = (props) => <FormElement form={this.props.form} labelWidth={62} width={300} style={{ paddingLeft: 16 }} {...props} />;

    render() {
        const {
            collapsed,
            visible,
            dataSource,
            categorys,
            total,
            pageNum,
            pageSize,
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
                            label="标题"
                            field="title"
                        />
                        <FormElement
                            label="作者"
                            field="author"
                        />
                        <FormElement
                            type="select-tree"
                            label="所属分类"
                            field="categoryId"
                            allowClear={true}
                            showSearch={true}
                            treeNodeFilterProp="title"
                            dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
                            options={categorys}
                            placeholder="请选择"
                            treeDefaultExpandAll
                        />
                        {collapsed ? null : (
                            <Fragment>
                                <FormElement
                                    type="select"
                                    label="是否推荐"
                                    field="recommend"
                                    allowClear={true}
                                    placeholder='请选择'
                                    options={[
                                        { value: 'true', label: '是' },
                                        { value: 'false', label: '否' },
                                    ]}
                                />
                                <FormElement
                                    label="开始时间"
                                    field="startTime"
                                    type='date-time'
                                    format="YYYY-MM-DD HH:mm:ss"
                                    placeholder="开始时间"
                                    onChange={this.onStartChange}
                                />
                                <FormElement
                                    label="结束时间"
                                    field="endTime"
                                    type='date-time'
                                    format="YYYY-MM-DD HH:mm:ss"
                                    placeholder="结束时间"
                                    onChange={this.onEndChange}
                                />
                            </Fragment>
                        )}
                        <FormElement layout width="auto">
                            <Button type="primary" onClick={this.handleSearch}>提交</Button>
                            <Button onClick={() => this.props.form.resetFields()}>重置</Button>
                        </FormElement>
                    </FormRow>

                </QueryBar>
                <ToolBar items={[{ type: 'primary', text: '添加', icon: 'plus', onClick: () => this.setState({ visible: true, id: null }) }]} />
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
                <ArticleEditModal
                    visible={visible}
                    id={id}
                    onOk={() => this.setState({ visible: false }, this.handleSearch)}
                    onCancel={() => this.setState({ visible: false })}
                />
            </PageContent>
        )
    }

}