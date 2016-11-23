define(['jquery'], function ($) {


    return {
        getEmployeesAsync: function () {
            return $.ajax("/GetAllEmployees");
        },

        getRotaAsync: function () {
            return $.ajax("/GetRotaForToday");
        },
        getEligibleEmployeesAsync: function (isMorning) {
            return $.ajax("/GetEmployeesForTodaysWheelOfFortune?morning=" + isMorning)
        },
        assignWinnerForTodayAsync: function (winner, isMorning) {
            return $.post('/AssignWinnerForToday', { morning: isMorning, employeeNumber: winner })
        }
    }

});