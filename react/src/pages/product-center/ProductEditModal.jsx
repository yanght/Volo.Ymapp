import React, { Component } from 'react';
import { Form, Row, Col, Button, Spin, Upload, Icon, Card, Tabs, Modal } from 'antd';
import _ from 'lodash';
import { FormElement } from '@/library/antd';
import PageContent from '@/layouts/page-content';
import config from '@/commons/config-hoc';
import modal from '@/components/modal-hoc';
import BraftEditor from 'braft-editor';
import 'braft-editor/dist/index.css';
import ProductPicture from './ProductPictures'


const { TabPane } = Tabs;
@config({ ajax: true })
@Form.create()
@modal(props => props.id === null ? '添加用户' : '修改用户')


export default class EditModal extends Component {
    state = {
        loading: false,
        data: {}, // 表单回显数据
        categorys: [],
        editorState: null,
    };

    componentDidMount() {
        const { id } = this.props;

        const isEdit = id !== null;

        if (isEdit) {
            this.setState({ loading: true });
            this.props.ajax.get(`/api/app/product/${id}`)
                .then(res => {
                    this.setState({ data: res || {} });
                })
                .finally(() => this.setState({ loading: false }));
        }
        // 获取树形分类
        this.props.ajax
            .get('/api/app/category/categoryTree')
            .then(res => {
                this.setState({ categorys: res });
            })
            .finally(() => this.setState({ loading: false }));
    }


    handleOk = () => {
        if (this.state.loading) return; // 防止重复提交

        this.props.form.validateFieldsAndScroll((err, values) => {
            if (err) return;

            const { id } = this.props;
            const isEdit = id !== null;
            values.description = this.state.editorState.toHTML();
            values.state = Number.parseInt(values.state);
            if (isEdit) {
                this.setState({ loading: true });
                this.props.ajax.put(`/api/app/product/${id}`, values, { successTip: '修改成功！' })
                    .then(() => {
                        const { onOk } = this.props;
                        onOk && onOk();
                    })
                    .finally(() => this.setState({ loading: false }));
            } else {
                this.props.ajax.post('/api/app/product', values, { successTip: '添加成功！' })
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


    handleEditorChange = (editorState) => {
        this.setState({
            editorState: editorState
        })
    }

    // 这样可以保证每次render时，FormElement不是每次都创建，这里可以进行一些共用属性的设置
    FormElement = (props) => <FormElement form={this.props.form} labelWidth={100} disabled={this.props.isDetail} {...props} />;

    render() {
        const { id } = this.props;
        const { getFieldDecorator } = this.props.form;
        const isEdit = id !== null;
        const { loading, data, categorys, editorState } = this.state;
        const span = 8;
        const excludeControls = [
            'letter-spacing',
            'line-height',
            'clear',
            'headings',
            'list-ol',
            'list-ul',
            'remove-styles',
            'superscript',
            'subscript',
            'hr',
            'text-align'
        ]
        //const selectedRoles = ['admin'];
        const FormElement = this.FormElement;

        return (
            <Spin spinning={loading}>
                <PageContent footer={false}>
                    <Form onSubmit={this.handleSubmit}>
                        {isEdit ? <FormElement type="hidden" field="id" initialValue={data.id} /> : null}
                        <FormElement type="hidden" field="concurrencyStamp" initialValue={data.concurrencyStamp} />
                        <Tabs defaultActiveKey="1" >
                            <TabPane tab="基本信息" key="1">
                                <Row>
                                    <Col>
                                        <FormElement
                                            label="名称"
                                            field="name"
                                            initialValue={data.name}
                                            required
                                        />
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={span}>
                                        <FormElement
                                            type="select-tree"
                                            label="所属分类"
                                            field="categoryId"
                                            allowClear={true}
                                            showSearch={true}
                                            initialValue={data.categoryId}
                                            treeNodeFilterProp="title"
                                            dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
                                            options={categorys}
                                            placeholder="请选择"
                                            treeDefaultExpandAll
                                            required
                                        />
                                    </Col>
                                    <Col span={span}>
                                        <FormElement
                                            type="select"
                                            label="商品状态"
                                            field="state"
                                            allowClear={true}
                                            initialValue={data.state}
                                            placeholder='请选择'
                                            options={[
                                                { value: '1', label: '正常' },
                                                { value: '2', label: '下架' },
                                                { value: '3', label: '停售' },
                                            ]}
                                        />
                                    </Col>
                                </Row>
                                <Row>
                                    <Col >
                                        <Card title="商品介绍:" bordered={false} >
                                            <div style={{ border: '1px solid #d1d1d1' }}>
                                                <BraftEditor
                                                    value={editorState}
                                                    excludeControls={excludeControls}
                                                    onChange={this.handleEditorChange}
                                                    onSave={this.submitContent}
                                                    contentStyle={{ height: 400 }}
                                                />
                                            </div>
                                        </Card>
                                    </Col>
                                </Row>
                            </TabPane>
                            <TabPane tab="图片" key="2">
                                <ProductPicture fileList={fileList}/>
                            </TabPane>
                            <TabPane tab="规格" key="3">
                                Content of Tab Pane 3
    </TabPane>
                        </Tabs>
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

