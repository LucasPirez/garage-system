import { useEffect, useState } from 'react'
import { JOBS_STATUS, JobStatusType } from '../../../core/constants/jobs-status'
import { JobWithVehicleType } from '../../../core/type/job'
import { classNameInput, classNameLabel } from '../../../core/constants/class-names'
import { status_translation } from '../../../core/constants/status-translation'
import { ModalPortal } from './modal-portal'
import { jobService } from '../../../core/services'
import { useToast } from '../../context/toast-context'
import { BaseModal } from './base-modal'

interface Props {
  closeModal: (data: JobWithVehicleType | null) => void
  job: JobWithVehicleType
  handleJobState: (data: {
    status: JobStatusType
    finalAmount: number
    id: string
  }) => void
}

interface ValueType {
  status: JobStatusType
  finalAmount: number
}

export const ModalStatus = ({ closeModal, job, handleJobState }: Props) => {
  const [disabledButton, setDisabledButton] = useState(true)
  const [values, setValues] = useState<ValueType>({
    status: job.status,
    finalAmount: job.finalAmount,
  })
  const { addToast } = useToast()

  // Modify, this can do it with one or two handle.
  useEffect(() => {
    if (job.finalAmount !== values.finalAmount || job.status !== values.status) {
      setDisabledButton(false)
    } else {
      setDisabledButton(true)
    }
    console.log('job', job.finalAmount, job.status)
    console.log('values', values.finalAmount, values.status)
  }, [values, job.finalAmount, job.status])

  const handleSubmit = async () => {
    try {
      await jobService.updateStatusAndAmount({
        id: job.id,
        finalAmount: values.finalAmount,
        status: values.status,
      })
      addToast({
        message: 'Actualizado con exito',
        severity: 'success',
        title: 'OK',
      })

      handleJobState({
        finalAmount: values.finalAmount,
        status: values.status,
        id: job.id,
      })
    } catch (error) {
      addToast({
        severity: 'error',
        message: '',
        title: 'Error',
      })
    }
  }
  return (
    <ModalPortal isOpen={job !== null} onClose={() => closeModal(null)}>
      <BaseModal
        onClose={() => closeModal(null)}
        onSave={handleSubmit}
        title={`${job.vehicle.model}  (${job.vehicle.plate})`}
        disabledSaveButton={disabledButton}
      >
        <div className=" mb-3 text-red-900 font-semibold">
          <span>Total en repuestos: </span>
          <span>
            $
            {job.spareParts
              .reduce((acc, act) => act.price * act.quantity + acc, 0)
              .toLocaleString('Es-es')}
          </span>
        </div>

        <div className="text-gray-600 mb-6">
          {Object.values(JOBS_STATUS).map((option) => (
            <button
              className={`px-2 py-1 mr-2 font-semibold text-gray-800 border border-gray-300 rounded transition-colors ${
                values.status === option ? 'bg-blue-600 text-white' : ''
              }`}
              key={option + Math.random()}
              onClick={() => setValues((prev) => ({ ...prev, status: option }))}
            >
              {status_translation[option]}
            </button>
          ))}
        </div>
        <div className="my-2 space-y-2">
          <label htmlFor="finalAmount" className={classNameLabel}>
            Monto Final
          </label>
          <input
            id="finalAmount"
            type="number"
            min="0"
            step="0.01"
            value={values.finalAmount}
            onChange={(e) =>
              setValues((prev) => ({
                ...prev,
                finalAmount: parseInt(e.target.value),
              }))
            }
            className={classNameInput}
          />
        </div>
      </BaseModal>
    </ModalPortal>
  )
}
