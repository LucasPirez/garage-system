import { FILTER } from '../../../core/constants/filter-jobs-status'
import { JobsResponseDto } from '../../../core/dtos/vehicleEntry/jobs-response.dto'

export const CardJob = ({ job }: { job: JobsResponseDto }) => {
  return (
    <div
      key={job.id}
      className="bg-gray-100 rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow w-[370px]">
      <div className="p-4 border-b border-gray-100">
        <div className="flex justify-between items-start">
          <div>
            <h3 className="text-lg font-semibold text-gray-800">{job.cause}</h3>
            <div className="flex items-center mt-1">
              {/* <Car className="w-4 h-4 text-gray-500 mr-1" /> */}
              <p className="text-sm text-gray-600">
                {job.vehicle.model} ({job.vehicle.plate})
                <span
                  className={`inline-block ml-2 px-2 py-1 text-xs font-medium rounded-full ${
                    job.status === FILTER.REALIZED
                      ? 'bg-green-100 text-green-800'
                      : job.status === FILTER.PENDING
                      ? 'bg-yellow-100 text-yellow-800'
                      : job.status === FILTER.CANCELLED
                      ? 'bg-blue-100 text-blue-800'
                      : 'bg-purple-100 text-purple-800'
                  }`}>
                  {job.status}
                </span>
              </p>
            </div>
          </div>
        </div>
      </div>
      <hr />
      <div className="p-4 space-y-3 -mt-2">
        <div className="flex items-start">
          {/* <User className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
          <div>
            <p className="text-xs text-gray-500">Cliente</p>
            <p className="text-sm">
              {job.client.firstName + job.client.lastName}
            </p>
            <p className="text-xs text-gray-500 mt-0.5">
              Placa: {job.vehicle.plate}
            </p>
          </div>
        </div>

        <div className="flex items-start">
          {/* <Calendar className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
          <div>
            <p className="text-xs text-gray-500">Fecha de ingreso</p>
            <p className="text-sm">{job.receptionDate.toLocaleString()}</p>
          </div>
        </div>

        <div className="flex items-start">
          {/* <DollarSign className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
          <div>
            <p className="text-xs text-gray-500">Presupuesto</p>
            <p className="text-sm">{job.budget}</p>
          </div>
        </div>

        <div className="flex items-start">
          {/* <Tool className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
          <div>
            <p className="text-xs text-gray-500">Repuestos</p>
            {/* <ul className="text-sm list-disc ml-4 mt-1">
              {job.parts.map((part, index) => (
                <li key={index}>{part}</li>
              ))}
            </ul> */}
          </div>
        </div>

        {job.details && (
          <div className="flex items-start">
            {/* <AlertTriangle className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
            <div>
              <p className="text-xs text-gray-500">Notas</p>
              <p className="text-sm">{job.details}</p>
            </div>
          </div>
        )}
      </div>
    </div>
  )
}
