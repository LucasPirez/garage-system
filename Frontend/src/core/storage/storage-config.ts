import { AuthResponseDto } from '../dtos/auth/auth-response.dto'

//Local Storage
export const LocalStorageKeys = {
  USER: 'USER',
  HASH: 'HASH',
  WORKSHOP: 'WORKSHOP',
} as const

export type LocalStorageObjectTypes = {
  [LocalStorageKeys.USER]: {
    email: string
    token: string
  }
  [LocalStorageKeys.HASH]: { hash: string }
  [LocalStorageKeys.WORKSHOP]: AuthResponseDto['workShop']
}

export type LocalStorageKeysType = keyof LocalStorageObjectTypes

//Session Storage
export const SessionStorageKeys = {
  TOKEN: 'TOKEN',
} as const

export type SessionStorageObjectTypes = {
  [SessionStorageKeys.TOKEN]: { token: string }
}
