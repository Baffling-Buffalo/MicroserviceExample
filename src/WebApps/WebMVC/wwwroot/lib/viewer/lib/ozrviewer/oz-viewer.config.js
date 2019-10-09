function ozConfig($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        debug: true,
        modules: [{
            name: 'ozTotoFramework',
            files: [
                'custom_components/oz-viewer/mobile/oztotoframework.js'
            ]
        }, {
            name: 'ozHTML5JS',
            files: [
                'custom_components/oz-viewer/ozhtml5/jquery.dynatree.js',
                'custom_components/oz-viewer/ozhtml5/OZJSViewer.js',
                'custom_components/oz-viewer/ozhtml5/jQuery-FontSpy.js'
            ]
        }]
    });
}

angular.module('ozViewer')
    .config(ozConfig);