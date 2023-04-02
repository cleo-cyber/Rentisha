const links = document.querySelector('.navlink');
const burger = document.querySelector('.burger');
const btns = document.querySelector('.btns')




//Event Listners

burger.addEventListener('click', slideNav);//open Navbar




//Funtions
function slideNav(e) {
    links.classList.toggle('open-nav')
    burger.classList.toggle('toggle')
    btns.classList.toggle('open-nav')
    links.style.animation = `fade`
}

