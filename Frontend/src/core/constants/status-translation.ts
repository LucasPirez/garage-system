import { FILTER } from './filter-jobs-status'

export const status_translation = {
  [FILTER.ALL]: 'Todos',
  [FILTER.PENDING]: 'En Progreso',
  [FILTER.REALIZED]: 'Completado',
} as const
