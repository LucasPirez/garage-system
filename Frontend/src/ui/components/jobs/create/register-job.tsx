import { ChangeEvent, useState } from 'react'
import { JobCreateDto } from '../../../../core/services/jobs-service'
import { jobService } from '../../../../core/services'
import { useRegisterJobContext } from '../../../context/register-job-context'
import { CustomerCreateDto } from '../../../../core/dtos/customer/customer-request.dto'
import { VehicleCreateDto } from '../../../../core/dtos/vehicle/vehicle-request.dto'
import { triggerCoolDown } from '../../../../core/helpers/triggerCoolDown'
import { useToast } from '../../../context/toast-context'
import { useLoader } from '../../../context/loader-context'
import { CustomError } from '../../../../core/helpers/custom-error'
import { RegisterJobForm } from './register-job-form'
import { ButtonSubmit } from '../../buttons/button-submit'
import { v7 } from 'uuid'

export type FormDataType = Omit<JobCreateDto, 'workshopId' | 'id' | 'vehicle'> &
  Omit<VehicleCreateDto, 'customerId' | 'id'> &
  Omit<
    CustomerCreateDto,
    'workshopId' | 'vehicleId' | 'email' | 'phoneNumber' | 'id'
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
  const { customerSelected, vehicleSelected } = useRegisterJobContext()
  const { showToast } = useToast()
  const { showLoader, hideLoader } = useLoader()

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target

    setFormData((prev) => ({ ...prev, [name]: value }))
  }

  const handleSuccess = () => {
    setFormData(FormInitialState)
    showToast.success({
      message: 'Servicio registrado correctamente',
    })
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
      if (!triggerCoolDown()) {
        showToast.error({
          message: 'Demasiadas solicitudes, por favor espere un momento.',
        })
        return
      }
      showLoader()

      const receptionDateISO = new Date(receptionDate).toISOString()

      const basePayload = {
        cause,
        details,
        receptionDate: receptionDateISO,
        id: v7(),
      }

      if (!customerSelected && !vehicleSelected) {
        const customerId = v7()
        await jobService.createWithVehicleAndCustomer({
          ...basePayload,
          vehicleDto: { color, model, plate, id: v7(), customerId },
          customerDto: {
            email: [email],
            firstName,
            lastName,
            phoneNumber: [phoneNumber],
            id: customerId,
          },
        })
        handleSuccess()
        return
      }

      if (!vehicleSelected && customerSelected) {
        await jobService.createWithVehicle({
          ...basePayload,
          vehicleDto: {
            id: v7(),
            color,
            model,
            plate,
            customerId: customerSelected.id,
          },
        })
        handleSuccess()

        return
      }

      if (!vehicleSelected || !customerSelected) {
        throw new Error('Vehículo o customer no seleccionado')
      }

      await jobService.create({
        ...basePayload,
        vehicle: { ...vehicleSelected, customerId: customerSelected.id },
      })

      handleSuccess()
    } catch (error) {
      if (error instanceof CustomError) {
        showToast.error({
          message: error.message,
        })
        return
      }
      showToast.error({
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
          <RegisterJobForm onChange={handleChange} formData={formData} />
          <div className="flex flex-col sm:flex-row gap-4 justify-end">
            <button
              type="reset"
              onClick={() => setFormData(FormInitialState)}
              className="px-4 py-3 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50   font-medium active:scale-95">
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
