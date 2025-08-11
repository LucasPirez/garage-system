import { ChangeEvent, useEffect, useState } from 'react'
import { Head } from './head'
import { Body } from './body'
import {
  CustomerAndVehicleType,
  ClientsAndVehiclesStoreType,
  useStoreClientsAndVehicles,
} from '../../../core/store/clients-vehicles-store'
import { workshopService } from '../../../core/services'
import { VehicleType } from '../../../core/type/vehicle'
import { ButtonClose } from '../buttons/button-close-icon'
import { useToast } from '../../context/toast-context'

interface Props {
  handleClientSelect?: (clientSelected: CustomerAndVehicleType | null) => void
  handleVehicleSelect?: (vehicle: VehicleType | null) => void
}

export const SearchTable = ({
  handleClientSelect,
  handleVehicleSelect,
}: Props) => {
  const [visible, setVisible] = useState(false)
  const [clientsSearch, setClientsSearch] = useState<
    ClientsAndVehiclesStoreType['customers']
  >([])
  const customers = useStoreClientsAndVehicles((state) => state.customers)
  const setStoreCustomers = useStoreClientsAndVehicles(
    (customers) => customers.setCustomers
  )
  const { addToast } = useToast()

  useEffect(() => {
    if (customers.length) return // eslint-disable-next-line no-extra-semi
    ;(async () => {
      try {
        const result = await workshopService.getAllCustomers()

        setStoreCustomers(
          result.map((client) => ({
            ...client,
            phoneNumber: client.phoneNumber?.[0] || '',
            email: client.email?.[0] || '',
            vehicles: client.vehicles,
          }))
        )
      } catch (error) {
        console.log(error)
        addToast({
          severity: 'error',
          title: 'Error',
          message: 'Intente mas tarde',
        })
      }
    })()
  }, [])

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { value } = event.target

    if (!value.trim()?.length) {
      setVisible(false)
      return
    }
    setVisible(true)

    // Optimize this.
    const filteredClients = customers
      .filter((customer) => {
        const fullName =
          `${customer.firstName} ${customer.lastName}`.toLowerCase()
        return (
          fullName.includes(value.toLowerCase()) ||
          customer.phoneNumber[0]?.includes(value) ||
          customer.vehicles.some((vehicle) =>
            vehicle.plate.toLowerCase().includes(value.toLowerCase())
          )
        )
      })
      .splice(0, 4)

    setClientsSearch(filteredClients)
  }

  return (
    <>
      <div>
        <div className="relative">
          <input
            type="text"
            placeholder="Buscar por nombre, Telefono o Patente"
            onChange={handleChange}
            onFocus={() => setVisible(true)}
            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none"
          />
          <div className="absolute inset-y-0 right-0 flex items-center pr-3">
            {!visible ? (
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
              <ButtonClose onClick={() => setVisible(false)} />
            )}
          </div>
        </div>
      </div>

      <div className="bg-white rounded-lg shadow-md border-2 border-t-0 border-gray-300 overflow-hidden">
        <div className="overflow-x-auto">
          <table
            className={`min-w-full divide-y divide-gray-200  ${
              visible ? '' : 'hidden'
            }`}>
            <Head />
            <Body
              data={clientsSearch}
              onVisibilityChange={setVisible}
              handleClientSelect={handleClientSelect}
              handleVehicleSelect={handleVehicleSelect}
            />
          </table>
        </div>
      </div>
    </>
  )
}
