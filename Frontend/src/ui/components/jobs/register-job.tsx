import { ChangeEvent, useState } from 'react'
import {
  CustomerCreateDto,
  JobCreateDto,
  VehicleCreateDto,
} from '../../../core/services/jobs-service'
import { InputsFormCustomer } from '../inputs-form-customer'
import { InputsFormVehicle } from '../inputs-form-vehicle'
import { InputsFormJob } from '../inputs-form-job'
import {
  customerService,
  jobService,
  vehicleService,
} from '../../../core/services'
import { useRegisterJobContext } from '../../context/register-job-context'

type FormDataType = Omit<JobCreateDto, 'workshopId' | 'vehicleId'> &
  Omit<VehicleCreateDto, 'customerId'> &
  Omit<
    CustomerCreateDto,
    'workshopId' | 'vehicleId' | 'email' | 'phoneNumber'
  > & { email: string; phoneNumber: string }

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

// interface Props {
//   onSubmit: (e: FormEvent<HTMLFormElement>) => void
// }

export const RegisterJob = () => {
  const [formData, setFormData] = useState<FormDataType>(FormInitialState)
  const {
    clientSelected,
    handleClientSelect,
    vehicleSelected,
    handleVehicleSelect,
  } = useRegisterJobContext()

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
      let customerId: string | undefined = clientSelected?.id
      let vehicleId: string | undefined = vehicleSelected?.id

      if (!clientSelected?.firstName) {
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
    } catch (error) {
      console.log(error)
      alert('Ocurrio un error')
    }
  }

  return (
    <>
      <div>
        <form onSubmit={handleSubmit} className="p-8">
          <div className="mb-3">
            <div className="flex items-center mb-6">
              <span className="text-2xl mr-3">👤</span>
              <h2 className="text-xl font-semibold text-gray-800">Cliente</h2>
            </div>
            {clientSelected?.firstName ? (
              <div className="inline-flex items-center gap-3 bg-white border border-gray-200 rounded-lg px-4 py-3 shadow-sm hover:shadow-md">
                <div className="flex items-center gap-2">
                  <div className="w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center">
                    <span className="text-blue-600 font-medium text-sm">
                      {clientSelected.firstName[0]}
                      {clientSelected.lastName[0]}
                    </span>
                  </div>
                  <div className="flex flex-col">
                    <span className="text-gray-900 font-medium text-sm">
                      {clientSelected.firstName} {clientSelected.lastName}
                    </span>
                    <span className="text-gray-500 text-xs">
                      {clientSelected?.phoneNumber}
                    </span>
                  </div>
                </div>

                <button
                  className="ml-2 p-1 rounded-full text-gray-400 hover:text-gray-600 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-1 transition-colors duration-150"
                  onClick={() => {
                    handleClientSelect(null)
                    handleVehicleSelect(null)
                  }}
                  aria-label="Deseleccionar cliente">
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                    className="size-6">
                    <path d="M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z" />
                  </svg>
                </button>
              </div>
            ) : (
              <InputsFormCustomer state={formData} onChange={handleChange} />
            )}
          </div>
          <div className="border-t border-gray-500 my-8"></div>

          <div className="mb-4">
            <div className="flex items-center mb-4">
              <span className="text-2xl mr-3">🚙</span>
              <h2 className="text-xl font-semibold text-gray-800">Vehículo</h2>
              {vehicleSelected && (
                <button
                  className="inline-flex items-center justify-center p-2 rounded-md text-gray-700 hover:text-gray-900 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-blue-500"
                  aria-expanded="false"
                  type="button"
                  onClick={() => handleVehicleSelect(null)}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                    className="size-6">
                    <path d="M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z" />
                  </svg>
                </button>
              )}
            </div>

            {vehicleSelected ? (
              <div className="flex gap-2 flex-wrap ">
                {clientSelected?.vehicle.map((vehicle) => (
                  <div
                    key={vehicle.id}
                    className={`
                      relative p-4 rounded-lg border-2 cursor-pointer w-[210px]  
                      ${
                        vehicleSelected?.id === vehicle.id
                          ? 'border-blue-500 bg-blue-50'
                          : 'border-gray-200 bg-white'
                      }
                    `}
                    onClick={() => handleVehicleSelect(vehicle)}>
                    <div className="flex items-center gap-3 ">
                      <div
                        className={`
                        p-2 rounded-full
                        ${
                          vehicleSelected?.id === vehicle.id
                            ? 'bg-blue-400 text-blue-600'
                            : 'bg-gray-100 text-gray-600'
                        }
                      `}></div>

                      <div className="flex-1 min-w-0">
                        <p className="font-medium text-gray-900">
                          {vehicle.model}
                        </p>
                        <div className="flex items-center gap-2 mb-1">
                          <span className="font-semibold  text-lg text-gray-600 ">
                            {vehicle.plate}
                          </span>
                        </div>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            ) : (
              <InputsFormVehicle state={formData} onChange={handleChange} />
            )}
          </div>

          <div className="border-t border-gray-500 my-8"></div>

          <div className="mb-8">
            <div className="flex items-center mb-6">
              <span className="text-2xl mr-3">🔧</span>
              <h2 className="text-xl font-semibold text-gray-800">Servicio</h2>
            </div>

            <InputsFormJob state={formData} onChange={handleChange} />
          </div>

          <div className="bg-gray-50 -mx-8 -mb-8 px-8 py-6 border-t border-gray-200">
            <div className="flex flex-col sm:flex-row gap-4 justify-end">
              <button
                type="reset"
                onClick={() => setFormData(FormInitialState)}
                className="px-6 py-3 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2 transition duration-200 font-medium">
                🗑️ Limpiar Formulario
              </button>

              <button
                type="submit"
                className="px-8 py-3 bg-blue-600 text-white rounded-lg hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 transition duration-200 font-medium shadow-sm">
                ✅ Registrar Servicio
              </button>
            </div>
          </div>
        </form>
      </div>

      <div className="text-center mt-6 text-sm text-gray-500">
        <p>Los campos marcados con * son obligatorios</p>
      </div>
    </>
  )
}
