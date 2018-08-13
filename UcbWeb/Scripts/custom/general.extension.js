$ (document).ready (function () {
    $ (id + 'input[data-val-required][type="radio"]').each (function (index, e) {
        removeRequired (e);

    });
});

function removeRequired(e) {
    var elementid = e.id;
    var label = $('label[for =' + elementid + ']');
    label.find (".required-input").remove ();
}