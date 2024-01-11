// Hàm cập nhật số lượng mục trong giỏ hàng
function updateCartItemCount(count) {
    var cartItemCountSpan = document.getElementById('CartItemCount');

    if (cartItemCountSpan) {
        cartItemCountSpan.textContent = count;
    }
}

// Hàm thêm vào giỏ hàng
function addToCart() {
    var currentCount = parseInt(document.getElementById('CartItemCount').textContent);

    var newCount = currentCount + 1;

    updateCartItemCount(newCount);
}

// Lấy thẻ menu và mega menu
var menuVehicles = document.getElementById('menuVehicles');
var megaMenuVehicles = document.getElementById('megaMenuVehicles');

// Hàm hiển thị mega menu
function showMenu() {
    megaMenuVehicles.classList.add('active');
}

// Hàm ẩn mega menu
function hideMenu() {
    megaMenuVehicles.classList.remove('active');
}

// Hàm toggle mega menu (click hoặc hover)
function toggleMenu() {
    megaMenuVehicles.classList.toggle('active');
}

// Thêm sự kiện click vào thẻ menu
menuVehicles.addEventListener('click', toggleMenu);

// Thêm sự kiện mouseenter và mouseleave để xử lý hover
menuVehicles.addEventListener('mouseenter', showMenu);
menuVehicles.addEventListener('mouseleave', hideMenu);

// Sử dụng jQuery để thêm sự kiện click cho đối tượng có id "logoutLink"
$(function () {
    $("#logoutLink").click(function (e) {
        e.preventDefault();
        $.post("@Url.Action("Logout","Customers")", function (res) {
            if (res.status === "done") {
                // Đóng cửa sổ hiện tại
                window.location.href = "~/Customers/Login";
            }
        });
    });
});
