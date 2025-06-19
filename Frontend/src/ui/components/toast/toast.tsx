'use client'

import type React from 'react'
import { useEffect, useState } from 'react'
import { X, CheckCircle, XCircle, AlertTriangle, Info } from 'lucide-react'
import { ToastSeverity, Toast as ToastType } from '../../../core/type/toast'

interface ToastProps {
  toast: ToastType
  onRemove: (id: string) => void
}

const severityConfig: Record<
  ToastSeverity,
  {
    bgColor: string
    borderColor: string
    textColor: string
    icon: React.ReactNode
  }
> = {
  success: {
    bgColor: 'bg-green-50',
    borderColor: 'border-green-200',
    textColor: 'text-green-800',
    icon: <CheckCircle className="h-5 w-5 text-green-400" />,
  },
  error: {
    bgColor: 'bg-red-50',
    borderColor: 'border-red-200',
    textColor: 'text-red-800',
    icon: <XCircle className="h-5 w-5 text-red-400" />,
  },
  warning: {
    bgColor: 'bg-yellow-50',
    borderColor: 'border-yellow-200',
    textColor: 'text-yellow-800',
    icon: <AlertTriangle className="h-5 w-5 text-yellow-400" />,
  },
  info: {
    bgColor: 'bg-blue-50',
    borderColor: 'border-blue-200',
    textColor: 'text-blue-800',
    icon: <Info className="h-5 w-5 text-blue-400" />,
  },
}

export function Toast({ toast, onRemove }: ToastProps) {
  const [isVisible, setIsVisible] = useState(false)
  const [isLeaving, setIsLeaving] = useState(false)
  const config = severityConfig[toast.severity]

  useEffect(() => {
    const timer = setTimeout(() => setIsVisible(true), 5)
    return () => clearTimeout(timer)
  }, [])

  const handleRemove = () => {
    setIsLeaving(true)
    setTimeout(() => onRemove(toast.id), 300)
  }

  return (
    <div
      className={`
        transform transition-all duration-300 ease-in-out h-auto
        ${
          isVisible && !isLeaving
            ? 'translate-x-0 opacity-100'
            : 'translate-x-full opacity-0'
        }
        max-w-sm w-full ${config.bgColor} ${
        config.borderColor
      } border rounded-lg shadow-lg pointer-events-auto  
      `}>
      <div className="p-4 px-2">
        <div className="flex items-start">
          <div className="flex-shrink-0">{config.icon}</div>
          <div className="ml-2 w-0 flex-1">
            <p className={`text-sm font-medium ${config.textColor}`}>
              {toast.title}
            </p>
            <p className={`mt-2 text-sm ${config.textColor} opacity-90`}>
              {toast.message}
            </p>
          </div>
          <div className="absolute top-4 right-2 flex-shrink-0 flex">
            <button
              className={`
                inline-flex ${config.textColor} hover:opacity-75 focus:outline-none 
                focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-50 focus:ring-gray-600
                rounded-md
              `}
              onClick={handleRemove}>
              <span className="sr-only">Cerrar</span>
              <X className="h-5 w-5" />
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}
