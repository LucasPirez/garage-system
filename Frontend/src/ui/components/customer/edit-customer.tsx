import { ChangeEvent } from 'react'
import { InputsFormCustomer } from './inputs-form-customer'
import type { CustomerFormType } from '../../../core/type/customer'

export const EditCustomer = ({ customer }: { customer: CustomerFormType }) => {
  const handleChange = (
    event: ChangeEvent<HTMLSelectElement | HTMLInputElement>
  ) => {}

  return (
    <>
      <div className="bg-white rounded-lg p-4 ">
        <div className="flex items-start justify-between">
          <div className="flex-1">
            <h1 className="text-xl font-semibold text-gray-900 mb-1 text-center">
              {customer.firstName} {customer.lastName}
            </h1>
          </div>
        </div>
      </div>
      <InputsFormCustomer state={customer} onChange={handleChange} />
    </>
  )
}
