import { AxiosInstance } from 'axios'
import { CustomerResponseDto } from './dtos/customer/customer-response.dto'
import { VehicleResponseDto } from './dtos/vehicle/vehicle-response.dto'
import { VehicleEntryResponseDto } from './dtos/vehicleEntry/vehicleEntry-response.dto'

const getWorkshopId = () => {
  return 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'
}

export class WorkshopService {
  private readonly PATHS = {
    VEHICLES: (workshopId: string) => `workshops/${workshopId}/vehicles`,
    CUSTOMERS: (workshopId: string) => `workshops/${workshopId}/customers`,
    VEHICLE_ENTRIES: (workshopId: string) =>
      `workshops/${workshopId}/vehicles-entries`,
  }
  constructor(private readonly client: AxiosInstance) {}

  private async get<T>(url: string): Promise<T> {
    const { data } = await this.client.get<T>(url)
    return data
  }

  async getAllCustomers(): Promise<CustomerResponseDto[]> {
    return await this.get(this.PATHS.CUSTOMERS(getWorkshopId()))
  }

  async getAllVehicles(): Promise<VehicleResponseDto> {
    return await this.get(this.PATHS.VEHICLES(getWorkshopId()))
  }

  async getAllVehicleEntries(): Promise<VehicleEntryResponseDto> {
    return await this.get(this.PATHS.VEHICLE_ENTRIES(getWorkshopId()))
  }
}
