import React, { createContext, useContext, useRef, useState } from 'react'

interface StateType {
  showLoader: ({ size }?: { size?: number }) => void
  hideLoader: () => void
}

export const LoaderContext = createContext<StateType | null>(null)

export const LoaderProvider = ({ children }: { children: React.ReactNode }) => {
  const [loader, setLoader] = useState(false)
  const refSize = useRef(80)

  const showLoader = ({ size }: { size?: number } = {}) => {
    if (size) {
      refSize.current = size
    }
    setLoader(true)
  }

  const hideLoader = () => {
    refSize.current = 80
    setLoader(false)
  }

  const loaderStyle: React.CSSProperties = {
    width: refSize.current,
    height: refSize.current,
    border: `${6}px solid #f3f4f6`,
    borderTop: `${6}px solid blue`,
    borderRadius: '50%',
    animation: 'spin 1s linear infinite',
  }

  return (
    <LoaderContext.Provider value={{ showLoader, hideLoader }}>
      {loader && (
        <div className={'fixed right-[43%] top-[44%] z-[9999]'}>
          <div style={loaderStyle}></div>
        </div>
      )}
      {children}
    </LoaderContext.Provider>
  )
}

export const useLoader = () => {
  const context = useContext(LoaderContext)

  if (!context) {
    throw new Error('The context must be used within provider')
  }

  return context
}
