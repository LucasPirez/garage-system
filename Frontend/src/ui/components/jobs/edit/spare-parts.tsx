import { useState } from 'react'
import { SparePart } from '../../../../core/dtos/vehicleEntry/jobs-response.dto'
import { FormDataType } from './edit-jobs'
import { ButtonClose } from '../../common/button-close-icon'

interface Props {
  formData: FormDataType
  setFormData: (state: React.SetStateAction<FormDataType | null>) => void
  handleChange: (
    field: keyof FormDataType,
    value: string | number | null | SparePart[]
  ) => void
}

export const SpareParts = ({ formData, setFormData, handleChange }: Props) => {
  const [newSparePart, setNewSparePart] = useState<SparePart>({
    name: '',
    price: 0,
    quantity: 1,
  })

  const addSparePart = () => {
    if (newSparePart.name.trim()) {
      const exists = formData.spareParts.some(
        (part) =>
          part.name.toLowerCase() === newSparePart.name.toLowerCase().trim()
      )
      if (!exists && newSparePart.quantity) {
        setFormData((prev) => {
          if (!prev) return prev
          return {
            ...prev,
            spareParts: [
              ...prev.spareParts,
              {
                name: newSparePart.name.trim(),
                price: newSparePart.price,
                quantity: newSparePart.quantity,
              },
            ],
          }
        })
        setNewSparePart({ name: '', price: 0, quantity: 1 })
      }
    }
  }

  const removeSparePart = (index: number) => {
    setFormData((prev) => {
      if (!prev) return prev
      return {
        ...prev,
        spareParts: prev.spareParts.filter((_, i) => i !== index),
      }
    })
  }
  const classInputSpare =
    'pl-1 w-full md:px-3 py-2 border border-gray-300 focus:max-sm:w-48 rounded-md shadow-sm bg-gray-50 text-gray-700'

  const classInputNumbers =
    ' w-full sm:max-w-24  max-w-16 px-1 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-right'

  return (
    <>
      {formData.spareParts.map((part, index) => (
        <div
          key={index + Math.random()}
          className="flex items-center gap-1 md:gap-2">
          <input
            type="text"
            value={part.name}
            readOnly
            className={classInputSpare}
          />

          <input
            type="text"
            value={'x ' + part.quantity}
            readOnly
            className={classInputNumbers}
          />

          <input
            type="text"
            value={'$' + part.price}
            onChange={(e) => {
              const updatedParts = [...formData.spareParts]
              updatedParts[index].price = Number(e.target.value) || 0
              handleChange('spareParts', updatedParts)
            }}
            className={classInputNumbers}
          />

          <ButtonClose onClick={() => removeSparePart(index)} />
        </div>
      ))}
      <div className="flex gap-1 md:gap-2">
        <input
          type="text"
          placeholder="Nombre del repuesto"
          value={newSparePart.name}
          onChange={(e) =>
            setNewSparePart({ ...newSparePart, name: e.target.value })
          }
          className={classInputSpare}
        />

        <input
          type="number"
          min="0"
          placeholder="Cantidad"
          value={newSparePart.quantity || undefined}
          onChange={(e) =>
            setNewSparePart({
              ...newSparePart,
              quantity: Number(e.target.value),
            })
          }
          onKeyDown={(e) =>
            e.key === 'Enter' && (e.preventDefault(), addSparePart())
          }
          className={classInputNumbers}
        />

        <input
          type="number"
          min="0"
          step="0.01"
          placeholder="Precio"
          value={newSparePart.price || ''}
          onChange={(e) =>
            setNewSparePart({
              ...newSparePart,
              price: Number(e.target.value),
            })
          }
          onKeyDown={(e) =>
            e.key === 'Enter' && (e.preventDefault(), addSparePart())
          }
          className={classInputNumbers}
        />

        <button
          type="button"
          onClick={addSparePart}
          disabled={!newSparePart.name.trim()}
          className="ml-2 p-2 py-2 border border-gray-300 rounded-md shadow-sm bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 disabled:opacity-50 disabled:cursor-not-allowed text-green-600">
          +
        </button>
      </div>

      {formData.spareParts.length > 0 && (
        <div className="flex justify-end items-center gap-2 py-2 border-b border-gray-400">
          <span className="font-medium text-gray-700">Total:</span>
          <span className="font-bold text-lg text-gray-900">
            $
            {formData.spareParts
              .reduce((sum, part) => sum + part.price, 0)
              .toLocaleString('ES-es')}
          </span>
        </div>
      )}
    </>
  )
}
