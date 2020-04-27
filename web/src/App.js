import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import './styles/App.css';

//polish components
import Stars from './components/Stars';
import Header from './pages/Header';
import About from './pages/About';
import Improve from './pages/Improve';
import Promo from './pages/Promo';
import Authors from './pages/Authors';
import Footer from './pages/Footer';
import Download from './pages/Download';
import ActiveSection from './components/ActiveSection';
import Language from './components/Language';
import ButtonDownload from './components/ButtonDownload';
//english components
import HeaderEn from './pagesEnglish/HeaderEn';
import AboutEn from './pagesEnglish/AboutEn';
import ImproveEn from './pagesEnglish/ImproveEn';
import PromoEn from './pagesEnglish/PromoEn';
import AuthorsEn from './pagesEnglish/AuthorsEn';
import FooterEn from './pagesEnglish/FooterEn';
import DownloadEn from './pagesEnglish/DownloadEn';
import ActiveSectionEn from './components/ActiveSectionEn';
import ButtonDownloadEn from './components/ButtonDownloadEn';



class App extends React.Component {



  render() {



    return (
      <Router basename = {process.env.PUBLIC_URL}>
        <Language />

        <Stars/>

        <Switch>


          <Route path="/" exact>
            <main>

              <div className='wrapMain'>


              <ButtonDownload/>

                <ActiveSection />

                <Header />

                <About />

                <Improve />

                <Promo />

                <Download />

                <Authors />

                <Footer />

              </div>

            </main>
          </Route>

          <Route path="/english" exact>
            <main>
              <main>

                <div className='wrapMainEn'>

                <ButtonDownloadEn/>


                  <ActiveSectionEn />

                  <HeaderEn />

                  <AboutEn/>

                  <ImproveEn />

                  <PromoEn />

                  <DownloadEn />

                  <AuthorsEn />

                  <FooterEn />

                </div>

              </main>
            </main>
          </Route>

        </Switch>


      </Router>
    );
  }

}

export default App;
