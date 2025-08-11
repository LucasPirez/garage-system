import { AxiosError } from 'axios'
import { CustomError, ErrorResponse } from './custom-error'

export const handleRequest = async <T>(fn: () => Promise<T>): Promise<T> => {
  try {
    return await fn()
  } catch (error) {
    if (error instanceof AxiosError) {
      if (error?.response?.data && isErrorResponse(error.response.data)) {
        console.log(error.response.data.Message)
        console.log(error.response.data?.Detail)

        throw new CustomError(
          error.response.data.Message,
          error.response.data.StatusCode,
          error.response.data?.Detail
        )
      } else {
        console.log('Invalid error structure', error.response?.data?.errors)
        throw new Error(`Invalid error structure from backend: error structure must be {
                            StatusCode: number
                            Message: string
                            Detail?: string
                            }`)
      }
    }
    console.log(error)
    throw new CustomError('Ocurrio un Error inesperado.', 500, error)
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
