angular.module('ozViewer')
    .component('ozhtml5', {
        templateUrl: 'custom_components/oz-viewer/mobile/oz-viewer.template.html',
        controller: ['$scope', '$log', '$http', '$window', '$stateParams', '$uibModal', 'cfpLoadingBar', 'toastr', ozhtml5viewerCtrl],
        bindings: {
            ozActions: '<',
            ozParams: '<',
            ozEvents: '<',

            // for mail detail
            mailTitle: '<',
            images: '<',
            emailTemplate: '<',

            successMessage: '<',
            ozCustomParams: '<'
        }
    });