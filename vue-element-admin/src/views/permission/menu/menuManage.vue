<template>
    <div>
        <top-tool></top-tool>
        <el-form :inline="true" class="demo-form-inline">
            <el-form-item>
                <el-button type="primary" @click="AddMenu">添加</el-button>
            </el-form-item>
        </el-form>
        <el-table :data="tableData" style="width: 100%;margin-bottom: 20px;" row-key="id" border default-expand-all
            :tree-props="{children: 'children', hasChildren: 'hasChildren'}">
            <el-table-column prop="menuName" label="菜单名单" sortable width="180">
            </el-table-column>
            <el-table-column prop="menuNameEn" label="菜单name" sortable width="180">
            </el-table-column>
            <el-table-column prop="menuPath" label="菜单链接">
            </el-table-column>
            <el-table-column prop="componentPath" label="组件路径">
            </el-table-column>
            <el-table-column prop="isShow" label="是否显示" :formatter="formatStatus">
            </el-table-column>
            <el-table-column label="操作">
                <template slot-scope="scope">
                    <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                    <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-dialog width="40%" :destroy-on-close="true" title="菜单添加" :visible.sync="dialogTableVisible.Add">
            <menu-add ref="digMenuAdd" @Refresh="Refresh"></menu-add>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogTableVisible.Add = false">取 消</el-button>
                <el-button type="primary" @click="SaveAdd">确 定</el-button>
            </div>
        </el-dialog>
        <el-dialog width="40%" :destroy-on-close="true" title="菜单编辑" :visible.sync="dialogTableVisible.Edit">
            <menu-edit ref="digMenuEdit" @Refresh="Refresh"></menu-edit>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogTableVisible.Edit = false">取 消</el-button>
                <el-button type="primary" @click="SaveEdit">确 定</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
    import menuAdd from './menuAdd';
    import menuEdit from './menuEdit';
    import TopTool from '../components/TopTool';
    import { getTable } from "@/api/menu";
    export default {
        name: 'VueElementAdminMenu',
        components: {
            menuAdd,
            menuEdit,
            TopTool
        },
        data() {
            return {
                dialogTableVisible: {
                    Add: false,
                    Edit: false
                },
                tableData: [],
            };
        },

        mounted() {
            this.LoadData();
        },

        methods: {
            /**
             * @description: 添加菜单
             * @return {*}
             */
            AddMenu() {
                this.dialogTableVisible.Add = true;
            },
            /**
             * @description: 保存添加
             * @return {*}
             */
            SaveAdd() {
                this.$refs.digMenuAdd.submitForm('ruleForm');
            },
            /**
             * @description: 刷新菜单
             * @return {*}
             */
            Refresh() {
                this.dialogTableVisible.Add = false;
                this.LoadData();
            },
            /**
             * @description: 
             * @return {*}
             */
            LoadData() {
                getTable().then((res) => {
                    this.tableData = res.data;
                }).catch((err) => {

                });
            },
            /**
             * @description: 格式化
             * @return {*}
             */
            formatStatus(row, column, cellValue, index) {
                return cellValue ? "显示" : "隐藏";
            },
            handleEdit(index, row) {
                this.dialogTableVisible.Edit = true;

                this.$nextTick(function () {
                    this.$refs.digMenuEdit.initData(row.id);
                });
            },
            handleDelete(index, row) {
                console.log(index, row);
            },
            /**
             * @description: 保存编辑
             * @return {*}
             */
            SaveEdit() {
                this.$refs.digMenuEdit.$refs['ruleForm'].validate((valid) => {
                    if (valid) {                        
                        console.log(this.$refs.digMenuEdit.ruleForm);
                        //调接口
                    } else {
                        console.log('error submit!!');
                        return false;
                    }
                });
            }
        },
    };
</script>