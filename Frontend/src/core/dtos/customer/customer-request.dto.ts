export interface CustomerCreateDto {
  id: string
  firstName: string
  lastName: string
  phoneNumber: string[]
  email: string[]
  workshopId: string
}

export interface CustomerUpdateDto {
  firstName: string
  lastName: string
  phoneNumber: string
  email: string
}
