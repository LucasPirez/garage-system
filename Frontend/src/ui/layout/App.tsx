import { Route, Routes } from 'react-router-dom'
import { Navbar } from '../nav/navbar'
import { Jobs } from '../pages/Jobs'
import { RegisterPage } from '../pages/register'
import { Clients } from '../pages/Clients'
import { EditJob } from '../components/jobs/edit/edit-jobs'
import { ToastProvider } from '../context/toast-context'
import { ToastContainer } from '../components/toast/toast-container'
import { PATHS } from '../../core/constants/paths'

function App() {
  return (
    <ToastProvider>
      <ToastContainer />
      <Routes>
        <Route path="/" element={<Navbar />}>
          <Route path={`${PATHS.JOBS}/:id `} element={<EditJob />}></Route>
          <Route path={`${PATHS.JOBS}`} element={<Jobs />}></Route>
          <Route path={`${PATHS.REGISTER}`} element={<RegisterPage />} />
          <Route path={`${PATHS.CLIENTS}`} element={<Clients />} />
          <Route path="*" element={<h1>404 not found</h1>} />
        </Route>
      </Routes>
    </ToastProvider>
  )
}

export default App
