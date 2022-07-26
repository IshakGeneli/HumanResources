$(function () {

    //$('#month-filter').on('change', function () {
    //    zabutoConfig(parseInt(this.value) + 1, 2022)

    //});

    //$('#year-filter').on('change', function () {
    //    alert(this);
    //});

    zabutoConfig(new Date().getUTCMonth() + 1, new Date().getFullYear());

    function zabutoConfig(month, year) {

        $.ajax({
            url: 'Permit/GetEmployeesWithPermits',
            type: 'Get',
            dataType: 'JSON',
            data: { month: month, year: year },
            success: function (employees) {
                var jsonEmployees = jQuery.parseJSON(employees);

                $.each(jsonEmployees,
                    (index, employee) => {

                        var permits = [];

                        employee.Permits.forEach((permit) => {
                            var dateList = getDatesBetweenTwoDates(permit.StartDate.slice(0, -9), permit.EndDate.slice(0, -9));
                            var weekDays = getWeekDays(dateList);
                            var hasBadge = false;

                            switch (permit.Type) {
                                case 2: // permit.Type = 2 (Enum = PermitType: Unexcused = 2) Mazeretsiz ise 2 
                                    hasBadge = true;
                                default:
                            }

                            for (var i = 0; i < weekDays.length; i++) {
                                permits.push({ date: formatDate(weekDays[i]), badge: hasBadge });
                            }

                        });

                        console.log(employee.Month)

                        var a = $("#my-calendar-" + employee.Id).zabuto_calendar({
                            language: "tr",
                            cell_border: true,
                            year: employee.Year,
                            month: employee.Month,
                            nav_icon: {
                                prev: '<i class="fa fa-chevron-circle-left"></i>',
                                next: '<i class="fa fa-chevron-circle-right"></i>'
                            },
                            show_previous: true,
                            show_next: true,
                            legend: [
                                { type: "text", label: "Mazeretsiz", badge: "00" },
                                { type: "block", label: "Gelmediği Günler" }
                            ],
                            data: permits,
                        });

                        console.log(a)

                    });
            }
        });
    }
});