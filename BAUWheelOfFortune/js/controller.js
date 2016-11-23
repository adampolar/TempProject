define(['view', 'model', 'jquery'], function (view, model, $) {

    var morningMode;
    var employees;

    var getEmployeeName = function(employees, empNo) {
        var emp = $.grep(
                employees, function (el) {
                    return el.StaffNumber === empNo
                })[0];
        return emp.FirstName + ' ' + emp.SecondName;
    }

    var acceptWinnerCallback = function () {
        view.hideWinner();
        start();
    };

    var alertWinnerCallback = function () {

        model.assignWinnerForTodayAsync(window.wheelOfFortune.employeeNumber, morningMode).done(function () {
            view.displayWinner(getEmployeeName(employees, window.wheelOfFortune.employeeNumber), acceptWinnerCallback);
        });
    }

    var fillEmployeeDetailList = function(toFill, employees) {
        var filledEmployees = [];

        $(toFill).each(function (i, empNo) {
            filledEmployees[i] = { 'FullName': getEmployeeName(employees, empNo), 'EmployeeNumber': empNo }
        });
        return filledEmployees;
    }


    var start = function () {
        view.clear();
        $.when(model.getEmployeesAsync(), model.getRotaAsync()).done(function (employeesResp, rota) {
            rota = rota[0];
            employees = employeesResp[0];
            if (rota.Morning !== null && rota.Afternoon !== null) {

                view.drawWinner(getEmployeeName(employees, rota.Morning), true);
                view.drawWinner(getEmployeeName(employees, rota.Afternoon), false);

            } else if (rota.Morning === null) {
                morningMode = true;
                model.getEligibleEmployeesAsync(true).done(function (emps) {
                    emps = fillEmployeeDetailList(emps, employees);
                    view.drawWheel(emps, true, alertWinnerCallback);
                });

            } else {
                morningMode = false;
                view.drawWinner(getEmployeeName(employees, rota.Morning), true);
                model.getEligibleEmployeesAsync(false).done(function (emps) {
                    emps = fillEmployeeDetailList(emps, employees);
                    view.drawWheel(emps, false, alertWinnerCallback);
                });

            }
        })
    }


    return {
        start: start
    }

});