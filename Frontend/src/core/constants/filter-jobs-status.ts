import { JOBS_STATUS } from './jobs-status'

export const FILTER = { ALL: 'Todos', ...JOBS_STATUS } as const
