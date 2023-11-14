import './App.css';
import Product from './components/Product';
import ProductCreationForm from './components/ProductCreationForm';
import SignInForm from './components/SignInForm';
import SignUpForm from './components/SignUpForm';
import {Route, Routes} from 'react-router-dom'
import User from './components/User';
import UsersList from './components/UsersList';
import { Products } from './pages/Products';
import ChatsList from './components/ChatsList';

function App() {
  return (
    <div>
      <Routes>
        <Route path='/auth/signin' element={<SignInForm/>}/>
        <Route path='/auth/signup' element={<SignUpForm/>}/>

        <Route path='/products' element={<Products/>}/>
        <Route path='/productCreation' element={<ProductCreationForm/>}/>
        <Route path='/products/:id' element={<Product/>}/>

        <Route path='/users' element={<UsersList/>}/>
        <Route path='/users/:id' element = {<User/>}/>

        <Route path='/chats' element={<ChatsList/>}/>
        
      </Routes>
    </div>
  );
}

export default App;
