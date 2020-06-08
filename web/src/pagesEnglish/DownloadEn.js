import React from 'react';
import '../styles/DownloadEn.css';

const DownloadEn = () => {
    return (
        <section className='downloadEn'>
            <div className="wrapDownloadEn">
                <p>
                    Choose your version</p>
                <div className='wrapButtons'>
                    <a className="mobile" href = "https://mega.nz/file/8mgT0BJI#bTWdCJcpiTclulFPnZj6rtDPWzbputPCRe-Y0XPMvAs" target = "_blank">Android</a>
                    <a className="windows" href = "https://mega.nz/file/ZuIhCa5R#lEHNpYa-7mKSbcSMrdMS9Xut_g2Cj9XtIA4xeP75kok" target = "_blank">Windows</a>
                </div>
            </div>

        </section>
    )
}

export default DownloadEn;