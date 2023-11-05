import './App.css';
import SignInForm from './components/SignInForm';
import SignUpForm from './components/SignUpForm';
import {Route, Routes} from 'react-router-dom'

function App() {
  return (
    <div>
      <Routes>
        <Route path='/auth/signin' element={<SignInForm/>}/>
        <Route path='/auth/signup' element={<SignUpForm/>}/>
      </Routes>
    </div>
  );
}

export default App;
