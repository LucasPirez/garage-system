import { ChangeEvent, useState } from 'react'
import { InputsFormCustomer } from './inputs-form-customer'
import type {
  CustomerFormType,
  CustomerType,
} from '../../../core/type/customer'
import { ButtonSubmit } from '../buttons/button-submit'
import { useToast } from '../../context/toast-context'
import { updateCustomerVehicleService } from '../../../core/services'
import { triggerCoolDown } from '../../../core/helpers/triggerCoolDown'

export const EditCustomer = ({
  customer,
}: {
  customer: Omit<CustomerType, 'vehicle'>
}) => {
  const [customerSelected, setCustomerSelected] =
    useState<CustomerFormType>(customer)
  const [disabled, setDisabled] = useState(true)
  const { addToast } = useToast()

  const handleChange = (
    event: ChangeEvent<HTMLSelectElement | HTMLInputElement>
  ) => {
    const { name, value } = event.target

    setDisabled(false)

    setCustomerSelected((prev) => ({
      ...prev,
      [name]: value,
    }))
  }
  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
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
      await updateCustomerVehicleService.updateCustomer(
        customerSelected,
        customer.id
      )
      addToast({
        severity: 'success',
        title: 'Exito',
        message: 'Cliente actualizado correctamente',
      })

      setDisabled(true)
    } catch (error) {
      addToast({
        severity: 'error',
        title: 'Error',
        message: 'Error al actualizar el cliente',
      })
    }
  }

  return (
    <>
      <div className="bg-white rounded-lg p-4 ">
        <div className="flex items-start justify-between">
          <div className="flex-1">
            <h1 className="text-xl font-semibold text-gray-900 mb-1 text-center">
              {customerSelected.firstName} {customerSelected.lastName}
            </h1>
          </div>
        </div>
      </div>
      <form onSubmit={handleSubmit}>
        <InputsFormCustomer state={customerSelected} onChange={handleChange} />
        <div className="flex justify-end mt-4">
          <ButtonSubmit label="Actualizar Cliente" disabled={disabled} />
        </div>
      </form>
    </>
  )
}
