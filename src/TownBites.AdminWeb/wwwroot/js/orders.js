let connection = null;

async function startSignalR(restaurantId) {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:5001/hubs/orders")
        .withAutomaticReconnect()
        .build();

    connection.on("NewOrder", function (order) {

        toastr.success(`New Order #${order.id}`);

        refreshOrders();

        refreshDashboard();

        playNotification();
    });

    connection.on("OrderUpdated", function (order) {

        toastr.info(`Order #${order.id} updated`);

        refreshOrders();

        refreshDashboard();
    });

    connection.onreconnected(() => {
        connection.invoke("JoinRestaurant", restaurantId.toString());
    });

    await connection.start();

    await connection.invoke(
        "JoinRestaurant",
        restaurantId.toString());

    console.log("SignalR Connected");
}

function playNotification() {

    const audio = new Audio("/sounds/new-order.mp3");

    audio.play().catch(() => {
        // Browser may block autoplay until user interaction
    });

}

function refreshOrders() {

    location.reload();

}

function refreshDashboard() {

}