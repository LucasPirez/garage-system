import { SearchTable } from '../components/search-table/search-table'
import {
  RegisterJobProvider,
  useRegisterJobContext,
} from '../context/register-job-context'
import { RegisterJob } from '../components/jobs/register-job'

export const classNameInput =
  'w-full px-4 py-3 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200'

export const RegisterPage = () => {
  return (
    <RegisterJobProvider>
      <RegisterService />
    </RegisterJobProvider>
  )
}

export const RegisterService = () => {
  const {
    searchTable,
    handleVisibility,
    handleClientSelect,
    handleVehicleSelect,
  } = useRegisterJobContext()

  return (
    <section className="relative">
      <section className="absolute w-[70%] z-10 right-0">
        <SearchTable
          isVisible={searchTable}
          onVisibilityChange={handleVisibility}
          handleClientSelect={handleClientSelect}
          handleVehicleSelect={handleVehicleSelect}
        />
      </section>
      <RegisterJob />
    </section>
  )
}
