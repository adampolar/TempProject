define(['jquery'], function ($) {
    //TODO:break out magic and repeated vals into consts in this file
    require(['winwheel','tweenMax']);

    var handImage;

    var drawHand = function () {

        if (!handImage) {
            setupHand();
        }

        var wheel = window.wheelOfFortune.handWheel;

        var ctx = wheel.ctx;

        if (ctx) {
            ctx.save();
            ctx.translate(200, 150);
            ctx.rotate(wheel.degToRad(-40));
            ctx.translate(-200, -150);
            ctx.drawImage(handImage, 300, 165);
            ctx.restore();
        }
    }


    var setupHand = function () {
        handImage = new Image();
        handImage.onload = drawHand;
        handImage.src = '/img/pointing_hand.png';
    }

    function colourInWinner() {
        wheel = window.wheelOfFortune.handWheel;

        var winningSegmentNumber = wheel.getIndicatedSegmentNumber();

        for (var x = 1; x < wheel.segments.length; x++) {
            wheel.segments[x].fillStyle = 'gray';
        }
        wheel.segments[winningSegmentNumber].fillStyle = 'yellow';
        wheel.draw();
        drawHand();
    }

    var displayWinnerPopup = function(name, acceptWinnerCallback) {

        $("#the-unlucky-one-text").html("The winner is ....<br>" + name);

        $("#the-unlucky-one-close").unbind().click(function () {
            acceptWinnerCallback();
        });

        setTimeout(function() { $("#the-unlucky-one-container").show()}, 1000);
    }



    window.wheelOfFortune = {};
    window.wheelOfFortune.drawHand = drawHand;

    return {
        drawWinner: function (winner, isMorning) {
            $((isMorning ? '#morning' : '#afternoon') + ' .title').append(winner);
        },
        drawWheel: function (emps, isMorning, alertWinnerCallback) {
            window.wheelOfFortune.alertPrize = function () {
                window.wheelOfFortune.employeeNumber = handWheel.segments[window.wheelOfFortune.handWheel.getIndicatedSegmentNumber()].employeeNumber;
                alertWinnerCallback();
            };


            $((isMorning ? "#morning" : "#afternoon") + " .wheel-of-fortune").append("<canvas id='handWheel' width='600' height='600'></canvas>");

            var segments = [];

            $(emps).each(function (i, emp) {
                segments[i] = { 'text': emp.FullName, 'employeeNumber': emp.EmployeeNumber }
            });


            var handWheel = new Winwheel({
                'canvasId': 'handWheel',
                'textFontFamily': 'Courier',
                'textFontSize': 20,
                'fillStyle': 'gray',
                'outerRadius': 270,
                'centerY': 300,
                'numSegments': emps.length,
                'segments': segments,
                'animation':
                {
                    'type': 'spinToStop',
                    'duration': 5,
                    'spins': 8,
                    'callbackAfter': 'window.wheelOfFortune.drawHand()',
                    'callbackFinished': 'window.wheelOfFortune.alertPrize()'
                }
            });
            window.wheelOfFortune.handWheel = handWheel;
            drawHand();

            $((isMorning ? "#morning" : "#afternoon") + " .wheel-of-fortune").append('<button class="wheel-of-fortune-button">Spin the Wheel</button>');

            $((isMorning ? "#morning" : "#afternoon") + " .wheel-of-fortune-button").click(function () {
                handWheel.startAnimation();
            })

        },
        clear: function() {
            $('#morning .wheel-of-fortune').empty();
            $('#morning .title').html("Morning: ");
            $('#afternoon .wheel-of-fortune').empty();
            $('#afternoon .title').html("Afternoon: ");
        },
        hideWinner: function () {
            $("#the-unlucky-one-container").hide();
        },
        displayWinner: function (employeeName, hideWinnerCallback) {
            colourInWinner();
            displayWinnerPopup(employeeName, hideWinnerCallback);
        }
    }

});