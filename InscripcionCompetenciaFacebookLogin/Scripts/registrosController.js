var app = angular.module("registrosApp");

app.controller("registrosController", function ($scope, $filter, $timeout, registros, registrosFactory) {
    $scope.registros = registros;


    var currentItemIndex = -1;
    $scope.itemSelected = false;
    $scope.currentItem = {};
    $scope.errorVoto = false;
    $scope.successVoto = false;

    //pagination
    $scope.currentPage = 1;
    $scope.maxSize = 5;
    $scope.numPerPage = 10;
    $scope.filteredRegistros = $scope.registros.slice(0, $scope.numPerPage);

    $scope.itemWinning = function () {
        if ($scope.currentItem == false) return false;

        return $scope.currentItem.Winning == true && $scope.currentItem.Votos > 0;
    }

    $scope.$watch("currentPage + numPerPage", function () {
        var begin = (($scope.currentPage - 1) * $scope.numPerPage)
        var end = begin + $scope.numPerPage;

        $scope.filteredRegistros = $scope.registros.slice(begin, end);
    });

    $scope.selectItem = function (itemId) {
        $scope.itemSelected = false;

        var found = $filter('filter')($scope.registros, { Id: itemId }, true);
        $scope.currentItemIndex = $scope.registros.indexOf(found[0]);
        $scope.currentItem = found[0];
        $timeout(function () {
            $scope.itemSelected = true;
        }, 500);
    };

    $scope.voteOnItem = function (itemId) {
        var promise = registrosFactory.vote(itemId);

        promise.then(function (data) {
            if (data.data.success) {
                $scope.registros[$scope.currentItemIndex].Votos = data.data.votos;
                $scope.successVoto = true;
            } else {
                $scope.errorVoto = true;
            }

            $timeout(function () {
                $scope.successVoto = false;
                $scope.errorVoto = false;
            }, 4000);
        }, function (error) {
            // some error
        });
    };

    $scope.deleteItem = function (itemId) {
        registrosFactory.delete(itemId);
    };
});