import React from 'react';
import '../styles/Download.css';

const Download = () => {
    return (
        <section className='download'>
            <div className="wrapDownload">
                <p>Wybierz swoją wersję</p>
                <div className = 'wrapButtons'>
                    <a className="mobile" href = "https://mega.nz/file/8mgT0BJI#bTWdCJcpiTclulFPnZj6rtDPWzbputPCRe-Y0XPMvAs" target = "_blank">Android</a>
                    <a className="windows" href = "https://mega.nz/file/ZuIhCa5R#lEHNpYa-7mKSbcSMrdMS9Xut_g2Cj9XtIA4xeP75kok" target = "_blank">Windows</a>
                </div>
            </div>
        </section>
    )
}

export default Download;