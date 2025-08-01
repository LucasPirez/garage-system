import { ChangeEvent } from 'react'
import { classNameInput } from '../../pages/register'
import type { CustomerFormType } from '../../../core/type/customer'
import { classNameLabel } from '../../../core/constants/class-names'

interface Props {
  state: CustomerFormType
  onChange: (e: ChangeEvent<HTMLInputElement>) => void
}

export const InputsFormCustomer = ({ state, onChange }: Props) => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-6 gap-y-3">
      <div>
        <label htmlFor="customerName" className={classNameLabel}>
          Nombre <span className="text-red-700 ">*</span>
        </label>
        <input
          type="text"
          id="customerName Nombre"
          name="firstName"
          minLength={3}
          maxLength={100}
          pattern="[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+"
          title="Solo se permiten letras y espacios"
          className={classNameInput}
          placeholder="Ej: Juan"
          required
          value={state.firstName}
          onChange={onChange}
        />
      </div>

      <div>
        <label htmlFor="customerName" className={classNameLabel}>
          Apellido
        </label>
        <input
          type="text"
          id="customerName Apellido"
          name="lastName"
          minLength={3}
          maxLength={100}
          pattern="[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+"
          title="Solo se permiten letras y espacios"
          className={classNameInput}
          placeholder="Ej: Pérez García"
          value={state.lastName}
          onChange={onChange}
        />
      </div>
      <div>
        <label htmlFor="customerPhone" className={classNameLabel}>
          Número de Teléfono
        </label>
        <input
          type="tel"
          id="customerPhone"
          name="phoneNumber"
          pattern="[0-9]{8,15}"
          title="Ingrese un número de teléfono válido (8-15 dígitos)"
          className={classNameInput}
          placeholder="Ej: 3424383459"
          value={state.phoneNumber}
          onChange={onChange}
        />
      </div>

      <div>
        <label htmlFor="customerEmail" className={classNameLabel}>
          Correo Electrónico
        </label>
        <input
          type="email"
          id="customerEmail"
          name="email"
          className={classNameInput}
          placeholder="ejemplo@correo.com"
          value={state.email}
          onChange={onChange}
        />
      </div>
    </div>
  )
}
