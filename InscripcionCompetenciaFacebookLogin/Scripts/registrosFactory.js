var app = angular.module("registrosApp");

app.factory("registrosFactory", function ($http, $q, serverURL) {
    return {
        vote: function (itemId) {
            var deferred = $q.defer();

            var data = {
                id: itemId
            };

            $http.post(serverURL + '/Vote', data).then(function (data) {
                deferred.resolve(data);
            }, function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        },
        delete: function (itemId) {
            var data = {
                id: itemId
            };

            $http.post(serverURL + '/BorrarRegistro', data);
        }
    }
});