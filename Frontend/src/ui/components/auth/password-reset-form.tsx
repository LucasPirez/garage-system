import { useState } from 'react'
import { useToast } from '../../context/toast-context'
import { useLoader } from '../../context/loader-context'
import { authService } from '../../../core/services'
import { CustomError } from '../../../core/helpers/custom-error'
import { triggerCoolDown } from '../../../core/helpers/triggerCoolDown'

export function PasswordResetForm({
  handleForgot,
}: {
  handleForgot: React.Dispatch<React.SetStateAction<boolean>>
}) {
  const [email, setEmail] = useState('')
  const { showLoader, hideLoader, isLoading } = useLoader()
  const { showToast, addToast } = useToast()

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!triggerCoolDown()) {
      addToast({
        message: 'Por favor espera un momento antes de enviar nuevamente',
        title: 'Demasiados intentos',
        severity: 'info',
      })
      return
    }
    showLoader()

    try {
      await authService.requestChangePassword(email)

      setEmail('')
    } catch (err) {
      if (err instanceof CustomError) {
        showToast.error({
          message: err.message,
        })
        return
      }
      showToast.error({
        message: 'Ocurrió un error. Por favor intenta nuevamente',
      })
    } finally {
      hideLoader()
    }
  }

  return (
    <div className="min-h-screen  flex  px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8">
        <div>
          <p className="mt-2 text-lg text-center font-semibold text-gray-800">
            Ingresa tu email y te enviaremos un enlace para restablecer tu
            contraseña
          </p>
        </div>

        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
          <div>
            <label
              htmlFor="email"
              className="block text-sm font-medium text-gray-700">
              Email
            </label>
            <div className="mt-1">
              <input
                id="email"
                name="email"
                type="email"
                autoComplete="email"
                required
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="appearance-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500 focus:z-10 sm:text-sm"
                placeholder="tu@email.com"
              />
            </div>
          </div>
          <div>
            <button
              type="submit"
              disabled={isLoading}
              className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50 disabled:cursor-not-allowed">
              {isLoading ? (
                <div className="flex items-center">Enviando...</div>
              ) : (
                'Enviar enlace de recuperación'
              )}
            </button>
          </div>
        </form>
        <div className="text-center">
          <p
            className="text-sm text-blue-600 hover:text-blue-500 cursor-pointer font-semibold"
            onClick={() => handleForgot(false)}>
            ← Volver al inicio de sesión
          </p>
        </div>
      </div>
    </div>
  )
}
