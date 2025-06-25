import type { CustomerResponseDto } from '../dtos/customer/customer-response.dto'
import type { CustomerCreateDto } from '../services/jobs-service'

export interface CustomerType extends CustomerResponseDto {}

export type CustomerFormType = Omit<
  CustomerCreateDto,
  'workshopId' | 'vehicleId' | 'email' | 'phoneNumber'
> & { email: string; phoneNumber: string }
