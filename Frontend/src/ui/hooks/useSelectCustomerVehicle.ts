import { useState } from 'react'
import { CustomerType } from '../../core/type/customer'
import { VehicleType } from '../../core/type/vehicle'
import { CustomerAndVehicleType } from '../../core/store/clients-vehicles-store'

export const useSelectCustomerVehicle = () => {
  const [customer, setCustomer] = useState<Omit<
    CustomerType,
    'vehicle'
  > | null>(null)
  const [vehicles, setVehicles] = useState<VehicleType[] | null>(null)

  const handleSelect = (select: CustomerAndVehicleType | null) => {
    if (!select) {
      alert('error en la seleccion del cliente y vehiculo')
      return
    }
    const { vehicles, ...customer } = select

    setCustomer({
      ...customer,
      phoneNumber: customer.phoneNumber,
      email: customer.email,
    })

    setVehicles(vehicles)
  }

  const handleUpdateVehicle = (vehicleUpdate: VehicleType) => {
    const vehicleSet = vehicles?.map((v) =>
      v.id === vehicleUpdate.id ? vehicleUpdate : v
    )
    setVehicles(vehicleSet ?? null)
  }

  return {
    customer,
    vehicles,
    handleSelect,
    handleUpdateVehicle,
  }
}
