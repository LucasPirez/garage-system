import { ChangeEvent, useState } from 'react'
import { classNameInput } from '../../pages/register'
import { classNameLabel } from '../../../core/constants/class-names'
import type { VehicleCreateDto } from '../../../core/dtos/vehicle/vehicle-request.dto'

interface Props {
  state: Omit<VehicleCreateDto, 'customerId'>
  onChange: (e: ChangeEvent<HTMLSelectElement | HTMLInputElement>) => void
}

type FormatType = 'OLD' | 'NEW'

export const InputsFormVehicle = ({ state, onChange }: Props) => {
  const [format, setFormat] = useState<FormatType>('OLD')
  const handleChangePlate = (event: ChangeEvent<HTMLInputElement>) => {
    let { value } = event.target

    if (value.length < state.plate.length) {
      onChange(event)
      return
    }

    if (format === 'NEW') {
      if (value.length === 2 || value.length === 6) {
        value = value + '-'
      }

      event.target.value = value.toUpperCase()
    } else {
      if (value.length === 3) {
        value = value + '-'
      }

      event.target.value = value.toUpperCase()
    }

    onChange(event)
  }

  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
      <div>
        <label htmlFor="plate" className={classNameLabel}>
          Patente
        </label>
        <input
          type="text"
          id="plate"
          name="plate"
          pattern="^([A-Z]{2}-\d{3}-[A-Z]{2}|[A-Z]{3}-\d{3})$"
          title="Formato requerido: AB-123-EU  | AAA-000"
          required
          className={classNameInput}
          placeholder="AB-123-45 o AAA-000"
          onChange={handleChangePlate}
          value={state.plate}
        />
        <div className=" flex mt-2 space-x-3">
          <label className="flex p-1 items-center border-2 border-gray-200 rounded-lg cursor-pointerue-50">
            <input
              type="radio"
              name="plate-format"
              value="old"
              defaultChecked
              onChange={() => setFormat('OLD')}
              className="w-4 h-4 text-blue-600 border-gray-300 focus:ring-blue-500 focus:ring-2"
            />
            <span className="ml-3 text-sm font-medium text-gray-700 group-hover:text-blue-700">
              AAA-111
            </span>
          </label>

          <label className="flex p-1 items-center border-2 border-gray-200 rounded-lg cursor-pointe">
            <input
              type="radio"
              name="plate-format"
              value="new"
              onChange={() => setFormat('NEW')}
              className="w-4 h-4 text-blue-600 border-gray-300 focus:ring-blue-500 focus:ring-2"
            />
            <span className="ml-3 text-sm font-medium text-gray-700 group-hover:text-blue-700">
              AA-111-BB
            </span>
          </label>
        </div>
      </div>

      <div>
        <label htmlFor="model" className={classNameLabel}>
          Modelo
        </label>
        <input
          type="text"
          id="model"
          name="model"
          required
          minLength={2}
          maxLength={50}
          className={classNameInput}
          placeholder="Ej: Gol Power"
          value={state.model}
          onChange={onChange}
        />
      </div>

      <div>
        <label htmlFor="color" className={classNameLabel}>
          Color
        </label>
        <select
          id="color"
          name="color"
          className={classNameInput}
          onChange={onChange}
          value={state.color}
        >
          <option value="">Seleccionar color</option>
          <option value="Blanco">Blanco</option>
          <option value="Negro">Negro</option>
          <option value="Gris">Gris</option>
          <option value="Azul">Azul</option>
          <option value="Rojo">Rojo</option>
          <option value="Verde">Verde</option>
          <option value="Amarillo">Amarillo</option>
          <option value="Pink">Rosado</option>
          <option value="Plateado">Plateado</option>
          <option value="Marrón">Marrón</option>
          <option value="Otro">Otro</option>
        </select>
      </div>
    </div>
  )
}
