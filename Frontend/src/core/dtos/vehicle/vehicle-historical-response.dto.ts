import { SparePart } from '../vehicleEntry/jobs-response.dto'

export interface VehicleHistoricalResponseDto {
  id: string
  receptionDate: string
  deliveryDate: string | null
  notificationSend: boolean
  budget: number
  cause: string
  details: string
  finalAmount: number
  status: 'InProgress' | 'Completed'
  spareParts: SparePart[]
}
