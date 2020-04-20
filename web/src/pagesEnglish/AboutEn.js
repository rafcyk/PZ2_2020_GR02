import React from 'react';
import '../styles/AboutEn.css';
import AlienShip from '../images/AlienShip.png'

const AboutEn = () => {
    return (
        <section className='aboutEn'>
            <div className='wrapAboutEn'>
                <img src={AlienShip} alt="alien ship" />
                <p>
                    Suddenly you move to space! Survive by destroying your enemies. Be faster and more efficient than others. Enter our wonderful world of fun! How long will you survive?</p>
            </div>
        </section>
    )
}

export default AboutEn;