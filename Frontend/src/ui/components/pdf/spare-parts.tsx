import { Text, View, StyleSheet } from '@react-pdf/renderer'
import { JobType } from '../../../core/type/job'

const styles = StyleSheet.create({
  tableHeader: {
    flexDirection: 'row',
    backgroundColor: '#f3f4f6',
    borderWidth: 1,
    borderColor: '#d1d5db',
    borderTopLeftRadius: 5,
    borderTopRightRadius: 5,
  },
  tableRow: {
    flexDirection: 'row',
    borderLeftWidth: 1,
    borderRightWidth: 1,
    borderBottomWidth: 1,
    borderColor: '#d1d5db',
  },
  tableCell: {
    padding: 8,
    borderRightWidth: 1,
    borderColor: '#d1d5db',
    fontSize: 9,
  },
  tableCellHeader: {
    padding: 8,
    borderRightWidth: 1,
    borderColor: '#d1d5db',
    fontSize: 9,
    fontWeight: 'bold',
  },
  tableCellName: {
    flex: 2,
  },
  tableCellQuantity: {
    flex: 1,
    textAlign: 'center',
  },
  tableCellPrice: {
    flex: 1,
    textAlign: 'right',
  },
  tableCellTotal: {
    flex: 1,
    textAlign: 'right',
    fontWeight: 'bold',
  },
  sectionTitle: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#374151',
    marginBottom: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#e5e7eb',
    paddingBottom: 3,
  },
  subTotalRow: {
    flexDirection: 'row',
  },
  bottomRight: {
    borderBottomRightRadius: 5,
    borderBottomWidth: 1,
    borderBottomColor: '#e5e7eb',
  },
  bottomLeft: {
    borderBottomLeftRadius: 5,
    borderBottomWidth: 1,
    borderLeftWidth: 1,
    borderLeftColor: '#e5e7eb',
    borderBottomColor: '#e5e7eb',
  },
  noBorderRight: {
    borderRightWidth: 0,
  },
})

export const formatCurrency = (amount: number) => {
  return '$' + amount.toLocaleString('ES-es')
}

const HEADER_SPARE_PARTS = ['Repuesto',  'Precio']

const getStyleByIndex = (index: number) => {
  switch (index) {
    case 0:
      return styles.tableCellName
    case 1:
      return styles.tableCellQuantity
    case 2:
      return styles.tableCellPrice
    case 3:
      return styles.tableCellTotal
    default:
      return {}
  }
}

export const SpareParts = ({ data }: { data: JobType['spareParts'] }) => {
  return (
    <View>
      <Text style={styles.sectionTitle}>REPUESTOS UTILIZADOS</Text>

      <View style={styles.tableHeader}>
        {HEADER_SPARE_PARTS.map((header, index) => (
          <Text
            key={header}
            style={[styles.tableCellHeader, getStyleByIndex(index)]}>
            {header}
          </Text>
        ))}
      </View>

      {data.map((part, index) => (
        <View key={`${part.name}-${index}`} style={styles.tableRow}>
          <Text style={[styles.tableCell, styles.tableCellName]}>
            {part.name}
          </Text>
          {/* <Text style={[styles.tableCell, styles.tableCellQuantity]}>
            {part.quantity}
          </Text> */}
          {/* <Text style={[styles.tableCell, styles.tableCellPrice]}>
            {formatCurrency(part.price)}
          </Text> */}
          <Text style={[styles.tableCell, styles.tableCellTotal]}>
            {formatCurrency(part.price * part.quantity)}
          </Text>
        </View>
      ))}

      <View style={styles.subTotalRow}>
        <Text
          style={[
            styles.tableCell,
            styles.tableCellName,
            styles.noBorderRight,
          ]}></Text>
        {/* <Text
          style={[
            styles.tableCell,
            styles.tableCellQuantity,
            styles.noBorderRight,
          ]}></Text> */}
        <Text
          style={[styles.tableCell, styles.tableCellPrice, styles.bottomLeft]}>
          Subtotal Repuestos
        </Text>
        <Text
          style={[styles.tableCell, styles.tableCellTotal, styles.bottomRight]}>
          {formatCurrency(
            data.reduce((acc, spare) => acc + spare.price * spare.quantity, 0)
          )}
        </Text>
      </View>
    </View>
  )
}
