import React from 'react';
import '../styles/AuthorsEn.css';
import authorFirst from '../images/author.png';
import authorSecond from '../images/authorSecond.png';

const AuthorsEn = () => {
    return (
        <section className='authorsEn'>
            <div className="wrapAuthorsEn">
                <div className="authorEn">
                    <img src={authorFirst} alt="" />
                    <p className="name">Kacper Buczkowski</p>
                    <p className="desc">
                        The main C # programmer responsible for the Android application.</p>
                </div>
                <div className="authorEn">
                    <img src={authorSecond} alt="" />
                    <p className="name">Wiesław Bikowski</p>
                    <p className="desc">
                        The originator and C # programmer responsible for the Windows application and the author of graphics for the game.</p>
                </div>
            </div>
        </section>
    )
}

export default AuthorsEn;