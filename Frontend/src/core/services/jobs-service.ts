import { AxiosInstance } from 'axios'
import {
  JobsResponseDto,
  SparePart,
} from '../dtos/vehicleEntry/jobs-response.dto'
import { JobUpdateDto } from '../dtos/vehicleEntry/job-update.dto'
import { JOBS_STATUS } from '../constants/jobs-status'
import { getWorkshopId } from './worshop-service'
import { CustomerCreateDto } from '../dtos/customer/customer-request.dto'
import {
  BaseVehicleDto,
  VehicleCreateDto,
} from '../dtos/vehicle/vehicle-request.dto'
import { handleRequest } from '../helpers/handle-errors'

export interface JobCreateDto {
  id: string
  receptionDate: string
  cause: string
  details: string
  workshopId: string
  vehicle: BaseVehicleDto
}

export interface JobCreateWithVehicle
  extends Omit<JobCreateDto, 'workshopId' | 'vehicle'> {
  vehicleDto: VehicleCreateDto
}

export interface JobCreateWithVehicleAndCustomer
  extends Omit<JobCreateDto, 'workshopId' | 'vehicle'> {
  vehicleDto: VehicleCreateDto
  customerDto: Omit<CustomerCreateDto, 'workshopId'>
}

export class JobsService {
  private readonly BASE_PATH = '/repair-order'

  constructor(private readonly client: AxiosInstance) {}

  async create(job: Omit<JobCreateDto, 'workshopId'>): Promise<void> {
    await handleRequest(() =>
      this.client.post(this.BASE_PATH, {
        ...job,
        workshopId: getWorkshopId(),
      })
    )
  }

  async createWithVehicle(job: JobCreateWithVehicle): Promise<void> {
    await handleRequest(() =>
      this.client.post(`${this.BASE_PATH}/with-vehicle`, {
        ...job,
        workshopId: getWorkshopId(),
      })
    )
  }

  async createWithVehicleAndCustomer(
    job: JobCreateWithVehicleAndCustomer
  ): Promise<void> {
    await handleRequest(() =>
      this.client.post(`${this.BASE_PATH}/with-vehicle-and-customer`, {
        ...job,
        workshopId: getWorkshopId(),
      })
    )
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
