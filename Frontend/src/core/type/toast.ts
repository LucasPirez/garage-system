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
  showToast: {
    success: (toast: Partial<Omit<Toast, 'id' | 'severity'>>) => void
    error: (toast: Partial<Omit<Toast, 'id' | 'severity'>>) => void
  }
  removeToast: (id: string) => void
  clearAllToasts: () => void
}
