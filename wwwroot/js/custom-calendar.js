
function getMonthNames(language) {
    var monthNames = []
    switch (language) {
        case "en":
        case "undefined":
            monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            break;
        case "tr" :
            monthNames = ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"];
            break;
        default:
            monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    }
    return monthNames;
}
function drawCalendar(obj) {
    var count = getDayCountInMonth(obj.month, obj.year);

    var calendar = document.getElementById("calendar-" + obj.calendarId);
    calendar.dataset.dateMonth = new Date(obj.year, obj.month)
        .toISOString()
        .substring(5, 7)
    calendar.dataset.dateYear = new Date(obj.year, obj.month)
        .toISOString()
        .substring(0, 4)

    calendar.style.maxWidth = "fit-content";

    var calendarHeader = drawCalendarHeader(obj);

    clearCalendar(calendar);
    calendar.appendChild(calendarHeader);

    var calendarBody = document.createElement("div");
    calendarBody.id = "calendar-body-" + obj.calendarId;
    calendarBody.classList.add("calendar-body");

    calendar.appendChild(calendarBody);

    drawSquare(count, obj);
}

function drawCalendarHeader(obj) {
    var header = document.createElement("div");
    header.id = "calendar-header-" + obj.calendarId;
    header.classList.add("calendar-header");

    var headerLeft = document.createElement("div");
    headerLeft.classList.add("calendar-header-left");

    var prevBtn = document.createElement("button");
    headerLeft.appendChild(prevBtn);
    prevBtn.classList.add("calendar-btn");
    prevBtn.id = "calendar-previous-btn";
    prevBtn.innerHTML = "<i class='bi bi-arrow-left-circle'></i>";

    calendarTitle = document.createElement("span");
    calendarTitle.classList.add("calendar-title");

    var monthNames = getMonthNames(obj.language);
    calendarTitle.innerHTML = monthNames[obj.month - 1] + " " + obj.year;
    headerLeft.appendChild(calendarTitle);

    nextBtn = document.createElement("button");
    headerLeft.appendChild(nextBtn);
    nextBtn.classList.add("calendar-btn");
    nextBtn.id = "calendar-next-btn";
    nextBtn.innerHTML = "<i class='bi bi-arrow-right-circle'></i>";

    header.appendChild(headerLeft);

    var headerRight = document.createElement("div");
    headerRight.classList.add("calendar-header-right");

    var legend = '<div class="calendar-legend d-flex">' + createLegendString(obj.legends) + '</div>';

    var legendTag = stringToHTML(legend);
    headerRight.appendChild(legendTag);
    header.appendChild(headerRight);

    return header;
}

function drawSquare(count, obj) {

    for (var i = 0; i < count; i++) {
        var square = document.createElement("div");
        square.className = "square";
        square.className += " day" + (i + 1);
        square.dataset.date = new Date(obj.year, obj.month - 1, i + 2).toISOString().split("T")[0];

        square.innerHTML = i + 1;
        obj.data.forEach((cell) => {
            if (cell.date == square.dataset.date) {
                switch (cell.status) {
                    case "danger":
                        square.className += " bg-danger";
                        break;
                    case "primary":
                        square.className += " bg-primary";
                        break;
                    case "warning":
                        square.className += " bg-warning";
                        break;
                    default:
                        break;
                }
            }

        });
        document.getElementById("calendar-body-" + obj.calendarId).appendChild(square);
    }

    var restOfDays = 31 - count;
    for (var i = 0; i < restOfDays; i++) {
        var square = document.createElement("div");
        square.classList.add("square", `day${(i + 1)}`, "disabled-day");
        square.innerHTML = i + 1;
        document.getElementById("calendar-body-" + obj.calendarId).appendChild(square);
    }
}

function clearCalendar(calendar) {
    calendar.innerHTML = "";
}

function getDayCountInMonth(month, year) {
    //month++;
    return new Date(year, month, 0).getDate();
}

function getPreviousMonth(month, year) {
    if (month === "01") {
        return { month: 12, year: parseInt(year) - 1 };
    } else {
        return { month: parseInt(month) - 1, year: year };
    }
}

function getNextMonth(month, year) {
    if (month === "12") {
        return { month: 1, year: parseInt(year) + 1 };
    } else {
        return { month: parseInt(month) + 1, year: year };
    }
}

function createLegendString(legends) {
    var str = ""
    legends.forEach(legend => {
        str += '<div class="legend-group mx-1">' +
            ' <span class="legend bg-' + legend.type + '"></span> <span>' + legend.text + '</span>' +
            '</div>'
    })

    return str;
}

function stringToHTML(str) {
    var div = document.createElement("div");
    div.innerHTML = str;
    return div.firstChild;
}