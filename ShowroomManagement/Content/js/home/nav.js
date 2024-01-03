function updateCartItemCount(count) {
    var cartItemCountSpan = document.getElementById('CartItemCount');

    if (cartItemCountSpan) {
        cartItemCountSpan.textContent = count;
    }
}
function addToCart() {
    var currentCount = parseInt(document.getElementById('CartItemCount').textContent);

    var newCount = currentCount + 1;

    updateCartItemCount(newCount);
}
$(function () {
    $("#logoutLink").click(function (e) {
        e.preventDefault();
        $.post("@Url.Action("Logout","Customers")", function (res) {
            if (res.status === "done") {
                //close the window now.
                window.location.href = "~/Customers/Login";
            }
        });
    });

});