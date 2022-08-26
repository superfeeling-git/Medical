<template>
    <div>
        <el-container class="container">
            <el-aside class="aside" width="200">
                <h1 class="title">病区选择</h1>
                <div class="search">
                    <el-input placeholder="请输入内容" prefix-icon="el-icon-search" size="mini"></el-input>
                </div>
                <el-tree :default-expand-all="true" :data="data" :props="defaultProps" @node-click="handleNodeClick">
                </el-tree>
            </el-aside>
            <el-main class="main">
                <div class="title">
                    （普通内科-消化内科-十一病区）病房设置
                    <el-button disabled size="mini">默认按钮</el-button>
                </div>
                <div>
                    <ul class="room">
                        <li>
                            <div class="roomNum">201病房</div>
                            <div class="bed">
                                <button v-for="(item, index) in 10" :key="index">
                                    {{item}}床
                                </button>
                            </div>
                            <div class="btn">
                                <i class="el-icon-edit"></i>
                                <i class="el-icon-delete"></i>
                            </div>
                        </li>
                        <li>
                            <div class="roomNum">201病房</div>
                            <div class="bed">
                                <button v-for="(item, index) in 10" :key="index">
                                    {{item}}床
                                </button>
                            </div>
                            <div class="btn">
                                <i class="el-icon-edit"></i>
                                <i class="el-icon-delete"></i>
                            </div>
                        </li>
                        <li>
                            <div class="roomNum">201病房</div>
                            <div class="bed">
                                <button v-for="(item, index) in 10" :key="index">
                                    {{item}}床
                                </button>
                            </div>
                            <div class="btn">
                                <i class="el-icon-edit"></i>
                                <i class="el-icon-delete"></i>
                            </div>
                        </li>
                        <li>
                            <div class="roomNum">201病房</div>
                            <div class="bed">
                                <button v-for="(item, index) in 10" :key="index">
                                    {{item}}床
                                </button>
                            </div>
                            <div class="btn">
                                <i class="el-icon-edit"></i>
                                <i class="el-icon-delete"></i>
                            </div>
                        </li>
                        <li>
                            <div class="roomNum">201病房</div>
                            <div class="bed">
                                <button v-for="(item, index) in 10" :key="index">
                                    {{item}}床
                                </button>
                            </div>
                            <div class="btn">
                                <i class="el-icon-edit"></i>
                                <i class="el-icon-delete"></i>
                            </div>
                        </li>
                    </ul>
                </div>
            </el-main>
        </el-container>
    </div>
</template>

<script>
    export default {
        name: 'VueElementAdminBedList',

        data() {
            return {
                data: [{
                    label: '一级 1',
                    children: [{
                        label: '二级 1-1',
                        children: [{
                            label: '三级 1-1-1'
                        }]
                    }]
                }, {
                    label: '一级 2',
                    children: [{
                        label: '二级 2-1',
                        children: [{
                            label: '三级 2-1-1'
                        }]
                    }, {
                        label: '二级 2-2',
                        children: [{
                            label: '三级 2-2-1'
                        }]
                    }]
                }, {
                    label: '一级 3',
                    children: [{
                        label: '二级 3-1',
                        children: [{
                            label: '三级 3-1-1'
                        }]
                    }, {
                        label: '二级 3-2',
                        children: [{
                            label: '三级 3-2-1'
                        }]
                    }]
                }],
                defaultProps: {
                    children: 'children',
                    label: 'label'
                }
            };
        },

        mounted() {

        },

        methods: {
            handleNodeClick(data) {
                console.log(data);
            }
        },
    };
</script>

<style scoped>
    .container {
        background: rgb(163, 226, 232);
        padding: 16px;
    }

    .aside,
    .main {
        background: #fff;
    }

    .main {
        height: calc(100vh - 116px);
    }

    .aside {
        margin-right: 16px;
        margin-bottom: 0;
        padding: 0;
    }

    h1.title,
    div.title {
        font-size: 14px;
        text-indent: 21px;
        border-bottom: 1px solid #ccc;
    }

    div.title {
        font-size: 12px;
        height: 20px;
        text-indent: 0;
        font-weight: bold;
        position: relative;
    }

    div.title button {
        position: absolute;
        right: 8px;
        top: -13px;
    }

    .search {
        width: 80%;
        margin: 0 auto;
    }

    .el-tree {
        margin-left: 10px;
        margin-top: 16px;
    }

    ul.room,
    ul.room li {
        margin: 0;
        padding: 0;
        margin-top: 21px;
        list-style-type: none;
    }

    ul.room li {
        border: 1px solid rgb(226, 226, 226);
        min-height: 120px;
    }

    ul.room li div {
        float: left;
    }

    ul.room li div.roomNum {
        width: 14%;
        background: #f0f3f7;
        text-align: center;
        font-weight: bold;
        font-size: 12px;
        color: #595959;
        line-height: 120px;
    }


    ul.room li div.bed {
        width: 74%;
        padding-top: 16px;
        padding-left: 16px;
        height: 120px;
        border-left: 1px solid rgb(226, 226, 226);
        border-right: 1px solid rgb(226, 226, 226);
    }

    ul.room li div.bed button {
        width: 79px;
        height: 32px;
        text-align: center;
        background: #fff;
        border: 1px solid rgb(134, 134, 134);
        border-radius: 3px;
        margin-right: 12px;
        margin-bottom: 16px;
        font-size: 12px;
        color: #707070;
    }

    ul.room li div.btn{
        padding-left: 12px;
        text-align: center;
        line-height: 120px;
    }

    ul.room li div.btn i{
        margin-left: 18px;
        color: #3f3f3f;
    }

    ul.room li div.btn i:first{
        margin-left: 21px;
    }
</style>