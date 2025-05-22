import { useState } from 'react'
import { JobsFilterType } from '../../core/type/jobs-filter'
import { FILTER } from '../../core/constants/filter-jobs-status'
import { ButtonFilterJobs } from '../components/button-filter-jobs'

export const Jobs = () => {
  const [statusFilter, setStatusFilter] = useState<JobsFilterType>(FILTER.ALL)

  return (
    <>
      <div className="flex flex-col md:flex-row justify-between items-start md:items-center mb-6 gap-4">
        <h2 className="text-xl font-semibold text-gray-800">
          Listado de Trabajos
        </h2>

        <div className="flex flex-col sm:flex-row gap-3">
          <div className="inline-flex rounded-md shadow-sm bg-red-50 ">
            {Object.entries(FILTER).map(([ke, value], i) => (
              <ButtonFilterJobs
                onClick={() => setStatusFilter(value)}
                key={i + ke}
                label={value}
                status={statusFilter}
              />
            ))}
          </div>

          <button className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors">
            Nuevo Trabajo
          </button>
        </div>
      </div>
    </>
  )
}
