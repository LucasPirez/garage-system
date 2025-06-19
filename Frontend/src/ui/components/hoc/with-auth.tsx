/* eslint-disable @typescript-eslint/no-explicit-any */
import { Navigate } from 'react-router-dom'
import { localStorageService } from '../../../core/storage/storages'
import { PATHS } from '../../../core/constants/paths'

const withAuth = (Component: React.FC) => {
  return (props: any) => {
    const isAuthenticated = !!localStorageService.getItem(
      localStorageService.keys.USER
    )
    return isAuthenticated ? (
      <Component {...props} />
    ) : (
      <Navigate to={PATHS.LOGIN} />
    )
  }
}

export default withAuth
