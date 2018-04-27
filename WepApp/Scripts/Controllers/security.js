webApp.controller("securityCtrl",
    ["$scope", "SecurityModule","$location",
        function ($scope, security, $location) {
            $scope.login;
            $scope.password;
            $scope.email;
            var url;
            $scope.register = function (username, password, email) {
                security.Register(username, password, email);
            }
            $scope.tryLogin = function (username, password) {
                security.Login(username, password);
            }
            getUriVk = function () {
                security.GetVkInfo().then(function (uri) {
                    url = uri.data.Data;
                })
            }
            getUriVk();
            $scope.VKLogin = function () {
                window.open(url, "_self");
            }
            $scope.logout = function () {
                security.Logout();
            }
        }
    ]
);