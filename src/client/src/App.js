import './App.css';
import { Route, Routes } from 'react-router-dom'
import { Products } from './pages/Products/Products';
import SignIn from './pages/SignIn';
import SignUp from './pages/SignUp';
import Messages from './pages/Messages/Messages';
import RequireAuth from './components/Auth/RequireAuth'
import Product from './components/Product/Product'

function App() {
  return (
    <div>
      <Routes>
        <Route path='/signin' element={<SignIn />} />
        <Route path='/signup' element={<SignUp />} />
        <Route path='/products' element={<RequireAuth><Products /></RequireAuth>} />
        <Route path='/messages' element={<RequireAuth><Messages /></RequireAuth>} />
        <Route path='products/:id' element={<RequireAuth><Product/></RequireAuth>}/>

        <Route path='*' element={<RequireAuth><Products /></RequireAuth>}/>

      </Routes>
    </div>
  );
}

export default App;
