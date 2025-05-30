import { AxiosInstance } from 'axios'
import { type CustomerResponseDto } from '../dtos/customer/customer-response.dto'
import { type VehicleResponseDto } from '../dtos/vehicle/vehicle-response.dto'
import { type JobsResponseDto } from '../dtos/vehicleEntry/jobs-response.dto'
import { workshopId } from '../constants/workshopId'
import { fakeData } from '.'

const getWorkshopId = () => {
  return workshopId
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
    console.log(data)

    return data
  }

  async getAllCustomers(): Promise<CustomerResponseDto[]> {
    // return await this.get(this.PATHS.CUSTOMERS(getWorkshopId()))

    return fakeData.data as CustomerResponseDto[]
  }

  async getAllVehicles(): Promise<VehicleResponseDto> {
    return await this.get(this.PATHS.VEHICLES(getWorkshopId()))
  }

  async getAllVehicleEntries(): Promise<JobsResponseDto[]> {
    // return await this.get(this.PATHS.VEHICLE_ENTRIES(getWorkshopId()))

    return fakeData.dataJobs as JobsResponseDto[]
  }
}
