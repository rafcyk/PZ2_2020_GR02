import React from 'react';
import '../styles/Download.css';

const Download = () => {
    return (
        <section className='download'>
            <div className="wrapDownload">
                <p>Wybierz swoją wersję</p>
                <div className = 'wrapButtons'>
                    <a className="mobile" href = "https://mega.nz/file/cuR1WShS#6FwHtgfrCN5Ui8w1c5rstGTGMbVj5fDb3II4_Mc4K0w" target = "_blank">Android</a>
                    <a className="windows" href = "https://mega.nz/file/ZuIhCa5R#lEHNpYa-7mKSbcSMrdMS9Xut_g2Cj9XtIA4xeP75kok" target = "_blank">Windows</a>
                </div>
            </div>
        </section>
    )
}

export default Download;