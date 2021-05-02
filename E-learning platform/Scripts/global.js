$(function () {
    $(".list-group").each(function () {
        var count = $(this).find(".list-group-item").not(".active").size();
        if (count == 0) {
            $(this).append('\
            <a href="#" class="list-group-item">\
                        <h4 class="list-group-item-heading">\
                            Brak elementów\
                        </h4>\
                    </a>\
            ');
        }
    });
});