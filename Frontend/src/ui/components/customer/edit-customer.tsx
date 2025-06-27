import { ChangeEvent, useState } from 'react'
import { InputsFormCustomer } from './inputs-form-customer'
import type {
  CustomerFormType,
  CustomerType,
} from '../../../core/type/customer'
import { ButtonSubmit } from '../common/button-submit'
import { useToast } from '../../context/toast-context'
import { updateCustomerVehicleService } from '../../../core/services'

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
        <ButtonSubmit
          label="Actualizar Cliente"
          className="mt-4"
          disabled={disabled}
        />
      </form>
    </>
  )
}
