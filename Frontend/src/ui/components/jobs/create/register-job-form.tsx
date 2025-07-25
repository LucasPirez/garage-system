import { ChangeEvent } from 'react'
import { useRegisterJobContext } from '../../../context/register-job-context'
import { ButtonClose } from '../../buttons/button-close-icon'
import { InputsFormCustomer } from '../../customer/inputs-form-customer'
import { InputsFormVehicle } from '../../vehicle/inputs-form-vehicle'
import { InputsFormJob } from '../inputs-form-job'
import { CustomerItem } from './customer-item'
import { VehicleItem } from './vehicle-item'
import { FormDataType } from './register-job'

interface Props {
  onChange: (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => void
  formData: FormDataType
}

export const RegisterJobForm = ({ onChange, formData }: Props) => {
  const {
    customerSelected,
    handleCustomerSelect,
    vehicleSelected,
    handleVehicleSelect,
  } = useRegisterJobContext()

  return (
    <>
      <div className="mb-3">
        <div className="flex items-center mb-6">
          <span className="text-2xl mr-3">👤</span>
          <h2 className="text-xl font-semibold text-gray-800">Cliente</h2>
        </div>
        {customerSelected?.firstName ? (
          <div className="inline-flex items-center gap-3 bg-white border border-gray-200 rounded-lg px-4 py-3 shadow-sm hover:shadow-md">
            <CustomerItem customer={customerSelected} />

            <ButtonClose
              onClick={() => {
                handleCustomerSelect(null)
                handleVehicleSelect(null)
              }}
            />
          </div>
        ) : (
          <InputsFormCustomer state={formData} onChange={onChange} />
        )}
      </div>
      <div className="border-t border-gray-500 mt-5 mb-4"></div>

      <div className="mb-4">
        <div className="flex justify-between items-center mb-4">
          <div className="flex items-center">
            <span className="text-2xl mr-3">🚙</span>
            <h2 className="text-xl font-semibold text-gray-800">Vehículo</h2>
          </div>
          {vehicleSelected && (
            <button
              className=" text-blue-500  border border-blue-500 rounded-lg px-3 py-1 text-sm font-medium hover:bg-blue-50 transition-colors"
              onClick={() => handleVehicleSelect(null)}
            >
              <span>+</span> Agregar otro vehículo
            </button>
          )}
        </div>

        {vehicleSelected ? (
          <div className="flex gap-2 flex-wrap ">
            {customerSelected?.vehicles.map((vehicle) => (
              <VehicleItem
                onClick={() => handleVehicleSelect(vehicle)}
                selected={vehicleSelected?.id === vehicle.id}
                vehicle={vehicle}
              />
            ))}
          </div>
        ) : (
          <InputsFormVehicle state={formData} onChange={onChange} />
        )}
      </div>

      <div className="border-t border-gray-500 mt-7 mb-4"></div>

      <div className="mb-3">
        <div className="flex items-center mb-5">
          <span className="text-2xl mr-3">🔧</span>
          <h2 className="text-xl font-semibold text-gray-800">Servicio</h2>
        </div>

        <InputsFormJob state={formData} onChange={onChange} />
      </div>
    </>
  )
}
