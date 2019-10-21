import React, { Component } from 'react';
import { Form, Row, Col, Button, Spin, Upload, Icon, message, Card } from 'antd';
import _ from 'lodash';
import { FormElement } from '@/library/antd';
import PageContent from '@/layouts/page-content';
import config from '@/commons/config-hoc';
import validator from '@/library/utils/validation-rule';
import modal from '@/components/modal-hoc';
import BraftEditor from 'braft-editor';
import 'braft-editor/dist/index.css';

@config({ ajax: true })
@Form.create()
@modal(props => props.id === null ? '添加文章' : '修改文章')

export default class EditModal extends Component {
    state = {
        loading: false,
        data: {}, // 表单回显数据
        roles: [],
        categorys: [],
        imageUrl: '',
        editorState: null,
    };

    componentDidMount() {
        const { id } = this.props;

        const isEdit = id !== null;

        if (isEdit) {
            this.setState({ loading: true });
            this.props.ajax.get(`/api/app/article/${id}`)
                .then(res => {
                    this.setState({ data: res || {}, editorState: BraftEditor.createEditorState(res.mainContent) });
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
            const { editorState } = this.state;
            values.mainContent = editorState.toHTML();
            if (isEdit) {
                this.setState({ loading: true });
                this.props.ajax.put(`/api/app/article/${id}`, values, { successTip: '修改成功！' })
                    .then(() => {
                        const { onOk } = this.props;
                        onOk && onOk();
                    })
                    .finally(() => this.setState({ loading: false }));
            } else {
                this.props.ajax.post('/api/app/article', values, { successTip: '添加成功！' })
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
        const { loading, data, categorys, imageUrl, editorState } = this.state;
        const span = 8;
        const uploadButton = (
            <div>
                <Icon type={this.state.loading ? 'loading' : 'plus'} />
                <div className="ant-upload-text">Upload</div>
            </div>
        );
        //const selectedRoles = ['admin'];
        const FormElement = this.FormElement;
        function onChange(checkedValues) {
            console.log('checked = ', checkedValues);
        }
        function beforeUpload(file) {
            const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
            if (!isJpgOrPng) {
                message.error('You can only upload JPG/PNG file!');
            }
            const isLt2M = file.size / 1024 / 1024 < 2;
            if (!isLt2M) {
                message.error('Image must smaller than 2MB!');
            }
            return isJpgOrPng && isLt2M;
        }
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
        return (
            <Spin spinning={loading}>
                <PageContent footer={false}>
                    <Form onSubmit={this.handleSubmit}>
                        {isEdit ? <FormElement type="hidden" field="id" initialValue={data.id} /> : null}
                        <FormElement type="hidden" field="concurrencyStamp" initialValue={data.concurrencyStamp} />
                        <Row>
                            <Col span={span}>
                                <FormElement
                                    label="标题"
                                    field="title"
                                    initialValue={data.title}
                                    required
                                />
                            </Col>
                            <Col span={span}>
                                <FormElement
                                    label="作者"
                                    field="author"
                                    initialValue={data.author}
                                />
                            </Col>
                            <Col span={span}>
                                <FormElement
                                    label="来源"
                                    field="source"
                                    initialValue={data.source}
                                />
                            </Col>
                        </Row>
                        <Row>
                            <Col span={24}>
                                <FormElement
                                    label="简介"
                                    field="describe"
                                    rows={4}
                                    type='textarea'
                                    initialValue={data.describe}
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
                                    label="是否推荐"
                                    field="recommend"
                                    type="checkbox"
                                    defaultChecked={data.recommend}
                                />
                            </Col>
                        </Row>

                        <Row>
                            <Col span={2}>

                            </Col>
                            <Col span={12}>
                                <Upload
                                    name="pictureUrl"
                                    listType="picture-card"
                                    className="avatar-uploader"
                                    showUploadList={false}
                                    action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
                                    beforeUpload={beforeUpload}
                                    onChange={this.handleChange}
                                >
                                    {imageUrl ? <img src={imageUrl} alt="avatar" style={{ width: '100%' }} /> : uploadButton}
                                </Upload>
                            </Col>
                        </Row>
                        {/* <Row>
                            <Col span={24}>
                                <FormElement
                                    label="正文"
                                    field="mainContent"
                                    rows={4}
                                    type='textarea'
                                    initialValue={data.mainContent}
                                    required
                                />
                            </Col>
                        </Row> */}
                        <Row>
                            <Col >
                                <Card title="正文:" bordered={false} >
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
                    </Form>
                </PageContent>
                <div className="ant-modal-footer">
                    <Button onClick={this.handleOk} type="primary">保存</Button>
                    <Button onClick={this.handleReset}>重置</Button>
                    <Button onClick={this.handleCancel}>取消</Button>
                </div>
            </Spin >
        );
    }
}

