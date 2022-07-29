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

    async function getEmployeesWithPermits() {

        var employeeList;

        await $.ajax({
            url: 'Permit/GetEmployeesWithPermits',
            type: 'Get',
            dataType: 'JSON',
            success: function (employees) {
                var jsonEmployees = jQuery.parseJSON(employees);

                employeeList = jsonEmployees;
            }
        });
        return Promise.resolve(employeeList);
    }

    async function calendarConfig(month, year) {

        const LANGUAGE = "tr";

        var legends = [
            { type: "warning", text: "Mazeretli", value: 0 },
            { type: "danger", text: "Mazeretsiz", value: 0 },
            { type: "primary", text: "İzinli", value: 0 }
        ]

        var employeeList = await getEmployeesWithPermits();

        $.each(employeeList, (index, employee) => {

            var permits = [];
            var permitDates = [];
            var infoText = "";

            var excusedPermitDates = [];
            var unexcusedPermitDates = [];
            var onLeavePermitDates = [];
            var monthlyExcusedPermitCount = 0;
            var monthlyUnexcusedPermitCount = 0;
            var monthlyOnLeavePermitCount = 0;

            employee.Permits.forEach(permit => {

                var dateList = getDatesBetweenTwoDates(permit.StartDate.slice(0, -9), permit.EndDate.slice(0, -9));

                permitDates = getWeekDays(dateList);
                var status = "";

                switch (permit.Type) { // see PermitType enum
                    case 1:
                        status = "warning"
                        excusedPermitDates = getDatesBetweenTwoDates(permit.StartDate.slice(0, -9), permit.EndDate.slice(0, -9));
                        break;
                    case 2: // permit.Type = 2 (Enum = PermitType: Unexcused = 2) Mazeretsiz ise 2
                        status = "danger";
                        unexcusedPermitDates = getDatesBetweenTwoDates(permit.StartDate.slice(0, -9), permit.EndDate.slice(0, -9));
                        break;
                    case 3: // permit.Type = 3 (Enum = PermitType: OnLeave = 3) İzinli ise 3
                        status = "primary";
                        onLeavePermitDates = getDatesBetweenTwoDates(permit.StartDate.slice(0, -9), permit.EndDate.slice(0, -9));
                        break;
                    default:
                }

                monthlyExcusedPermitCount = getDateCountByMonth(getWeekDays(excusedPermitDates), month);
                monthlyUnexcusedPermitCount = getDateCountByMonth(getWeekDays(unexcusedPermitDates), month);
                monthlyOnLeavePermitCount = getDateCountByMonth(getWeekDays(onLeavePermitDates), month);

                legends[0].value = monthlyExcusedPermitCount;
                legends[1].value = monthlyUnexcusedPermitCount;
                legends[2].value = monthlyOnLeavePermitCount;

                for (var i = 0; i < permitDates.length; i++) {
                    permits.push({ date: formatDate(permitDates[i]), status: status, type: permit.Type });
                }

                infoText = `Kalan izin sayısı: ${employee.RemainPermitCount}`
            });

            drawCalendar({
                calendarId: employee.Id,
                language: LANGUAGE,
                month: month,
                year: year,
                data: permits,
                info: infoText,
                legends: legends
            });

            $("body").on('click', `#calendar-header-${employee.Id} .calendar-header-left #calendar-previous-btn`, function () {
                var calendarId = this.parentElement.parentElement.id.split("-")[2];
                var calendar = document.getElementById("calendar-" + calendarId);
                var currentMonth = calendar.dataset.dateMonth;
                var currentYear = calendar.dataset.dateYear;
                var previousMonth = getPreviousMonth(currentMonth, currentYear).month;
                var previousYear = getPreviousMonth(currentMonth, currentYear).year;

                monthlyExcusedPermitCount = getDateCountByMonth(excusedPermitDates, previousMonth);
                monthlyUnexcusedPermitCount = getDateCountByMonth(unexcusedPermitDates, previousMonth);
                monthlyOnLeavePermitCount = getDateCountByMonth(onLeavePermitDates, previousMonth);

                legends[0].value = monthlyExcusedPermitCount;
                legends[1].value = monthlyUnexcusedPermitCount;
                legends[2].value = monthlyOnLeavePermitCount;

                drawCalendar({
                    calendarId: employee.Id,
                    language: LANGUAGE,
                    month: previousMonth,
                    year: previousYear,
                    data: permits,
                    info: infoText,
                    legends: legends
                });
            });

            $("body").on('click', `#calendar-header-${employee.Id} .calendar-header-left #calendar-next-btn`, function () {
                var calendarId = this.parentElement.parentElement.id.split("-")[2];
                var calendar = document.getElementById("calendar-" + calendarId);
                var currentMonth = calendar.dataset.dateMonth;
                var currentYear = calendar.dataset.dateYear;
                var nextMonth = getNextMonth(currentMonth, currentYear).month;
                var nextYear = getNextMonth(currentMonth, currentYear).year;

                monthlyExcusedPermitCount = getDateCountByMonth(excusedPermitDates, nextMonth);
                monthlyUnexcusedPermitCount = getDateCountByMonth(unexcusedPermitDates, nextMonth);
                monthlyOnLeavePermitCount = getDateCountByMonth(onLeavePermitDates, nextMonth);

                legends[0].value = monthlyExcusedPermitCount;
                legends[1].value = monthlyUnexcusedPermitCount;
                legends[2].value = monthlyOnLeavePermitCount;

                drawCalendar({
                    calendarId: employee.Id,
                    language: LANGUAGE,
                    month: nextMonth,
                    year: nextYear,
                    data: permits,
                    info: infoText,
                    legends: legends
                });
            });
        });
    }

    //function calendarConfig(month, year) {

    //    const LEGENDS = [
    //        { type: "warning", text: "Mazeretli", value: 0 },
    //        { type: "danger", text: "Mazeretsiz", value: 0 },
    //        { type: "primary", text: "İzinli", value: 0 }
    //    ]

    //    const LANGUAGE = "tr";

    //    $.ajax({
    //        url: 'Permit/GetEmployeesWithPermits',
    //        type: 'Get',
    //        dataType: 'JSON',
    //        success: function (employees) {

    //            var jsonEmployees = jQuery.parseJSON(employees);

    //            $.each(jsonEmployees,
    //                (index, employee) => {

    //                    var permits = [];
    //                    var infoText = "";

    //                    employee.Permits.forEach((permit) => {
    //                        var dateList = getDatesBetweenTwoDates(permit.StartDate.slice(0, -9), permit.EndDate.slice(0, -9));

    //                        var weekDays = getWeekDays(dateList);
    //                        var status = "";
    //                        var monthlyExcusedPermitCount = 0;
    //                        var monthlyUnexcusedPermitCount = 0;
    //                        var monthlyOnLeavePermitCount = 0;

    //                        switch (permit.Type) { // see PermitType enum
    //                            case 1:
    //                                status = "warning"
    //                                monthlyExcusedPermitCount = getDateCountByMonth(weekDays, month);
    //                                break;
    //                            case 2: // permit.Type = 2 (Enum = PermitType: Unexcused = 2) Mazeretsiz ise 2
    //                                status = "danger";
    //                                monthlyUnexcusedPermitCount = getDateCountByMonth(weekDays, month);
    //                                break;
    //                            case 3: // permit.Type = 3 (Enum = PermitType: OnLeave = 3) İzinli ise 3
    //                                status = "primary";
    //                                monthlyOnLeavePermitCount = getDateCountByMonth(weekDays, month);
    //                                break;
    //                            default:
    //                        }

    //                        for (var i = 0; i < weekDays.length; i++) {
    //                            permits.push({ date: formatDate(weekDays[i]), status: status });
    //                        }

    //                        LEGENDS[0].value = monthlyExcusedPermitCount;
    //                        LEGENDS[1].value = monthlyUnexcusedPermitCount;
    //                        LEGENDS[2].value = monthlyOnLeavePermitCount;

    //                        infoText = `Kalan izin sayısı: ${employee.RemainPermitCount}`

    //                    });

    //                    drawCalendar({
    //                        calendarId: employee.Id,
    //                        language: LANGUAGE,
    //                        month: month,
    //                        year: year,
    //                        data: permits,
    //                        info: infoText,
    //                        legends: LEGENDS
    //                    });

    //                    $("body").on('click', `#calendar-header-${employee.Id} .calendar-header-left #calendar-previous-btn`, function () {
    //                        var calendarId = this.parentElement.parentElement.id.split("-")[2];
    //                        var calendar = document.getElementById("calendar-" + calendarId);
    //                        var currentMonth = calendar.dataset.dateMonth;
    //                        var currentYear = calendar.dataset.dateYear;
    //                        var previousMonth = getPreviousMonth(currentMonth, currentYear).month;
    //                        var previousYear = getPreviousMonth(currentMonth, currentYear).year;
    //                        drawCalendar({
    //                            calendarId: employee.Id,
    //                            language: LANGUAGE,
    //                            month: previousMonth,
    //                            year: previousYear,
    //                            data: permits,
    //                            info: infoText,
    //                            legends: LEGENDS
    //                        });
    //                    });

    //                    $("body").on('click', `#calendar-header-${employee.Id} .calendar-header-left #calendar-next-btn`, function () {
    //                        var calendarId = this.parentElement.parentElement.id.split("-")[2];
    //                        var calendar = document.getElementById("calendar-" + calendarId);
    //                        var currentMonth = calendar.dataset.dateMonth;
    //                        var currentYear = calendar.dataset.dateYear;
    //                        var nextMonth = getNextMonth(currentMonth, currentYear).month;
    //                        var nextYear = getNextMonth(currentMonth, currentYear).year;
    //                        drawCalendar({
    //                            calendarId: employee.Id,
    //                            language: LANGUAGE,
    //                            month: nextMonth,
    //                            year: nextYear,
    //                            data: permits,
    //                            info: infoText,
    //                            legends: LEGENDS
    //                        });
    //                    });

    //                });
    //        }
    //   });
    //}
});