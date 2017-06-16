
"use strict";

define([], function (angular) {

    window.identityServer = (function () {
        "use strict";

        var identityServer = {
            getModel: function () {
                var modelJson = document.getElementById("modelJson");
                var encodedJson = '';
                if (typeof (modelJson.textContent) !== undefined) {
                    encodedJson = modelJson.textContent;
                } else {
                    encodedJson = modelJson.innerHTML;
                }
                var json = Encoder.htmlDecode(encodedJson);
                var model = JSON.parse(json);
                return model;
            }
        };

        return identityServer;
    })();
});