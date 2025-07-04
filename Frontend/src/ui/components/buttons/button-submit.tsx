interface Props {
  disabled?: boolean
  label: string
  className?: string
}

export const ButtonSubmit = ({ label, disabled, className }: Props) => {
  return (
    <button
      type="submit"
      disabled={disabled}
      className={`px-4 py-2 bg-blue-600 text-white right-0 rounded-md hover:bg-blue-700 transition-colors disabled:opacity-70 disabled:cursor-not-allowed active:scale-95
        ${className}`}>
      {label}
    </button>
  )
}
