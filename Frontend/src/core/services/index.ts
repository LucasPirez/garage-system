import { AuthService } from './auth-service'
import { axiosInstance } from './axios-service'
import { CustomerService, JobsService, VehicleService } from './jobs-service'
import { UpdateCustomerVehicleService } from './update-customer-vehicle-service'
import { WorkshopService } from './worshop-service'

const workshopService = new WorkshopService(axiosInstance)

const customerService = new CustomerService(axiosInstance)

const vehicleService = new VehicleService(axiosInstance)

const jobService = new JobsService(axiosInstance)

const authService = new AuthService(axiosInstance)

const updateCustomerVehicleService = new UpdateCustomerVehicleService(
  axiosInstance
)

export {
  workshopService,
  customerService,
  vehicleService,
  jobService,
  authService,
  updateCustomerVehicleService,
}
