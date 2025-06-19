//Local Storage
export const LocalStorageKeys = {
  USER: 'USER',
  HASH: 'HASH',
} as const

export type LocalStorageObjectTypes = {
  [LocalStorageKeys.USER]: string
  [LocalStorageKeys.HASH]: { hash: string }
}

export type LocalStorageKeysType = keyof LocalStorageObjectTypes

//Session Storage
export const SessionStorageKeys = {
  TOKEN: 'TOKEN',
} as const

export type SessionStorageObjectTypes = {
  [SessionStorageKeys.TOKEN]: { token: string }
}
