
import React from 'react';
import '../styles/ImproveEn.css';
import ship from '../images/shipImprove.png'

const ImproveEn = () => {
    return(
        <section className = 'improveEn'>

             <div className = 'wrapImproveEn'>
                <img src={ship} alt="second alien ship"/>
                <p>Choose your ship! Improve your missiles and get more and more points! Invent tactics and destroy more and more enemy monsters</p>
            </div>
        </section>
    )
}

export default ImproveEn;