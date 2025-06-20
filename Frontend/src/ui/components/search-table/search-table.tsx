import { ChangeEvent, useEffect, useState } from 'react'
import { Head } from './head'
import { Body } from './body'
import {
  ClientAndVehicleType,
  ClientsAndVehiclesStoreType,
  useStoreClientsAndVehicles,
} from '../../../core/store/clients-vehicles-store'
import { workshopService } from '../../../core/services'
import { VehicleType } from '../../../core/type/vehicle'
import { ButtonClose } from '../common/button-close-icon'

interface Props {
  onVisibilityChange: (visible: boolean) => void
  isVisible: boolean
  handleClientSelect?: (clientSelected: ClientAndVehicleType | null) => void
  handleVehicleSelect?: (vehicle: VehicleType | null) => void
}

export const SearchTable = ({
  isVisible,
  onVisibilityChange,
  handleClientSelect,
  handleVehicleSelect,
}: Props) => {
  const [clientsSearch, setClientsSearch] = useState<
    ClientsAndVehiclesStoreType['clients']
  >([])
  const clients = useStoreClientsAndVehicles((clients) => clients.clients)
  const setStoreClients = useStoreClientsAndVehicles(
    (clients) => clients.setClients
  )

  useEffect(() => {
    // eslint-disable-next-line no-extra-semi
    ;(async () => {
      try {
        const result = await workshopService.getAllCustomers()

        setStoreClients(result)
      } catch (error) {
        console.log(error)
        alert(error)
      }
    })()
  }, [])

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { value } = event.target

    if (!value.trim()?.length) {
      onVisibilityChange(false)
      return
    }
    onVisibilityChange(true)

    const filteredClients = clients.filter((client) => {
      const fullName = `${client.firstName} ${client.lastName}`.toLowerCase()
      return (
        fullName.includes(value.toLowerCase()) ||
        client.phoneNumber[0]?.includes(value)
      )
    })
    console.log(filteredClients)

    setClientsSearch(filteredClients)
  }

  return (
    <div className="w-full max-w-6xl mx-auto p-3">
      <div>
        <div className="relative">
          <input
            type="text"
            placeholder="Buscar por nombre, Telefono"
            onChange={handleChange}
            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none"
          />
          <div className="absolute inset-y-0 right-0 flex items-center pr-3">
            {!isVisible ? (
              <svg
                className="w-5 h-5 text-gray-400"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24">
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
                />
              </svg>
            ) : (
              <ButtonClose onClick={() => onVisibilityChange(false)} />
            )}
          </div>
        </div>
      </div>

      <div className="bg-white rounded-lg shadow-md border-2 border-t-0 border-gray-300 overflow-hidden">
        <div className="overflow-x-auto">
          <table
            className={`min-w-full divide-y divide-gray-200  ${
              isVisible ? '' : 'hidden'
            }`}>
            <Head />
            <Body
              data={clientsSearch}
              onVisibilityChange={onVisibilityChange}
              handleClientSelect={handleClientSelect}
              handleVehicleSelect={handleVehicleSelect}
            />
          </table>
        </div>
      </div>
    </div>
  )
}
