
let bookItem = document.querySelectorAll(".slider");
let sliders = document.querySelector(".sliders");
let count = 0;
let width;
let arrow = document.querySelector(".circle");
let list = document.querySelector(".list");
let book = document.querySelector(".row .booking");
let grid = document.querySelector(".grid");

list.addEventListener("click",function(){
    book.classList.remove("row");
});

grid.addEventListener("click",function(){
    book.classList.add("row");
});

window.addEventListener("scroll",function(){
    if(window.scrollY == 0){
        arrow.style.opacity = "0";
    }
    else{
        arrow.style.opacity = "1";
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

function init(){
    
    width = document.querySelector(".sliderItem").offsetWidth;
    sliders.style.width = width * bookItem.length + 'px';
    bookItem.forEach(item => {
        item.style.width = width + 'px';
    });

    rollSlider();
}

init();
window.addEventListener('resize',init);

document.querySelector('.rightBtn').addEventListener("click",function(){
    count++;
    if(count >= bookItem.length){
        count = 0;
    }

    rollSlider();
});

document.querySelector('.leftBtn').addEventListener("click",function(){
    count--;
    if(count < 0){
        count = bookItem.length - 1;
    }

    rollSlider();
})

function rollSlider(){
    sliders.style.transform = 'translate(-' + count * width + 'px)';
}

$(".history-collection").hide();
$(".fantasy-collection").hide();
$(".cook-collection").hide();
$(".romance-collection").hide();

$(".hist-plus").click(function(){
    $(".history-collection").slideDown(); 
    $(".hist-plus").css("display","none");  
    console.log("plus")
})
$(".hist-minus").click(function(){
    $(".history-collection").slideUp();
    $(".hist-plus").css("display","block");
})

$(".fantasy-plus").click(function(){
    $(".fantasy-collection").slideDown(); 
    $(".fantasy-plus").css("display","none");  
})
$(".fantasy-minus").click(function(){
    $(".fantasy-collection").slideUp(); 
    $(".fantasy-plus").css("display","block");  
})
$(".cook-plus").click(function(){ 
    $(".cook-collection").slideDown(); 
    $(".cook-plus").css("display","none");  
})
$(".cook-minus").click(function(){ 
    $(".cook-collection").slideUp(); 
    $(".cook-plus").css("display","block");  
})
$(".romance-plus").click(function(){
    
    $(".romance-collection").slideDown(); 
    $(".romance-plus").css("display","none");  
})
$(".romance-minus").click(function(){
    
    $(".romance-collection").slideUp(); 
    $(".romance-plus").css("display","block");  
})