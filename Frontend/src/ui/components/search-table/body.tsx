import {
  CustomerAndVehicleType,
  ClientsAndVehiclesStoreType,
} from '../../../core/store/clients-vehicles-store'
import { VehicleType } from '../../../core/type/vehicle'

interface Props {
  data: ClientsAndVehiclesStoreType['customers']
  onVisibilityChange: (visible: boolean) => void
  handleClientSelect?: (clientSelected: CustomerAndVehicleType | null) => void
  handleVehicleSelect?: (vehicle: VehicleType | null) => void
}

export const Body = ({
  data,
  onVisibilityChange,
  handleClientSelect,
  handleVehicleSelect,
}: Props) => {
  if (!data?.length) {
    return (
      <tbody>
        <tr>
          <td
            colSpan={4}
            className="px-6 py-4 text-center text-sm text-gray-500"
          >
            'No se encontraron resultados'
          </td>
        </tr>
      </tbody>
    )
  }

  const handleClient = (customer: CustomerAndVehicleType) => {
    handleClientSelect?.(customer)

    if (customer?.vehicles?.length) {
      handleVehicleSelect?.(customer.vehicles[0])
    }

    onVisibilityChange(false)
  }

  return (
    <tbody className="bg-white divide-y divide-gray-200">
      {data?.map((customer) => (
        <tr
          key={customer.id}
          className="hover:bg-gray-100 cursor-pointer"
          onClick={() => handleClient(customer)}
        >
          <td className="px-6 py-4 whitespace-nowrap ">
            <div className="text-sm font-medium text-gray-900">
              {customer.firstName} {customer.lastName}
            </div>
          </td>

          {/* <td className="px-6 py-4 whitespace-nowrap">
            <div className="text-sm text-gray-900">
              {customer.email.join(', ')}
            </div>
          </td> */}
          <td className="px-1 py-4 ">
            {customer.vehicles?.length > 0 ? (
              <div className="space-y-1">
                {customer.vehicles.map((v) => (
                  <div key={v.id} className="text-sm text-gray-900 font-medium">
                    <p className="">
                      <span className="font-bold ">- </span>
                      {v.model}
                    </p>
                    <p className="opacity-90 ">({v.plate})</p>
                  </div>
                ))}
              </div>
            ) : (
              <span className="text-sm text-gray-500 italic">
                Sin vehículos
              </span>
            )}
          </td>
          <td className="px-6 py-4 whitespace-nowrap">
            <div className="text-sm text-gray-900">{customer.phoneNumber}</div>
          </td>
        </tr>
      ))}
    </tbody>
  )
}
