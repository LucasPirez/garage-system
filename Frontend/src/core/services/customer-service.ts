import { AxiosInstance } from 'axios'
import { CustomerCreateDto } from '../dtos/customer/customer-request.dto'
import { getWorkshopId } from './worshop-service'

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
