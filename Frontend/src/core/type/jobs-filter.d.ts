import { FILTER } from '../constants/filter-jobs-status'

export type JobsFilterType = (typeof FILTER)[keyof typeof FILTER]
