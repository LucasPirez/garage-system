import { FILTER } from './filter-jobs-status'

export const label_traduction = {
  [FILTER.ALL]: 'Todos',
  [FILTER.PENDING]: 'En Progreso',
  [FILTER.REALIZED]: 'Completado',
} as const
