export interface AuthResponseDto {
  email: string
  token: string
  workShop: {
    address: string
    createdAt: string
    email: string
    id: string
    name: string
    phoneNumber: string
  }
}
