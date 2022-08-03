let category = document.querySelector(".category");
let books = document.querySelector(".fluid");
let arrow = document.querySelector(".circle");


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
$(document).ready(function(){
    $(".all").show();
    $(".cook").hide();
    $(".history").hide();
    $(".fantacy").hide(); 
    $(".romance").hide();   
})

$(document).ready(function(){
    $(".all-category").click(function(){
        $(".all").show();
        $(".cook").hide();
        $(".history").hide();
        $(".fantacy").hide();
        $(".romance").hide();
    })
})

$(document).ready(function(){
    $(".cook-category").click(function(){
        $(".cook").show();
        $(".all").hide();
        $(".history").hide();
        $(".fantacy").hide();
        $(".romance").hide();
    })
})
$(document).ready(function(){
    $(".history-category").click(function(){
        $(".history").show();
        $(".cook").hide();
        $(".all").hide();
        $(".fantacy").hide();
        $(".romance").hide();
    })
})
$(document).ready(function(){
    $(".fantacy-category").click(function(){
        $(".fantacy").show();
        $(".cook").hide();
        $(".all").hide();
        $(".history").hide();
        $(".romance").hide();
    })
})

$(document).ready(function(){
    $(".romance-category").click(function(){
        $(".romance").show();
        $(".cook").hide();
        $(".all").hide();
        $(".history").hide();
        $(".fantacy").hide();
    })
})



$(document).ready(function () {
    $('.owl-one').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        dots: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 2
            }
        }
    });

    $('.owl-two').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 5
            }
        }
    });
})
