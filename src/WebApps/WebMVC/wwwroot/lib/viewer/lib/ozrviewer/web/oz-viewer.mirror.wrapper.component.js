angular
    .module('ozViewer')
    .component('mirrorViewer', {
        templateUrl: 'custom_components/oz-viewer/web/oz-viewer.wrapper.template.html',
        controller: 'webWrapperCtrl',
        bindings: {
            ozActions     : '<',
            ozPopupOptions: '<?',
            ozPopupDefault: '<?',

            ozEvents         : '<?',
            ozParams         : '<',
            ozClientParams   : '<',
            ozCustomParams   : '<?',
            ozOpt            : '<?',

            mailTitle    : '<?',
            images       : '<?',
            emailTemplate: '<?',
            confirmState : '<?',

            viewTitle: '<?',

            viewerPage: '<',
            viewerState: '<'
        }
    });