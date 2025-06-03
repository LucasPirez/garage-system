import { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom'
import { JobType } from '../../../../core/type/job'
import { SparePart } from '../../../../core/dtos/vehicleEntry/jobs-response.dto'
import { SpareParts } from './spare-parts'

export type FormDataType = Omit<JobType, 'client' | 'vehicle' | 'id'>

const initialData: FormDataType = {
  receptionDate: '',
  deliveryDate: null,
  status: 'InProgress',
  cause: '',
  details: '',
  budget: 1000,
  finalAmount: 0,
  notificationSent: false,
  spareParts: [],
}

export const EditJob = () => {
  const location = useLocation()
  const [formData, setFormData] = useState<FormDataType | null>(initialData)

  useEffect(() => {
    if (location.state) {
      const {
        client: _,
        vehicle: _ignored,
        ...rest
      } = location.state as JobType
      setFormData(rest)
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

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    console.log('Datos del formulario:', formData)
  }

  const classNameInput =
    'w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500'

  const classNameLabel = 'block text-sm font-medium text-gray-700'

  return (
    <>
      <div className="p-6 border-b border-gray-200">
        <h1 className="text-2xl font-bold text-gray-900">Orden de Servicio</h1>
        <p className="text-gray-600 mt-1">
          Complete los datos de la orden de servicio
        </p>
      </div>
      <div className="p-6">
        <form onSubmit={handleSubmit} className="space-y-6">
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
                Fecha de Entrega
              </label>
              <input
                id="deliveryDate"
                type="datetime-local"
                value={formData.deliveryDate ?? new Date().toISOString()}
                onChange={(e) =>
                  handleInputChange('deliveryDate', e.target.value || null)
                }
                className={classNameInput}
              />
            </div>
          </div>
          <div className="space-y-2">
            <label htmlFor="status" className={classNameLabel}>
              Estado
            </label>
            <select
              id="status"
              value={formData.status}
              onChange={(e) => handleInputChange('status', e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 bg-white">
              {['pending', 'Realizado'].map((option) => (
                <option key={option} value={option}>
                  {option}
                </option>
              ))}
            </select>
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
                step="0.01"
                value={formData.budget}
                onChange={(e) =>
                  handleInputChange(
                    'budget',
                    Number.parseFloat(e.target.value) || 0
                  )
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
                step="0.01"
                value={formData.finalAmount}
                onChange={(e) =>
                  handleInputChange(
                    'finalAmount',
                    Number.parseFloat(e.target.value) || 0
                  )
                }
                className={classNameInput}
              />
            </div>
          </div>
          <div className="space-y-2">
            <label className={classNameLabel}>Repuestos</label>
            <div className="space-y-2">
              <SpareParts
                formData={formData}
                handleChange={handleInputChange}
                setFormData={setFormData}
              />
            </div>
          </div>
          <button
            type="submit"
            className=" bg-blue-600 text-white py-2 px-4 rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 font-medium float-end">
            Guardar Cambios
          </button>
        </form>
      </div>
    </>
  )
}
