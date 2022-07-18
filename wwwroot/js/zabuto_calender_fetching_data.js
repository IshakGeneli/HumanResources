
function getDatesBetweenTwoDates(startDateString, endDateString) {
    var startDate = new Date(startDateString);
    var endDate = new Date(endDateString);
    var dateList = [];
    for (var dt = startDate; dt <= endDate; dt.setDate(dt.getDate() + 1)) {
        dateList.push(new Date(dt));
    }
    return dateList;
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

$(function () {
    $.ajax({
        url: 'Permit/GetEmployeesWithPermits',
        type: 'Get',
        success: function (employees) {
            
            var jsonEmployees = jQuery.parseJSON(employees);

            $.each(jsonEmployees,
                (index, employee) => {

                    var permits = [];

                    employee.Permits.forEach((permit) => {
                        var dateList = getDatesBetweenTwoDates(permit.StartDate.slice(0, -9), permit.EndDate.slice(0, -9));

                        var hasBadge = false;

                        switch (permit.Type) {
                            case 2: // permit.Type = 2 (Enum = PermitType: Unexcused = 2) Mazeretsiz ise 2 
                                hasBadge = true;
                            default:
                        }

                        for (var i = 0; i < dateList.length; i++) {
                            permits.push({ date: formatDate(dateList[i]), badge: hasBadge });
                        }
                    });


                    $("#my-calendar-" + employee.Id).zabuto_calendar({
                        language: "tr",
                        cell_border: true,
                        //nav_icon: {
                        //    prev: '<i class="fa fa-chevron-circle-left"></i>',
                        //    next: '<i class="fa fa-chevron-circle-right"></i>'
                        //},
                        show_previous: false,
                        show_next: false,
                        legend: [
                            { type: "text", label: "Mazeretsiz", badge: "00" },
                            { type: "block", label: "Gelmediği Günler" }
                        ],
                        data: permits
                    });

                });
        }
    });
});