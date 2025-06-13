export const JOBS_STATUS = {
  PENDING: 'InProgress',
  REALIZED: 'Completed',
} as const

export type JobStatusType = (typeof JOBS_STATUS)[keyof typeof JOBS_STATUS]
