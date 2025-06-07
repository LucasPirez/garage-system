import { AxiosInstance } from 'axios'
import { workshopId } from '../constants/workshopId'

export interface JobCreateDto {
  receptionDate: string
  cause: string
  details: string
  vehicleId: string
  workshopId: string
}

export interface VehicleCreateDto {
  plate: string
  model: string
  color: string
  customerId: string
}

export interface CustomerCreateDto {
  firstName: string
  lastName: string
  phoneNumber: string[]
  email: string[]
  workshopId: string
}

export class JobsService {
  private readonly PATHS = {
    create: '/repair-order',
  }
  constructor(private readonly client: AxiosInstance) {}

  async create(job: Omit<JobCreateDto, 'workshopId'>): Promise<void> {
    const response = await this.client.post(this.PATHS.create, {
      ...job,
      workshopId,
    })

    if (response.status !== 201) {
      alert('Error al crear trabajo')
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
      workshopId,
    })

    return data.id
  }
}
