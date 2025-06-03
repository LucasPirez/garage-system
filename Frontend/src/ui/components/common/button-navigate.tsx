import { ReactNode } from 'react'
import { useNavigate } from 'react-router-dom'

interface Props {
  path: string
  label: string | ReactNode
  className: string
  data: unknown
}

export const ButtonNavigate = ({ path, className, label, data }: Props) => {
  const navigate = useNavigate()

  return (
    <button
      className={
        ' p-2 rounded-full text-gray-600  hover:bg-gray-200 ' + className
      }
      onClick={() => navigate(path, { state: data })}>
      {label}
    </button>
  )
}
