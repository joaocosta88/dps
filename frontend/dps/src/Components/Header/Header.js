import Navbar from 'react-bootstrap/Navbar';
import MainNavigationSection from './MainNavigationSection';
import LoggedInSection from './LoggedInSection';

function Header() {
  return (
    <Navbar expand="lg" className="bg-body-tertiary">
        
        <MainNavigationSection />
        <LoggedInSection />
    </Navbar>
  );
}

export default Header;