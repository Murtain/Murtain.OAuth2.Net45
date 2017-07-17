
"use strict";

define(['encoder'], function () {

    window.identityServer = (function () {
        "use strict";

        var identityServer = {
            getModel: function () {
                var json = document.getElementById("IdentityModelString").value;

                console.log(json);

                return JSON.parse(json);
            }
        };

        return identityServer;
    })();
});