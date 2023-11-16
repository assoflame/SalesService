import './App.css';
import {Route, Routes} from 'react-router-dom'
import { Products } from './pages/Products/Products';
import SignIn from './pages/SignIn';
import SignUp from './pages/SignUp';
import Messages from './pages/Messages';

function App() {
  return (
    <div>
      <Routes>
        <Route path='/signin' element={<SignIn/>}/>
        <Route path='/signup' element={<SignUp/>}/>
        <Route path='/products' element={<Products/>}/>
        <Route path='/messages' element={<Messages/>}/>


        {/* <Route path='/productCreation' element={<ProductCreationForm/>}/>
        <Route path='/products/:id' element={<Product/>}/>

        <Route path='/users' element={<UsersList/>}/>
        <Route path='/users/:id' element = {<User/>}/>

        <Route path='/chats' element={<ChatsList/>}/> */}
        
      </Routes>
    </div>
  );
}

export default App;
