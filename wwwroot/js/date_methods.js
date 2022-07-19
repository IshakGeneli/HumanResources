
function getDatesBetweenTwoDates(startDateString, endDateString) {
    var startDate = new Date(startDateString);
    var endDate = new Date(endDateString);
    var dateList = [];
    for (var dt = startDate; dt <= endDate; dt.setDate(dt.getDate() + 1)) {
        dateList.push(new Date(dt));
    }
    return dateList;
}

function getWeekDays(dateList) {
    var weekDays = [];
    for (var i = 0; i < dateList.length; i++) {
        if (dateList[i].getDay() != 0 && dateList[i].getDay() != 6) {
            weekDays.push(dateList[i]);
        }
    }
    return weekDays;
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

function getDayCountBetweenTwoDates(startDate, endDate) {
    var dateList = getDatesBetweenTwoDates(startDate.slice(0, -9), endDate.slice(0, -9));
    var weekDays = getWeekDays(dateList);
    return weekDays.length;
}