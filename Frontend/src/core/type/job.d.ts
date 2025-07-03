import { JobsResponseDto } from '../dtos/vehicleEntry/jobs-response.dto'

export type JobType = Omit<JobsResponseDto, 'vehicle' | 'client'>
