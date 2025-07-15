import withAuth from '../components/hoc/with-auth'
import { SearchTable } from '../components/search-table/search-table'
import { useSelectCustomerVehicle } from '../hooks/useSelectCustomerVehicle'
import { Vehicles } from '../components/historical/vehicles'
import { ButtonNavigate } from '../components/buttons/button-navigate'
import { PATHS } from '../../core/constants/paths'

const Historical = () => {
  const { customer, handleSelect, vehicles } = useSelectCustomerVehicle()

  return (
    <>
      <section className="lg:px-6 sm:px-4">
        <SearchTable handleClientSelect={handleSelect} />
      </section>

      {!customer ? (
        <h2 className="text-2xl text-gray-500 font-semibold mt-10 text-center">
          Busca para ver el historial
        </h2>
      ) : (
        <section className="flex flex-col  my-5 lg:px-6 sm:px-2">
          <h2 className="text-2xl text-gray-800 font-semibold mb-3 text-center">
            {customer.firstName} {customer.lastName}
          </h2>
          <ButtonNavigate path={PATHS.EDITS} label="Editar" />
        </section>
      )}

      {vehicles?.length ? <Vehicles vehicles={vehicles} /> : ''}
    </>
  )
}

const HistoricalPage = withAuth(Historical)

export { HistoricalPage as Historical }
