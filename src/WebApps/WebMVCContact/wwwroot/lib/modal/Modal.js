var Modal = function () {

    this.html = function (obj) {

        var modal = '<div class="modal fade" id="'+obj.modalId+'">' +
                        '<div class="modal-dialog">' +
                            '<div class="modal-content">' +
                                '<div class="modal-header">' +
                                    '<h4 class="modal-title">' +obj.title+'</h4>' +
                                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                                    '<span aria-hidden="true">&times;</span>' +
                                    '</button>' +
                                '</div>' +
                                '<form action="'+obj.action+'" asp-antiforgery="true" method="post" id="asdf">' +
                                    '<div class="modal-body">' +
                                        '<p>' +obj.body+'</p>' +
                                        '<select hidden multiple="multiple" name="ContactIds" id="contactIds"></select>' +
                                    '</div>' +
                                    '<div class="modal-footer justify-content-between">' +
                                        '<button type="button" class="btn btn-default" data-dismiss="modal">asdf</button>' +
                                        '<button type="submit" class="btn btn-primary">Submit</button>' +
                                        //'<button type="button" onclick="DeleteContact()" class="btn btn-primary">asdf</button>' +
                                    '</div>' +
                                '</form>' +
                            '</div>' +
                        '</div>' +
                    '</div>';

        $("body").append(modal);
    }
}