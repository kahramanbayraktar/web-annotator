// Submit Annotation
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

// Create Annotation
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

// Display Annotation
function drawRect(x, y, w, h) {
    const $container = $("#img-container");
    $("#my-canvas").remove();

    $('<div id="my-canvas" class="child"/>')
        .appendTo($container)
        .css("left", x + "px")
        .css("top", y + "px")
        .css("width", w + "px")
        .css("height", h + "px")
        .css("border", "1px solid blue")
        .css("backgroundColor", "gray")
        .css("opacity", "0.2");
}

function clearRect(x, y, w, h) {
    $("#my-canvas").remove();
}