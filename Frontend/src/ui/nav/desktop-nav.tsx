import { Link } from 'react-router-dom'
import { NAV_LINKS } from './nav-links'

export const DesktopNav = () => {
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
        <Link
          to="/login"
          className="flex items-center p-3 bg-blue-500 text-white hover:bg-blue-600 rounded-md transition-colors">
          <span className="ml-3">Login</span>
        </Link>
      </li>
    </ul>
  )
}
