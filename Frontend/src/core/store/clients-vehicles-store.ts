import { create } from 'zustand'
import { VehicleType } from '../type/vehicle'
import { CustomerType } from '../type/customer'

export type CustomerAndVehicleType = CustomerType & { vehicle: VehicleType[] }

export interface ClientsAndVehiclesStoreType {
  clients: CustomerAndVehicleType[]
  addVehicle: (vehicleData: VehicleType, clientId: string) => void
  setVehicles: (vehicleData: VehicleType[], clientId: string) => void
  setClients: (clients: CustomerAndVehicleType[]) => void
}

export const useStoreClientsAndVehicles = create<ClientsAndVehiclesStoreType>(
  (set) => ({
    clients: [],
    setVehicles: (vehicles: VehicleType[], clientId: string) =>
      set((state) => ({
        ...state,
        clients: state.clients.map((client) =>
          client.id === clientId ? { ...client, vehicles } : client
        ),
      })),
    addVehicle: (vehicle: VehicleType, clientId: string) =>
      set((state) => ({
        ...state,
        clients: state.clients.map((client) =>
          client.id === clientId
            ? { ...client, vehicles: [...client.vehicle, vehicle] }
            : client
        ),
      })),

    setClients: (clients: CustomerAndVehicleType[]) =>
      set((state) => ({
        ...state,
        clients,
      })),
  })
)
