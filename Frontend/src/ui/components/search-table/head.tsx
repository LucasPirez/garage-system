export const Head = () => {
  const headers = ['Nombre', 'Vehículos', 'Teléfono']
  return (
    <thead className="bg-gray-50">
      <tr>
        {headers.map((header) => (
          <th
            key={Math.random() + header}
            className="px-6 py-3 text-left text-xs font-medium text-cyan-800 uppercase tracking-wider">
            {header}
          </th>
        ))}
      </tr>
    </thead>
  )
}
