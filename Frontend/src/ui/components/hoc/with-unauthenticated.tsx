/* eslint-disable @typescript-eslint/no-explicit-any */
import { Navigate } from 'react-router-dom'
import { localStorageService } from '../../../core/storage/storages'
import { PATHS } from '../../../core/constants/paths'

export const withUnauthenticated = (Component: React.FC) => {
  return (props: any) => {
    const unauthenticated = !localStorageService.getItem(
      localStorageService.keys.USER
    )
    return unauthenticated ? (
      <Component {...props} />
    ) : (
      <Navigate to={PATHS.JOBS} />
    )
  }
}
