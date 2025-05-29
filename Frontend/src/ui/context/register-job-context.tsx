import { createContext, ReactNode, useContext, useState } from 'react'
import { ClientAndVehicleType } from '../../core/store/clients-vehicles-store'
import { VehicleType } from '../../core/type/vehicle'

interface RegisterJobContextType {
  searchTable: boolean
  handleVisibility: (state: boolean) => void
  clientSelected: ClientAndVehicleType | null
  handleClientSelect: (clientSelected: ClientAndVehicleType | null) => void
  vehicleSelected: VehicleType | null
  handleVehicleSelect: (vehicle: VehicleType | null) => void
}

type Props = {
  children: ReactNode
}

const initialState: RegisterJobContextType = {
  searchTable: false,
  handleVisibility: () => {},
  clientSelected: null,
  vehicleSelected: null,
  handleClientSelect: () => {},
  handleVehicleSelect: () => {},
}

const RegisterJobContext = createContext<RegisterJobContextType>(initialState)

export const RegisterJobProvider: React.FC<Props> = ({ children }) => {
  const [searchTable, setSearchTable] = useState(false)
  const [clientSelected, setClientSelected] =
    useState<ClientAndVehicleType | null>(null)
  const [vehicleSelected, setVehicleSelected] = useState<VehicleType | null>(
    null
  )

  const handleVisibility = (state: boolean) => {
    setSearchTable(state)
  }

  const handleClientSelect = (client: ClientAndVehicleType | null) => {
    setClientSelected(client)
  }

  const handleVehicleSelect = (vehicle: VehicleType | null) => {
    setVehicleSelected(vehicle)
  }

  const value = {
    searchTable,
    handleVisibility,
    clientSelected,
    handleClientSelect,
    vehicleSelected,
    handleVehicleSelect,
  }

  return (
    <RegisterJobContext.Provider value={value}>
      {children}
    </RegisterJobContext.Provider>
  )
}

export const useRegisterJobContext = () => {
  const context = useContext(RegisterJobContext)
  if (!context) throw new Error('The context can be used inside of provider')

  return context
}
