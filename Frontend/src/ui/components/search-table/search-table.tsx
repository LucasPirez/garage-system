import { ChangeEvent, useEffect, useState } from 'react'
import { Head } from './head'
import { Body } from './body'
import {
  ClientsAndVehiclesStoreType,
  useStoreClientsAndVehicles,
} from '../../../core/store/clients-vehicles-store'
import { workshopService } from '../../../core/services'
import { useRegisterJobContext } from '../../context/register-job-context'

export const SearchTable = () => {
  // const [searchTerm, setSearchTerm] = useState('')
  const [clientsSearch, setClientsSearch] = useState<
    ClientsAndVehiclesStoreType['clients']
  >([])
  const clients = useStoreClientsAndVehicles((clients) => clients.clients)
  const setStoreClients = useStoreClientsAndVehicles(
    (clients) => clients.setClients
  )

  const { searchTable, handleVisibility } = useRegisterJobContext()

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
      handleVisibility(false)
      return
    }
    handleVisibility(true)

    const filteredClients = clients.filter((client) => {
      const fullName = `${client.firstName} ${client.lastName}`.toLowerCase()
      return (
        fullName.includes(value.toLowerCase()) ||
        client.phoneNumber[0]?.includes(value)
      )
    })

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
          </div>
        </div>
      </div>

      <div className="bg-white rounded-lg shadow-md border-2 border-t-0 border-gray-300 overflow-hidden">
        <div className="overflow-x-auto">
          <table
            className={`min-w-full divide-y divide-gray-200  ${
              searchTable ? '' : 'hidden'
            }`}>
            <Head />
            <Body data={clientsSearch} />
          </table>
        </div>
      </div>
    </div>
  )
}
