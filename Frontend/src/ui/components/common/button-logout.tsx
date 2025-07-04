import { useLogout } from '../../hooks/useLogout'

export const ButtonLogout = () => {
  const { handleLogout } = useLogout()

  return (
    <button
      className="bg-red-400 hover:bg-red-600 px-4 py-2 rounded-md text-sm font-medium transition-colors lg:ml-3 lg:py-2 lg:px-3 lg:translate-y-5 "
      onClick={handleLogout}>
      Cerrar sesión
    </button>
  )
}
