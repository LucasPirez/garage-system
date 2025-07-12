import { create } from 'zustand'
import { VehicleType } from '../type/vehicle'
import { CustomerType } from '../type/customer'

export type CustomerAndVehicleType = CustomerType & { vehicles: VehicleType[] }

export interface ClientsAndVehiclesStoreType {
  customers: CustomerAndVehicleType[]
  addVehicle: (vehicleData: VehicleType, customerId: string) => void
  setVehicles: (vehicleData: VehicleType[], customerId: string) => void
  setCustomers: (customers: CustomerAndVehicleType[]) => void
  updateVehicle: (vehicle: VehicleType) => void
  updateCustomer: (customer: CustomerType) => void
}

export const useStoreClientsAndVehicles = create<ClientsAndVehiclesStoreType>(
  (set) => ({
    customers: [],
    setVehicles: (vehicles: VehicleType[], customerId: string) =>
      set((state) => ({
        ...state,
        customers: state.customers.map((customer) =>
          customer.id === customerId ? { ...customer, vehicles } : customer
        ),
      })),
    addVehicle: (vehicle: VehicleType, customerId: string) =>
      set((state) => ({
        ...state,
        customers: state.customers.map((customer) =>
          customer.id === customerId
            ? { ...customer, vehicles: [...customer.vehicles, vehicle] }
            : customer
        ),
      })),

    setCustomers: (customers: CustomerAndVehicleType[]) =>
      set((state) => ({
        ...state,
        customers,
      })),
    updateCustomer: (customerPayload: CustomerType) =>
      set((state) => ({
        ...state,
        customers: state.customers.map((customer) =>
          customer.id === customerPayload.id
            ? { ...customerPayload, vehicles: customer.vehicles }
            : customer
        ),
      })),
    updateVehicle: (vehiclePayload: VehicleType) =>
      set((state) => ({
        ...state,
        customers: state.customers.map((customer) => {
          const vehicles = customer.vehicles.map((vehicle) =>
            vehicle.id === vehiclePayload.id ? vehiclePayload : vehicle
          )

          return { ...customer, vehicles }
        }),
      })),
  })
)
