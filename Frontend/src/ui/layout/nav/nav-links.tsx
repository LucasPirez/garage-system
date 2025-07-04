import { Link, useLocation } from 'react-router-dom'
import { NAV_LINKS } from '../../../core/constants/nav-links'
import { useEffect, useState } from 'react'

interface Props {
  onClick?: () => void
}

type NavLinkType = (typeof NAV_LINKS)[number]['href']

export const NavLinks = ({ onClick }: Props) => {
  const { pathname } = useLocation()
  const [selectedLink, setSelectedLink] = useState<NavLinkType>(
    NAV_LINKS[0].href
  )

  useEffect(() => {
    setSelectedLink(pathname as NavLinkType)
  }, [pathname])

  return (
    <>
      {NAV_LINKS.map((link) => (
        <li key={link.href}>
          <Link
            to={link.href}
            className={` ${
              selectedLink === link.href ? 'bg-gray-200 ' : ''
            } lg:p-3 lg:justify-start lg:text-base lg:text-gray-700 lg:hover:bg-gray-100 rounded-md transition-color
            md:hover:text-gray-900 md:px-2 md:py-2  md:text-sm md:font-medium flex justify-end  hover:text-gray-900 px-3 py-2 text-base font-medium`}
            onClick={onClick}>
            <span className="ml-3">{link.label}</span>
          </Link>
        </li>
      ))}
    </>
  )
}
