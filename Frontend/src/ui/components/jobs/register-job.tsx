import { ChangeEvent, FormEvent, useState } from 'react'
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
  const { clientSelected, handleClientSelect } = useRegisterJobContext()

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
      const customerResponse = await customerService.create({
        email: [email],
        firstName,
        lastName,
        phoneNumber: [phoneNumber],
      })

      const vehicleResponse = await vehicleService.create({
        color,
        customerId: customerResponse,
        model,
        plate,
      })

      await jobService.create({
        cause,
        details,
        receptionDate: new Date(receptionDate).toISOString(),
        vehicleId: vehicleResponse,
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
              <div>
                <span>{clientSelected.firstName}</span>{' '}
                <span>{clientSelected.lastName}</span>
                <button
                  className="inline-flex items-center justify-center p-2 rounded-md text-gray-700 hover:text-gray-900 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-blue-500"
                  aria-expanded="false"
                  onClick={() => handleClientSelect(null)}>
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
            </div>

            {clientSelected?.vehicle.length ? (
              <>
                <span>{clientSelected.vehicle[0]?.plate}</span>
                <span>{clientSelected.vehicle[0]?.model}</span>
                <button
                  className="inline-flex items-center justify-center p-2 rounded-md text-gray-700 hover:text-gray-900 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-blue-500"
                  aria-expanded="false"
                  onClick={() => handleClientSelect(null)}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                    className="size-6">
                    <path d="M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z" />
                  </svg>
                </button>
              </>
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
