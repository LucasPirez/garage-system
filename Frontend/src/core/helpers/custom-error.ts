export class CustomError extends Error {
  constructor(message: string) {
    super(message)
  }
}

export interface ErrorResponse {
  StatusCode: number
  Message: string
  Detail?: string
}
