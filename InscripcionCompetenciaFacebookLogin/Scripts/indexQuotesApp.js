var app = angular.module("indexApp", ['ngAnimate']);


app.value("citas", [{ text: "Life is about making an impact, not making an income.", author: "Kevin Kruse" },
    { text: "Strive not to be a success, but rather to be of value.", author: "Albert Einstein" },
    { text: "You can never cross the ocean until you have the courage to lose sight of the shore.", author: "Christopher Columbus" },
    { text: "Las grandes ideas son aquellas de las que lo único que nos sorprende es que no se nos hayan ocurrido antes.", author: "Noel Clarasó" },
    { text: "Los dos días más importantes en tu vida son el día en el que naces, y el día en que descubres para qué.", author: "Mark Twain" },
    { text: "Be who you are and say what you feel, because those who mind don't matter, and those who matter don't mind.", author: "Bernard M. Baruch" },
    { text: "Cambia antes de que tengas que hacerlo.", author: "Jack Welch" },
    { text: "Tu vida es la obra de tus pensamientos.", author: "Patrick Devlyn" },
    { text: "Si no crees en ti, nadie lo hará.", author: "John Davinson Rockefeller" },
    { text: "Sueño no es lo que ves mientras duermes, sueño es lo que no te deja dormir.", author: "Abdul Kalam" },
    { text: "Live as if you were to die tomorrow. Learn as if you were to live forever.", author: "Mahatma Gandhi" },
    { text: "Sólo es posible avanzar cuando se mira lejos. Solo cabe progresar cuando se piensa en grande.", author: "José Ortega Y Gasset" },
    { text: "Computers are good in following instructions, but not in reading your mind.", author: "Donald Knuth" }]);

app.controller("indexController", function($scope, $interval, $timeout, citas){
    $scope.currentQuote = selectQuote();
    $scope.showQuote = true;

    function selectQuote() {
        return citas[Math.floor(Math.random() * citas.length)];
    };

    $interval(function () {
        $scope.showQuote = false;

        $timeout(function () {
            $scope.currentQuote = selectQuote();
            $scope.showQuote = true;
        }, 2000);
    }, 15000);
});
