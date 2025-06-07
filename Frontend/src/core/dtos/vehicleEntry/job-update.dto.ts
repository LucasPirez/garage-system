export interface JobUpdateDto {
  id: string
  receptionDate: string
  cause: string
  details: string
  budget: number
  deliveryDate: string
  notificationSent: boolean
  finalAmount: number
  spareParts: SparePart[]
}

export interface SparePart {
  name: string
  price: number
  quantity: number
}
