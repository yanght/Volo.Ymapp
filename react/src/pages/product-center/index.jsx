import React, { Component } from 'react';
import { Table } from 'antd';
import FixBottom from '@/layouts/fix-bottom';
import {
    QueryBar,
    QueryItem,
    ToolItem,
    Pagination,
    Operator,
    ToolBar,
} from "@/library/antd";
import PageContent from '@/layouts/page-content';
import config from '@/commons/config-hoc';

@config({
    path: '/productlist',
    title: { local: 'productlist', text: '商品列表', icon: 'lock' },
    ajax: true
})

export default class ProductList extends React.Component {

    state = {
        loading: false,
        dataSource: [],
        total: 0,
        pageSize: 10,
        pageIndex: 1,
        params: {},
        id: void 0,
        visible: false,
    };


    // TODO 查询条件
    queryItems = [
        [
            {
                type: 'input',
                field: 'goodsId',
                label: '商品编号',
            },
            {
                type: 'input',
                field: 'goodsName',
                label: '商品名',
            },
        ],
    ];

    // TODO 顶部工具条
    toolItems = [
        {
            type: 'primary',
            text: '添加',
            icon: 'plus',
            onClick: () => {
                // TODO
            },
        },
    ];

    // TODO 底部工具条
    bottomToolItems = [
        {
            type: 'primary',
            text: '导出',
            icon: 'export',
            onClick: () => {
                // TODO
            },
        },
    ];

    columns = [
        {
            title: '主图', render: (text, record) => (
                <img src={record.MasterPic} width='100px' height='75px'>
                </img>
            ),
        },
        { title: '编号', dataIndex: 'GoodsID' },
        { title: '商品名', dataIndex: 'GoodsName' },
        { title: '状态', dataIndex: 'State' },
        { title: '商品分类', dataIndex: 'GoodsType' },
        { title: '供货商拿货', dataIndex: 'SupplierId' },
        { title: '推荐', dataIndex: 'RecommendType' },
        {
            title: '操作',
            key: 'operator',
            // render: (text, record) => {
            //     const { id, customerNo } = record;
            //     const successTip = `删除“${customerNo}”成功！`;
            //     const items = [
            //         {
            //             label: '修改',
            //             onClick: () => {
            //                 this.handleEdit(id);
            //             },
            //         },
            //         {
            //             label: '删除',
            //             color: 'red',
            //             confirm: {
            //                 title: `您确定要删除“${customerNo}”？`,
            //                 onConfirm: () => {
            //                     this.setState({ loading: true });
            //                     this.props.ajax
            //                         .del(`/user-center/${id}`, null, { successTip })
            //                         .then(() => this.handleSearch())
            //                         .finally(() => this.setState({ loading: false }));
            //                 },
            //             },
            //         },
            //     ];

            //     return (<Operator items={items} />);
            // },
        },
    ];

    componentDidMount() {
        this.handleSearch();
    }

    handleSearch = () => {
        const { params, pageIndex, pageSize } = this.state;

        this.setState({ loading: true });
        this.props.ajax
            .post('/DriverMallCity/Goods/FilterGoodsList', { ...params, pageIndex, pageSize })
            .then(res => {
                console.log(res);
                if (res.Success) {
                    const { list: dataSource, total } = res.Data;

                    this.setState({
                        dataSource: res.Data.List,
                        total: res.Data.Count,
                    });
                }
            })
            .finally(() => this.setState({ loading: false }));
    };

    handleAdd = () => {
        this.setState({ id: void 0, visible: true });
    };

    handleEdit = (id) => {
        this.setState({ id, visible: true });
    };

    render() {
        const {
            loading,
            dataSource,
            total,
            pageIndex,
            pageSize,
            visible,
            id,
        } = this.state;

        return (
            <PageContent loading={loading}>
                <QueryBar>
                    <QueryItem
                        loadOptions={this.fetchOptions}
                        items={this.queryItems}
                        onSubmit={params => this.setState({ params }, this.handleSearch)}
                    />
                </QueryBar>

                <ToolBar items={this.toolItems} />

                <Table
                    columns={this.columns}
                    dataSource={dataSource}
                    rowKey="id"
                    pagination={false}
                />

                <Pagination
                    total={total}
                    pageNum={pageIndex}
                    pageSize={pageSize}
                    onPageNumChange={pageIndex => this.setState({ pageIndex }, this.handleSearch)}
                    onPageSizeChange={pageSize => this.setState({ pageSize, pageIndex: 1 }, this.handleSearch)}
                />
                <FixBottom>
                    <ToolItem items={this.bottomToolItems} />
                </FixBottom>

                {/* <UserCenterEdit
                    id={id}
                    visible={visible}
                    onOk={() => this.setState({ visible: false })}
                    onCancel={() => this.setState({ visible: false })}
                /> */}
            </PageContent>
        );
    }
}