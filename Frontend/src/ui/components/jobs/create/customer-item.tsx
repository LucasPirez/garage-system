import { CustomerType } from '../../../../core/type/customer'

interface Props {
  customer: Omit<CustomerType, 'vehicle'>
}

export const CustomerItem = ({ customer }: Props) => {
  return (
    <div className="flex items-centergap-2">
      <div className="w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center">
        <span className="text-blue-600 font-medium text-sm">
          {customer.firstName[0]}
          {customer.lastName[0]}
        </span>
      </div>
      <div className="flex flex-col ml-2">
        <span className=" text-gray-900 font-medium text-lg">
          {customer.firstName} {customer.lastName}
        </span>
        <span className="text-gray-500 text-base">{customer.phoneNumber}</span>
      </div>
    </div>
  )
}
