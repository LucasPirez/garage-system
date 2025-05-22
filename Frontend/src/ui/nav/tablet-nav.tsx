import { Link } from 'react-router-dom'
import { navLinks } from './nav-links'

export const TabletNav = () => {
  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6">
      <div className="flex justify-between h-16">
        <div className="flex items-center">
          <div className="flex-shrink-0 flex items-center">
            <span className="text-xl font-bold text-gray-800">Logo</span>
          </div>
        </div>

        <div className="flex items-center space-x-4">
          {navLinks.map((link) => (
            <Link
              key={link.href}
              to={link.href}
              className="text-gray-700 hover:text-gray-900 px-3 py-2 rounded-md text-sm font-medium">
              {link.label}
            </Link>
          ))}
          <Link
            to="/login"
            className="bg-blue-500 text-white hover:bg-blue-600 px-4 py-2 rounded-md text-sm font-medium transition-colors">
            Login
          </Link>
        </div>
      </div>
    </div>
  )
}
