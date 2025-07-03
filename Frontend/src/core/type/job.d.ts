import { JobsResponseDto } from '../dtos/vehicleEntry/jobs-response.dto'

export type JobType = Omit<JobsResponseDto, 'vehicle' | 'client'>

export type JobWithVehicleAndCustomerType = JobsResponseDto

export type JobWithVehicleType = Omit<JobsResponseDto, 'client'>

export type JobWithCustomerType = Omit<JobsResponseDto, 'vehicle'>
