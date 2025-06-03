import { VehicleType } from '../../../core/type/vehicle'

interface Props {
  vehicle: VehicleType
  onClick: () => void
  selected: boolean
}

export const VehicleItem = ({ vehicle, onClick, selected }: Props) => {
  return (
    <div
      key={vehicle.id}
      className={`
          relative p-4 rounded-lg border-2 cursor-pointer w-[210px]  
          ${
            selected ? 'border-blue-500 bg-blue-50' : 'border-gray-200 bg-white'
          }
        `}
      onClick={onClick}>
      <div className="flex items-center gap-3 ">
        <div
          className={`
            p-2 rounded-full
            ${
              selected
                ? 'bg-blue-400 text-blue-600'
                : 'bg-gray-100 text-gray-600'
            }
          `}></div>

        <div className="flex-1 min-w-0">
          <p className="font-medium text-gray-900">{vehicle.model}</p>
          <div className="flex items-center gap-2 mb-1">
            <span className="font-semibold  text-lg text-gray-600 ">
              {vehicle.plate}
            </span>
          </div>
        </div>
      </div>
    </div>
  )
}
