////let updates = document.querySelectorAll(".update");

////updates.forEach(update => {
////    update.addEventListener("click", function (e) {
////        e.preventDefault();
////        var data = document.getElementById("stock").value;
////        let link = update.getAttribute("href") + "/?count=" + data
////        fetch(link).then(resp => resp.json()).then(datas => {
////            if (datas.status == 200) {
////                location.href = "https://localhost:44339/Basket/Cart";
////            }
////        })
////    })
////})

$(document).ready(function () {
    $(".search-section").hide();

    $(".show").click(function (event) {
        event.preventDefault();
        $(".search-section").slideDown();
    })
    $(".remove").click(function () {
        $(".search-section").slideUp();
    })
})