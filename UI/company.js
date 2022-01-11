const company={
    template:`
    <table class="table table-striped">
    <thead>
        <tr>
            <th>
                Id
            </th>
            <th>
                Akronim
            </th>
            <th>
                Nazwa
            </th>
        </tr>
        </thead>
        <tbody>
            <tr v-for="com in companies">
                <td>{{com.id}}</td>
                <td><a href="">{{com.companyName}}</a></td>
                <td>{{com.companyDescription}}</td>
            </tr>
        </tbody>
    </table>
    `,

    data(){
        return{
            companies:[]
        }
    },
    methods:{
        refreshData(){
            axios.get(variables.API_URL+"company/getall")
            .then((response) =>{
                this.companies=response.data;
            })
        }
    },
    mounted:function(){
        this.refreshData();
    }
}