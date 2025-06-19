import { AbstractStorage } from './abstract-storage'
import {
  LocalStorageKeys,
  LocalStorageObjectTypes,
  SessionStorageKeys,
  SessionStorageObjectTypes,
} from './storage-config'

class LocalStorageUtility extends AbstractStorage<LocalStorageObjectTypes> {
  public keys = LocalStorageKeys
  constructor() {
    super(localStorage)
  }
}

class SessionStorage extends AbstractStorage<SessionStorageObjectTypes> {
  public keys = SessionStorageKeys

  constructor() {
    super(sessionStorage)
  }
}

const sessionKeys = SessionStorageKeys
const localKeys = LocalStorageKeys

const sessionStorageService = new SessionStorage()
const localStorageService = new LocalStorageUtility()

export { sessionStorageService, localStorageService, sessionKeys, localKeys }
