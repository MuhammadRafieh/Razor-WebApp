// Application-wide behavior for the Literary Archive.

(function () {
    'use strict';

    // Confirm destructive actions before submission.
    document.querySelectorAll('form[data-confirm]').forEach(function (form) {
        form.addEventListener('submit', function (event) {
            var message = form.getAttribute('data-confirm');
            if (message && !confirm(message)) {
                event.preventDefault();
            }
        });
    });
})();
