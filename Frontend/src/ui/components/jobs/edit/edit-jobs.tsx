import { useEffect, useRef, useState } from 'react'
import { useLocation, useNavigate } from 'react-router-dom'
import { JobType } from '../../../../core/type/job'
import { SparePart } from '../../../../core/dtos/vehicleEntry/jobs-response.dto'
import { SpareParts } from './spare-parts'
import { jobService } from '../../../../core/services'
import { useToast } from '../../../context/toast-context'
import { ArrowLeft } from 'lucide-react'
import {
  classNameInput,
  classNameLabel,
} from '../../../../core/constants/class-names'
import { triggerCoolDown } from '../../../../core/helpers/triggerCoolDown'
import { useLoader } from '../../../context/loader-context'

export type FormDataType = Omit<
  JobType,
  'client' | 'vehicle' | 'id' | 'status' | 'createdAt'
>

const initialData: FormDataType = {
  receptionDate: '',
  deliveryDate: null,
  cause: '',
  details: '',
  budget: 1000,
  finalAmount: 0,
  notificationSent: false,
  spareParts: [],
}

export const EditJob = () => {
  const refId = useRef<string | null>(null)
  const navigate = useNavigate()
  const location = useLocation()
  const [formData, setFormData] = useState<FormDataType | null>(initialData)
  const toast = useToast()
  const { hideLoader, showLoader } = useLoader()

  useEffect(() => {
    if (location.state) {
      const { id, ...job } = location.state as JobType
      refId.current = id
      setFormData(job)
    } else {
      const id =
        location.pathname.split('/')[location.pathname.split('/').length - 1]
      if (id?.length) {
        // eslint-disable-next-line no-extra-semi
        ;(async () => {
          const jobResponse = await jobService.getById(id)
          refId.current = id

          setFormData(jobResponse)
        })()
      }
    }
  }, [])

  if (!formData) return <></>

  const handleInputChange = (
    field: keyof typeof formData,
    value: string | number | null | SparePart[]
  ) => {
    setFormData((prev) => {
      if (!prev) return prev

      return {
        ...prev,
        [field]: value,
      }
    })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!triggerCoolDown()) {
      toast.addToast({
        severity: 'error',
        title: 'Error',
        message: 'Demasiadas solicitudes, por favor espere un momento.',
      })
      return
    }

    showLoader()
    try {
      await jobService.update({
        ...formData,
        id: refId.current ?? '',
        deliveryDate: formData.deliveryDate
          ? new Date(formData.deliveryDate).toISOString()
          : null,
        receptionDate: new Date(formData.receptionDate).toISOString(),
      })

      navigate('.', {
        replace: true,
        state: { ...formData, id: refId.current },
      })

      toast.addToast({
        title: 'Actualizado',
        message: 'Actualizado con exitooooo',
        severity: 'success',
      })
    } catch (error) {
      toast.addToast({
        title: 'Error',
        message: 'Ocurrio un error al actualizar',
        severity: 'error',
      })
    } finally {
      hideLoader()
    }
  }

  const handleUpdateSpareParts = async (payload: SparePart[]) => {
    if (!triggerCoolDown()) {
      toast.addToast({
        severity: 'error',
        title: 'Error',
        message: 'Demasiadas solicitudes, por favor espere un momento.',
        duration: 1000,
      })
      return
    }
    showLoader()
    try {
      if (!refId.current)
        throw new Error('Error, no se obtiene el id del trabajo.')

      await jobService.updateSpareParts(payload, refId.current)

      handleInputChange('spareParts', payload)

      navigate('.', {
        replace: true,
        state: { ...{ ...formData, spareParts: payload }, id: refId.current },
      })

      toast.addToast({
        title: 'Actualizada',
        message: 'Se agrego o elimino con exito el repuesto',
        severity: 'success',
      })
    } catch (error) {
      toast.addToast({
        title: 'Error',
        message: 'Ocurrio un error al los repuestos',
        severity: 'error',
      })
    } finally {
      hideLoader()
    }
  }

  return (
    <>
      <div className="p-2 md:p-6 border-b border-gray-200">
        <div className="flex items-center gap-4" onClick={() => navigate(-1)}>
          <ArrowLeft className="hover:scale-105 cursor-pointer " />
          <h1 className="text-2xl font-bold text-gray-900">
            Orden de Servicio
          </h1>
        </div>
      </div>

      <section className="py-4  md:p-6 ">
        <form onSubmit={handleSubmit} className="mb-3 space-y-6  ">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <label htmlFor="receptionDate" className={classNameLabel}>
                Fecha de Recepción
              </label>
              <input
                id="receptionDate"
                type="date"
                value={formData.receptionDate.split('T')[0]}
                onChange={(e) =>
                  handleInputChange('receptionDate', e.target.value)
                }
                required
                className={classNameInput}
              />
            </div>
            <div className="space-y-2">
              <label htmlFor="deliveryDate" className={classNameLabel}>
                Fecha de Finalizado
              </label>
              <input
                id="deliveryDate"
                type="date"
                value={formData.deliveryDate?.split('T')[0] ?? ''}
                onChange={(e) =>
                  handleInputChange('deliveryDate', e.target.value || null)
                }
                className={classNameInput}
              />
            </div>
          </div>

          <div className="space-y-2">
            <label htmlFor="cause" className={classNameLabel}>
              Causa
            </label>
            <input
              id="cause"
              type="text"
              value={formData.cause}
              onChange={(e) => handleInputChange('cause', e.target.value)}
              placeholder="Motivo del servicio"
              required
              className={classNameInput}
            />
          </div>
          <div className="space-y-2">
            <label htmlFor="details" className={classNameLabel}>
              Detalles
            </label>
            <textarea
              id="details"
              value={formData.details}
              onChange={(e) => handleInputChange('details', e.target.value)}
              placeholder="Descripción detallada del servicio"
              rows={3}
              className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 resize-vertical"
            />
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <label htmlFor="presupuest" className={classNameLabel}>
                Presupuesto
              </label>
              <input
                id="presupuest"
                type="number"
                min="0"
                step="1000"
                value={formData.budget}
                onChange={(e) =>
                  handleInputChange('budget', Number.parseFloat(e.target.value))
                }
                required
                className={classNameInput}
              />
            </div>
            <div className="space-y-2">
              <label htmlFor="finalAmount" className={classNameLabel}>
                Monto Final
              </label>
              <input
                id="finalAmount"
                type="number"
                min="0"
                step="1000"
                value={formData.finalAmount}
                onChange={(e) =>
                  handleInputChange(
                    'finalAmount',
                    Number.parseFloat(e.target.value)
                  )
                }
                className={classNameInput}
              />
            </div>
          </div>
          <div className="border-b border-gray-400 flex justify-end pb-3">
            <button
              type="submit"
              className="  bg-blue-600 text-white py-2 px-4 rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 font-medium "
            >
              Guardar Cambios
            </button>
          </div>
        </form>
        <div className="space-y-2">
          <label className={classNameLabel}>Repuestos</label>
          <div className="space-y-2">
            <SpareParts
              spareParts={formData.spareParts}
              handleSpareParts={handleUpdateSpareParts}
            />
          </div>
        </div>
      </section>
    </>
  )
}
