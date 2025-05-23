import { axiosInstance } from './axios-service'
import { WorkshopService } from './worshop-service'

const workshopService = new WorkshopService(axiosInstance)

export { workshopService }
