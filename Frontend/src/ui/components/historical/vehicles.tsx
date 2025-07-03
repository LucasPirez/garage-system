import { useEffect, useState } from 'react'
import { VehicleType } from '../../../core/type/vehicle'
import { SelectVehicle } from '../vehicle/select-vehicle'
import { JobType } from '../../../core/type/job'
import { triggerCoolDown } from '../../../core/helpers/triggerCoolDown'
import { useToast } from '../../context/toast-context'
import { vehicleService } from '../../../core/services'
import { ContainerCard } from '../jobs/card-job/container-card'
import { FILTER } from '../../../core/constants/filter-jobs-status'
import { label_traduction } from '../../../core/constants/label-traduction-status'
import { CardDataJob } from '../jobs/card-job/card-data-job'

export const Vehicles = ({ vehicles }: { vehicles: VehicleType[] }) => {
  const [selectVehicle, setSelectVehicle] = useState<VehicleType | null>(null)
  const [historical, setHistorical] = useState<JobType[] | null>(null)
  const { addToast } = useToast()

  useEffect(() => {
    handleSelectVehicle(vehicles[0])
  }, [vehicles])

  const handleSelectVehicle = async (vehicle: VehicleType | null) => {
    if (!vehicle || vehicle.id === selectVehicle?.id) return

    if (!triggerCoolDown({ time: 1500 })) {
      addToast({
        severity: 'info',
        message: 'Por favor espera un momento antes de realizar otra consulta',
        title: 'Consulta en proceso',
        duration: 2000,
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
    <section className="lg:px-6 sm:px-2">
      <section>
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
      <h3 className="text-xl text-gray-800 font-semibold mt-8 mb-4">
        Historial
      </h3>
      <section className="flex flex-wrap gap-3">
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
    </section>
  )
}
