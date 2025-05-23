import { AxiosInstance } from 'axios'

export class JobsServices {
  private readonly PATHS = {
    VEHICLES: (workshopId: string) => `workshops/${workshopId}/vehicles`,
    CUSTOMERS: (workshopId: string) => `workshops/${workshopId}/customers`,
    VEHICLE_ENTRIES: (workshopId: string) =>
      `workshops/${workshopId}/vehicles-entries`,
  }
  constructor(private readonly client: AxiosInstance) {}
}
