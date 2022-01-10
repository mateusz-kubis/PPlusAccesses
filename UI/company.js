const company={
    template:`
    <table class="table table-striped">
    <thead>
        <tr>
            <th>
                CompanyId
            </th>
            <th>
                CompanyName
            </th>
            <th>
                CompanyDescription
            </th>
        </tr>
        <tbody>
            <tr v-for="com in companies">
                <td>{{com.id}}</td>
                <td>{{com.companyName}}</td>
                <td>{{com.companyDescription}}</td>
            </tr>
        </tbody>
    </thead>
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