$(function () {
    $("#AddQuestion").click(function () {
        $("#Questions").append('\
                <div class="QuestionPanel panel panel-default">\
                  <div class="panel-heading">\
                    <span class="panel-title">Pytanie: <input class="form-control questionfield" type="text" />\</span>\
                  </div>\
                  <div class="panel-body">\
                    <div class="Answers"></div>\
                  </div>\
                    <div class="panel-footer">\
                        <input type="button" value="Dodaj odpowiedź" class="btn btn-default AddAnswer" />\
                        <input type="button" value="Usuń pytanie" class="btn btn-default RemoveQuestion" />\
                    </div>\
                </div>\
                ');
    });

    $(document).on("click", ".AddAnswer", function () {
        $(this).closest(".panel").find(".Answers").append('\
                <div class="form-group">\
                    <label class="control-label col-md-2">Odpowiedź</label>\
                    <div class="col-md-4">\
                        <input class="form-control" type="text" />\
                    </div>\
                    <label class="control-label col-md-2">Poprawna?</label>\
                    <div class="col-md-2">\
                        <input class="form-control check-box" type="checkbox" value="True" />\
                    </div>\
                    <div class="col-md-2">\
                        <input type="button" value="Usuń odpowiedź" class="btn btn-default RemoveAnswer" />\
                    </div>\
                </div>');
    });
    $(document).on("click", ".RemoveQuestion", function () {
        $(this).closest(".panel").remove();
    });

    $(document).on("click", ".RemoveAnswer", function () {
        $(this).closest(".form-group").remove();
    });

    $("#test-form").submit(function () {
        var QuestionNum = 0;
        var AnswerNum = 0;
        $(".QuestionPanel").each(function () {
            $(this).find(".questionfield").eq(0).attr("name", "Questions[" + QuestionNum + "].Text");
            $(this).find(".form-group").each(function () {
                $(this).find("input[type=text]").eq(0).attr("name", "Questions[" + QuestionNum + "].Answers[" + AnswerNum + "].Text");
                $(this).find("input[type=checkbox]").eq(0).attr("name", "Questions[" + QuestionNum + "].Answers[" + AnswerNum + "].IsCorrect");
                AnswerNum++;
            });
            AnswerNum = 0;
            QuestionNum++;
        });
    });
});