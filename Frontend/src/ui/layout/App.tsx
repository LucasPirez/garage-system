import { Route, Routes } from 'react-router-dom'
import { Navbar } from '../nav/navbar'
import { Jobs } from '../pages/Jobs'
import { RegisterPage } from '../pages/register'
import { Clients } from '../pages/Clients'

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navbar />}>
        <Route path="/trabajos" element={<Jobs />} />
        <Route path="/registrar" element={<RegisterPage />} />
        <Route path="/clientes" element={<Clients />} />
        <Route path="*" element={<h1>404 not found</h1>} />
      </Route>
    </Routes>
  )
}

export default App
