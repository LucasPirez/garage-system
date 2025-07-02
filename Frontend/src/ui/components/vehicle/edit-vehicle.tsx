import { ChangeEvent, useEffect, useState } from 'react'
import { VehicleType } from '../../../core/type/vehicle'
import { InputsFormVehicle } from './inputs-form-vehicle'
import { useToast } from '../../context/toast-context'
import { updateCustomerVehicleService } from '../../../core/services'
import { ButtonSubmit } from '../common/button-submit'
import { triggerCoolDown } from '../../../core/helpers/triggerCoolDown'
import { ModalDeleteVehicle } from '../modal/modal-delete'
import { SelectVehicle } from './select-vehicle'

export const EditVehicle = ({
  vehicle,
  setVehicle,
}: {
  vehicle: VehicleType[]
  setVehicle: (vehicleUpdate: VehicleType) => void
}) => {
  const [vehicleSelect, setVehicleSelect] = useState<VehicleType>(vehicle[0])
  const [disabled, setDisabled] = useState(true)
  const [deleteVehicle, setDeleteVehicle] = useState<VehicleType | null>(null)
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

    if (!triggerCoolDown()) {
      addToast({
        severity: 'error',
        title: 'Error',
        message: 'Demasiadas solicitudes, por favor espere un momento.',
      })
      return
    }

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
    }
  }

  return (
    <>
      <div className="bg-white rounded-lg py-4 shadow-sm">
        <div className="flex items-center justify-between mb-4">
          <h2 className="text-lg font-medium text-gray-900">Vehiculos</h2>
        </div>

        {vehicle.length === 0 ? (
          <div className="text-center py-8">
            <p className="text-gray-500">No hay autos registrados</p>
          </div>
        ) : (
          <SelectVehicle
            onSelectVehicle={(vehicle) => setVehicleSelect(vehicle)}
            vehicleSelect={vehicleSelect}
            vehicles={vehicle}
            onDeleteVehicle={(vehicle) => setDeleteVehicle(vehicle)}
          />
        )}
      </div>
      <form onSubmit={onSubmit} className="mt-4">
        <InputsFormVehicle state={vehicleSelect} onChange={handleChange} />
        <ButtonSubmit
          label="Actualizar Vehiculo"
          disabled={disabled}
          className="mt-4 float-end"
        />
      </form>
      {deleteVehicle && (
        <ModalDeleteVehicle
          handleUnSelectVehicle={() => setDeleteVehicle(null)}
          vehicle={deleteVehicle}
        />
      )}
    </>
  )
}
