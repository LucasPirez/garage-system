import type React from 'react'

import { useState } from 'react'
import { authService } from '../../core/services'
import { useSearchParams } from 'react-router-dom'

export default function ChangePassword() {
  const [password, setPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')
  const [errors, setErrors] = useState<{
    password?: string
    confirmPassword?: string
  }>({})
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [searchParams] = useSearchParams()
  const token = searchParams.get('token')

  const validatePasswords = () => {
    const newErrors: { password?: string; confirmPassword?: string } = {}

    if (!password) {
      newErrors.password = 'La contraseña es requerida'
    } else if (password.length < 8) {
      newErrors.password = 'La contraseña debe tener al menos 8 caracteres'
    }

    if (!confirmPassword) {
      newErrors.confirmPassword = 'Confirma tu contraseña'
    } else if (password !== confirmPassword) {
      newErrors.confirmPassword = 'Las contraseñas no coinciden'
    }

    setErrors(newErrors)
    return Object.keys(newErrors).length === 0
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!validatePasswords()) {
      return
    }

    setIsSubmitting(true)

    try {
      await authService.changePassword({
        password: password,
        resetPasswordToken: token ?? '',
      })

      setPassword('')
      setConfirmPassword('')
      setErrors({})
    } catch (error) {
      console.log('error')
    } finally {
      setIsSubmitting(false)
    }
  }

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value
    setPassword(value)

    if (errors.password) {
      setErrors((prev) => ({ ...prev, password: undefined }))
    }
  }

  const handleConfirmPasswordChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = e.target.value
    setConfirmPassword(value)

    if (errors.confirmPassword) {
      setErrors((prev) => ({ ...prev, confirmPassword: undefined }))
    }
  }

  const passwordsMatch =
    password && confirmPassword && password === confirmPassword
  const isFormValid = password.length >= 8 && passwordsMatch

  return (
    <div className="min-h-screen flex items-start mt-8 justify-center  px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8">
        <div>
          <h2 className=" text-center text-2xl font-semibold text-gray-900">
            Ingresa tu nueva contraseña y confírmala
          </h2>
        </div>

        <form className="mt-2 space-y-6" onSubmit={handleSubmit}>
          <div className="space-y-4">
            <div>
              <label
                htmlFor="password"
                className="block text-sm font-medium text-gray-700"
              >
                Nueva Contraseña
              </label>
              <input
                id="change-password"
                name="password"
                type="password"
                value={password}
                onChange={handlePasswordChange}
                className={`mt-1 appearance-none relative block w-full px-3 py-2 border ${
                  errors.password ? 'border-red-300' : 'border-gray-300'
                } placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm`}
                placeholder="Ingresa tu nueva contraseña"
              />
              {errors.password && (
                <p className="mt-1 text-sm text-red-600">{errors.password}</p>
              )}
            </div>

            <div>
              <label
                htmlFor="confirmPassword"
                className="block text-sm font-medium text-gray-700"
              >
                Confirmar Contraseña
              </label>
              <input
                id="confirmPassword"
                name="confirmPassword"
                type="password"
                value={confirmPassword}
                onChange={handleConfirmPasswordChange}
                className={`mt-1 appearance-none relative block w-full px-3 py-2 border ${
                  errors.confirmPassword ? 'border-red-300' : 'border-gray-300'
                } placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm`}
                placeholder="Confirma tu nueva contraseña"
              />
              {errors.confirmPassword && (
                <p className="mt-1 text-sm text-red-600">
                  {errors.confirmPassword}
                </p>
              )}
              {passwordsMatch && (
                <p className="mt-1 text-sm text-green-600">
                  ✓ Las contraseñas coinciden
                </p>
              )}
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
              } transition-colors duration-200`}
            >
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

          <div className="mt-4 space-y-2">
            <div className="flex items-center text-sm">
              <div
                className={`w-2 h-2 rounded-full mr-2 ${
                  password.length >= 8 ? 'bg-green-500' : 'bg-gray-300'
                }`}
              ></div>
              <span
                className={
                  password.length >= 8 ? 'text-green-600' : 'text-gray-500'
                }
              >
                Al menos 8 caracteres
              </span>
            </div>
            <div className="flex items-center text-sm">
              <div
                className={`w-2 h-2 rounded-full mr-2 ${
                  passwordsMatch ? 'bg-green-500' : 'bg-gray-300'
                }`}
              ></div>
              <span
                className={passwordsMatch ? 'text-green-600' : 'text-gray-500'}
              >
                Las contraseñas coinciden
              </span>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}
