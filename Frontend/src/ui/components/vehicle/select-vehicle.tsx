import type { VehicleType } from '../../../core/type/vehicle'

interface Props {
  vehicles: VehicleType[]
  vehicleSelect: VehicleType | null
  onSelectVehicle: (vehicle: VehicleType) => void
  onDeleteVehicle?: (vehicle: VehicleType) => void
}

export const SelectVehicle = ({
  vehicleSelect,
  vehicles,
  onSelectVehicle,
  onDeleteVehicle,
}: Props) => {
  return (
    <>
      <div className="flex flex-wrap gap-4">
        {vehicles.map((vehicle) => (
          <div
            key={vehicle.id}
            onClick={() => {
              onSelectVehicle(vehicle)
            }}
            className={`${
              vehicle.id === vehicleSelect?.id ? 'bg-blue-200 ' : ''
            }  max-w-56 h-auto p-3 shadow-md border-2 border-gry-500 shadow-gray-400 rounded-lg cursor-pointer`}>
            <div className="flex justify-between items-start">
              <div className="flex-1">
                <div className="flex justify-between items-start mb-1">
                  <h3 className="font-medium text-gray-900">{vehicle.model}</h3>
                </div>
                <p className="text-sm text-gray-600">Placa: {vehicle.plate}</p>
              </div>
              {onDeleteVehicle && (
                <button
                  onClick={() => onDeleteVehicle(vehicle)}
                  className="p-1.5 mx-1 text-red-600 hover:text-red-600 hover:bg-red-50 rounded-md transition-colors">
                  <svg
                    className="w-5 h-5"
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24">
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={1.5}
                      d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
                    />
                  </svg>
                </button>
              )}
            </div>
          </div>
        ))}
      </div>
    </>
  )
}
