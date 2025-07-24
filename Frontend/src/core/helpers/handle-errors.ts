import { AxiosError } from 'axios'
import { CustomError, ErrorResponse } from './custom-error'

export const handleRequest = async <T>(fn: () => Promise<T>): Promise<T> => {
  try {
    return await fn()
  } catch (error) {
    if (error instanceof AxiosError) {
      if (error?.response?.data && isErrorResponse(error.response.data)) {
        throw new CustomError(
          error.response.data.Message,
          error.response.data.StatusCode,
          error.response.data?.Detail
        )
      } else {
        throw new Error(`Invalid error structure from backend: error structure must be {
                            StatusCode: number
                            Message: string
                            Detail?: string
                            }`)
      }
    }
    throw new CustomError('Ocurrio un Error inesperado.')
  }
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const isErrorResponse = (data: any): data is ErrorResponse => {
  if (
    typeof data === 'object' &&
    data !== undefined &&
    typeof data.StatusCode === 'number' &&
    typeof data.Message === 'string'
  ) {
    return true
  }
  return false
}
