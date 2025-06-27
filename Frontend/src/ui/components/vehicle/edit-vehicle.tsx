import { ChangeEvent, useEffect, useState } from 'react'
import { VehicleType } from '../../../core/type/vehicle'
import { InputsFormVehicle } from './inputs-form-vehicle'
import { useToast } from '../../context/toast-context'
import { updateCustomerVehicleService } from '../../../core/services'
import { ButtonSubmit } from '../common/button-submit'

export const EditVehicle = ({
  vehicle,
  setVehicle,
}: {
  vehicle: VehicleType[]
  setVehicle: (vehicleUpdate: VehicleType) => void
}) => {
  const [vehicleSelect, setVehicleSelect] = useState<VehicleType>(vehicle[0])
  const [disabled, setDisabled] = useState(true)
  const [isCoolDown, setIsCoolDown] = useState(false)
  const { addToast } = useToast()
  useEffect(() => {
    setVehicleSelect(vehicle[0])
  }, [vehicle])

  const handleChange = (
    event: ChangeEvent<HTMLSelectElement | HTMLInputElement>
  ) => {
    const { name, value } = event.target

    setDisabled(false)

    if (name === 'plate') {
      setVehicleSelect((prev) => ({
        ...prev,
        [name]: value.toUpperCase(),
      }))
      return
    }

    setVehicleSelect((prev) => ({
      ...prev,
      [name]: value,
    }))
  }

  const onSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    if (isCoolDown) return

    try {
      await updateCustomerVehicleService.updateVehicle(
        vehicleSelect,
        vehicleSelect.id
      )
      addToast({
        severity: 'success',
        title: 'Exito',
        message: 'Vehiculo actualizado correctamente',
      })
      setVehicle(vehicleSelect)

      setDisabled(true)
    } catch (error) {
      addToast({
        severity: 'error',
        title: 'Error',
        message: 'Error al actualizar el vehiculo',
      })
    } finally {
      setIsCoolDown(true)
      setTimeout(() => {
        setIsCoolDown(false)
      }, 5000)
    }
  }

  return (
    <>
      <div className="bg-white rounded-lg py-4 shadow-sm">
        <div className="flex items-center justify-between mb-4">
          <h2 className="text-lg font-medium text-gray-900">Autos</h2>
          <span className="text-sm text-gray-500">{vehicle.length}</span>
        </div>

        {vehicle.length === 0 ? (
          <div className="text-center py-8">
            <p className="text-gray-500">No hay autos registrados</p>
          </div>
        ) : (
          <div className="flex flex-wrap gap-4">
            {vehicle.map((car) => (
              <div
                key={car.id}
                onClick={() => {
                  setVehicleSelect(car)
                }}
                className={`${
                  car.id === vehicleSelect.id ? 'bg-blue-200 ' : ''
                }  max-w-56 h-auto p-3 shadow-md border-2 border-blue-200 shadow-gray-400 rounded-lg cursor-pointer`}>
                <div className="flex justify-between items-start">
                  <div className="flex-1">
                    <div className="flex justify-between items-start mb-1">
                      <h3 className="font-medium text-gray-900">{car.model}</h3>
                    </div>
                    <p className="text-sm text-gray-600">Placa: {car.plate}</p>
                  </div>
                  <div className="flex items-center ml-3 space-x-1">
                    <button
                      onClick={(e) => {
                        e.stopPropagation()
                      }}
                      className="p-1.5 text-red-600 hover:text-red-600 hover:bg-red-50 rounded-md transition-colors">
                      <svg
                        className="w-5 h-5"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24">
                        <path
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          strokeWidth={1.5}
                          d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
                        />
                      </svg>
                    </button>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
      <form onSubmit={onSubmit} className="mt-4">
        <InputsFormVehicle state={vehicleSelect} onChange={handleChange} />
        <ButtonSubmit
          label="Actualizar Vehiculo"
          disabled={disabled}
          className="mt-4"
        />
      </form>
    </>
  )
}
