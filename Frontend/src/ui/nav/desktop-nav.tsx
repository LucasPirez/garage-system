import { Link } from 'react-router-dom'
import { NAV_LINKS } from './nav-links'
import { useLogout } from '../components/common/useLogout'

export const DesktopNav = () => {
  const { handleLogout } = useLogout()

  return (
    <ul className="space-y-2">
      {NAV_LINKS.map((link) => (
        <li key={link.href}>
          <Link
            to={link.href}
            className="flex items-center p-3 text-gray-700 hover:bg-gray-100 rounded-md transition-colors">
            <span className="ml-3">{link.label}</span>
          </Link>
        </li>
      ))}
      <li className="pt-4">
        <button
          className="flex items-center ml-3 p-3 bg-red-400 hover:bg-red-600 px-4 py-2 rounded-md text-sm font-medium transition-colors"
          onClick={handleLogout}>
          Logout
        </button>
      </li>
    </ul>
  )
}
