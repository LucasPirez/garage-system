import { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom'
import { JobType } from '../../../../core/type/job'

const initialData: JobType = {
  receptionDate: '',
  deliveryDate: null,
  status: 'InProgress',
  cause: '',
  details: '',
  budget: 1000,
  finalAmount: 0,
  client: {
    firstName: '',
    id: '',
    lastName: '',
  },
  id: '',
  notificationSent: false,
  vehicle: {
    id: '',
    model: '',
    plate: '',
  },
  // spareParts: [
  //   { name: 'Aceite', price: 500 },
  //   { name: 'Filtro', price: 300 },
  // ],
}
// interface SparePart {
//   name: string
//   price: number
// }

export const EditJob = () => {
  const location = useLocation()
  const [formData, setFormData] = useState<Omit<
    JobType,
    'client' | 'vehicle' | 'id'
  > | null>(initialData)
  const [newSparePart, setNewSparePart] = useState<{
    name: string
    price: number
  }>({ name: '', price: 0 })

  useEffect(() => {
    if (location.state) {
      setFormData(location.state)
    }
  }, [])

  if (!formData) return <></>

  const handleInputChange = (
    field: keyof typeof formData,
    value: string | number | null
  ) => {
    setFormData((prev) => {
      if (!prev) return prev

      return {
        ...prev,
        [field]: value,
      }
    })
  }

  // const addSparePart = () => {
  //   if (newSparePart.name.trim()) {
  //     const exists = formData.spareParts.some(
  //       (part) =>
  //         part.name.toLowerCase() === newSparePart.name.toLowerCase().trim()
  //     )
  //     if (!exists) {
  //       setFormData((prev) => ({
  //         ...prev,
  //         spareParts: [
  //           ...prev.spareParts,
  //           {
  //             name: newSparePart.name.trim(),
  //             price: newSparePart.price,
  //           },
  //         ],
  //       }))
  //       setNewSparePart({ name: '', price: 0 })
  //     }
  //   }
  // }

  // const removeSparePart = (index: number) => {
  //   setFormData((prev) => ({
  //     ...prev,
  //     spareParts: prev.spareParts.filter((_, i) => i !== index),
  //   }))
  // }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    console.log('Datos del formulario:', formData)
  }

  console.log(formData)

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
              {/* {formData.spareParts.map((part, index) => (
                <div key={index} className="flex items-center gap-2">
                  <input
                    type="text"
                    value={part.name}
                    readOnly
                    className="flex-1 px-3 py-2 border border-gray-300 rounded-md shadow-sm bg-gray-50 text-gray-700"
                  />
                  <div className="w-32">
                    <input
                      type="number"
                      min="0"
                      step="0.01"
                      value={part.price}
                      onChange={(e) => {
                        // const updatedParts = [...formData.spareParts]
                        // updatedParts[index].price = Number(e.target.value) || 0
                        // handleInputChange('spareParts', updatedParts)
                      }}
                      className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-right"
                    />
                  </div>
                  <button
                    type="button"
                    // onClick={() => removeSparePart(index)}
                    className="px-3 py-2 border border-gray-300 rounded-md shadow-sm bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-red-500 focus:border-red-500 text-red-600">
                    ✕
                  </button>
                </div>
              ))} */}
              <div className="flex gap-2">
                <input
                  type="text"
                  placeholder="Nombre del repuesto"
                  value={newSparePart.name}
                  onChange={(e) =>
                    setNewSparePart({ ...newSparePart, name: e.target.value })
                  }
                  className="flex-1 px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
                <div className="w-32">
                  <input
                    type="number"
                    min="0"
                    step="0.01"
                    placeholder="Precio"
                    value={newSparePart.price}
                    onChange={(e) =>
                      setNewSparePart({
                        ...newSparePart,
                        price: Number(e.target.value) || 0,
                      })
                    }
                    // onKeyPress={(e) =>
                    //   e.key === 'Enter' && (e.preventDefault(), addSparePart())
                    // }
                    className={classNameInput + 'text-right'}
                  />
                </div>
                <button
                  type="button"
                  // onClick={addSparePart}
                  disabled={!newSparePart.name.trim()}
                  className="px-3 py-2 border border-gray-300 rounded-md shadow-sm bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 disabled:opacity-50 disabled:cursor-not-allowed text-green-600">
                  +
                </button>
              </div>
            </div>

            {/* {formData.spareParts.length > 0 && (
              <div className="flex justify-end items-center gap-2 pt-2 border-t border-gray-200">
                <span className="font-medium text-gray-700">Total:</span>
                <span className="font-bold text-lg text-gray-900">
                  $
                  {formData.spareParts
                    .reduce((sum, part) => sum + part.price, 0)
                    .toFixed(2)}
                </span>
              </div>
            )} */}
          </div>
          <div className="flex gap-4 pt-4">
            <button
              type="submit"
              className="flex-1 bg-blue-600 text-white py-2 px-4 rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 font-medium">
              Guardar Orden
            </button>
            <button
              type="button"
              onClick={() => setFormData(initialData)}
              className="px-6 py-2 border border-gray-300 rounded-md shadow-sm bg-white text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 font-medium">
              Resetear
            </button>
          </div>
        </form>
      </div>
    </>
  )
}
