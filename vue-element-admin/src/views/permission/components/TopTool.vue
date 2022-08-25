<template>
    <div class="font12 tool">
        <div class="floatleft">
            <slot></slot>
        </div>
        <div class="floatright">
            <i class="el-icon-refresh"></i>刷新
        </div>
    </div>
</template>

<script>
export default {
    name: 'VueElementAdminTopTool',

    data() {
        return {
            
        };
    },

    props:{
        title: {
            type: String,
            default: '默认',
        },
    },

    mounted() {
        
    },

    methods: {
        
    },
};
</script>

<style scoped>
.tool{
    width: 100%;
    height: 60px;
    line-height: 60px;
    margin-bottom: 30px;
    border-bottom: 1px solid #ccc;
}
.tool .floatleft{
    margin-left: 16px;
}
.tool .floatright{
    margin-right: 16px;
}
</style>