export class CustomError extends Error {
  public readonly name = 'CustomError'
  public readonly cause?: unknown
  public readonly statusCode?: number

  constructor(message: string, statusCode?: number, cause?: unknown) {
    super(message)
    this.cause = cause
    this.statusCode = statusCode

    Object.setPrototypeOf(this, new.target.prototype)
  }
}

export interface ErrorResponse {
  StatusCode: number
  Message: string
  Detail?: string
}
