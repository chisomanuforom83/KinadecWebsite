//// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
//// Toggle the menu visibility when clicking the hamburger icon
////document.getElementById('hamburger').addEventListener('click', function () {
////    const navList = document.getElementById('nav-list');
////    navList.classList.toggle('active');
////});




//// Get references to elements
//const hamburger = document.getElementById('hamburger');
//const navList = document.getElementById('nav-list');

//// Toggle the active class to show or hide the nav list
//hamburger.addEventListener('click', () => {
//    navList.classList.toggle('active');
//});





//let currentSlide = 0;
//const slides = document.querySelectorAll('.slide');

//// Show the current slide
//function showSlide(index) {
//    slides.forEach((slide, i) => {
//        slide.classList.toggle('active', i === index);
//    });
//}

//// Go to next slide
//function nextSlide() {
//    currentSlide = (currentSlide + 1) % slides.length;
//    showSlide(currentSlide);
//}

//// Go to previous slide
//function prevSlide() {
//    currentSlide = (currentSlide - 1 + slides.length) % slides.length;
//    showSlide(currentSlide);
//}

//// Auto-slide every 4 seconds
//setInterval(nextSlide, 4000);

//// Initialize first slide
//showSlide(currentSlide);




//const slider = document.querySelector('.slider');
//let currentIndex = 0;

//function showSlide(index) {
//    const slideWidth = document.querySelector('.banner-slider').offsetWidth;
//    slider.style.transform = `translateX(-${index * slideWidth}px)`;
//}

//// Example: slide every 5 seconds
//setInterval(() => {
//    currentIndex = (currentIndex + 1) % 3; // assuming 3 slides
//    showSlide(currentIndex);
//}, 5000);





    //const slider = document.querySelector('.slider');
    //const totalSlides = document.querySelectorAll('.slide').length;
    //let currentIndex = 0;

    //function showSlide(index) {
    //    const slideWidth = document.querySelector('.banner-slider').offsetWidth;
    //slider.style.transform = `translateX(-${index * slideWidth}px)`;
    //}

    //setInterval(() => {
    //    currentIndex = (currentIndex + 1) % totalSlides;
    //showSlide(currentIndex);
    //}, 4000); // change slide every 4 seconds

    //// Optional: handle window resize
    //window.addEventListener('resize', () => {
    //    showSlide(currentIndex);
    //});



let slideIndex = 0;
const slides = document.querySelector('.slider');
const allSlides = document.querySelectorAll('.slide');
const dots = document.querySelectorAll('.dot');
const slideCount = allSlides.length;
let autoSlideInterval;
const autoSlideDelay = 3000; // Time in milliseconds between slides

function showSlides(n) {
    if (n >= slideCount) {
        slideIndex = 0;
    } else if (n < 0) {
        slideIndex = slideCount - 1;
    } else {
        slideIndex = n;
    }

    slides.style.transform = `translateX(-${slideIndex * 100}%)`;

    // Update active dot
    dots.forEach(dot => dot.classList.remove('active'));
    dots[slideIndex].classList.add('active');
}

function plusSlides(n) {
    clearInterval(autoSlideInterval); // Stop auto-sliding on manual navigation
    showSlides(slideIndex + n);
    startAutoSlide(); // Restart auto-sliding
}

function currentSlide(n) {
    clearInterval(autoSlideInterval); // Stop auto-sliding on manual navigation
    showSlides(n - 1);
    startAutoSlide(); // Restart auto-sliding
}

function startAutoSlide() {
    autoSlideInterval = setInterval(() => {
        plusSlides(1);
    }, autoSlideDelay);
}

// Initialize the slider
showSlides(slideIndex);
startAutoSlide();
