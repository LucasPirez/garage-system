import { useEffect, useState } from 'react'
import { JobsFilterType } from '../../core/type/jobs-filter'
import { FILTER } from '../../core/constants/filter-jobs-status'
import { ButtonFilterJobs } from '../components/common/button-filter-jobs'
import { CardJob } from '../components/jobs/card-job'

import { workshopService } from '../../core/services'
import { JobsResponseDto } from '../../core/dtos/vehicleEntry/jobs-response.dto'

export const Jobs = () => {
  const [statusFilter, setStatusFilter] = useState<JobsFilterType>(FILTER.ALL)
  const [jobs, setJobs] = useState<JobsResponseDto[] | null>(null)
  const [jobsFilter, setJobsFilter] = useState<JobsResponseDto[]>([])

  useEffect(() => {
    // eslint-disable-next-line no-extra-semi
    ;(async () => {
      try {
        const result = await workshopService.getAllVehicleEntries()

        setJobs(result)
        setJobsFilter(result)
      } catch (error) {
        alert('Error')
      }
    })()
  }, [])

  const handleFilter = (value: JobsFilterType) => {
    setStatusFilter(value)

    const filteredJobs =
      jobs?.filter((k) => k.status === value || value === FILTER.ALL) ?? []

    setJobsFilter(filteredJobs)
  }

  return (
    <>
      <div className="flex flex-col md:flex-row justify-between items-start md:items-center mb-6 mx-6 gap-4">
        <h2 className="text-xl font-semibold text-gray-800">
          Listado de Trabajos
        </h2>

        <div className="flex flex-col sm:flex-row gap-3 ">
          <div className="inline-flex rounded-md shadow-sm bg-red-50 ">
            {Object.entries(FILTER).map(([ke, value], i) => (
              <ButtonFilterJobs
                onClick={() => handleFilter(value)}
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

      <div className="flex flex-wrap justify-center gap-4 ">
        {jobsFilter.map((job) => (
          <CardJob job={job} />
        ))}
      </div>

      {/* {filteredJobs.length === 0 && (
          <div className="text-center py-12">
            <p className="text-gray-500">
              No hay trabajos{' '}
              {statusFilter !== 'todos' ? `con estado "${statusFilter}"` : ''}{' '}
              para mostrar.
            </p>
          </div>
        )} */}
    </>
  )
}
