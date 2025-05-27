import { Route, Routes } from 'react-router-dom'
import { Navbar } from '../nav/navbar'
import { Jobs } from '../pages/Jobs'
import { RegisterService } from '../pages/register-service'

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navbar />}>
        <Route path="/trabajos" element={<Jobs />} />
        <Route path="/vehiculos" element={<RegisterService />} />
        <Route path="*" element={<h1>login</h1>} />
      </Route>
    </Routes>
  )
}

export default App
