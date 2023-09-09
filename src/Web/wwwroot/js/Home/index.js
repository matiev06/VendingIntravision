
var cartItems = [];
let balanceCart = 0;
let quantityCart = 0;


function IsUnavailableBeverage(itemId) {


    var selectedBeverage = beverages.find(function (beverage) {
        return beverage.id === itemId;
    });

    var newBalance = balance - selectedBeverage.price;

    var item = cartItems.find(function (beverage) {
        return beverage.id === itemId;
    });

    var qua = selectedBeverage.quantity;
    if (item) {
        qua = qua - item.quantity;
    }

    if (newBalance >= 0 && qua > 0) {
        return true;
    } else {
        return false;
    }

    return true;
}


function UpdateInfoValidBeverages() {

    var quantity = 0;
    var newBalance = balance;

    cartItems.forEach(function (item) {
        quantity += item.quantity
    });

    beverages.find(function (beverage) {

        var boolQuantity = beverage.quantity > quantity;

        var boolPrice = beverage.price <= newBalance;


        var $beverageCountSpan = $('.count-beverage').filter(function () {
            return $(this).data('id') === beverage.id;
        });

        if (boolQuantity && boolPrice) {

            var classNameToAdd = 'd-none';
            if (!$beverageCountSpan.hasClass(classNameToAdd)) {
                $beverageCountSpan.addClass(classNameToAdd);
            }
            $beverageCountSpan.data("value", "true")
        }
        else {
            $beverageCountSpan.removeClass("d-none");
            $beverageCountSpan.text("доступно 0");
            $beverageCountSpan.data("value", "false")
        }

    });

    quantityCart = quantity;

}

function PushToCart(itemId, itemName, itemQuantity, itemPrice) {

    cartItems.push({ id: itemId, name: itemName, quantity: itemQuantity, price: itemPrice });
}

function ChangeLiCart(itemId, itemName, ItemQuantity, isNew) {


    var plusLink = $("<button>").addClass("text-button addCart").attr("data-id", itemId).text("+");
    var productName = $("<span>").addClass("item-name").data("id", itemId).text(itemName);
    var itemQuantity = $("<span>").addClass("item-quantity").text(ItemQuantity);
    var minusLink = $("<button>").addClass("text-button removeCart").attr("data-id", itemId).text("-");

    if (!isNew) {
        var liElement = $("#cartList li").filter(function () {
            return $(this).find(".item-name").data('id') == itemId;
        });
        liElement.empty();
        liElement.append(plusLink, " ", productName, ": ", itemQuantity, " ", minusLink);
    } else {
        liElement = $("<li>").addClass("list-group-item");
        liElement.append(plusLink, " ", productName, ": ", itemQuantity, " ", minusLink);
        $('#cartList').append(liElement);
    }

}

function AddToCart(elem) {

    var itemId = $(elem).data('id');

    var itemExists = cartItems.find(function (item) {
        return item.id === itemId;
    });

    var selectedBeverage;

    if (itemExists) {

        var itemName = itemExists.name;
        var itemPrice = itemExists.price;
        var currentQuantity = itemExists.quantity + 1;

        if (!IsUnavailableBeverage(itemId)) {
            UpdateInfoValidBeverages();
            return
        }

        ChangeLiCart(itemId, itemName, currentQuantity, false);


        itemExists.quantity = currentQuantity;

    }
    else {

        if (!IsUnavailableBeverage(itemId)) {
            UpdateInfoValidBeverages();
            return
        }

        if (!selectedBeverage) {
            selectedBeverage = beverages.find(function (beverage) {
                return beverage.id === itemId;
            });
        }

        var itemName = selectedBeverage.name;
        var itemPrice = selectedBeverage.price;
        PushToCart(itemId, itemName, 1, itemPrice);

        ChangeLiCart(itemId, itemName, currentQuantity, true);


    }


    if (!selectedBeverage) {
        selectedBeverage = beverages.find(function (beverage) {
            return beverage.id === itemId;
        });
    }

    
    updateBalance(balance - selectedBeverage.price);
    UpdateInfoValidBeverages();

}

function RemCartListLi() {

    var liElement = $("#cartList li").closest("li");
    liElement.remove();
}

function RemFromCart(elem) {

    var itemId = $(elem).data('id');


    var itemExists = cartItems.find(function (item) {
        return item.id === itemId;
    });

    var selectedBeverage;

    if (itemExists && itemExists.quantity > 1) {

        currentQuantity = itemExists.quantity - 1;

        var itemName = itemExists.name;

        ChangeLiCart(itemId, itemName, currentQuantity, false)

        itemExists.quantity = currentQuantity;

    }
    else {
        var itemIndex = cartItems.findIndex(function (item) {
            return item.id === itemExists.id;
        });

        if (itemIndex !== -1) {
            cartItems.splice(itemIndex, 1);
        }

        RemCartListLi();
    }

    if (!selectedBeverage) {
        selectedBeverage = beverages.find(function (beverage) {
            return beverage.id === itemId;
        });
    }

    updateBalance(balance + selectedBeverage.price);
    UpdateInfoValidBeverages();


}

function BuyBeverages(elem) {

    var purchaseData = {
        IdUser: userID,
        CartItems: cartItems
    };

    $.ajax({
        type: 'POST',
        url: '/api/Beverages/PurchaseBeverage',
        contentType: 'application/json',
        data: JSON.stringify(purchaseData),
        success: function (response, textStatus, xhr) {

            if (xhr.status === 204) {
                alert('Корзина пуста');
            } else {
                cartItems = [];
                RemCartListLi();
                alert(response.message);
            }
        },
        error: function (error) {
            alert('Ошибка покупки напитков.');
        }
    });
}

function GetChange() {

    $.ajax({
        type: 'GET',
        url: '/GetChange/' + userID,
        contentType: 'application/json',
        success: function (response) {

            var $divChange = $('#changeDiv');
            var span = $("<span>").text(response.changeText);


            $divChange.empty();
            $divChange.append(span);

            updateBalance(response.balance);
            UpdateInfoValidBeverages();
        },
        error: function (error) {
            alert('Ошибка получение сдачи');
        }
    });
}

function AddCoinToBalance(elem) {
    const coinValue = parseInt($(elem).data('value'));

    var userData = {
        Id: userID,
        Balance: coinValue
    };

    $.ajax({
        type: 'POST',
        url: '/api/Account/AddBalance',
        contentType: 'application/json',
        data: JSON.stringify(userData),
        success: function (response) {
            updateBalance(balance + coinValue);

            UpdateInfoValidBeverages();
        },
        error: function (error) {
            console.error(error);
            alert("Ошибка");
        }
    });
}

$('.addCart').on("click", function () {
    AddToCart(this);
});

$("#cartList").on("click", ".addCart", function () {
    AddToCart(this);
});

$("#cartList").on("click", ".removeCart", function () {
    RemFromCart(this);
});

$('#buy-button').on("click", function () {
    BuyBeverages(this);
});


$('.change-button').on("click", function () {
    GetChange(this);
});

$('.coin-btn').on("click", function () {
    AddCoinToBalance(this);
});

function updateBalance(newBalance) {
    balance = newBalance;
    $('.balance').text(balance);
}


$(document).ready(function () {
    UpdateInfoValidBeverages();
});


