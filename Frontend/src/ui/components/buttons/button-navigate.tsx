import { ReactNode } from 'react'
import { useNavigate } from 'react-router-dom'

interface Props {
  path: string
  label: string | ReactNode
  className?: string
  data?: unknown
}

export const ButtonNavigate = ({ path, className, label, data }: Props) => {
  const navigate = useNavigate()

  return (
    <button
      className={' text-gray-600  hover:scale-110' + className}
      onClick={() => navigate(path, { state: data })}
    >
      {label}
    </button>
  )
}
