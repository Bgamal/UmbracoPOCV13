angular.module("umbraco").controller("OpenAiContentGeneratorController", function($scope, $http) {
    var vm = this;
    vm.prompt = "";
    vm.result = "";
    vm.loading = false;

    vm.generate = function() {
        vm.loading = true;
        $http.post("/umbraco/api/OpenAiContent/Generate", { prompt: vm.prompt })
            .then(function(response) {
                vm.result = response.data;
            })
            .finally(function() {
                vm.loading = false;
            });
    };
}); 