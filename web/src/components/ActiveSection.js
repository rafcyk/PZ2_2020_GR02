import React, { useEffect } from 'react';
import '../styles/ActiveSection.css';

function getElement() {
    const sections = document.getElementsByTagName('section');

    const { scrollY } = window;
    for (const section of sections) {
        if (scrollY + 500 >= section.offsetTop && scrollY < section.offsetTop + section.offsetHeight - 500) {
            return section.className;
        }
    }
}


const ActiveSection = () => {


    function onScroll() {

        let listEl = document.querySelectorAll('li');

        const element = getElement();

        listEl.forEach(element => {
            element.removeAttribute('id');
        });

        console.log(listEl);

        switch (element) {
            case 'header':
                listEl[0].id = 'activeEl';
                break;

            case 'about':
                listEl[1].id = 'activeEl';
                break;

            case 'improve':
                listEl[2].id = 'activeEl';
                break;

            case 'promo':
                listEl[3].id = 'activeEl';
                break;

            case 'download':
                listEl[4].id = 'activeEl';
                break;

            case 'authors':
                listEl[5].id = 'activeEl';
                break;

            default:
                break;
        }
    }

    useEffect(() => {
        onScroll();
        window.addEventListener('scroll', onScroll);
        return (() => {
            window.removeEventListener('scroll', onScroll);
        })
    }, []);


    return (
        <ul className='activeSection'>
            <li>Strona główna</li>
            <li>O grze</li>
            <li>Technika</li>
            <li>Trailer</li>
            <li>Pobierz</li>
            <li>Autorzy</li>
        </ul>
    )
}

export default ActiveSection;