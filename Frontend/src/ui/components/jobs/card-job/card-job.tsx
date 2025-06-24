import { useState } from 'react'
import { FILTER } from '../../../../core/constants/filter-jobs-status'
import { label_traduction } from '../../../../core/constants/label-traduction-status'
import { JobsResponseDto } from '../../../../core/dtos/vehicleEntry/jobs-response.dto'
import { JobType } from '../../../../core/type/job'
import { CardIcons } from './icons'
import { MoreHorizontal } from 'lucide-react'

export const CardJob = ({
  job,
  setIsModalOpen,
}: {
  job: JobsResponseDto
  setIsModalOpen: (data: JobType | null) => void
}) => {
  const [seeMore, setSeeMore] = useState(false)

  const handleSeeMore = () => {
    if (window.innerWidth > 640) return

    setSeeMore(!seeMore)
  }

  return (
    <>
      <div
        key={job.id}
        className={`relative bg-gray-100 rounded-lg shadow-md  hover:shadow-lg transition-shadow w-[340px] max-h-[450px]  overflow-auto ${
          !seeMore ? 'max-sm:max-h-[180px] max-sm:overflow-hidden' : ''
        }`}
        onClick={handleSeeMore}>
        <div className="p-4 relative">
          <CardIcons job={job} />
          <div className="flex justify-between items-start ">
            <div>
              <h3 className="text-lg font-semibold text-gray-800">
                {job.cause}
              </h3>
              <div className="flex items-center justify-between mt-1  w-[270px] max-w-auto">
                {/* <Car className="w-4 h-4 text-gray-500 mr-1" /> */}
                <p className="text-sm text-gray-600">
                  {job.vehicle.model} ({job.vehicle.plate})
                </p>
                <span
                  onClick={() => setIsModalOpen(job)}
                  className={`inline-block ml-2 px-3 py-1 text-xs font-medium cursor-pointer rounded-full ${
                    job.status === FILTER.REALIZED
                      ? 'bg-green-100 text-green-800'
                      : job.status === FILTER.PENDING
                      ? 'bg-yellow-100 text-yellow-800'
                      : 'bg-purple-100 text-purple-800'
                  }`}>
                  {label_traduction[job.status]}
                </span>
              </div>
            </div>
          </div>
        </div>
        <div className="mr-14 h-[1px] bg-gray-300"></div>
        <div className="p-4 space-y-3 -mt-2">
          <div className="flex items-start">
            {/* <User className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
            <div>
              <p className="text-xs text-gray-500">Cliente</p>
              <p className="text-sm">
                {job.client.firstName + ' ' + job.client.lastName}
              </p>
            </div>
          </div>

          <div className="flex items-start">
            {/* <Calendar className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
            <div>
              <p className="text-xs text-gray-500">Fecha de ingreso</p>
              <p className="text-sm">
                {job.receptionDate.toLocaleString().split('T')[0]}
              </p>
            </div>
          </div>

          <div className="flex items-start">
            {/* <DollarSign className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
            <div>
              <p className="text-xs text-gray-500">Presupuesto</p>
              <p className="text-sm">$ {job.budget}</p>
            </div>
          </div>

          <div className="flex items-start">
            {/* <Tool className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
            <div>
              <p className="text-xs text-gray-500">Repuestos</p>
              <ul className="text-sm list-disc ml-4 mt-1">
                {job.spareParts.map((part, index) => (
                  <li key={index + part.name + part.price}>
                    <span>x{part.quantity} </span>
                    <span>
                      {part.name} {'  '}
                    </span>{' '}
                    <span>${part.price.toLocaleString('Es-es')} </span>
                  </li>
                ))}
              </ul>
            </div>
          </div>
          {job.details && (
            <div className="flex items-start">
              {/* <AlertTriangle className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
              <div>
                <p className="text-xs text-gray-500">Detalles</p>
                <p className="text-sm">{job.details}</p>
              </div>
            </div>
          )}
        </div>
        {!seeMore ? (
          <MoreHorizontal
            className="absolute bottom-0 right-40 sm:opacity-0"
            opacity={0.6}
          />
        ) : (
          ''
        )}
      </div>
    </>
  )
}
