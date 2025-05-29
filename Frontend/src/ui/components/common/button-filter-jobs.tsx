import { MouseEvent } from 'react'
import { JobsFilterType } from '../../../core/type/jobs-filter'
import { FILTER } from '../../../core/constants/filter-jobs-status'

interface Props {
  onClick: (e: MouseEvent<HTMLButtonElement>) => void
  status: JobsFilterType
  label: JobsFilterType
}

const label_traduction = {
  [FILTER.ALL]: 'Todos',
  [FILTER.PENDING]: 'En Progreso',
  [FILTER.REALIZED]: 'Completado',
}

export const ButtonFilterJobs = ({ onClick, status, label }: Props) => {
  return (
    <button
      onClick={onClick}
      className={`px-4 py-2 text-sm font-medium  ${
        status === label
          ? 'bg-blue-600 text-white'
          : 'bg-white text-gray-700 hover:bg-gray-50'
      } border-t border-b border-r border-l border-gray-300 ${
        label === FILTER.ALL
          ? 'rounded-l-lg'
          : label === FILTER.REALIZED
          ? 'rounded-r-lg'
          : ''
      }`}>
      {label_traduction[label]}
    </button>
  )
}
