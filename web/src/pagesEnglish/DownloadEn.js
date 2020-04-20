import React from 'react';
import '../styles/DownloadEn.css';

const DownloadEn = () => {
    return (
        <section className='downloadEn'>
            <div className="wrapDownloadEn">
                <p>
                    Choose your version</p>
                <div className='wrapButtons'>
                    <button className="mobile">Android</button>
                    <button className="windows">Windows</button>
                </div>
            </div>

        </section>
    )
}

export default DownloadEn;