abstract class AbstractStorage<K extends Record<string, unknown>> {
  private Storage: Storage

  constructor(storage: Storage) {
    this.Storage = storage
  }

  setItem<T extends Extract<keyof K, string>>(key: T, value: K[T]): void {
    try {
      const jsonValue = JSON.stringify(value)
      this.Storage.setItem(key, jsonValue)
    } catch {
      alert('Error en al setear el local storage')
    }
  }

  getItem<T extends Extract<keyof K, string>>(key: T): K[T] | null {
    try {
      const jsonValue = this.Storage.getItem(key)
      const value = jsonValue != null ? JSON.parse(jsonValue) : null

      return value
    } catch {
      return null
    }
  }

  removeItem(key: Extract<keyof K, string>): void {
    try {
      this.Storage.removeItem(key)
    } catch {
      alert('Error al remover item del local storage')
    }
  }

  clear(): void {
    try {
      this.Storage.clear()
    } catch {
      alert('error al limpiar el localstorage')
    }
  }
}

export { AbstractStorage }
