import { ChangeEvent, useState } from 'react'
import { JobCreateDto } from '../../../../core/services/jobs-service'
import { InputsFormCustomer } from '../../customer/inputs-form-customer'
import { InputsFormVehicle } from '../../vehicle/inputs-form-vehicle'
import { InputsFormJob } from '../inputs-form-job'
import {
  customerService,
  jobService,
  vehicleService,
} from '../../../../core/services'
import { useRegisterJobContext } from '../../../context/register-job-context'
import { ButtonClose } from '../../buttons/button-close-icon'
import { VehicleItem } from './vehicle-item'
import { CustomerItem } from './customer-item'
import { ButtonSubmit } from '../../buttons/button-submit'
import { CustomerCreateDto } from '../../../../core/dtos/customer/customer-request.dto'
import { VehicleCreateDto } from '../../../../core/dtos/vehicle/vehicle-request.dto'
import { triggerCoolDown } from '../../../../core/helpers/triggerCoolDown'
import { useToast } from '../../../context/toast-context'
import { useLoader } from '../../../context/loader-context'

type FormDataType = Omit<JobCreateDto, 'workshopId' | 'vehicleId'> &
  Omit<VehicleCreateDto, 'customerId'> &
  Omit<
    CustomerCreateDto,
    'workshopId' | 'vehicleId' | 'email' | 'phoneNumber'
  > & {
    email: string
    phoneNumber: string
  }

const FormInitialState: FormDataType = {
  cause: '',
  color: '',
  details: '',
  email: '',
  firstName: '',
  lastName: '',
  model: '',
  phoneNumber: '',
  plate: '',
  receptionDate: new Date().toISOString().split('T')[0],
}

export const RegisterJob = () => {
  const [formData, setFormData] = useState<FormDataType>(FormInitialState)
  const {
    customerSelected,
    handleCustomerSelect,
    vehicleSelected,
    handleVehicleSelect,
  } = useRegisterJobContext()
  const { addToast } = useToast()
  const { showLoader, hideLoader } = useLoader()

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target

    setFormData((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const {
      cause,
      color,
      details,
      email,
      firstName,
      lastName,
      phoneNumber,
      plate,
      model,
      receptionDate,
    } = formData
    try {
      let customerId: string | undefined = customerSelected?.id
      let vehicleId: string | undefined = vehicleSelected?.id

      if (!triggerCoolDown()) {
        addToast({
          severity: 'error',
          title: 'Error',
          message: 'Demasiadas solicitudes, por favor espere un momento.',
        })
        return
      }
      showLoader()
      if (!customerSelected?.firstName) {
        const customerResponse = await customerService.create({
          email: [email],
          firstName,
          lastName,
          phoneNumber: [phoneNumber],
        })
        customerId = customerResponse
      }

      if (!customerId) throw new Error('Id del customer es' + typeof vehicleId)

      if (!vehicleSelected) {
        const vehicleResponse = await vehicleService.create({
          color,
          customerId: customerId,
          model,
          plate,
        })
        vehicleId = vehicleResponse
      }

      if (!vehicleId) throw new Error('Id del vehiculo es' + typeof vehicleId)

      await jobService.create({
        cause,
        details,
        receptionDate: new Date(receptionDate).toISOString(),
        vehicleId,
      })

      setFormData(FormInitialState)
      addToast({
        severity: 'success',
        title: 'Éxito',
        message: 'Servicio registrado correctamente',
      })
    } catch (error) {
      addToast({
        severity: 'error',
        title: 'Error',
        message: 'Ocurrio un error al registrar el servicio',
      })
    } finally {
      hideLoader()
    }
  }

  return (
    <>
      <div>
        <form onSubmit={handleSubmit} className="pt-9">
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
              <InputsFormCustomer state={formData} onChange={handleChange} />
            )}
          </div>
          <div className="border-t border-gray-500 mt-5 mb-4"></div>

          <div className="mb-4">
            <div className="flex justify-between items-center mb-4">
              <div className="flex items-center">
                <span className="text-2xl mr-3">🚙</span>
                <h2 className="text-xl font-semibold text-gray-800">
                  Vehículo
                </h2>
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
              <InputsFormVehicle state={formData} onChange={handleChange} />
            )}
          </div>

          <div className="border-t border-gray-500 mt-7 mb-4"></div>

          <div className="mb-3">
            <div className="flex items-center mb-5">
              <span className="text-2xl mr-3">🔧</span>
              <h2 className="text-xl font-semibold text-gray-800">Servicio</h2>
            </div>

            <InputsFormJob state={formData} onChange={handleChange} />
          </div>

          <div className="flex flex-col sm:flex-row gap-4 justify-end">
            <button
              type="reset"
              onClick={() => setFormData(FormInitialState)}
              className="px-4 py-3 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50   font-medium active:scale-95"
            >
              🗑️ Limpiar Formulario
            </button>

            <ButtonSubmit label="✅ Registrar Servicio" className="py-3" />
          </div>
        </form>
      </div>

      <div className="text-center text-sm text-gray-500">
        <p>Los campos marcados con * son obligatorios</p>
      </div>
    </>
  )
}
