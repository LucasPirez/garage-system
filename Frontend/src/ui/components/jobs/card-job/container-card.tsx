import { MoreHorizontal } from 'lucide-react'
import { useState } from 'react'

export const ContainerCard = ({
  children,
  className,
}: {
  children: React.ReactNode
  className?: string
}) => {
  const [seeMore, setSeeMore] = useState(false)

  const handleSeeMore = () => {
    if (window.innerWidth > 640) return

    setSeeMore(!seeMore)
  }

  return (
    <div
      className={`relative bg-gray-100 rounded-lg shadow-md  hover:shadow-lg transition-shadow w-[340px] max-h-[450px]  overflow-auto  ${
        !seeMore ? 'max-sm:max-h-[180px] max-sm:overflow-hidden' : ''
      } ${className}`}
      onClick={handleSeeMore}>
      {!seeMore ? (
        <MoreHorizontal
          className="absolute bottom-0 right-40 sm:opacity-0"
          opacity={0.6}
        />
      ) : (
        ''
      )}
      {children}
    </div>
  )
}
