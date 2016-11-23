requirejs.config({
    baseUrl: '/js',
    paths: {
        jquery: 'lib/jquery-1.10.2.min',
        winwheel: 'lib/winwheel',
        tweenMax: 'lib/tweenMax'
    }
});

requirejs(['jquery', 'controller'], function ($, c) {
    //This function is called when scripts/helper/util.js is loaded.
    //If util.js calls define(), then this function is not fired until
    //util's dependencies have loaded, and the util argument will hold
    //the module value for "helper/util".
    $(function () {
        c.start();
    });
});