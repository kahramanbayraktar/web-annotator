// Jcrop
function activateCrop() {
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
}

function clearCrop() {
    JcropAPI = $("#img-analysis").data("Jcrop");
    JcropAPI.destroy();
    activateCrop();
}

// Create Annotation
$(document).ready(function () {
    activateCrop();
});

// Submit Annotation
$("#addForm").on("submit", function (e) {
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
            clearCrop();
            alert("Annotation created!");
            window.location.reload();
            console.log(result);
        });
});

function openForm() {
    document.getElementById("annoForm").style.display = "block";
}

function closeForm() {
    document.getElementById("annoForm").style.display = "none";
    clearCrop();
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

$("a.share").click(function (event) {
    event.preventDefault();
    console.log(event.target);
    copyURI(event);
    alert("Copied the URL. Now you can share it.");
});

function copyURI(evt) {
    evt.preventDefault();
    navigator.clipboard.writeText(evt.target.getAttribute('href')).then(() => {
        /* clipboard successfully set */
    }, () => {
        /* clipboard write failed */
    });
}

// Search
$("#searchForm").on("submit", function (e) {
    e.preventDefault();
    const data = $(this).closest("form").serialize();

    $.ajax({
            type: "POST",
            url: "/Search?handler=Search",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "json",
            data: data,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            }
        })
        .done(function (result) {
            //$("#result").append(result);
            $("#result").empty();
            const json = JSON.parse(result);
            for (var i = 0; i < json.length; i++) {
                $("#result").append("<hr>");
                $("#result").append(`<a href="${json[i].id}" target="_blank">${json[i].body}</a>`);
            }
            
            console.log(result);
        });
});