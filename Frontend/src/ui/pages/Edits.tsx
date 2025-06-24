import { useState } from 'react'
import { SearchTable } from '../components/search-table/search-table'
import { CustomerType } from '../../core/type/customer'
import withAuth from '../components/hoc/with-auth'
import { VehicleType } from '../../core/type/vehicle'

const Edits = () => {
  const [visible, setVisible] = useState(false)
  const [customer, setCustomer] = useState<CustomerType | null>(null)
  const [vehicle, setVehicle] = useState<VehicleType | null>(null)

  return (
    <>
      <SearchTable
        isVisible={visible}
        onVisibilityChange={setVisible}
        handleClientSelect={setCustomer}
        handleVehicleSelect={setVehicle}
      />

      {customer ? (
        <div>
          <p>
            {customer.firstName} - {customer.lastName}
          </p>

          <div>
            {customer?.vehicle.map((vehicle) => (
              <p>
                {vehicle.model}- {vehicle.plate}
              </p>
            ))}
          </div>
        </div>
      ) : (
        ''
      )}
    </>
  )
}

const EditComponent = withAuth(Edits)

export { EditComponent as Edits }
