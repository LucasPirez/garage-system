/* eslint-disable @typescript-eslint/no-explicit-any */
import { Navigate } from 'react-router-dom'
import { PATHS } from '../../../core/constants/paths'
import { useAuth } from '../../context/auth-context'

export const withUnauthenticated = (Component: React.FC) => {
  return (props: any) => {
    const { isLoggedIn } = useAuth()

    return !isLoggedIn ? <Component {...props} /> : <Navigate to={PATHS.JOBS} />
  }
}
