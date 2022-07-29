let bookItem = document.querySelectorAll(".slider");
let sliders = document.querySelector(".sliders");
let count = 0;
let width;
let plus = document.querySelectorAll(".plus");
let minus = document.querySelectorAll(".minus");
let quantity = document.querySelector(".quantity");
let counter = 0;
let arrow = document.querySelector(".circle");
let image = document.querySelectorAll(".another");
let formats = document.querySelectorAll(".selectFormat");


formats.forEach(format => {
    format.addEventListener("click", function () {
        let id = format.getAttribute("id");
        format.style.border = "1px solid red";
        format.classList.add("text-danger");
    })
})
 

image.forEach(images => {
    images.addEventListener("click",function(){
        let id = images.getAttribute("data-id");
        let mainImage = document.querySelectorAll(".img-zoom");

        mainImage.forEach(mains => {
            let mainId = mains.getAttribute("data-id");
    
            if(mainId == id){
                mains.classList.add("active")
            }
            else{
                mains.classList.remove("active");
            }
        })
       
    })
})
window.addEventListener("scroll",function(){
    if(window.scrollY == 0){
        arrow.style.opacity = "0";
    }
    else{
        arrow.style.opacity = "1";
    }
})

plus.forEach(pluses => {
    pluses.addEventListener("click", function () {
        counter++;
        quantity.innerHTML = counter;
        
    });
})

minus.forEach(minuses => {
    minuses.addEventListener("click", function () {
        counter--;

        if (counter <= 0) {
            counter = 0;
        }

        quantity.innerHTML = counter;
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

document.querySelector(".rightBtn").addEventListener("click",function(){
    count++;

    if(count >= bookItem.length){
        count = 0;
    }

    rollSlider();
});

document.querySelector(".leftBtn").addEventListener("click",function(){
    count--;

    if(count < 0){
        count = bookItem.length - 1;
    }

    rollSlider();
})

function rollSlider(){
    sliders.style.transform = 'translate(-'+ count * width + 'px)';
}
