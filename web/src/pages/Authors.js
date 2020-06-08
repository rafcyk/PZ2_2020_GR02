import React from 'react';
import '../styles/Authors.css';
import authorFirst from '../images/author.png';
import authorSecond from '../images/authorSecond.png';

const Authors = () => {
    return(
        <section className = 'authors'>
            <div className="wrapAuthors">
                <div className="author">
                    <img src={authorFirst} alt=""/>
                    <p className="name">Kacper Buczkowski</p>
                    <p className="desc">Główny programista C# odpowiadający za aplikację na systemy Android.</p>
                </div>
                <div className="author">
                <img src={authorSecond} alt=""/>
                <p className="name">Wiesław Bikowski</p>
                <p className="desc">Pomysłodawca oraz programista C# odpowiadający za aplikację na system Windows oraz autor grafik do gry.</p>
                </div>
            </div>
        </section>
    )
}

export default Authors;