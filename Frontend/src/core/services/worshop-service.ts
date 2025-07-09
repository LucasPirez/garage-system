import { AxiosInstance } from 'axios'
import { type CustomerResponseDto } from '../dtos/customer/customer-response.dto'
import { type VehicleResponseDto } from '../dtos/vehicle/vehicle-response.dto'
import { type JobsResponseDto } from '../dtos/vehicleEntry/jobs-response.dto'
import { localStorageService, localKeys } from '../storage/storages'

export const getWorkshopId = (): string => {
  const workshopId = localStorageService.getItem(localKeys.WORKSHOP)?.id

  if (!workshopId) throw new Error()

  return workshopId
}

export class WorkshopService {
  private readonly PATHS = {
    VEHICLES: (workshopId: string) => `workshops/${workshopId}/vehicles`,
    CUSTOMERS: (workshopId: string) => `workshops/${workshopId}/customers`,
    VEHICLE_ENTRIES: (workshopId: string) =>
      `workshops/${workshopId}/repair-order`,
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

  async getAllVehicleEntries(): Promise<JobsResponseDto[]> {
    return await this.get(this.PATHS.VEHICLE_ENTRIES(getWorkshopId()))
  }
}
