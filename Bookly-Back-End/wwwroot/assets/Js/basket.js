let messages = document.querySelector(".messages");
let message = document.querySelector(".text-message");
let loc = document.querySelector(".order-country");
let order = document.querySelector(".location-order");
let count = 0;

messages.addEventListener("click",function(){
    message.style.display = "block";
    this.style.display = "none";
});

loc.addEventListener("click",function(){
    count++;
    if(count % 2 == 0){
        order.style.display = "block";
    }
    else{
        order.style.display = "none";
    }
    
})

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