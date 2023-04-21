const accordion = document.querySelectorAll('.opener');
const openContent = document.querySelectorAll('.content');
accordion.forEach((accordion) => {
    accordion.addEventListener('click', () => {
        openContent.forEach((item) => {
            item.classList.toggle('open')
        })
    })
})


// Image Replace

const bigDetail = document.querySelector('#big-detail');
const smallImgs = document.querySelectorAll('.smaller-detail-img img');

smallImgs[0].addEventListener('click', () => {
    bigDetail.src = smallImgs[0].src
})
smallImgs[1].addEventListener('click', () => {
    bigDetail.src = smallImgs[1].src
})
smallImgs[2].addEventListener('click', () => {
    bigDetail.src = smallImgs[2].src
})