import { Outlet } from 'react-router-dom'
import { MobileNav } from './mobile-nav'
import { NavLinks } from './nav-links'
import { ButtonLogout } from '../../components/buttons/button-logout'
import { useAuth } from '../../context/auth-context'

export const Navbar = () => {
  const { isLoggedIn } = useAuth()
  return (
    <div className="flex h-screen max-w-[1200px] mx-auto shadow-lg shadow-gray-300 lg:flex md:block">
      <div className="hidden md:flex lg:h-screen lg:w-48 lg:px-2 lg:flex-col lg:bg-white lg:shadow-lg  justify-between h-16 max-w-8xl mx-auto px-4 sm:px-6 ">
        <span className="text-xl font-bold text-gray-800 lg:pl-9 lg:mt-2 my-auto">
          Logo
        </span>
        <nav className="lg:flex-1 lg:p-4 hidden md:block   bg-white my-auto ">
          <ul className="lg:space-y-2 max-lg:flex max-lg:items-center max-lg:space-x-4">
            <NavLinks />
            {isLoggedIn && <ButtonLogout />}
          </ul>
        </nav>
      </div>

      <div className="flex-1 overflow-y-auto">
        <nav className="md:hidden bg-white shadow-md">
          <MobileNav />
        </nav>

        <section className="p-4 w-full">
          <Outlet />
        </section>
      </div>
    </div>
  )
}
