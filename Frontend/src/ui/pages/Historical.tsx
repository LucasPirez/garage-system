import { useState } from 'react'
import withAuth from '../components/hoc/with-auth'
import { SearchTable } from '../components/search-table/search-table'
import { useSelectCustomerVehicle } from '../hooks/useSelectCustomerVehicle'
import { CustomerItem } from '../components/jobs/create/customer-item'
import { SelectVehicle } from '../components/vehicle/select-vehicle'
import { VehicleType } from '../../core/type/vehicle'
import { useToast } from '../context/toast-context'

const Historical = () => {
  const [visible, setVisible] = useState(false)
  const [selectVehicle, setSelectVehicle] = useState<VehicleType | null>(null)
  const { customer, handleSelect, vehicles } = useSelectCustomerVehicle()
  const { addToast } = useToast()

  const handleSelectVehicle = async (vehicle: VehicleType | null) => {
    setSelectVehicle(vehicle)

    try {
      console.log('hola')
    } catch (error) {
      addToast({
        severity: 'error',
        message: 'Error al obtener el historial del vehiculo',
        title: 'Error',
      })
    }
  }

  return (
    <>
      <section className="lg:px-6 sm:px-4">
        <SearchTable
          isVisible={visible}
          onVisibilityChange={setVisible}
          handleClientSelect={handleSelect}
          handleVehicleSelect={handleSelectVehicle}
        />
      </section>

      {!customer && (
        <h2 className="text-2xl text-gray-500 font-semibold mt-10">
          Busca para ver el historial
        </h2>
      )}
      <section className="flex flex-col  my-5 lg:px-6 sm:px-2">
        {customer ? <CustomerItem customer={customer} /> : ''}
      </section>

      <section className="lg:px-6 sm:px-2">
        {vehicles ? (
          <SelectVehicle
            vehicles={vehicles}
            vehicleSelect={selectVehicle}
            onSelectVehicle={handleSelectVehicle}
          />
        ) : (
          ''
        )}
      </section>
    </>
  )
}

const HistoricalPage = withAuth(Historical)

export { HistoricalPage as Historical }
