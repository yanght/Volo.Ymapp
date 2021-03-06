import React, { Component } from 'react'
import { Helmet } from 'react-helmet';
import { Form, Icon, Input, Button } from 'antd';
import { setAccessToken } from '@/commons';
import config from '@/commons/config-hoc';
import Local from '@/layouts/header-i18n';
import Color from '@/layouts/header-color-picker';
import { ROUTE_BASE_NAME } from '@/router/AppRouter';
import './style.less'

function hasErrors(fieldsError) {
    return Object.keys(fieldsError).some(field => fieldsError[field]);
}

@Form.create()
@config({
    path: '/login',
    noFrame: true,
    noAuth: true,
    keepAlive: false,
    connect(state) {
        return { local: state.system.i18n.login }
    },
    ajax: true
})
export default class extends Component {
    state = {
        loading: false,
        message: '',
    };

    componentDidMount() {
        const { form: { validateFields, setFieldsValue } } = this.props;
        // 一开始禁用提交按钮
        validateFields(() => void 0);

        // 开发时方便测试，填写表单
        if (process.env.NODE_ENV === 'development') {
            setFieldsValue({ userName: 'admin', password: '1q2w3E*' });
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.form.validateFields((err, values) => {
            if (!err) {
                this.setState({ loading: true, message: '' });

                // TODO 发送请求进行登录，一下为前端硬编码，模拟请求
                const { userName, password } = values;
                const params = {
                    client_id: 'Ymapp_Web',
                    client_secret: '1q2w3e*',
                    grant_type: 'password',
                    username: userName,
                    password: password
                };
                console.log(params);
                this.props.ajax.post('/connect/token', params, { headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8' } })
                    .then((res) => {
                        console.log(res);
                        if (res.access_token) {
                            setAccessToken(res);
                        }

                        // 跳转页面，优先跳转上次登出页面
                        const lastHref = window.sessionStorage.getItem('last-href');

                        // 强制跳转 进入系统之后，需要一些初始化工作，需要所有的js重新加载
                        window.location.href = lastHref || `${ROUTE_BASE_NAME}/`;
                        // this.props.history.push(lastHref || '/');
                    })
                    .finally(() => this.setState({ loading: false }));
            }
        });
    };

    render() {
        const { local } = this.props;
        const { getFieldDecorator, getFieldsError, getFieldError, isFieldTouched } = this.props.form;
        const { loading, message } = this.state;

        // Only show error after a field is touched.
        const userNameError = isFieldTouched('userName') && getFieldError('userName');
        const passwordError = isFieldTouched('password') && getFieldError('password');
        return (
            <div styleName="root" className="login-bg">
                <Helmet
                    title={local.title}
                />

                <div styleName="menu">
                    <Color />
                    <Local style={{ color: '#fff' }} />
                </div>
                <div styleName="logo" />
                <div styleName="note" />
                <div styleName="box">
                    <div styleName="header">{local.title}</div>
                    <Form onSubmit={this.handleSubmit}>
                        <Form.Item
                            validateStatus={userNameError ? 'error' : ''}
                            help={userNameError || ''}
                        >
                            {getFieldDecorator('userName', {
                                rules: [{ required: true, message: local.userNameEmptyTip }],
                            })(
                                <Input allowClear autoFocus prefix={<Icon type="user" style={{ fontSize: 13 }} />} placeholder="用户名" />
                            )}
                        </Form.Item>
                        <Form.Item
                            validateStatus={passwordError ? 'error' : ''}
                            help={passwordError || ''}
                        >
                            {getFieldDecorator('password', {
                                rules: [{ required: true, message: local.passwordEmptyTip }],
                            })(
                                <Input.Password prefix={<Icon type="lock" style={{ fontSize: 13 }} />} placeholder="密码" />
                            )}
                        </Form.Item>
                        <Button
                            styleName="submit-btn"
                            loading={loading}
                            type="primary"
                            htmlType="submit"
                            disabled={hasErrors(getFieldsError())}
                        >
                            {local.submit}
                        </Button>
                    </Form>
                    <div styleName="error-tip">{message}</div>
                    <div styleName="tip">
                        <span>{local.userName}：admin </span>
                        <span>{local.password}：111</span>
                    </div>
                </div>
            </div>
        );
    }
}

