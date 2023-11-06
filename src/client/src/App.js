import './App.css';
import Product from './components/Product';
import ProductCreationForm from './components/ProductCreationForm';
import ProductsList from './components/ProductsList';
import SignInForm from './components/SignInForm';
import SignUpForm from './components/SignUpForm';
import {Route, Routes} from 'react-router-dom'

function App() {
  return (
    <div>
      <Routes>
        <Route path='/auth/signin' element={<SignInForm/>}/>
        <Route path='/auth/signup' element={<SignUpForm/>}/>

        <Route path='/products' element={<ProductsList/>}/>
        <Route path='/productCreation' element={<ProductCreationForm/>}/>
        <Route path='/products/:id' element={<Product/>}/>

      </Routes>
    </div>
  );
}

export default App;
