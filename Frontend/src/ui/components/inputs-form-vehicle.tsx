import { ChangeEvent } from 'react'
import { VehicleCreateDto } from '../../core/services/jobs-service'

interface Props {
  state: Omit<VehicleCreateDto, 'customerId'>
  onChange: (e: ChangeEvent<HTMLSelectElement | HTMLInputElement>) => void
}

export const InputsFormVehicle = ({ state, onChange }: Props) => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
      <div>
        <label
          htmlFor="plate"
          className="block text-sm font-medium text-gray-700 mb-2">
          Patente
        </label>
        <input
          type="text"
          id="plate"
          name="plate"
          defaultValue={''}
          title="Formato requerido: AB-123-45"
          required
          className="w-full px-4 py-3 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200 uppercase font-mono"
          placeholder="AB-123-45"
          onChange={onChange}
          value={state.plate}
        />
      </div>

      <div>
        <label
          htmlFor="model"
          className="block text-sm font-medium text-gray-700 mb-2">
          Modelo
        </label>
        <input
          type="text"
          id="model"
          name="model"
          defaultValue={''}
          required
          minLength={2}
          maxLength={50}
          className="w-full px-4 py-3 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200"
          placeholder="Ej: Ranger Raptor"
          value={state.model}
          onChange={onChange}
        />
      </div>

      <div>
        <label
          htmlFor="color"
          className="block text-sm font-medium text-gray-700 mb-2">
          Color
        </label>
        <select
          id="color"
          name="color"
          className="w-full px-4 py-3 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition duration-200"
          onChange={onChange}
          value={state.color}>
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
