import { useState } from 'react'
import withAuth from '../components/hoc/with-auth'
import { SearchTable } from '../components/search-table/search-table'
import { useSelectCustomerVehicle } from '../hooks/useSelectCustomerVehicle'
import { SelectVehicle } from '../components/vehicle/select-vehicle'
import { VehicleType } from '../../core/type/vehicle'
import { useToast } from '../context/toast-context'
import { vehicleService } from '../../core/services'
import { CardDataJob } from '../components/jobs/card-job/card-data-job'
import { JobType } from '../../core/type/job'
import { ContainerCard } from '../components/jobs/card-job/container-card'
import { FILTER } from '../../core/constants/filter-jobs-status'
import { label_traduction } from '../../core/constants/label-traduction-status'
import { triggerCoolDown } from '../../core/helpers/triggerCoolDown'

const Historical = () => {
  const [visible, setVisible] = useState(false)
  const [selectVehicle, setSelectVehicle] = useState<VehicleType | null>(null)
  const [historical, setHistorical] = useState<JobType[] | null>(null)
  const { customer, handleSelect, vehicles } = useSelectCustomerVehicle()
  const { addToast } = useToast()

  const handleSelectVehicle = async (vehicle: VehicleType | null) => {
    if (!vehicle || vehicle.id === selectVehicle?.id) return

    if (!triggerCoolDown({ time: 1500 })) {
      addToast({
        severity: 'info',
        message: 'Por favor espera un momento antes de realizar otra consulta',
        title: 'Consulta en proceso',
      })
      return
    }

    setSelectVehicle(vehicle)

    try {
      const result = await vehicleService.getHistoryByVehicleId(vehicle.id)

      setHistorical(result)
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

      {!customer ? (
        <h2 className="text-2xl text-gray-500 font-semibold mt-10 text-center">
          Busca para ver el historial
        </h2>
      ) : (
        <section className="flex flex-col  my-5 lg:px-6 sm:px-2">
          <h2 className="text-2xl text-gray-800 font-semibold mb-3 text-center">
            {customer.firstName} {customer.lastName}
          </h2>
        </section>
      )}

      <section className="lg:px-6 sm:px-2">
        <h3 className="text-xl text-gray-800 font-semibold mb-3">Vehiculos</h3>
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
      <section className="flex flex-wrap gap-3 mt-10 lg:px-6 sm:px-2">
        {historical?.map((job) => (
          <ContainerCard key={job.id} className="p-3 space-y-3">
            <div className="flex justify-between">
              <h3 className="text-lg font-semibold text-gray-800">
                {job.cause}
              </h3>
              <span
                className={`inline-block ml-2 px-3 py-1 text-xs font-medium cursor-pointer rounded-full h-6 ${
                  job.status === FILTER.REALIZED
                    ? 'bg-green-100 text-green-800'
                    : job.status === FILTER.PENDING
                    ? 'bg-yellow-100 text-yellow-800'
                    : 'bg-purple-100 text-purple-800'
                }`}>
                {label_traduction[job.status]}
              </span>
            </div>

            <CardDataJob job={job} />
          </ContainerCard>
        ))}
      </section>
    </>
  )
}

const HistoricalPage = withAuth(Historical)

export { HistoricalPage as Historical }
