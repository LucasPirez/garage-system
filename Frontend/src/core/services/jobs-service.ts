import { AxiosError, AxiosInstance } from 'axios'
import {
  JobsResponseDto,
  SparePart,
} from '../dtos/vehicleEntry/jobs-response.dto'
import { JobUpdateDto } from '../dtos/vehicleEntry/job-update.dto'
import { JOBS_STATUS } from '../constants/jobs-status'
import { getWorkshopId } from './worshop-service'
import { CustomerCreateDto } from '../dtos/customer/customer-request.dto'
import { VehicleCreateDto } from '../dtos/vehicle/vehicle-request.dto'
import { JobType } from '../type/job'
import { CustomError, ErrorResponse } from '../helpers/custom-error'

export interface JobCreateDto {
  receptionDate: string
  cause: string
  details: string
  vehicleId: string
  workshopId: string
}

export interface JobCreateWithVehicle
  extends Omit<JobCreateDto, 'workshopId' | 'vehicleId'> {
  vehicleDto: VehicleCreateDto
}

export interface JobCreateWithVehicleAndCustomer
  extends Omit<JobCreateDto, 'workshopId' | 'vehicleId'> {
  vehicleDto: Omit<VehicleCreateDto, 'customerId'>
  customerDto: Omit<CustomerCreateDto, 'workshopId'>
}

export class JobsService {
  private readonly BASE_PATH = '/repair-order'

  constructor(private readonly client: AxiosInstance) {}

  async create(job: Omit<JobCreateDto, 'workshopId'>): Promise<void> {
    try {
      await this.client.post(this.BASE_PATH, {
        ...job,
        workshopId: getWorkshopId(),
      })
    } catch (error) {
      if (error instanceof AxiosError) {
        if (error.response && error.response.status === 409) {
          const errorResponse = error.response.data as ErrorResponse

          throw new CustomError(errorResponse.Message)
        }
      }
      throw new CustomError('Ocurrio un Error inesperado.')
    }
  }

  async createWithVehicle(job: JobCreateWithVehicle): Promise<void> {
    try {
      await this.client.post(`${this.BASE_PATH}/with-vehicle`, {
        ...job,
        workshopId: getWorkshopId(),
      })
    } catch (error) {
      if (error instanceof AxiosError) {
        if (error.response && error.response.status === 409) {
          const errorResponse = error.response.data as ErrorResponse

          throw new CustomError(errorResponse.Message)
        }
      }
      throw new CustomError('Ocurrio un Error inesperado.')
    }
  }

  async createWithVehicleAndCustomer(
    job: JobCreateWithVehicleAndCustomer
  ): Promise<void> {
    try {
      await this.client.post(`${this.BASE_PATH}/with-vehicle-and-customer`, {
        ...job,
        workshopId: getWorkshopId(),
      })
    } catch (error) {
      if (error instanceof AxiosError) {
        if (error.response && error.response.status === 409) {
          const errorResponse = error.response.data as ErrorResponse

          throw new CustomError(errorResponse.Message)
        }
      }
      throw new CustomError('Ocurrio un Error inesperado.')
    }
  }

  async getById(id: string): Promise<JobsResponseDto> {
    const response = await this.client.get<JobsResponseDto>(
      `${this.BASE_PATH}/${id}`
    )

    if (response.status !== 200) {
      alert('Error al obtener el trabajo')
    }

    return response.data
  }

  async update(data: JobUpdateDto): Promise<void> {
    const response = await this.client.put<JobsResponseDto>(
      `${this.BASE_PATH}`,
      data
    )

    if (response.status !== 200) {
      alert('Error al actualizar el trabajo')
    }
  }

  async updateStatusAndAmount(data: {
    id: string
    finalAmount: number
    status: (typeof JOBS_STATUS)[keyof typeof JOBS_STATUS]
  }): Promise<void> {
    const response = await this.client.patch(`${this.BASE_PATH}`, data)

    if (response.status !== 200) {
      alert('Error al actualizar el trabajo')
    }
  }

  async updateSpareParts(payload: SparePart[], id: string): Promise<void> {
    await this.client.patch(`${this.BASE_PATH}/${id}/spare-parts`, payload)
  }
}

export class VehicleService {
  private readonly BASE_PATH = '/vehicle'

  constructor(private readonly client: AxiosInstance) {}

  async create(vehicle: VehicleCreateDto): Promise<string> {
    const { data } = await this.client.post(this.BASE_PATH, vehicle)

    return data.id
  }

  async getHistoryByVehicleId(vehicleId: string): Promise<JobType[]> {
    const { data } = await this.client.get<JobType[]>(
      `${this.BASE_PATH}/${vehicleId}/repair-orders`
    )

    return data
  }
}

export class CustomerService {
  private readonly PATHS = {
    create: '/customer',
  }
  constructor(private readonly client: AxiosInstance) {}

  async create(
    customer: Omit<CustomerCreateDto, 'workshopId'>
  ): Promise<string> {
    const { data } = await this.client.post(this.PATHS.create, {
      ...customer,
      workshopId: getWorkshopId(),
    })

    return data.id
  }
}
