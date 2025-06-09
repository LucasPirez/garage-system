import { Route, Routes } from 'react-router-dom'
import { Navbar } from '../nav/navbar'
import { Jobs } from '../pages/Jobs'
import { RegisterPage } from '../pages/register'
import { Clients } from '../pages/Clients'
import { EditJob } from '../components/jobs/edit/edit-jobs'
import { ToastProvider } from '../context/toast-context'
import { ToastContainer } from '../components/toast/toast-container'

function App() {
  return (
    <ToastProvider>
      <ToastContainer />
      <Routes>
        <Route path="/" element={<Navbar />}>
          <Route path="/trabajos/:id" element={<EditJob />}></Route>
          <Route path="/trabajos" element={<Jobs />}></Route>
          <Route path="/registrar" element={<RegisterPage />} />
          <Route path="/clientes" element={<Clients />} />
          <Route path="*" element={<h1>404 not found</h1>} />
        </Route>
      </Routes>
    </ToastProvider>
  )
}

export default App
