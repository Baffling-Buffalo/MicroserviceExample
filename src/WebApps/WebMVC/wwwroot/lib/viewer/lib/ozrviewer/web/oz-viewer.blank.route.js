function ozViewerBlankConfig($stateProvider) {
    $stateProvider
        .state('ozHTML5', {
            url: "/ozhtml5",
            component: 'iFrameViewer',
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load('ozHTML5JS', {
                        insertBefore: '#loadBefore',
                        timeout: 300000
                    });
                }
            }
        })
        .state('ozHTML5Mirror', {
            url: "/mirror",
            component: 'iFrameViewer',
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load('ozHTML5JS', {
                        insertBefore: '#loadBefore',
                        timeout: 300000
                    });
                }
            }
        })
}
ozViewerBlankConfig.$inject = ['$stateProvider'];

angular.module('ozViewer')
    .config(ozViewerBlankConfig);