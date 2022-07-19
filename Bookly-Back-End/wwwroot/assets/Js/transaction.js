$(".product-datas").hide();
$(".hide").hide();

$(".up").hide();
$(".down").click(function(){
    $(".product-datas").slideDown();
    $(".down").hide();
    $(".up").show();
    $(".show").hide();
    $(".hide").show();
});

$(".up").click(function(){
    $(".product-datas").slideUp();
    $(".down").show();
    $(".up").hide();
    $(".hide").hide();
    $(".show").show();
})