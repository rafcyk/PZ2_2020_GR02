import React from "react";
import {Link} from "react-scroll";
import '../styles/Stars.css';
import SpaceShip from '../images/SpaceShip.png';

const Stars = () => {

    const starsTop = [];
    const starsBottom = [];


    for(let i = 0; i < 10;i ++){
        starsTop.push(<div style = {{
            left: Math.floor(Math.random() * 100) + '%',
            animationDelay: Math.random() + 's',
        }}
        
        className = 'starTop'></div>)

    }

    for(let i = 0; i < 10;i ++){
        starsBottom.push(<div style = {{
            right: Math.floor(Math.random() * 100) + '%',
            animationDelay: Math.random() + 's',
        }}
        
        className = 'starBottom'></div>)
    }

    return(
        <>
            {starsTop}
            {starsBottom}
            <img src={SpaceShip} alt="Space Ship" className = 'spaceShip'/>
            <Link activeClass="active" to="download" spy={true} smooth={true} offset={-70} duration={700} className = 'downloadSection'>
                Pobierz
            </Link>
        </>
    )
}

export default Stars;