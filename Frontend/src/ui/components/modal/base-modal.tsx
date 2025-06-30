import React from 'react'

interface Props {
  title: string
  onSave: () => void
  onClose: () => void
  children: React.ReactNode
  disabledSaveButton?: boolean
}

export const BaseModal = ({
  title,
  onClose,
  onSave,
  children,
  disabledSaveButton,
}: Props) => {
  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-xl font-semibold text-gray-800">{title}</h2>
        <button
          onClick={onClose}
          className="text-gray-400  hover:text-gray-600 text-2xl leading-none">
          ×
        </button>
      </div>
      {children}

      <div className="flex gap-3 justify-end">
        <button
          onClick={onSave}
          disabled={disabledSaveButton}
          className="px-4 py-2 disabled:opacity-60 disabled:bg-blue-800  bg-blue-600 text-white rounded hover:bg-blue-600 transition-colors">
          Confirmar cambios
        </button>
      </div>
    </div>
  )
}
