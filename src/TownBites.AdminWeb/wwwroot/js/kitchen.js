let kitchenConnection = null;

async function startKitchenSignalR(restaurantId) {

    kitchenConnection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7276/hubs/orders")
        .withAutomaticReconnect()
        .build();

    kitchenConnection.on("NewOrder", function (order) {

        addKitchenCard(order);

        playNotification();

    });

    kitchenConnection.on("OrderUpdated", function (order) {

        updateKitchenCard(order);

    });

    await kitchenConnection.start();

    await kitchenConnection.invoke(
        "JoinRestaurant",
        restaurantId.toString());

}

function addKitchenCard(order) {

    let html = `
            <div class="col-md-4" id="order-${order.id}">
            <div class="card border-warning shadow">
            <div class="card-header">
            Order #${order.id}
            </div>
            <div class="card-body">
            <h5>${order.customerName}</h5>
            <p>
            ₹ ${order.totalAmount}
            </p>
            <span class="badge bg-warning"> Pending </span>
            <div class="mt-3">
            <button class="btn btn-primary" onclick="acceptOrder(${order.id})">
            Accept
            </button>
            </div>
            </div>
            </div>
            </div>`;
    $("#kitchenOrders").prepend(html);
}

function updateKitchenCard(order) {

    let badge = "bg-warning";

    if (order.status === "Preparing")
        badge = "bg-primary";

    if (order.status === "Ready")
        badge = "bg-success";

    if (order.status === "Delivered") {

        $("#order-" + order.id).remove();

        return;
    }

    $("#order-" + order.id)
        .find(".badge")
        .attr("class", "badge " + badge)
        .text(order.status);

}

function toggleFullScreen() {

    if (!document.fullscreenElement) {

        document.documentElement.requestFullscreen();

    }
    else {

        document.exitFullscreen();

    }

}

setInterval(function () {

    $(".timer").each(function () {

        let seconds = $(this).data("seconds");

        seconds++;

        $(this).data("seconds", seconds);

        $(this).text(formatTime(seconds));

    });

}, 1000);