/// <reference path="../App.js" />

(function () {
    "use strict";

    // The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();

            Office.context.document.addHandlerAsync(Office.EventType.DocumentSelectionChanged, getDataFromSelection);
        });
    };

    // Reads data from current document selection and displays a notification
    function getDataFromSelection() {
        Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
            function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                    document.getElementById('inputText').value = result.value;
                } else {
                    app.showNotification('Error:', result.error.message);
                }
            }
        );
    }
})();