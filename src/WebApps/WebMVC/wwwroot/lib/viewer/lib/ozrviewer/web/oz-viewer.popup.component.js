function PopUpEmailTargetCtrl($scope, $log, authService) {
    authService.applySession($scope);

    $scope.input = {};
    var $ctrl = this;

    $ctrl.$onInit = function () {
        $scope.input.type = $ctrl.resolve.defaultOption ? $ctrl.resolve.defaultOption : 'formEmail';

        if($ctrl.resolve.options) {
            $ctrl.resolve.options.forEach(function(action) {
                if(action.includes('-')) {
                    var actionParts = action.split('-');
                    $scope["show" + actionParts[0]] = true;
                    $scope["text" + actionParts[0]] = actionParts[1];
                } else {
                    $scope["show" + action] = true;
                }
            });
        }

        $scope.input.email = $ctrl.resolve.email ? $ctrl.resolve.email : $scope.userEmail;
    };

    $ctrl.ok = function () {
        $ctrl.close({$value: $scope.input});
        $log.debug("Scope.data ---------" + $scope.input);
    };

}

angular
    .module('ozViewer')
    .component('multiEmailInputType', {
        templateUrl: 'custom_components/oz-viewer/web/oz-viewer.popup.2.html',
        controller: ['$scope', '$log', 'authService', PopUpEmailTargetCtrl],
        bindings: {
            resolve: '<',
            close: '&',
            dismiss: '&'
        }
    });