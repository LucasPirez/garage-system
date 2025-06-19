import { useState } from 'react'
import { NAV_LINKS } from './nav-links'
import { Link } from 'react-router-dom'
import { useLogout } from '../components/common/useLogout'

export const MobileNav = () => {
  const [isOpen, setIsOpen] = useState(false)
  const { handleLogout } = useLogout()

  const toggleMenu = () => {
    setIsOpen(!isOpen)
  }

  return (
    <>
      <div className="max-w-7xl mx-auto px-4">
        <div className="flex justify-between h-16">
          <div className="flex items-center">
            <div className="flex-shrink-0 flex items-center">
              <span className="text-xl font-bold text-gray-800">Logo</span>
            </div>
          </div>

          <div className="flex items-center">
            <button
              onClick={toggleMenu}
              className="inline-flex items-center justify-center p-2 rounded-md text-gray-700 hover:text-gray-900 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-blue-500"
              aria-expanded="false">
              {isOpen ? (
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                  fill="currentColor"
                  className="size-6">
                  <path d="M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z" />
                </svg>
              ) : (
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                  fill="currentColor"
                  className="size-5">
                  <path
                    fillRule="evenodd"
                    d="M2 4.75A.75.75 0 0 1 2.75 4h14.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75ZM2 10a.75.75 0 0 1 .75-.75h14.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 10Zm0 5.25a.75.75 0 0 1 .75-.75h14.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                    clipRule="evenodd"
                  />
                </svg>
              )}
            </button>
          </div>
        </div>
      </div>

      <div className={`${isOpen ? 'block' : 'hidden'}`}>
        <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
          {NAV_LINKS.map((link) => (
            <Link
              key={link.href}
              to={link.href}
              className="flex items-center text-gray-700 hover:text-gray-900 hover:bg-gray-100 px-3 py-2 rounded-md text-base font-medium"
              onClick={() => setIsOpen(!open)}>
              <span className="ml-3">{link.label}</span>
            </Link>
          ))}
          <button
            className="bg-red-400 hover:bg-red-600 px-4 py-2 rounded-md text-sm font-medium transition-colors"
            onClick={handleLogout}>
            Logout
          </button>
        </div>
      </div>
    </>
  )
}
