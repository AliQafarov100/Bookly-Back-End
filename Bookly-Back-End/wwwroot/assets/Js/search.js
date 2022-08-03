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