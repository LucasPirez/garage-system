export type ToastSeverity = 'success' | 'error' | 'warning' | 'info'

export interface Toast {
  id: string
  title: string
  message: string
  severity: ToastSeverity
  duration?: number
}

export interface ToastContextType {
  toasts: Toast[]
  addToast: (toast: Omit<Toast, 'id'>) => void
  removeToast: (id: string) => void
  clearAllToasts: () => void
}
