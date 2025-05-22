import { Route, Routes } from 'react-router-dom'
import { Navbar } from '../nav/navbar'
import { Jobs } from '../pages/Jobs'

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navbar />}>
        <Route path="/trabajos" element={<Jobs />} />
        <Route path="*" element={<h1>login</h1>} />
      </Route>
    </Routes>
  )
}

export default App
