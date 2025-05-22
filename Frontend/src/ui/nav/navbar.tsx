import { Outlet } from 'react-router-dom'
import { MovileNav } from './mobile-nav'
import { TabletNav } from './tablet-nav'
import { DesktopNav } from './desktop-nav'

export const Navbar = () => {
  return (
    <div className="flex h-screen">
      <div className="hidden lg:flex  lg:h-screen lg:w-48 lg:flex-col lg:bg-white lg:shadow-lg">
        <div className="p-4 border-b">
          <span className="text-xl font-bold text-gray-800">Logo</span>
        </div>
        <nav className="flex-1 p-4">
          <DesktopNav />
        </nav>
      </div>

      <div className="flex-1 overflow-y-auto">
        <nav className="hidden md:block lg:hidden bg-white shadow-md">
          <TabletNav />
        </nav>

        <nav className="md:hidden bg-white shadow-md">
          <MovileNav />
        </nav>

        <section className="p-4 w-full">
          <Outlet />
        </section>
      </div>
    </div>
  )
}
