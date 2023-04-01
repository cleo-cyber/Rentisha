const aside = document.querySelector("aside");

const menuBtn = document.querySelector("#menu-btn");

const closeBtn = document.querySelector("#close-btn");

const themeChange = document.querySelector(".theme-toggler");

// Close sidebar
menuBtn.addEventListener('click', () => {
    aside.style.display = "block"
})


// Open Side Br
closeBtn.addEventListener('click', () => {
    aside.style.display = 'none'
})

// Change theme color

themeChange.addEventListener('click', () => {
    document.body.classList.toggle('dark-themes')
})