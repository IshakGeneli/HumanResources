$(function () {

    $.ajax({
        url: 'Employee/GetEmployees',
        type: 'Get',
        success: function (employees) {

            var jsonEmployees = jQuery.parseJSON(employees);

            var employeeNames = []
            var annualPermitCounts = []

            $.each(jsonEmployees, (index, employee) => {

                employeeNames.push(employee.FullName);
                annualPermitCounts.push(employee.AnnualPermitCount);
            });

            // BEGIN FOR CHART 1

            const data = {
                labels: employeeNames,
                datasets: [{
                    label: 'Gün',
                    backgroundColor: '#fcba03',
                    borderColor: '#fcba03',
                    lineTension: 0,
                    data: annualPermitCounts,
                }]
            };

            const config = {
                type: 'bar',
                data: data,
                options: {
                    plugins: {
                        legend: {
                            position: 'left',
                        },
                        title: {
                            display: true,
                            text: 'Yıllık İzin Miktarı'
                        }
                    }
                },
                plugins: [ChartDataLabels]
            };

            const chart1 = new Chart(document.getElementById('chart1'), config);

            // END FOR CHART 1
        }
    })

    $.ajax({
        url: 'Permit/GetEmployeesWithPermits',
        type: 'Get',
        success: function (employees) {

            var jsonEmployees = jQuery.parseJSON(employees);

            let employeeNames = [];
            let excusedPermitCounts = [];
            let unexcusedPermitCounts = [];
            let totalPermitCounts = [];

            $.each(jsonEmployees,
                (index, employee) => {

                    employeeNames.push(employee.EmployeeFullName);

                    var totalPermitDayCount = 0;
                    var excusedPermitDayCount = 0;
                    var unexcusedPermitDayCount = 0;

                    employee.Permits.forEach((permit) => {
                        if (permit.Type == 1 ) {
                            excusedPermitDayCount += getDayCountBetweenTwoDates(permit.StartDate, permit.EndDate);
                        }
                        else if (permit.Type == 2) {
                            unexcusedPermitDayCount += getDayCountBetweenTwoDates(permit.StartDate, permit.EndDate);
                        }
                        totalPermitDayCount += getDayCountBetweenTwoDates(permit.StartDate, permit.EndDate);

                    });
                    excusedPermitCounts.push(excusedPermitDayCount);
                    unexcusedPermitCounts.push(unexcusedPermitDayCount);
                    totalPermitCounts.push(totalPermitDayCount);

                });

            // BEGIN FOR CHART 2

            const data = {
                labels: employeeNames,
                datasets: [
                    {
                    label: 'Mazeretli',
                        backgroundColor: '#fff0c3',
                        borderColor: '#fff0c3',
                        lineTension: 0,
                        data: excusedPermitCounts,
                    },
                    {
                        label: 'Mazeretsiz',
                        backgroundColor: '#dc3545',
                        borderColor: '#dc3545',
                        lineTension: 0,
                        data: unexcusedPermitCounts,
                    },
                    {
                        label: 'Toplam',
                        backgroundColor: '#007bff ',
                        borderColor: '#007bff ',
                        lineTension: 0,
                        data: totalPermitCounts,
                    }
                ]
            };

            const config = {
                type: 'line',
                data: data,
                options: {
                    plugins: {
                        legend: {
                            position: 'left',
                        },
                        title: {
                            display: true,
                            text: 'Personellerin Gelmediği Gün Sayıları'
                        }
                    }
                }
            };

            const chart2 = new Chart(document.getElementById('chart2'), config);

            // END FOR CHART 2
        }
    });

  
    $.ajax({
        url: 'Permit/GetEmployeesWithPermits',
        type: 'Get',
        success: function (employees) {

            var jsonEmployees = jQuery.parseJSON(employees);

            let employeeNames = [];
            let totalPermitCounts = [];

            $.each(jsonEmployees,
                (index, employee) => {

                    employeeNames.push(employee.EmployeeFullName);

                    var totalPermitDayCount = 0;

                    employee.Permits.forEach((permit) => {
                        
                        totalPermitDayCount += getDayCountBetweenTwoDates(permit.StartDate, permit.EndDate);

                    });
                    
                    totalPermitCounts.push(totalPermitDayCount);

                });

            // BEGIN FOR CHART 3

            const data = {
                labels: employeeNames,
                datasets: [
                    {
                        label: 'Toplam',
                        backgroundColor: getRandomColorArray(employeeNames.length),
                        lineTension: 0,
                        data: totalPermitCounts,
                    }
                ]
            };

            const config = {
                type: 'doughnut',
                data: data,
                options: {
                    plugins: {
                        legend: {
                            position: 'right',
                        },
                        title: {
                            display: true,
                            text: 'Personellerin Gelmediği Toplam Gün Sayıları'
                        },
                        datalabels: {
                            color: "#fff",
                        }
                    },
                    
                },
                plugins: [ChartDataLabels]
            };

            const chart3 = new Chart(document.getElementById('chart3'), config);

            // END FOR CHART 3
        }
    });

    $.ajax({
        url: 'Permit/GetEmployeesWithPermits',
        type: 'Get',
        success: function (employees) {

            const MONTHS = [
                'Ocak',
                'Şubat',
                'Mart',
                'Nisan',
                'Mayıs',
                //'Haziran',
                //'Temmuz',
                //'Ağustos',
                //'Eylül',
                //'Ekim',
                //'Kasım',
                //'Aralık'
            ];

            var jsonEmployees = jQuery.parseJSON(employees);
            console.log(jsonEmployees)
            var employeeNames = []
            var annualPermitCounts = []
            var datasets = []

            $.each(jsonEmployees, (index, employee) => {
                var count = [];
                $.each(employee.Permits, (index, permit) => {

                    count.push(getDayCountBetweenTwoDates(permit.StartDate, permit.EndDate));
                    
                });
                console.log(count)
                let obj = {
                    label: employee.EmployeeFullName,
                    backgroundColor: getRandomColor(),
                    borderColor: '#fcba03',
                    data: [1, 2]
                };

                datasets.push(obj);

                console.log(datasets)
                
                
                employeeNames.push(employee.FullName);
                annualPermitCounts.push(employee.AnnualPermitCount);
            });

            // BEGIN FOR CHART 4

            const data = {
                labels: MONTHS,
                datasets: datasets
            };

            const config = {
                type: 'bar',
                data: data,
                options: {
                    plugins: {
                        legend: {
                            position: 'left',
                        },
                        title: {
                            display: true,
                            text: 'Ay Bazlı ....'
                        }
                    }
                },
                plugins: [ChartDataLabels]
            };

            const chart4 = new Chart(document.getElementById('chart4'), config);

            // END FOR CHART 4
        }
    })

    function getRandomColorArray(length) {
        var colorArray = [];
        for (var i = 0; i < length; i++) {
            
            colorArray.push(getRandomColor());
        }
        return colorArray;
    }

    function getRandomColor() {
        var color = '#' + Math.floor(Math.random() * 16777215).toString(16);
        return color;
    }
});