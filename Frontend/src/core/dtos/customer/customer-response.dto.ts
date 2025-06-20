import { VehicleResponseDto } from '../vehicle/vehicle-response.dto'

export interface CustomerResponseDto {
  firstName: string
  lastName: string
  phoneNumber: string[]
  email: string[]
  id: string
  vehicle: VehicleResponseDto[]
}
