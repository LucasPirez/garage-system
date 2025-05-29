import { SearchTable } from '../components/search-table/search-table'
import { RegisterJobProvider } from '../context/register-job-context'
import { RegisterJob } from '../components/jobs/register-job'

export const classNameInput =
  'w-full px-4 py-3 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200'

export const RegisterService = () => {
  return (
    <RegisterJobProvider>
      <section className="relative">
        <section className="absolute w-[70%] z-10 right-0">
          <SearchTable />
        </section>
        <RegisterJob />
      </section>
    </RegisterJobProvider>
  )
}
