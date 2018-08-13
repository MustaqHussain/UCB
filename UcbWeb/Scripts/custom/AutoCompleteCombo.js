(function ($) {

    $.widget("ui.combobox", {


        _create: function () {
            var input, that = this,
                select = this.element.hide(),
                selected = select.children(":selected"),
                value = selected.val() ? selected.text() : "",
                wrapper = this.wrapper = $("<span>").addClass("ui-selectmenu ui-widget ui-state-default ui-corner-all ui-selectmenu-dropdown").insertAfter(select);

            function removeIfInValid(element) {
                var selectedValue = $(element).val(),
                    matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(selectedValue) + "$", "i"),
                    valid = false;
                select.children("option").each(function () {
                    if ($(this).text().match(matcher)) {
                        $(this.selected).attr("selected", "selected");
                        //this.selected = valid = true;
                        return false;
                    }
                    return true;
                });
                if (!valid) {
                    $(element)
                        .val("")
                        .attr("title", selectedValue + " is Invalid");
                    //.tooltip("open");
                    select.val("");
                    //                    setTimeout(function () {
                    //                        //input.tooltip("close").attr("title", "");
                    //                    }, 2500);
                    input.data("autocomplete").term = "";
                    $(select).change();
                    return false;
                }
                return true;
            }


            input = $("<input>")
                .appendTo(wrapper)
                .val(value)
                .attr("title", "")
                .attr("id", this.element.context.id + "_autocomplete")
                .autocomplete({
                    delay: 0,
                    minlength: 0,
                    source: function (request, response) {
                        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                        response(select.children("option").map(function () {
                            var text = $(this).text();
                            if (this.value && (!request.term || request.term == "*" || matcher.test(text)))
                                return {
                                    label: text,
                                    value: text,
                                    option: this
                                };
                        }));
                    },
                    select: function (event, ui) {
                        try {
                            $(ui.item.option).attr("selected", "selected");
                        } catch (e) {
                            ui.item.option.parentElement.selectedIndex = ui.item.option.index;
                        }


                        that._trigger("selected", event, { item: ui.item.option });
                        $(that.element).trigger("selected", event, { item: ui.item.option });
                        $(that.element).change();
                        $(that.element).valid();
                    },
                    change: function (event, ui) {
                        if (!ui.item)
                            return removeIfInValid(this);
                    }
                }).addClass("ui-state-default ui-widget ui-widget-content ui-corner-left ui-combobox-input ");

            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li>").data("item.autocomplete", item).append("<a>" + item.label + "</a>").appendTo(ul);
            };

            $("<a>").click(function () {
                    if ($(input).autocomplete("widget").is(":visible")) {
                        $(input).autocomplete("close");
                        removeIfInValid(input);
                        return;
                    }
                    $(this).blur();
                    input.autocomplete("search", "*");
                    input.focus();
                })
                .attr("tabIndex", -1)
                .attr("title", "Show All Items")
                .appendTo(wrapper)
                .button({
                    icons: { primary: "ui-icon-triangle-1-s" },
                    text: false
                })
                .removeClass("ui-corner-all")
                .addClass("ui-corner-right ui-combobox-toggle ui-combobox-link");


            //                        input.tooltip({
            //                            position: { of: this.button },
            //                            tooltipClass: "ui-state-highlight"
            //                        });
        },
        destroy: function () {
            this.wrapper.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        },
        disable: function () {
            this.wrapper.find('input').each(function () {
                $(this).attr('disabled', true).addClass("ui-state-disabled");
            });
            this.wrapper.find('a').each(function () {
                $(this).attr('disabled', true).addClass("ui-state-disabled").attr("aria-disabled", true);
            });
        },
        clearValue: function () {
            this.wrapper.find('input').each(function () {
                $(this).val('');
            });
        },

        enable: function (index, type) {
            this.wrapper.find('input').each(function () {
                $(this).attr('disabled', false).removeClass("ui-state-disabled");
            });
            this.wrapper.find('a').each(function () {
                $(this).attr('disabled', false).removeClass("ui-state-disabled").attr("aria-disabled", false);
            });
        }
    });
})(jQuery);
