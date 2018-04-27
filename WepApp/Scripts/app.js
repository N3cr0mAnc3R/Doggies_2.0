var webApp = angular.module("webApp", ["ngRoute", "ngMessages"]);
webApp.config([
    "$routeProvider",
    "$locationProvider",
    function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'template/main',
                controller: 'mainCtrl'
            })
            .when('/account/login', {
                templateUrl: 'template/security/login',
                controller: 'securityCtrl'
            })
            .when('/account/register', {
                templateUrl: 'template/security/register',
                controller: 'securityCtrl'
            })
            .when('/account/external/vk', {
                templateUrl: 'template/main',
                controller: 'RedirectCtrl'
            })
            .otherwise({
                redirectTo: '/'
            });
        $locationProvider.html5Mode(true);

        //VK.init({ apiId: 6461682 });
        //VK.Widgets.Auth("vk_auth", { "authUrl": "http://localhost/api/account/external/vk"});
    }
]);