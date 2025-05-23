import { useEffect, useState } from 'react'
import { JobsFilterType } from '../../core/type/jobs-filter'
import { FILTER } from '../../core/constants/filter-jobs-status'
import { ButtonFilterJobs } from '../components/button-filter-jobs'
import { CardJob } from '../components/jobs/card-job'

import { workshopService } from '../../core/services'
import { JobsResponseDto } from '../../core/dtos/vehicleEntry/jobs-response.dto'

// const mechanicJobs = [
//   {
//     id: 1,
//     service: 'Cambio de aceite y filtros',
//     vehicle: {
//       model: 'Toyota Corolla',
//       year: 2020,
//       plate: 'ABC-123',
//       owner: 'Juan Pérez',
//     },
//     date: '10/05/2023',
//     estimatedCompletion: '10/05/2023',
//     status: 'Completado',
//     cost: '$150',
//     mechanic: 'Carlos Rodríguez',
//     parts: ['Aceite sintético 5W-30', 'Filtro de aceite', 'Filtro de aire'],
//     priority: 'Normal',
//     notes: 'Cliente solicita revisión de frenos para próxima visita',
//   },
//   {
//     id: 2,
//     service: 'Reparación de sistema de frenos',
//     vehicle: {
//       model: 'Honda Civic',
//       year: 2019,
//       plate: 'XYZ-789',
//       owner: 'María González',
//     },
//     date: '12/05/2023',
//     estimatedCompletion: '14/05/2023',
//     status: 'En progreso',
//     cost: '$380',
//     mechanic: 'Roberto Sánchez',
//     parts: [
//       'Pastillas de freno delanteras',
//       'Discos de freno',
//       'Líquido de frenos',
//     ],
//     priority: 'Alta',
//     notes: 'Desgaste irregular en disco izquierdo, revisar suspensión',
//   },
//   {
//     id: 3,
//     service: 'Diagnóstico de falla en motor',
//     vehicle: {
//       model: 'Ford Ranger',
//       year: 2018,
//       plate: 'DEF-456',
//       owner: 'Ana Martínez',
//     },
//     date: '11/05/2023',
//     estimatedCompletion: '13/05/2023',
//     status: 'Pendiente',
//     cost: '$200-$1,500',
//     mechanic: 'Luis Hernández',
//     parts: ['Por determinar'],
//     priority: 'Alta',
//     notes: 'Vehículo no enciende, posible falla en sistema de inyección',
//   },
//   {
//     id: 4,
//     service: 'Alineación y balanceo',
//     vehicle: {
//       model: 'Chevrolet Silverado',
//       year: 2021,
//       plate: 'GHI-789',
//       owner: 'Pedro Ramírez',
//     },
//     date: '15/05/2023',
//     estimatedCompletion: '15/05/2023',
//     status: 'Programado',
//     cost: '$120',
//     mechanic: 'Miguel Torres',
//     parts: ['Ninguna'],
//     priority: 'Baja',
//     notes: 'Cliente reporta vibración en volante a alta velocidad',
//   },
//   {
//     id: 5,
//     service: 'Cambio de correa de distribución',
//     vehicle: {
//       model: 'Volkswagen Golf',
//       year: 2017,
//       plate: 'JKL-012',
//       owner: 'Laura Díaz',
//     },
//     date: '13/05/2023',
//     estimatedCompletion: '16/05/2023',
//     status: 'En progreso',
//     cost: '$650',
//     mechanic: 'Carlos Rodríguez',
//     parts: ['Kit de distribución', 'Bomba de agua', 'Anticongelante'],
//     priority: 'Normal',
//     notes: 'Mantenimiento preventivo a los 100,000 km',
//   },
//   {
//     id: 6,
//     service: 'Reparación de aire acondicionado',
//     vehicle: {
//       model: 'Nissan Sentra',
//       year: 2019,
//       plate: 'MNO-345',
//       owner: 'Sofía Vargas',
//     },
//     date: '14/05/2023',
//     estimatedCompletion: '17/05/2023',
//     status: 'Pendiente',
//     cost: '$350',
//     mechanic: 'Roberto Sánchez',
//     parts: ['Gas refrigerante', 'Compresor A/C'],
//     priority: 'Baja',
//     notes: 'Sistema no enfría, posible fuga de refrigerante',
//   },
// ]

export const Jobs = () => {
  const [statusFilter, setStatusFilter] = useState<JobsFilterType>(FILTER.ALL)
  const [jobs, setJobs] = useState<JobsResponseDto[] | null>(null)

  useEffect(() => {
    ;(async () => {
      try {
        const result = await workshopService.getAllVehicleEntries()

        setJobs(result)
        console.log(result)
      } catch (error) {
        alert('Error')
      }
    })()
  }, [])

  return (
    <>
      <div className="flex flex-col md:flex-row justify-between items-start md:items-center mb-6 gap-4">
        <h2 className="text-xl font-semibold text-gray-800">
          Listado de Trabajos
        </h2>

        <div className="flex flex-col sm:flex-row gap-3">
          <div className="inline-flex rounded-md shadow-sm bg-red-50 ">
            {Object.entries(FILTER).map(([ke, value], i) => (
              <ButtonFilterJobs
                onClick={() => setStatusFilter(value)}
                key={i + ke}
                label={value}
                status={statusFilter}
              />
            ))}
          </div>

          <button className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors">
            Nuevo Trabajo
          </button>
        </div>
      </div>

      <div className="flex flex-wrap justify-center gap-4 ">
        {jobs?.map((job) => (
          <CardJob job={job} />
        ))}
      </div>

      {/* {filteredJobs.length === 0 && (
          <div className="text-center py-12">
            <p className="text-gray-500">
              No hay trabajos{' '}
              {statusFilter !== 'todos' ? `con estado "${statusFilter}"` : ''}{' '}
              para mostrar.
            </p>
          </div>
        )} */}
    </>
  )
}
