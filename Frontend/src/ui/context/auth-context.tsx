import { createContext, useContext, useState } from 'react'
import { localStorageService } from '../../core/storage/storages'
import { AuthResponseDto } from '../../core/dtos/auth/auth-response.dto'
import { PATHS } from '../../core/constants/paths'
import { useNavigate } from 'react-router-dom'

interface AuthContextType {
  isLoggedIn: boolean
  login: (auth: AuthResponseDto) => void
  logout: () => void
}

const AuthContext = createContext<AuthContextType | undefined>(undefined)

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const navigate = useNavigate()
  const isAuthenticated = !!localStorageService.getItem(
    localStorageService.keys.USER
  )
  const [isLoggedIn, setIsLoggedIn] = useState(isAuthenticated)

  const login = (auth: AuthResponseDto) => {
    const { workShop, ...user } = auth
    localStorageService.setItem(localStorageService.keys.USER, user)
    localStorageService.setItem(localStorageService.keys.WORKSHOP, workShop)

    setIsLoggedIn(true)
    navigate(PATHS.JOBS)
  }

  const logout = () => {
    localStorageService.removeItem(localStorageService.keys.USER)

    setIsLoggedIn(false)
    navigate(PATHS.LOGIN)
  }

  return (
    <AuthContext.Provider value={{ isLoggedIn, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = () => {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within AuthProvider')
  return ctx
}
