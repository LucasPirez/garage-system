import { useState } from 'react'
import { SearchTable } from '../components/search-table/search-table'
import { CustomerType } from '../../core/type/customer'
import withAuth from '../components/hoc/with-auth'

const Clients = () => {
  const [visible, setVisible] = useState(false)
  const [customer, setCustomer] = useState<CustomerType | null>(null)

  return (
    <>
      <SearchTable
        isVisible={visible}
        onVisibilityChange={setVisible}
        handleClientSelect={setCustomer}
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

const ClientComponent = withAuth(Clients)

export { ClientComponent as Clients }
