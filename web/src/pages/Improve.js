import React from 'react';
import '../styles/Improve.css';
import ship from '../images/shipImprove.png'

const Improve = () => {
    return(
        <section className = 'improve'>

             <div className = 'wrapImprove'>
                <img src={ship} alt="second alien ship"/>
                <p>Wybierz swój statek! Ulepszaj swoje pociski oraz zdobywaj coraz więcej punktów! Wymyślaj taktyki i niszcz coraz więcej wrogich potworów!</p>
            </div>
        </section>
    )
}

export default Improve;