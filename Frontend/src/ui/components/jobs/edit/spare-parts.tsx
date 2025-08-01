import { useState } from 'react'
import { SparePart } from '../../../../core/dtos/vehicleEntry/jobs-response.dto'
import { ButtonClose } from '../../buttons/button-close-icon'
import { Plus } from 'lucide-react'

interface Props {
  spareParts: SparePart[]
  handleSpareParts: (spareParts: SparePart[]) => void
}

export const SpareParts = ({ spareParts, handleSpareParts }: Props) => {
  const [newSparePart, setNewSparePart] = useState<SparePart>({
    name: '',
    price: 0,
    quantity: 1,
  })

  const addSparePart = () => {
    if (newSparePart.name.trim()) {
      const exists = spareParts.some(
        (part) =>
          part.name.toLowerCase() === newSparePart.name.toLowerCase().trim()
      )

      if (!exists) {
        const sparePartToAdd = {
          name: newSparePart.name.trim(),
          price: newSparePart.price,
          quantity: 1,
        }

        handleSpareParts([...spareParts, sparePartToAdd])

        setNewSparePart({ name: '', price: 0, quantity: 1 })
      }
    }
  }

  const removeSparePart = (index: number) => {
    const sparePartFilter = spareParts.filter((_, i) => i !== index)

    handleSpareParts(sparePartFilter)
  }
  const classInputSpare =
    'pl-1 w-full md:px-3 py-2 border border-gray-300  rounded-md shadow-sm bg-gray-50 text-gray-700'

  const classInputNumbers =
    ' w-full   max-w-20 px-1 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-right'

  return (
    <>
      {spareParts.map((part, index) => (
        <div
          key={index + Math.random()}
          className="flex items-center gap-1 md:gap-2"
        >
          <input
            type="text"
            value={part.name}
            readOnly
            className={classInputSpare}
          />

          {/* <input
            type="text"
            value={'x ' + part.quantity}
            readOnly
            className={classInputNumbers}
          /> */}

          <input
            type="text"
            readOnly
            value={'$' + part.price}
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

        {/* <input
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
        /> */}

        <input
          type="number"
          min="0"
          step="1"
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
          className="ml-2 p-2 py-2 border-2 border-green-700 rounded-md shadow-sm bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 disabled:opacity-50 disabled:cursor-not-allowed text-green-600"
        >
          <Plus />
        </button>
      </div>

      {spareParts.length > 0 && (
        <div className="flex justify-end items-center gap-2 py-2 ">
          <span className="font-medium text-gray-700">Total:</span>
          <span className="font-bold text-lg text-gray-900">
            $
            {spareParts
              .reduce((sum, part) => sum + part.price, 0)
              .toLocaleString('ES-es')}
          </span>
        </div>
      )}
    </>
  )
}
