var group = (function () {

    function groupInit() {

        var selectedRows = [];

        var localizer = {};
        this.localization = function (obj) {
            localizer = obj;
        }

        hasClaim = {};
        this.claims = function (obj) {
            hasClaim = obj;
        }

        this.treeTableInit = function (data) {
            var html = this.buildList(data);
            $("#groupsTable tbody").html(html);
            $("#groupsTable").treetable();
        }

        this.buildList = function (data) {
            var html = '';
            for (item in data) {
                var parentId = data[item].ParentId == null ? "" : data[item].ParentId;
                var description = data[item].Description == null ? "" : data[item].Description;

                if (typeof (data[item].Children) === 'object') { // An array will return 'object'

                    html += '<tr><td style="display:none;">' + data[item].Id + '</td><td><div class="tt" data-tt-id="' + data[item].Id + '" data-tt-parent="' + parentId + '">' + data[item].GroupName + '</div></td><td>' + description + '</td><td>' + data[item].NumberOfForms + '</td><td>' + (hasClaim.update != "True" ? '' : '<a href="/FormGroup/Update?id=' + data[item].Id + '" class="btn btn-xs btn-default">' + localizer.update + '</a>') + ' <a href="#" class="btn btn-xs btn-default">' + localizer.details + '</a></td></tr>';
                    html += this.buildList(data[item].Children); // Submenu found. Calling recursively same method
                } else {
                    html += '<tr><td style="display:none;">' + data[item].Id + '</td><td><div class="tt" data-tt-id="' + data[item].Id + '" data-tt-parent="' + parentId + '">' + data[item].GroupName + '</div></td><td>' + description + '</td><td>' + data[item].NumberOfForms + '</td><td>' + (hasClaim.update != "True" ? '' : '<a href="/FormGroup/Update?id=' + data[item].Id + '" class="btn btn-xs btn-default">' + localizer.update + '</a>') + ' <a href="#" class="btn btn-xs btn-default">' + localizer.details + '</a></td></tr>';
                }
               
            }
            return html;
        }

        $("#btnDelete").click(function () {
            if (selectedRows.length > 0) {
                $('#deleteGroupsModal').modal('show');
            } else {
                toastr.warning(localizer.toastr.selectAtLeastOneGroupMessage);
            }
        });

        //select row in table
        $(document).on("click", "#groupsTable td", function (e) {
            var cellIndex = $(this).index();
            var elementName = e.target.nodeName;

            if (elementName == 'DIV' || cellIndex == 4)
                return;

            //get row of td that was clicked
            var row = $(this).closest('tr');

            var id = row.find('td:first').html();
            var index = $.inArray(id, selectedRows);

            if (index === -1) {
                selectedRows.push(id);
            } else {
                selectedRows.splice(index, 1);
            }

            row.toggleClass('selected');
        });

        $(".getGroupsBeforeSubmit").submit(function () {
            if (selectedRows.length > 0) {
                for (var i = 0; i < selectedRows.length; i++) {
                    $(this).append(`<input type="hidden" name="GroupIds" value="${selectedRows[i]}" /> `);
                }
                return true;
            } else {
                toastr.warning(localizer.toastr.selectAtLeastOneGroupMessage);
                return false;
            }

        });

    } return new groupInit();

})();

