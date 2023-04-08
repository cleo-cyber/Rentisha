const accordion = document.querySelectorAll('.opener');
const openContent = document.querySelectorAll('.content');
accordion.forEach((accordion) => {
    accordion.addEventListener('click', () => {
        openContent.forEach((item) => {
            item.classList.toggle('open')
        })
    })
})
