import { useAuth } from '../../context/auth-context'

export const ButtonLogout = () => {
  const { logout } = useAuth()

  return (
    <button
      className="bg-red-400 hover:bg-red-600 px-4 py-2 rounded-md text-sm font-medium transition-colors lg:ml-3 lg:py-2 lg:px-3 lg:translate-y-5 "
      onClick={logout}>
      Cerrar sesión
    </button>
  )
}
