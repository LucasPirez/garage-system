import { AxiosInstance } from 'axios'
import { VehicleCreateDto } from '../dtos/vehicle/vehicle-request.dto'
import { JobType } from '../type/job'

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
