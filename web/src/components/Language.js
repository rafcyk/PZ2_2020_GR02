import React from 'react';
import '../styles/Language.css';
import { NavLink } from 'react-router-dom';
import polish from '../images/pl.png';
import english from '../images/en.png';

const Language = () => {
    return (
        <>
            <div className='wrapLanguage'>
                <NavLink to = "/"><img src={polish} alt="polish"/></NavLink>
                <NavLink to = "english"><img src={english} alt="english"/></NavLink>
            </div>
        </>
    )
}

export default Language;