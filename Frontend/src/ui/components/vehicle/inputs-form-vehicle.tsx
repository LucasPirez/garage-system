import { ChangeEvent } from 'react'
import { classNameInput } from '../../pages/register'
import { classNameLabel } from '../common/class-names'
import type { VehicleCreateDto } from '../../../core/dtos/vehicle/vehicle-request.dto'

interface Props {
  state: Omit<VehicleCreateDto, 'customerId'>
  onChange: (e: ChangeEvent<HTMLSelectElement | HTMLInputElement>) => void
}

export const InputsFormVehicle = ({ state, onChange }: Props) => {
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
          placeholder="AB-123-45 | AAA-000"
          onChange={onChange}
          value={state.plate}
        />
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
          placeholder="Ej: Ranger Raptor"
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
