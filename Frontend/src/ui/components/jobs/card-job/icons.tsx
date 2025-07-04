import { MessageCircleIcon } from 'lucide-react'
import { ButtonNavigate } from '../../buttons/button-navigate'
import { JobWithVehicleAndCustomerType } from '../../../../core/type/job'
import { lazy, Suspense } from 'react'

const DownloadPDF = lazy(() =>
  import('../../pdf/react-pdf').then((module) => ({
    default: module.DownloadPDF,
  }))
)

export const CardIcons = ({ job }: { job: JobWithVehicleAndCustomerType }) => {
  return (
    <div className="flex flex-col gap-5 items-center absolute right-3 top-4">
      <ButtonNavigate
        className=""
        label={
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
            className="w-5 h-5 text-blue-600 ">
            <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7" />
            <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z" />
          </svg>
        }
        data={job}
        path={job.id}
      />
      <Suspense fallback={<></>}>
        <DownloadPDF data={job} />
      </Suspense>
      {job.client.phoneNumber && (
        <button
          onClick={() => {
            const url = `https://web.whatsapp.com/send/?phone=${job.client.phoneNumber}`
            window.open(url, '_blank')
          }}>
          <MessageCircleIcon
            className="w-5 h-5  text-blue-600
                     hover:scale-110"
          />
        </button>
      )}
    </div>
  )
}
