import React from 'react';
import '../styles/About.css';
import AlienShip from '../images/AlienShip.png'

const About = () => {
    return(
        <section className = 'about'>
            <div className = 'wrapAbout'>
                <img src={AlienShip} alt="alien ship"/>
                <p>Nagle przenosisz się kosmos! Przeżyj niszcząc swoich wrogów. Bądź szybszy i sprawniejszy od innych. Wejdź w nasz wspaniały świat zabawy! Jak długo uda Ci się przeżyć?</p>
            </div>
        </section>
    )
}

export default About;