const routes=[
    {
        path: '/home', component:home
    },
    {
        path: '/company', component:company
    },
    {
        path: '/access', component:access
    }
]

const router = new VueRouter({
    routes
})

const app = new Vue({
    router
}).$mount('#app')