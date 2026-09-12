(function ($) {
    $.validator.addMethod("nodigits", function (value, element) {
        return this.optional(element) || !/\d/.test(value);
    });

    $.validator.unobtrusive.adapters.add("nodigits", [], function (options) {
        options.rules["nodigits"] = true;
        options.messages["nodigits"] = options.message;
    });
})(jQuery);