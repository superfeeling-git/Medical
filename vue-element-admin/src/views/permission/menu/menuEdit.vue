<template>
    <div>
        <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
            <el-form-item label="父菜单" prop="name">
                <el-cascader placeholder="默认顶级菜单" :props="{ checkStrictly: true,value:'id',label:'name' }" v-model="pid"
                    :options="options" clearable>
                </el-cascader>
            </el-form-item>
            <el-form-item label="菜单名称" prop="menuName">
                <el-input v-model="ruleForm.menuName" @input="UpdateEnName"></el-input>
            </el-form-item>
            <el-form-item label="name" prop="menuNameEn">
                <el-input v-model="ruleForm.menuNameEn"></el-input>
            </el-form-item>
            <el-form-item label="菜单链接" prop="menuPath">
                <el-input v-model="ruleForm.menuPath"></el-input>
            </el-form-item>
            <el-form-item label="组件路径" prop="componentPath">
                <el-input v-model="ruleForm.componentPath"></el-input>
            </el-form-item>
            <el-form-item label="是否显示" prop="isShow">
                <el-switch v-model="ruleForm.isShow"></el-switch>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
    import { getList, menuAdd } from "@/api/menu";
    import pinyin from 'js-pinyin';
    export default {
        name: 'VueElementAdminMenuEdit',

        data() {
            return {
                pid: [],
                ruleForm: {
                    parnetId: {},
                    menuName: '',
                    menuNameEn: '',
                    menuPath: '',
                    componentPath: '',
                    isShow: false
                },
                rules: {
                    menuName: [
                        { required: true, message: '请输入菜单名称', trigger: 'blur' }
                    ]
                },
                options: []
            };
        },

        created() {
            console.log(pinyin.getFullChars('张三').toLowerCase());
        },

        mounted() {
            getList().then(m => {
                this.options = this.GetNodes(m.data);
            });
        },

        methods: {
            /**
             * @description: 初始化数据
             * @return {*}
             */
            initData(id) {
               //this.ruleForm
            },
            submitForm(formName) {
                this.$refs[formName].validate((valid) => {
                    if (valid) {
                        this.ruleForm.parnetId = this.pid.slice(-1)[0];
                        let _this = this;
                        menuAdd(this.ruleForm).then(m => {
                            this.$message({
                                showClose: true,
                                type: 'success',
                                message: '添加成功',
                                onClose: function (e) {
                                    console.log(_this);
                                    _this.$emit('Refresh');
                                }
                            });
                        });
                        console.log(this.ruleForm);
                    } else {
                        console.log('error submit!!');
                        return false;
                    }
                });
            },
            resetForm(formName) {
                this.$refs[formName].resetFields();
            },
            /**
             * @description: 当级联更改时，同步parnetId的值
             * @param {*} v
             * @return {*}
             */
            ChangeparnetId(v) {
                this.ruleForm.parnetId = v.slice(-1)[0];
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
            },
            /**
             * @description: 更新拼音
             * @return {*}
             */
            UpdateEnName() {
                this.ruleForm.menuNameEn = pinyin.getFullChars(this.ruleForm.menuName).toLowerCase();
            }
        },
    };
</script>

<style>
    .demo-ruleForm label.el-form-item__label {
        font-weight: normal;
    }
</style>