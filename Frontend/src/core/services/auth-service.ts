import { AxiosInstance } from 'axios'
import { AuthResponseDto } from '../dtos/auth/auth-response.dto'
import { handleRequest } from '../helpers/handle-errors'
import { RequestChangePasswordDto } from '../dtos/auth/change-password.dto'

export class AuthService {
  private readonly BASE_PATH = '/auth'
  constructor(private readonly client: AxiosInstance) {}

  async login(auth: {
    email: string
    password: string
  }): Promise<AuthResponseDto> {
    const response = await handleRequest(() =>
      this.client.post<AuthResponseDto>(`${this.BASE_PATH}/login`, auth)
    )

    return response.data
  }

  async requestChangePassword(email: string) {
    await handleRequest(() =>
      this.client.patch(`${this.BASE_PATH}/request-reset-password`, { email })
    )
  }

  async changePassword(payload: RequestChangePasswordDto) {
    await handleRequest(() =>
      this.client.patch(`${this.BASE_PATH}/change-password`, payload)
    )
  }
}
