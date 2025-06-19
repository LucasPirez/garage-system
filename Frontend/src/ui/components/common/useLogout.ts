import { useNavigate } from 'react-router-dom'
import { localStorageService } from '../../../core/storage/storages'
import { PATHS } from '../../../core/constants/paths'

export const useLogout = () => {
  const navigate = useNavigate()

  const handleLogout = () => {
    localStorageService.removeItem(localStorageService.keys.USER)

    navigate(PATHS.LOGIN)
  }

  return { handleLogout }
}
