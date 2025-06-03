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

const classNameInput =
  'w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500'

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
      if (!exists) {
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
  console.log(formData)

  return (
    <>
      {formData.spareParts.map((part, index) => (
        <div key={index + Math.random()} className="flex items-center gap-2">
          <input
            type="text"
            value={part.name}
            readOnly
            className="flex-1 px-3 py-2 border border-gray-300 rounded-md shadow-sm bg-gray-50 text-gray-700"
          />
          <div className="w-24">
            <input
              type="text"
              value={'x ' + part.quantity}
              readOnly
              className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-right"
            />
          </div>
          <div className="w-32">
            <input
              type="text"
              value={'$' + part.price}
              onChange={(e) => {
                const updatedParts = [...formData.spareParts]
                updatedParts[index].price = Number(e.target.value) || 0
                handleChange('spareParts', updatedParts)
              }}
              className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-right"
            />
          </div>
          <ButtonClose onClick={() => removeSparePart(index)} />
        </div>
      ))}
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
        <div className="w-24">
          <input
            type="number"
            min="0"
            placeholder="Precio"
            value={newSparePart.quantity ?? null}
            onChange={(e) =>
              setNewSparePart({
                ...newSparePart,
                quantity: Number(e.target.value) || 0,
              })
            }
            onKeyDown={(e) =>
              e.key === 'Enter' && (e.preventDefault(), addSparePart())
            }
            className={classNameInput + 'text-right'}
          />
        </div>
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
            onKeyDown={(e) =>
              e.key === 'Enter' && (e.preventDefault(), addSparePart())
            }
            className={classNameInput + 'text-right'}
          />
        </div>
        <button
          type="button"
          onClick={addSparePart}
          disabled={!newSparePart.name.trim()}
          className="px-3 py-2 border border-gray-300 rounded-md shadow-sm bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 disabled:opacity-50 disabled:cursor-not-allowed text-green-600">
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
              .toFixed(2)}
          </span>
        </div>
      )}
    </>
  )
}
