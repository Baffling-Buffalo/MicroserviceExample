function ozMirrorConfig($stateProvider) {
    $stateProvider
        .state('mirror', {
            url: "/mirror",
            component: 'iFrameMirrorViewer'
        })
}
ozMirrorConfig.$inject = ['$stateProvider'];

angular.module('ozViewer')
    .config(ozMirrorConfig);