import { JobType } from '../../../../core/type/job'

export const CardDataJob = ({ job }: { job: JobType }) => {
  return (
    <>
      <div className="flex items-start gap-2">
        {/* <Calendar className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
        <div>
          <p className="text-xs text-gray-500">Fecha de Ingreso</p>
          <p className="text-sm">
            {job.receptionDate.toLocaleString().split('T')[0]}
          </p>
        </div>
        {job.deliveryDate && (
          <>
            <span>|</span>
            <div>
              <p className="text-xs text-gray-500">Fecha de Entrega</p>
              <p className="text-sm">
                {job.deliveryDate.toLocaleString().split('T')[0]}
              </p>
            </div>
          </>
        )}
      </div>

      <div className="flex items-start">
        {/* <DollarSign className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
        <div>
          <p className="text-xs text-gray-500">Presupuesto</p>
          <p className="text-sm">$ {job.budget}</p>
        </div>
      </div>

      <div className="flex items-start">
        {/* <Tool className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
        <div>
          <p className="text-xs text-gray-500">Repuestos</p>
          <ul className="text-sm list-disc ml-4 mt-1">
            {job.spareParts.map((part, index) => (
              <li key={index + part.name + part.price}>
                <span>x{part.quantity} </span>
                <span>
                  {part.name} {'  '}
                </span>{' '}
                <span>${part.price.toLocaleString('Es-es')} </span>
              </li>
            ))}
          </ul>
        </div>
      </div>
      {job.details && (
        <div className="flex items-start">
          {/* <AlertTriangle className="w-5 h-5 text-gray-500 mr-2 mt-0.5" /> */}
          <div>
            <p className="text-xs text-gray-500">Detalles</p>
            <p className="text-sm">{job.details}</p>
          </div>
        </div>
      )}
    </>
  )
}
