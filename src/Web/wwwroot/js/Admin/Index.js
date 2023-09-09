
function BlockBtn(elem) {

    var $buttonToEnable = $(elem);

    var dataId = $buttonToEnable.data("id");
    var id = $(elem).attr("id");
    var idnameInput = "#" + id + "Input-" + dataId;

    var $inputToEnable = $(idnameInput);

    if ($buttonToEnable.hasClass("block-btn")) {
        $buttonToEnable.toggleClass("btn-secondary btn-danger");
        $buttonToEnable.toggleClass("block-btn unblock-btn");
        $buttonToEnable.val(true);
        $inputToEnable.val(true);
    } else {
        $buttonToEnable.removeClass("btn-danger unblock-btn");
        $buttonToEnable.addClass("btn-secondary block-btn");
        $buttonToEnable.val(false);
        $inputToEnable.val(false);
    }
}

function ValueDivToInput(elem) {
    var dataId = $(elem).data("id");
    var id = $(elem).attr("id");
    var updatedValue = $(elem).text();

    var containsName = id.toLowerCase().includes("name");
    var containsBlock = id.toLowerCase().includes("block");

    var number;
    if (!containsName && !containsBlock) {
        var regex = /\d+/;

        match = updatedValue.match(regex);
        number = parseInt(match[0], 10);
    } else {
        number = updatedValue;
    }

    var idnameInput = "#" + id + "Input-" + dataId;

    var $inputToEnable = $(idnameInput);


    var res = $inputToEnable.attr("value", number);

}

function BtnValueDivToInput(elem) {
    var dataId = $(elem).data("id");
    var id = $(elem).attr("id");
    var updatedValue = $(elem).val();

    var idnameInput = "#" + id + "Input-" + dataId;

    var $inputToEnable = $(idnameInput);


    var res = $inputToEnable.attr("value", updatedValue);

}

function ImageInput(elem) {
    var id = $(elem).data("id");
    var inputId = "imageInput-" + id;

    $("#" + inputId).click();
}

function ChangeImageInput(elem) {
    var id = $(elem).data("id");
    var selectedFile = $(elem)[0].files[0];
    if (selectedFile) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var clsn = ".image-preview[data-id='" + id + "']";
            var $imagePreview = $(clsn);
            $imagePreview.attr("src", e.target.result);
        };
        reader.readAsDataURL(selectedFile);

    }
}

function RemBeverage(elem) {

    var dataId = $(elem).data("id");
    var confirmDelete = confirm("Вы уверены, что хотите удалить этот элемент?");
    if (confirmDelete) {


        var purchaseData = {
            Beverage: { Id: dataId }
        };

        $.ajax({
            type: 'POST',
            url: '/admin/removebeverage/?key=MySecretKey',
            contentType: 'application/json',
            data: JSON.stringify(purchaseData),
            success: function (response) {
                console.log(response);
                location.reload();
            },
            error: function (error) {
                // Обработка ошибок
                console.error(error);
                alert('Ошибка.');
            }
        });
    }

}


$(".image-preview").on("click", function () {
    ImageInput(this);
});

$(".unblock-btn, .block-btn").on("click", function () {
    BlockBtn(this);
});

$(".rem-beverage").on("click", function () {
    RemBeverage(this);
});

$(".beverage-btn").on("click", function () {
    BtnValueDivToInput(this);
});


$(document).ready(function () {

    $(".beverage-div").on("input", function () {
        ValueDivToInput(this);
    });

    $(".image-input").change(function () {
        ChangeImageInput(this);
    });


});