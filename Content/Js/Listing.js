const carousel = document.querySelector('.carousel');

let isDragging = false, prevPageX, prevSrollLeft;

//Functions

const dragging = (e) => {
    if (!isDragging) return
    e.preventDefault() //prevents image from being dragged
    carousel.scrollLeft = e.pageX;

}

const dragStart = (e) => {
    isDragging = true;
    prevPageX = e.pageX;
    prevSrollLeft = carousel.scrollLeft;
}
const dragStop = () => {
    isDragging = false;

}

const reveal = () => {
    var reveals = document.querySelectorAll('.reveal');
    //Iterate through node List
    for (var i = 0; i < reveals.length; i++) {
        var windowHeight = window.innerHeight;

        //get position of the listings

        var revealTop = reveals[i].getBoundingClientRect().top;

        var revealpoint = 150;

        if (revealTop < windowHeight - revealpoint) {
            reveals[i].classList.add('.active');

        }
        else {
            reveals[i].classList.remove('.active');

        }
    }
}

//Event Listners

carousel.addEventListener('mousemove', dragging);
carousel.addEventListener('mousedown', dragStart);
carousel.addEventListener('mouseup', dragStop);


window.addEventListener('scroll', reveal);