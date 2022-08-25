<template>
    <div class="login">
        <div>
            <div class="floatleft">
                <img :src="src">
            </div>
            <div class="floatleft loginForm">
                <h1>
                    <i class="el-icon-umbrella"></i>
                    智慧医疗中控系统
                </h1>
                <div class="positionCenter avatar">
                    <el-avatar :size="100" :src="circleUrl"></el-avatar>
                </div>
                <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
                    <el-form-item label="工号" prop="name">
                        <el-input v-model="ruleForm.name" prefix-icon="el-icon-user" placeholder="请输入"></el-input>
                    </el-form-item>
                    <el-form-item label="密码" prop="name">
                        <el-input v-model="ruleForm.name" prefix-icon="el-icon-lock" placeholder="请输入"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="submitForm('ruleForm')">立即创建</el-button>
                        <el-button @click="resetForm('ruleForm')">重置</el-button>
                    </el-form-item>
                </el-form>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'VueElementAdminDefault',

        data() {
            return {
                src: `${process.env.VUE_APP_BASE_API}/images/loginbg.jpg`,
                circleUrl: "https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png",
                ruleForm: {
                    name: '',
                    region: '',
                    date1: '',
                    date2: '',
                    delivery: false,
                    type: [],
                    resource: '',
                    desc: ''
                },
                rules: {
                    name: [
                        { required: true, message: '请输入工号', trigger: 'blur' }
                    ]
                }
            };
        },

        mounted() {

        },

        methods: {

        },
    };
</script>

<style scoped>    
    .positionCenter {
        margin: 0 auto;
    }

    div.login {
        height: 100%;
        background: #f2f8fd;
        overflow: hidden
    }

    div.login>div {
        width: 1056px;
        height: 520px;
        margin: 0 auto;
        margin-top: 90px;
        background: #fff;
    }

    div.loginForm h1 {
        font-size: 16px;
        text-align: center;
        width: 476px;
        height: 100px;
        line-height: 100px;
        color: rgb(78, 78, 78);
    }

    div.loginForm i {
        color: blue;
        font-size: 21px;
        font-weight: bold;
    }

    div.avatar {
        width: 100px;
    }

    .demo-ruleForm {
        width: 60%;
        margin-top: 42px;
        margin-left: 52px;
    }
</style>