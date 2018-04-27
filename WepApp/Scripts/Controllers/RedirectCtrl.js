webApp.controller("RedirectCtrl",
    ["$scope", "$location", "SecurityModule",
        function ($scope, $location, security) {
            parse($location.url());
            console.log($location.url());
            function parse(str) {
                var withHash = str.split('#');
                var parts = withHash[1].split('&');
                security.loginVK((parts[0].split('='))[1], (parts[1].split('='))[1], (parts[2].split('='))[1], (parts[3].split('='))[1]);
            }
        }
    ]
);