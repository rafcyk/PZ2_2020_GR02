import React from 'react';
import '../styles/Download.css';

const Download = () => {
    return (
        <section className='download'>
            <div className="wrapDownload">
                <p>Wybierz swoją wersję</p>
                <div className = 'wrapButtons'>
                    <button className="mobile">Android</button>
                    <button className="windows">Windows</button>
                </div>
            </div>
        </section>
    )
}

export default Download;