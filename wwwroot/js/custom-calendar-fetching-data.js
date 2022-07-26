$(function () {

    $('#month-filter').on('change', function () {

        var monthFilterValue = parseInt(this.value);
        var yearFilterValue = $('#year-filter').val();

        if (monthFilterValue == null) {
            monthFilterValue = new Date().getMonth() + 1;
        } else {
            monthFilterValue++;
        }

        if (yearFilterValue == null) {
            yearFilterValue = new Date().getFullYear();
        }

        calendarConfig(monthFilterValue, yearFilterValue);
    });

    $('#year-filter').on('change', function () {

        var monthFilterValue = $('#month-filter').val();
        var yearFilterValue = this.value;

        if (monthFilterValue == null) {
            monthFilterValue = new Date().getMonth() + 1;
        } else {
            monthFilterValue++;
        }

        if (yearFilterValue == null) {
            yearFilterValue = new Date().getFullYear();
        }

        calendarConfig(monthFilterValue, yearFilterValue);
    });

    calendarConfig(new Date().getUTCMonth() + 1, new Date().getFullYear());

    function calendarConfig(month, year) {

        const LEGENDS = [
            { type: "warning", text: "Mazeretli" },
            { type: "danger", text: "Mazeretsiz" },
            { type: "primary", text: "İzinli" }
        ]

        const LANGUAGE = "tr";

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
                            var status = "";

                            switch (permit.Type) { // see PermitType enum
                                case 1:
                                    status = "warning"
                                    break;
                                case 2: // permit.Type = 2 (Enum = PermitType: Unexcused = 2) Mazeretsiz ise 2
                                    status = "danger";
                                    break;
                                case 3: // permit.Type = 3 (Enum = PermitType: OnLeave = 3) İzinli ise 3
                                    status = "primary";
                                    break;
                                default:
                            }

                            for (var i = 0; i < weekDays.length; i++) {
                                permits.push({ date: formatDate(weekDays[i]), status: status });
                            }

                        });

                        drawCalendar({
                            calendarId: employee.Id,
                            language: LANGUAGE,
                            month: month,
                            year: year,
                            data: permits,
                            legends: LEGENDS
                        });

                        $("body").on('click', `#calendar-header-${employee.Id} .calendar-header-left #calendar-previous-btn`, function () {
                            var calendarId = this.parentElement.parentElement.id.split("-")[2];
                            var calendar = document.getElementById("calendar-" + calendarId);
                            var currentMonth = calendar.dataset.dateMonth;
                            var currentYear = calendar.dataset.dateYear;
                            drawCalendar({
                                calendarId: employee.Id,
                                language: LANGUAGE,
                                month: getPreviousMonth(currentMonth, currentYear).month,
                                year: getPreviousMonth(currentMonth, currentYear).year,
                                data: permits,
                                legends: LEGENDS
                            });
                        });

                        $("body").on('click', `#calendar-header-${employee.Id} .calendar-header-left #calendar-next-btn`, function () {
                            var calendarId = this.parentElement.parentElement.id.split("-")[2];
                            var calendar = document.getElementById("calendar-" + calendarId);
                            var currentMonth = calendar.dataset.dateMonth;
                            var currentYear = calendar.dataset.dateYear;
                            drawCalendar({
                                calendarId: employee.Id,
                                language: LANGUAGE,
                                month: getNextMonth(currentMonth, currentYear).month,
                                year: getNextMonth(currentMonth, currentYear).year,
                                data: permits,
                                legends: LEGENDS
                            });
                        });


                    });
            }
        });
    }
});