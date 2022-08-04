
$(function () {
    //MODAL POPUP BEGIN
    var placeHolderModal = $("#place-holder-modal");
    var placeHolderInsideModal = $("#place-holder-inside-modal");

    $(document).on("click", 'button[data-toggle="ajax-modal"]', function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done((data) => {
            placeHolderModal.html(data);
            placeHolderModal.find('.modal').modal('show');
            formValidators();
        });
    });

    placeHolderModal.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');

        var actionUrl = form.attr('action');
        var sendData = form.serialize();

        $.post(actionUrl, sendData).done((data) => {
            if (form.valid()) {
                placeHolderModal.find('.modal').modal('hide');
                location.reload(true);
            }
        });
    });

    // INSIDE MODAL POPUP BEGIN
    $(document).on("click", 'button[data-toggle="ajax-modal-inside"]', function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done((data) => {
            placeHolderInsideModal.html(data);
            placeHolderInsideModal.find('.modal').modal('show');
            placeHolderModal.find('.modal').modal('hide');
            formValidators();
        });
    });

    placeHolderInsideModal.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done((data) => {
            if (form.valid()) {
                placeHolderInsideModal.find('.modal').modal('hide');
                location.reload(true);
                //$('button[data-target="#detailPermits"]').click();
            }
        });
    });
    // INSIDE MODAL POPUP BEGIN
    //MODAL POPUP END

    function formValidators() {

        const REQUIRED_MESSAGE = "Gerekli Alan";
        const NOW_DATE = "NOW_DATE"

        // Create Employee Form
        var createEmployeeForm = $("#create-employee-form");
        if (createEmployeeForm.length) {
            createEmployeeForm.validate({
                rules: {
                    "FullName": {
                        required: true
                    },
                    "BirthDate": {
                        required: true,
                        lowerOrEqualThan: NOW_DATE
                    },
                    "Department": {
                        required: true
                    },
                    "HireDate": {
                        required: true
                    }
                },
                messages: {
                    "FullName": {
                        required: REQUIRED_MESSAGE
                    },
                    "BirthDate": {
                        required: REQUIRED_MESSAGE,
                        lowerOrEqualThan: "Bugününün Tarihinden Büyük Olamaz"
                    },
                    "Department": {
                        required: REQUIRED_MESSAGE
                    },
                    "HireDate": {
                        required: REQUIRED_MESSAGE
                    }
                }
            })
        }
        // End of Create Employee Form

        // Edit Employee Form
        var editEmployeeForm = $("#edit-employee-form");
        if (editEmployeeForm.length) {
            editEmployeeForm.validate({
                rules: {
                    "FullName": {
                        required: true
                    },
                    "BirthDate": {
                        required: true
                    },
                    "Department": {
                        required: true
                    },
                    "HireDate": {
                        required: true
                    }
                },
                messages: {
                    "FullName": {
                        required: REQUIRED_MESSAGE
                    },
                    "BirthDate": {
                        required: REQUIRED_MESSAGE
                    },
                    "Department": {
                        required: REQUIRED_MESSAGE
                    },
                    "HireDate": {
                        required: REQUIRED_MESSAGE
                    }
                }
            })
        }
        // End of Edit Employee Form

        // Create Permit Form
        var createPermitForm = $("#create-permit-form");
        if (createPermitForm.length) {
            createPermitForm.validate({
                rules: {
                    "EmployeeId": {
                        required: true
                    },
                    "StartDate": {
                        required: true
                    },
                    "EndDate": {
                        required: true,
                        greaterOrEqualThan: "#StartDate"
                    },
                    "Type": {
                        required: true
                    }
                },
                messages: {
                    "EmployeeId": {
                        required: REQUIRED_MESSAGE
                    },
                    "StartDate": {
                        required: REQUIRED_MESSAGE
                    },
                    "EndDate": {
                        required: REQUIRED_MESSAGE,
                        greaterOrEqualThan: "Başlangıç Tarihinden Küçük Olamaz"
                    },
                    "Type": {
                        required: REQUIRED_MESSAGE
                    }
                }
            })
        }
        // End of Create Permit Form

        // Edit Permit Form
        var editPermitForm = $("#edit-permit-form");
        if (editPermitForm.length) {
            editPermitForm.validate({
                rules: {
                    "StartDate": {
                        required: true
                    },
                    "EndDate": {
                        required: true,
                        greaterOrEqualThan: "#StartDate"
                    },
                    "Type": {
                        required: true
                    }
                },
                messages: {
                    "StartDate": {
                        required: REQUIRED_MESSAGE
                    },
                    "EndDate": {
                        required: REQUIRED_MESSAGE,
                        greaterOrEqualThan: "Başlangıç Tarihinden Küçük Olamaz"
                    },
                    "Type": {
                        required: REQUIRED_MESSAGE
                    }
                }
            })
        }
        // End of Edit Permit Form

        // Create Report  Form
        var createReportForm = $("#create-report-form");
        if (createReportForm.length) {
            createReportForm.validate({
                rules: {
                    "ReportDate": {
                        required: true,
                        lowerOrEqualThan: NOW_DATE
                    },
                    "ProjectName": {
                        required: true
                    },
                    "ProjectType": {
                        required: true
                    },
                    "Description": {
                        required: true
                    },
                    "Percentage": {
                        required: true,
                        number: true,
                        max: 100,
                        min: 0
                    }
                },
                messages: {
                    "ReportDate": {
                        required: REQUIRED_MESSAGE,
                        lowerOrEqualThan: "Bugününün Tarihinden Büyük Olamaz"
                    },
                    "ProjectName": {
                        required: REQUIRED_MESSAGE
                    },
                    "ProjectType": {
                        required: REQUIRED_MESSAGE
                    },
                    "Description": {
                        required: REQUIRED_MESSAGE
                    },
                    "Percentage": {
                        required: REQUIRED_MESSAGE,
                        number: "Sadece Sayı Değeri Girilebilir",
                        max: "En Fazla 100 Değeri Girilebilir",
                        min: "En Az 0 Değeri Girilebilir"
                    }
                }
            })
        }
        // End of Create Report Form

        // Edit Report Form
        var editReportForm = $("#edit-report-form");
        if (editReportForm.length) {
            editReportForm.validate({
                rules: {
                    "ReportDate": {
                        required: true
                    },
                    "ProjectName": {
                        required: true
                    },
                    "ProjectType": {
                        required: true
                    },
                    "Description": {
                        required: true
                    },
                    "Percentage": {
                        required: true,
                        number: true,
                        max: 100,
                        min: 0
                    }
                },
                messages: {
                    "ReportDate": {
                        required: REQUIRED_MESSAGE
                    },
                    "ProjectName": {
                        required: REQUIRED_MESSAGE
                    },
                    "ProjectType": {
                        required: REQUIRED_MESSAGE
                    },
                    "Description": {
                        required: REQUIRED_MESSAGE
                    },
                    "Percentage": {
                        required: REQUIRED_MESSAGE,
                        number: "Sadece Sayı Değeri Girilebilir",
                        max: "En Fazla 100 Değeri Girilebilir",
                        min: "En Az 0 Değeri Girilebilir"
                    }
                }
            })
        }
        // End of Edit Report Form
    }

    // CUSTOM VALIDATIONS
    $.validator.addMethod(
        "greaterOrEqualThan",
        function (value, element, params) {
            var target = "";
            if (params == "NOW_DATE") {
                target = formatDate(new Date(Date.now()));
            } else {
                target = $(params).val();
            }
            var isValueNumeric = !isNaN(parseFloat(value)) && isFinite(value);
            var isTargetNumeric = !isNaN(parseFloat(target)) && isFinite(target);
            if (isValueNumeric && isTargetNumeric) {
                return Number(value) >= Number(target);
            }

            if (!/Invalid|NaN/.test(new Date(value))) {
                return new Date(value) >= new Date(target);
            }

            return false;
        },
        'Must be greater than {0}.');

    $.validator.addMethod(
        "lowerOrEqualThan",
        function (value, element, params) {
            var target = "";
            if (params == "NOW_DATE") {
                target = formatDate(new Date(Date.now()));
            } else {
                target = $(params).val();
            }
            var isValueNumeric = !isNaN(parseFloat(value)) && isFinite(value);
            var isTargetNumeric = !isNaN(parseFloat(target)) && isFinite(target);
            if (isValueNumeric && isTargetNumeric) {
                return Number(value) <= Number(target);
            }

            if (!/Invalid|NaN/.test(new Date(value))) {
                return new Date(value) <= new Date(target);
            }

            return false;
        },
        'Must be lower than {0}.');
    // END OF CUSTOM VALIDATIONS

});