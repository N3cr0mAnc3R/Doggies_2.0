webApp.directive("sysMessageAutoRemove", ["$rootScope", "$timeout", function ($rootScope, $timeout) {
        return {
            require: "ngModel",
            link: function (scope, element, attributes, ngModel) {
                var elmWraper = $(element);

                $timeout(function () {
                    elmWraper.fadeOut(1000, function () {
                        $rootScope.messages = jQuery.grep($rootScope.messages, function (value) {
                            return value !== ngModel;
                        });
                    });
                }, 5000);
            }
        };
    }]);