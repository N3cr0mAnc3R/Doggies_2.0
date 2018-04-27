webApp.factory("SecurityModule",
    ["$rootScope", "$http", "$location", "Tools",
        function ($rootScope, $http, $location, tools) {
            $rootScope.messages = [];
            var module = {
                Register: function (login, password, email) {
                    $http({
                        method: 'POST',
                        url: '/api/account/register',
                        data: {
                            Login: login,
                            Password: password,
                            Email: email
                        }
                    }).then(
                        tools.unwrapResponse(function (data) {
                            console.log($rootScope.messages);
                            if (data != null) {
                                console.log(data);

                            }
                        }))
                },
                Login: function (login, password) {
                    $http({
                        method: 'POST',
                        url: '/api/account/login',
                        data: {
                            Login: login,
                            Password: password
                        }
                    }).then(
                        tools.unwrapResponse(function (data) {
                            $rootScope.user = data;
                            console.log($rootScope.messages);
                            if (data != null) {
                                console.log(data);

                            }
                        }))
                },
                GetVkInfo: function () {
                    return $http({
                        method: 'GET',
                        url: '/api/account/GetVkInfo'
                    })
                },
                Logout: function () {
                    return $http({
                        method: 'POST',
                        url: '/api/account/logout'
                    }).then(function () {
                        $rootScope.user = null;
                        })
                },
                loginVK: function (access_token, expires_in, user_id, email) {
                    $http({
                        method: 'GET',
                        url: '/api/account/External/vk',
                        params: {
                            access_token: access_token,
                            expires_in: expires_in,
                            user_id: user_id,
                            email: email
                        }
                    }).then(tools.unwrapResponse(function (data) {
                        $rootScope.user = data;
                        $location.url($location.host);
                    }));
                },
                queryVk: function (uri) {
                    $http({
                        method: 'GET',
                        url: uri
                    });
                },
                // сохраняет последнее обещание загрузки
                getCurrentUser: function () {
                    if (!$rootScope.user) {
                        userPromise = $http({
                            method: "GET",
                            url: "/api/account/GetCurrentUser"
                        });

                        userPromise.then(tools.unwrapResponse(function (data) {
                            $rootScope.user = data;
                            if (data != null) {
                               
                            }
                        }))
                    }

                    return userPromise;
                }
            }
            module.getCurrentUser();
            return module;
        }
    ]
);