$("form").on("submit", function (e) {
    e.preventDefault();
    const data = $(this).closest("form").serialize();

    $.ajax({
        type: "POST",
        url: "/Index?handler=Annotation",
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        data: data,
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        }
    })
        .done(function (result) {
            closeForm();
            console.log(result);
        });
});

$(document).ready(function () {
    $("#img-analysis").Jcrop({
        onSelect: function (c) {
            console.log(c);

            $("#txtX").val(c.x);
            $("#txtY").val(c.y);
            $("#txtW").val(c.w);
            $("#txtH").val(c.h);

            $("#lblX").text("left: " + c.x);
            $("#lblY").text("top: " + c.y);
            $("#lblW").text("width: " + c.w);
            $("#lblH").text("height: " + c.h);

            openForm();
        }
    });
});

function openForm() {
    document.getElementById("annoForm").style.display = "block";
}

function closeForm() {
    document.getElementById("annoForm").style.display = "none";
}