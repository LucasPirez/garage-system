import { axiosInstance } from './axios-service'
import { CustomerService, JobsService, VehicleService } from './jobs-service'
import { WorkshopService } from './worshop-service'

const workshopService = new WorkshopService(axiosInstance)

const customerService = new CustomerService(axiosInstance)

const vehicleService = new VehicleService(axiosInstance)

const jobService = new JobsService(axiosInstance)

export { workshopService, customerService, vehicleService, jobService }
