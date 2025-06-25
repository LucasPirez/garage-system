import { ChangeEvent } from 'react'
import { JobCreateDto } from '../../../core/services/jobs-service'
import { classNameInput } from '../../pages/register'
import { classNameLabel } from '../common/class-names'

interface Props {
  state: Omit<JobCreateDto, 'workshopId' | 'vehicleId'>
  onChange: (e: ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => void
}

export const InputsFormJob = ({ state, onChange }: Props) => {
  return (
    <>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 gap-y-3">
        <div>
          <label htmlFor="receptionDate" className={classNameLabel}>
            Fecha de Recepción
          </label>
          <input
            type="date"
            id="receptionDate"
            name="receptionDate"
            required
            defaultValue={'2025-05-26'}
            max={new Date().toISOString().slice(0, 16)}
            className={classNameInput}
            value={state.receptionDate}
            onChange={onChange}
          />
          <p className="text-xs text-gray-500 mt-1">
            No puede ser una fecha futura
          </p>
        </div>

        <div>
          <label htmlFor="plate" className={classNameLabel}>
            Motivo
          </label>
          <input
            type="text"
            required
            id="MotivoServicio"
            name="cause"
            className={classNameInput + 'uppercase font-mono'}
            placeholder="Motivo"
            value={state.cause}
            onChange={onChange}
          />
        </div>
      </div>

      <div className="mt-6">
        <label htmlFor="observations" className={classNameLabel}>
          Observaciones Adicionales
        </label>
        <textarea
          id="observations"
          name="details"
          rows={4}
          maxLength={500}
          className={classNameInput + 'resize-none'}
          placeholder="Describa cualquier detalle adicional sobre el vehículo o el servicio requerido..."
          value={state.details}
          onChange={onChange}></textarea>
        <div className="flex justify-between items-center mt-1">
          <p className="text-xs text-gray-500">Máximo 500 caracteres</p>
        </div>
      </div>
    </>
  )
}
