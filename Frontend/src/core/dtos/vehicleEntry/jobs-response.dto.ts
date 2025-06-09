export interface JobsResponseDto {
  id: string
  receptionDate: string
  deliveryDate: string | null
  notificationSent: boolean
  createdAt: string
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
  }
  spareParts: SparePart[]
}

export interface SparePart {
  name: string
  price: number
  quantity: number
}
