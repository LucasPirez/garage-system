import { FILTER } from '../../../../core/constants/filter-jobs-status'
import { label_traduction } from '../../../../core/constants/label-traduction-status'
import {
  JobWithVehicleAndCustomerType,
  JobWithVehicleType,
} from '../../../../core/type/job'
import { CardIcons } from './icons'
import { ContainerCard } from './container-card'
import { CardDataJob } from './card-data-job'

export const CardJob = ({
  job,
  setIsModalOpen,
}: {
  job: JobWithVehicleAndCustomerType
  setIsModalOpen: (data: JobWithVehicleType | null) => void
}) => {
  return (
    <ContainerCard key={job.id}>
      <div className="p-4 relative">
        <CardIcons job={job} />
        <div className="flex justify-between items-start ">
          <div>
            <h3 className="text-lg font-semibold text-gray-800 mr-4">
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

        <CardDataJob job={job} />
      </div>
    </ContainerCard>
  )
}
