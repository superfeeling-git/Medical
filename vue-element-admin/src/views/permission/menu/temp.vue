<template>
    <div>
        <div v-if="Show" id="divele">默认文本</div>
        <button @click="changeText">Click</button>
    </div>
</template>

<script>
    export default {
        name: 'VueElementAdminTemp',

        data() {
            return {
                Show: false
            };
        },

        mounted() {
            
        },

        methods: {
            changeText() {
                this.Show = true;
                console.log(document.getElementById('divele').innerHTML);
            }
        },
    };
</script>