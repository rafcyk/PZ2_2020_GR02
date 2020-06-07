import React from 'react';
import '../styles/Promo.css'

const Promo = () => {
    return(
        <section className = 'promo'>
            <div className="wrapPromo">
            <iframe width="80%" height="80%" src="https://www.youtube.com/embed/CPtQGRv7ySo" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
            </div>
        </section>
    )
}

export default Promo;