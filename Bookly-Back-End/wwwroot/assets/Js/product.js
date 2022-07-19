let bookItem = document.querySelectorAll(".slider");
let sliders = document.querySelector(".sliders");
let count = 0;
let width;
let plus = document.querySelector(".plus");
let minus = document.querySelector(".minus");
let quantity = document.querySelector(".quantity");
let counter = 0;
let arrow = document.querySelector(".circle");
let image = document.querySelectorAll(".another");
 

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

plus.addEventListener("click",function(){
     counter++;
     quantity.innerHTML = counter;
     console.log(quantity.innerHTML)
});

minus.addEventListener("click",function(){
    counter--;

    if(counter <= 0){
        counter = 0;
    }

    quantity.innerHTML = counter;
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
