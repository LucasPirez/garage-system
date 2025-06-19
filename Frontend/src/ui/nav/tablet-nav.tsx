import { Link } from 'react-router-dom'
import { NAV_LINKS } from './nav-links'
import { useLogout } from '../components/common/useLogout'

export const TabletNav = () => {
  const { handleLogout } = useLogout()

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6">
      <div className="flex justify-between h-16">
        <div className="flex items-center">
          <div className="flex-shrink-0 flex items-center">
            <span className="text-xl font-bold text-gray-800">Logo</span>
          </div>
        </div>

        <div className="flex items-center space-x-4">
          {NAV_LINKS.map((link) => (
            <Link
              key={link.href}
              to={link.href}
              className="text-gray-700 hover:text-gray-900 px-3 py-2 rounded-md text-sm font-medium">
              {link.label}
            </Link>
          ))}
          <button
            className="bg-red-400 hover:bg-red-600 px-4 py-2 rounded-md text-sm font-medium transition-colors"
            onClick={handleLogout}>
            Logout
          </button>
        </div>
      </div>
    </div>
  )
}
