new Vue({
    el: '#idElementa',
    data() {
        return {
            usr: [],
            isViewReady: false
        };
    },
    methods: {
        refreshData: function () {
            var self = this;
            this.isViewReady = false;

            axios.get('https://localhost:5001/api/User')
                .then(function (response) {
                    self.usr = response.data;
                    self.isViewReady = true;
                })
                .catch(function (error) {
                    alert("ERROR: " + (error.message | error));
                });
        }
    },
    created: function () {
        this.refreshData();
    }
});