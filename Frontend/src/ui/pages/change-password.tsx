import type React from 'react'
import { useState } from 'react'
import { authService } from '../../core/services'
import { useNavigate, useSearchParams } from 'react-router-dom'
import { useToast } from '../context/toast-context'
import { CustomError } from '../../core/helpers/custom-error'
import { PATHS } from '../../core/constants/paths'

export default function ChangePassword() {
  const [newPassword, setNewPassword] = useState({
    password: '',
    confirmPassword: '',
  })
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [searchParams] = useSearchParams()
  const { showToast } = useToast()
  const navigate = useNavigate()
  const token = searchParams.get('token')

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    setIsSubmitting(true)

    try {
      await authService.changePassword({
        password: password,
        resetPasswordToken: token ?? '',
      })

      showToast.success({
        title: 'Contraseña cambiada exitosamente',
        message: 'Redireccionando al inicio de sesión.',
      })

      setTimeout(() => {
        navigate(PATHS.LOGIN)
      }, 2000)
    } catch (error) {
      if (error instanceof CustomError) {
        showToast.error({
          message: error.message,
        })
        return
      }
      showToast.error({
        message: 'Error al cambiar la contraseña',
      })
    } finally {
      setIsSubmitting(false)
    }
  }

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { value, name } = e.target
    setNewPassword((prev) => ({
      ...prev,
      [name]: value,
    }))
  }

  const ConditionalShowError = ({
    message,
    condition,
  }: {
    message: string
    condition: boolean
  }) => {
    return (
      <div className="ml-2 flex items-center text-sm font-semibold">
        <span className={condition ? 'text-green-600' : 'text-red-500'}>
          {message}
        </span>
      </div>
    )
  }

  const { password, confirmPassword } = newPassword

  const passwordsMatch =
    password === confirmPassword &&
    password?.length === confirmPassword?.length &&
    password.length > 0

  const isFormValid = password.length >= 8 && passwordsMatch

  return (
    <div className=" flex items-center justify-center md:border border-gray-200 rounded-lg py-12  sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8 m-auto  pt-8">
        <div>
          <h2 className=" text-center text-2xl font-semibold text-gray-900">
            Ingresa tu nueva contraseña
          </h2>
        </div>

        <form className="mt-2 space-y-6" onSubmit={handleSubmit}>
          <div className="space-y-4">
            <div>
              <label
                htmlFor="password"
                className="block text-sm font-medium text-gray-700">
                Nueva Contraseña
              </label>
              <input
                id="change-password"
                name="password"
                type="password"
                value={password}
                onChange={handlePasswordChange}
                className={`mt-1 appearance-none relative block w-full px-3 py-2 border placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm`}
                placeholder="Ingresa tu nueva contraseña"
              />
            </div>

            <div>
              <label
                htmlFor="confirmPassword"
                className="block text-sm font-medium text-gray-700">
                Confirmar Contraseña
              </label>
              <input
                id="confirmPassword"
                name="confirmPassword"
                type="password"
                value={confirmPassword}
                onChange={handlePasswordChange}
                className={`mt-1 appearance-none relative block w-full px-3 py-2 border placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm`}
                placeholder="Confirma tu nueva contraseña"
              />

              <div className="mt-4 space-y-2">
                <ConditionalShowError
                  message="Al menos 8 caracteres"
                  condition={password.length >= 8}
                />
                <ConditionalShowError
                  condition={passwordsMatch}
                  message="Las contraseñas coincioden"
                />
              </div>
            </div>
          </div>

          <div>
            <button
              type="submit"
              disabled={!isFormValid || isSubmitting}
              className={`group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white ${
                isFormValid && !isSubmitting
                  ? 'bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500'
                  : 'bg-gray-400 cursor-not-allowed'
              } transition-colors duration-200`}>
              {isSubmitting ? (
                <div className="flex items-center">
                  <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white mr-2"></div>
                  Cambiando...
                </div>
              ) : (
                'Cambiar Contraseña'
              )}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
