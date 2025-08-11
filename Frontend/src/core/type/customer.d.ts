import type { CustomerResponseDto } from '../dtos/customer/customer-response.dto'
import type { CustomerCreateDto } from '../dtos/customer/customer-request.dto'

export interface CustomerType extends Omit<CustomerResponseDto, 'vehicles'> {
  email: string
  phoneNumber: string
}

export type CustomerFormType = Omit<
  CustomerCreateDto,
  'workshopId' | 'vehicleId' | 'email' | 'phoneNumber' | 'id'
> & { email: string; phoneNumber: string }
