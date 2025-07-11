import { useEffect, useState } from 'react'
import { JobsFilterType } from '../../core/type/jobs-filter'
import { FILTER } from '../../core/constants/filter-jobs-status'
import { ButtonFilterJobs } from '../components/buttons/button-filter-jobs'
import { CardJob } from '../components/jobs/card-job/card-job'

import { workshopService } from '../../core/services'
import { ModalStatus } from '../components/modal/modal-status'
import {
  JobWithVehicleAndCustomerType,
  JobWithVehicleType,
} from '../../core/type/job'
import { JobStatusType } from '../../core/constants/jobs-status'
import withAuth from '../components/hoc/with-auth'
import { label_traduction } from '../../core/constants/label-traduction-status'
import { useToast } from '../context/toast-context'
import { useLoader } from '../context/loader-context'

const Jobs = () => {
  const [statusFilter, setStatusFilter] = useState<JobsFilterType>(FILTER.ALL)
  const [jobs, setJobs] = useState<JobWithVehicleAndCustomerType[] | null>(null)
  const [jobsFilter, setJobsFilter] = useState<JobWithVehicleAndCustomerType[]>(
    []
  )
  const [jobModal, setJobModal] = useState<JobWithVehicleType | null>(null)
  const [seeMore, setSeeMore] = useState(6)
  const { addToast } = useToast()
  const { showLoader, hideLoader } = useLoader()

  useEffect(() => {
    // eslint-disable-next-line no-extra-semi
    ;(async () => {
      try {
        showLoader()
        const result = await workshopService.getAllVehicleEntries()

        setJobs(result)
        setJobsFilter(result)
      } catch (error) {
        addToast({
          severity: 'error',
          message: 'Intente mas tarde',
          title: 'Error',
        })
      } finally {
        hideLoader()
      }
    })()
  }, [])

  const handleFilter = (value: JobsFilterType) => {
    setStatusFilter(value)

    const filteredJobs =
      jobs?.filter((k) => k.status === value || value === FILTER.ALL) ?? []

    setJobsFilter(filteredJobs)
  }

  const handleVisibilityModal = (data: JobWithVehicleType | null) => {
    setJobModal(data)
  }

  const handleJobModalState = ({
    id,
    finalAmount,
    status,
  }: {
    status: JobStatusType
    finalAmount: number
    id: string
  }) => {
    const jobsChange =
      jobs?.map((job) => {
        if (job.id !== id) return job

        return {
          ...job,
          status,
          finalAmount,
        }
      }) ?? null
    setJobs(jobsChange)

    const filteredJobs =
      jobsFilter?.map((job) => {
        if (job.id !== id) return job

        return {
          ...job,
          status,
          finalAmount,
        }
      }) ?? null

    setJobsFilter(filteredJobs)
    setJobModal(null)
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
        </div>
      </div>
      {jobModal && (
        <ModalStatus
          closeModal={handleVisibilityModal}
          job={jobModal}
          handleJobState={handleJobModalState}
        />
      )}
      <div className="flex flex-wrap justify-center gap-4 ">
        {jobsFilter.map((job, index) => {
          if (index + 1 > seeMore) return
          return (
            <CardJob
              job={job}
              setIsModalOpen={handleVisibilityModal}
              key={job.id}
            />
          )
        })}
      </div>
      {seeMore < jobsFilter.length ? (
        <div className="w-full flex justify-center">
          <button
            className="m-auto px-10 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors active:scale-95 text-lg my-8"
            onClick={() => setSeeMore(seeMore + 6)}
          >
            Ver más
          </button>
        </div>
      ) : (
        ''
      )}

      {jobsFilter.length === 0 && (
        <div className="text-center py-12">
          <p className="text-gray-500">
            No hay trabajos "{label_traduction[statusFilter]}" para mostrar.
          </p>
        </div>
      )}
    </>
  )
}

const JobComponent = withAuth(Jobs)

export { JobComponent as Jobs }
