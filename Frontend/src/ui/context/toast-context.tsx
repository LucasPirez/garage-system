import { createContext, useContext, useState, useCallback } from 'react'
import type { Toast, ToastContextType } from '../../core/type/toast'

const ToastContext = createContext<ToastContextType | undefined>(undefined)

export function ToastProvider({ children }: { children: React.ReactNode }) {
  const [toasts, setToasts] = useState<Toast[]>([])

  const createToast = (toast: Omit<Toast, 'id'>) => {
    const id = Math.random().toString(36).substring(2, 9)

    setToasts((prev) => [...prev, { ...toast, id }])

    setTimeout(() => {
      removeToast(id)
    }, toast.duration)
  }

  const showToast = {
    success: useCallback((toast: Partial<Omit<Toast, 'id' | 'severity'>>) => {
      const newToast: Omit<Toast, 'id'> = {
        duration: toast.duration || 5000,
        title: 'Exito',
        message: 'Accion realizada con exito.',
        severity: 'success',
        ...toast,
      }

      createToast(newToast)
    }, []),
    error: useCallback((toast: Partial<Omit<Toast, 'id' | 'severity'>>) => {
      const newToast: Omit<Toast, 'id'> = {
        duration: toast.duration || 5000,
        title: 'Error',
        message: 'Ocurrio un error.',
        severity: 'error',
        ...toast,
      }

      createToast(newToast)
    }, []),
  }

  const addToast = useCallback((toast: Omit<Toast, 'id'>) => {
    const id = Math.random().toString(36).substring(2, 9)
    const newToast: Toast = {
      ...toast,
      id,
      duration: toast.duration || 5000,
    }

    setToasts((prev) => [...prev, newToast])

    setTimeout(() => {
      removeToast(id)
    }, newToast.duration)
  }, [])

  const removeToast = useCallback((id: string) => {
    setToasts((prev) => prev.filter((toast) => toast.id !== id))
  }, [])

  const clearAllToasts = useCallback(() => {
    setToasts([])
  }, [])

  return (
    <ToastContext.Provider
      value={{ toasts, addToast, removeToast, clearAllToasts, showToast }}
    >
      {children}
    </ToastContext.Provider>
  )
}

export function useToast() {
  const context = useContext(ToastContext)
  if (context === undefined) {
    throw new Error('useToast must be used within a ToastProvider')
  }
  return context
}
