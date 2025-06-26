import { AxiosInstance } from 'axios'
import type { CustomerUpdateDto } from '../dtos/customer/customer-request.dto'
import type { VehicleUpdateDto } from '../dtos/vehicle/vehicle-request.dto'

export class UpdateCustomerVehicleService {
  private readonly BASE_PATH = {
    VEHICLE: 'vehicle',
    CUSTOMER: '/customer',
  }
  constructor(private readonly client: AxiosInstance) {}

  async updateCustomer(data: CustomerUpdateDto, id: string): Promise<void> {
    const response = await this.client.put(
      `${this.BASE_PATH.CUSTOMER}/${id}`,
      data
    )

    if (response.status !== 200) {
      alert('Error al actualizar el trabajo')
    }
  }

  async updateVehicle(data: VehicleUpdateDto, id: string): Promise<void> {
    const response = await this.client.put(
      `${this.BASE_PATH.VEHICLE}/${id}`,
      data
    )

    if (response.status !== 200) {
      alert('Error al actualizar el vehículo')
    }
  }
}
