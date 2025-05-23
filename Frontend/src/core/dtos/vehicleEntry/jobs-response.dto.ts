export interface JobsResponseDto {
  id: string
  receptionDate: Date
  deliveryDate: Date | null
  notificationSent: boolean
  status: 'InProgress' | 'Completed' | 'Cancelled'
  cause: string
  details: string
  budget: number
  finalAmount: number
  vehicle: {
    id: string
    plate: string
    model: string
  }
  client: {
    id: string
    firstName: string
    lastName: string
    phoneNumber: number[]
    email: string[]
  }
}
