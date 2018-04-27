webApp.factory("Tools",
    ["$rootScope", function ($rootScope) {
        Array.prototype.remove = function (item) {
            var index = this.indexOf(item);
            if (index != -1) {
                this.splice(index, 1);
            }
        }
        function CloneObject(source) {
            var newObj = {};
            for (key in source) {
                newObj[key] = source[key];
            }
            return newObj;
        }
        function isSameObjects(obj1, obj2) {
            for (key in obj1) {
                if (obj1[key] != obj2[key]) {
                    return false;
                }
            }
            for (key in obj2) {
                if (obj1[key] != obj2[key]) {
                    return false;
                }
            }
            return true;
        }
        function unwrapResponse(response) {

            return function (res) {
                if (res.data.Messages.length > 0) {
                    res.data.Messages.forEach(function (msg) {
                    $rootScope.messages.push(msg);
                    })
                }
                response(res.data.Data);
            }
            return (response.data);
        }
        return {
            CloneObject: CloneObject,
            SameObjects: isSameObjects,
            unwrapResponse: unwrapResponse
        };
    }
    ]);