<template>
    <div>
        <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
            <el-form-item label="父菜单" prop="name">
                <el-cascader :props="{ checkStrictly: true,value:'id',label:'name' }" :options="options"  clearable>
                </el-cascader>
            </el-form-item>
            <el-form-item label="菜单名称" prop="region">
                <el-input v-model="ruleForm.name"></el-input>
            </el-form-item>
            <el-form-item label="是否显示" prop="delivery">
                <el-switch v-model="ruleForm.delivery"></el-switch>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
    import { getList } from "@/api/menu";
    export default {
        name: 'VueElementAdminMenuAdd',

        data() {
            return {
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
                        { required: true, message: '请输入活动名称', trigger: 'blur' },
                        { min: 3, max: 5, message: '长度在 3 到 5 个字符', trigger: 'blur' }
                    ],
                    region: [
                        { required: true, message: '请选择活动区域', trigger: 'change' }
                    ],
                    date1: [
                        { type: 'date', required: true, message: '请选择日期', trigger: 'change' }
                    ],
                    date2: [
                        { type: 'date', required: true, message: '请选择时间', trigger: 'change' }
                    ],
                    type: [
                        { type: 'array', required: true, message: '请至少选择一个活动性质', trigger: 'change' }
                    ],
                    resource: [
                        { required: true, message: '请选择活动资源', trigger: 'change' }
                    ],
                    desc: [
                        { required: true, message: '请填写活动形式', trigger: 'blur' }
                    ]
                },
                options: []
            };
        },

        created() {

        },

        mounted() {
            getList().then(m => {
                this.options = this.GetNodes(m.data);
                console.log(this.options);
            });
        },

        methods: {
            submitForm(formName) {
                this.$refs[formName].validate((valid) => {
                    if (valid) {
                        alert('submit!');
                    } else {
                        console.log('error submit!!');
                        return false;
                    }
                });
            },
            resetForm(formName) {
                this.$refs[formName].resetFields();
            },
            GetNodes(list) {
                // 数组转treefunction composeTree(list = []) {
                const data = JSON.parse(JSON.stringify(list)) // 浅拷贝不改变源数据
                const result = []
                if (!Array.isArray(data)) {
                    return result
                }
                data.forEach(item => {
                    delete item.children
                })
                const map = {}
                data.forEach(item => {
                    map[item.id] = item
                })
                data.forEach(item => {
                    const parent = map[item.pid]
                    if (parent) {
                        (parent.children || (parent.children = [])).push(item)
                    } else {
                        result.push(item)
                    }
                })
                return result
            }
        },
    };
</script>

<style>
    .demo-ruleForm label.el-form-item__label {
        font-weight: normal;
    }
</style>