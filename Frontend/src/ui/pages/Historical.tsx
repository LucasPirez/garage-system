import { useState } from 'react'
import withAuth from '../components/hoc/with-auth'
import { SearchTable } from '../components/search-table/search-table'
import { useSelectCustomerVehicle } from '../hooks/useSelectCustomerVehicle'

const Historical = () => {
  const [visible, setVisible] = useState(false)
  const { customer, handleSelect, handleUpdateVehicle, vehicles } =
    useSelectCustomerVehicle()

  return (
    <section className="lg:px-6 sm:px-4">
      <SearchTable
        isVisible={visible}
        onVisibilityChange={setVisible}
        handleClientSelect={handleSelect}
      />
    </section>
  )
}

const HistoricalPage = withAuth(Historical)

export { HistoricalPage as Historical }
