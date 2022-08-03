function getAllUsersReportsByDate(date) {
    $(function () {
        $.ajax({
            url: "GetAllUsersReports",
            type: 'Get',
            dataType: 'JSON',
            success: function (reports) {
                var jsonReports = jQuery.parseJSON(reports);

                var filteredReports = [];

                jsonReports.forEach(report => {
                    if (report.ReportDate == date + "T00:00:00") {
                        filteredReports.push(report);
                    }
                });

                var tbody = $("#filtered-data");
                tbody.empty();

                var row = '';
                if (filteredReports.length > 0) {
                    filteredReports.forEach(report => {

                        var projectName = getProjectName(report);
                        var projectType = getProjectType(report);
                        var lateEntryBg = ''
                        if (report.IsLateEntry) {
                            lateEntryBg = "#f15e6c";
                        }
                        row = `<tr style="background:${lateEntryBg};">
                             <td>
                                ${report.OwnerFullName}
                            </td>
                            <td>
                                ${projectName}
                            </td>
                            <td>
                                ${projectType}
                            </td>
                            <td>
                                ${report.Description}
                            </td>
                            <td class="text-center">
                                ${report.Percentage}
                            </td>

                        </tr>`
                        tbody.append(row);
                    })
                } else {
                    row = "<span class='text-danger'>Veri yok</span>"
                    tbody.append(row);
                }

            }
        });
    });
}

function getCurrentUserReportsByDate(date) {
    $(function () {
        $.ajax({
            url: "GetUserReports",
            type: 'Get',
            dataType: 'JSON',
            success: function (reports) {
                var jsonReports = jQuery.parseJSON(reports);

                var filteredReports = [];

                jsonReports.forEach(report => {
                    if (report.ReportDate == date + "T00:00:00") {
                        filteredReports.push(report);
                    }
                });

                var tbody = $("#filtered-data");
                tbody.empty();
                var row = '';
                if (filteredReports.length > 0) {
                    filteredReports.forEach(report => {

                        var projectName = getProjectName(report);
                        var projectType = getProjectType(report);
                        row = `<tr>
                                    <td>
                                        ${projectName}
                                    </td>
                                    <td>
                                        ${projectType}
                                    </td>
                                    <td>
                                        ${report.Description}
                                    </td>
                                    <td class="text-center">
                                        ${report.Percentage}
                                    </td>
                                    <td class="text-right text-nowrap">
                                        <button type="button" class="btn btn-primary" data-toggle="ajax-modal" data-target="#editReport"
                                            data-url="/Report/Edit/${report.Id}">
                                            <i class="bi bi-pencil-square"></i>
                                        </button>
                                        <button type="button" class="btn btn-danger" data-toggle="ajax-modal" data-target="#deleteReport"
                                            data-url="/Report/Delete/${report.Id}">
                                            <i class="bi bi-trash3"></i>
                                        </button>
                                    </td>
                                </tr>`
                        tbody.append(row);
                    })
                } else {
                    row = "<span class='text-danger'>Veri yok</span>"
                    tbody.append(row);
                }

            }
        });
    });
}

function getProjectName(report) {
    var projectName = "";
    switch (report.ProjectName) {
        case 1:
            projectName = "Dalisto"
            break;
        case 2:
            projectName = "İnsan Kaynakları"
            break;
        case 3:
            projectName = "ContentManage"
            break;
        default:
            projectName = "Assign Enum Value on javascript code"
            break;
    }
    return projectName;
}

function getProjectType(report) {
    var projectType = "";
    switch (report.ProjectType) {
        case 1:
            projectType = "Web Dizayn"
            break;
        case 2:
            projectType = "Web Uygulaması"
            break;
        case 3:
            projectType = "Mobil Uygulama"
            break;
        default:
            projectType = "Assign Enum Value on javascript code"
            break;
    }
    return projectType;
}