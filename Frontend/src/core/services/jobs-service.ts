import { AxiosInstance } from 'axios'
import { JobsResponseDto } from '../dtos/vehicleEntry/jobs-response.dto'
import { JobUpdateDto } from '../dtos/vehicleEntry/job-update.dto'
import { JOBS_STATUS } from '../constants/jobs-status'
import { getWorkshopId } from './worshop-service'
import { CustomerCreateDto } from '../dtos/customer/customer-request.dto'
import { VehicleCreateDto } from '../dtos/vehicle/vehicle-request.dto'

export interface JobCreateDto {
  receptionDate: string
  cause: string
  details: string
  vehicleId: string
  workshopId: string
}

export class JobsService {
  private readonly BASE_PATH = '/repair-order'

  constructor(private readonly client: AxiosInstance) {}

  async create(job: Omit<JobCreateDto, 'workshopId'>): Promise<void> {
    const response = await this.client.post(this.BASE_PATH, {
      ...job,
      workshopId: getWorkshopId(),
    })

    if (response.status !== 201) {
      alert('Error al crear trabajo')
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
}

export class VehicleService {
  private readonly PATHS = {
    create: '/vehicle',
  }
  constructor(private readonly client: AxiosInstance) {}

  async create(vehicle: VehicleCreateDto): Promise<string> {
    const { data } = await this.client.post(this.PATHS.create, vehicle)

    return data.id
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
