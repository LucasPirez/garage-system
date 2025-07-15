import { SearchTable } from '../components/search-table/search-table'
import withAuth from '../components/hoc/with-auth'
import { EditCustomer } from '../components/customer/edit-customer'
import { EditVehicle } from '../components/vehicle/edit-vehicle'
import { useSelectCustomerVehicle } from '../hooks/useSelectCustomerVehicle'

const Edits = () => {
  const { customer, handleSelect, handleUpdateVehicle, vehicles } =
    useSelectCustomerVehicle()

  return (
    <section className="lg:px-6 sm:px-4">
      <SearchTable handleClientSelect={handleSelect} />
      {!customer && !vehicles && (
        <h2 className="text-2xl text-gray-500 text-center font-semibold mt-10">
          Selecciona un cliente para editar
        </h2>
      )}
      <section>
        <div className=" mt-3 pb-3">
          {customer ? <EditCustomer customer={customer} /> : ''}
        </div>
        {customer && vehicles && (
          <div className="border-b border-[3px] border-gray-300 mb-3"></div>
        )}

        {vehicles?.length ? (
          <EditVehicle vehicle={vehicles} setVehicle={handleUpdateVehicle} />
        ) : (
          ''
        )}
      </section>
    </section>
  )
}

const EditComponent = withAuth(Edits)

export { EditComponent as Edits }
