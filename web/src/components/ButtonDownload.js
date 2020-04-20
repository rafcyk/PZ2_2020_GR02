import React from "react";
import {Link} from "react-scroll";
import '../styles/ButtonDownload.css';

const Stars = (props) => {


    return(
        <>
            <Link activeClass="active" to="download" spy={true} smooth={true} offset={-70} duration={700} className = 'downloadSection'>
                Pobierz
            </Link>
        </>
    )
}

export default Stars;