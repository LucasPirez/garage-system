import { AxiosInstance } from 'axios'

interface AuthResponseDto {
  email: string
  token: string
  workShopId: string
  workShopName: string
}

export class AuthService {
  private readonly BASE_PATH = '/auth'
  constructor(private readonly client: AxiosInstance) {}

  async login(auth: {
    email: string
    password: string
  }): Promise<AuthResponseDto> {
    const response = await this.client.post<AuthResponseDto>(
      `${this.BASE_PATH}/login`,
      auth,
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    )

    return response.data
  }
}
