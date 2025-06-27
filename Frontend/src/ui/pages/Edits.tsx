import { useState } from 'react'
import { SearchTable } from '../components/search-table/search-table'
import withAuth from '../components/hoc/with-auth'
import type { VehicleType } from '../../core/type/vehicle'
import { EditCustomer } from '../components/customer/edit-customer'
import { EditVehicle } from '../components/vehicle/edit-vehicle'
import type { CustomerAndVehicleType } from '../../core/store/clients-vehicles-store'
import type { CustomerType } from '../../core/type/customer'

const Edits = () => {
  const [visible, setVisible] = useState(false)
  const [customer, setCustomer] = useState<Omit<
    CustomerType,
    'vehicle'
  > | null>(null)
  const [vehicle, setVehicle] = useState<VehicleType[] | null>(null)

  const handleSelect = (select: CustomerAndVehicleType | null) => {
    if (!select) {
      alert('error en la seleccion del cliente y vehiculo')
      return
    }
    const { vehicle, ...customer } = select

    setCustomer({
      ...customer,
      phoneNumber: customer.phoneNumber[0],
      email: customer.email[0],
    })

    setVehicle(vehicle)
  }

  const handleSetVehicle = (vehicleUpdate: VehicleType) => {
    const vehicleSet = vehicle?.map((v) =>
      v.id === vehicleUpdate.id ? vehicleUpdate : v
    )
    setVehicle(vehicleSet ?? null)
  }

  return (
    <section className="lg:px-6 sm:px-4">
      <SearchTable
        isVisible={visible}
        onVisibilityChange={setVisible}
        handleClientSelect={handleSelect}
      />
      <section>
        <div className=" mt-3 pb-3 border-b-4 border-gray-700/40">
          {customer ? <EditCustomer customer={customer} /> : ''}
        </div>

        {vehicle?.length ? (
          <EditVehicle vehicle={vehicle} setVehicle={handleSetVehicle} />
        ) : (
          ''
        )}
      </section>
    </section>
  )
}

const EditComponent = withAuth(Edits)

export { EditComponent as Edits }
