import { createContext, ReactNode, useContext, useState } from 'react'
import { ClientAndVehicleType } from '../../core/store/clients-vehicles-store'
import { VehicleType } from '../../core/type/vehicle'

interface RegisterJobContextType {
  searchTable: boolean
  handleVisibility: (state: boolean) => void
  customerSelected: ClientAndVehicleType | null
  handleCustomerSelect: (clientSelected: ClientAndVehicleType | null) => void
  vehicleSelected: VehicleType | null
  handleVehicleSelect: (vehicle: VehicleType | null) => void
}

type Props = {
  children: ReactNode
}

const initialState: RegisterJobContextType = {
  searchTable: false,
  handleVisibility: () => {},
  customerSelected: null,
  vehicleSelected: null,
  handleCustomerSelect: () => {},
  handleVehicleSelect: () => {},
}

const RegisterJobContext = createContext<RegisterJobContextType>(initialState)

export const RegisterJobProvider: React.FC<Props> = ({ children }) => {
  const [searchTable, setSearchTable] = useState(false)
  const [customerSelected, setCustomerSelected] =
    useState<ClientAndVehicleType | null>(null)
  const [vehicleSelected, setVehicleSelected] = useState<VehicleType | null>(
    null
  )

  const handleVisibility = (state: boolean) => {
    setSearchTable(state)
  }

  const handleCustomerSelect = (client: ClientAndVehicleType | null) => {
    setCustomerSelected(client)
  }

  const handleVehicleSelect = (vehicle: VehicleType | null) => {
    setVehicleSelected(vehicle)
  }

  const value = {
    searchTable,
    handleVisibility,
    customerSelected,
    handleCustomerSelect,
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
