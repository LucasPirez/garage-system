import {
  Document,
  Page,
  Text,
  View,
  StyleSheet,
  PDFDownloadLink,
  PDFViewer,
} from '@react-pdf/renderer'
import { HeaderPDF } from './header'
import { formatCurrency, SpareParts } from './spare-parts'
import { JobType } from '../../../core/type/job'
import { DownloadIcon } from 'lucide-react'

const styles = StyleSheet.create({
  page: {
    fontFamily: 'Helvetica',
    fontSize: 10,
    paddingTop: 35,
    paddingLeft: 35,
    paddingRight: 35,
    paddingBottom: 30,
    backgroundColor: '#fafafa',
  },
  twoColumnRow: {
    flexDirection: 'row',
    marginBottom: 20,
    gap: 30,
  },
  column: {
    flex: 1,
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
  infoRow: {
    lineHeight: 1.1,
  },
  label: {
    fontWeight: 'bold',
    marginRight: 5,
  },
  serviceSection: {
    backgroundColor: '#f9fafb',
    borderRadius: 4,
    marginBottom: 25,
  },
  serviceDates: {
    flexDirection: 'row',
    gap: 20,
  },
  workDescription: {
    marginBottom: 10,
  },
  workItem: {
    marginBottom: 12,
  },
  workLabel: {
    fontWeight: 'bold',
    color: '#374151',
  },
  workText: {
    color: '#6b7280',
    marginTop: 3,
    lineHeight: 1,
  },
  totalSection: {
    alignItems: 'flex-end',
    marginTop: 15,
  },
  totalBox: {
    backgroundColor: '#eff6ff',
    borderWidth: 2,
    borderColor: '#bfdbfe',
    borderRadius: 4,
    padding: 15,
    minWidth: 200,
  },
  totalRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  totalLabel: {
    fontSize: 14,
    fontWeight: 'bold',
  },
  totalAmount: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#2563eb',
  },
  footer: {
    textAlign: 'center',
    fontSize: 8,
    color: '#6b7280',
    marginTop: 20,
  },
})

const formatDate = (dateString: string) => {
  return new Date(dateString).toLocaleDateString('es-ES', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
  })
}

const InvoicePDF = ({ data }: { data: JobType }) => (
  <Document>
    <Page size="A4" style={styles.page}>
      <HeaderPDF status={data.status} />

      <View style={styles.twoColumnRow}>
        <View style={styles.column}>
          <Text style={styles.sectionTitle}>CLIENTE</Text>
          <View style={styles.infoRow}>
            <Text>
              <Text style={styles.label}>Nombre:</Text> {data.client.firstName}{' '}
              {data.client.lastName}
            </Text>
          </View>
        </View>
        <View style={styles.column}>
          <Text style={styles.sectionTitle}>VEHÍCULO</Text>
          <View style={styles.infoRow}>
            <Text>
              <Text style={styles.label}>Matrícula:</Text> {data.vehicle.plate}
            </Text>
          </View>
          <View style={styles.infoRow}>
            <Text>
              <Text style={styles.label}>Modelo:</Text> {data.vehicle.model}
            </Text>
          </View>
        </View>
      </View>

      <View style={styles.serviceSection}>
        <Text style={styles.sectionTitle}>FECHAS DEL SERVICIO</Text>
        <View style={styles.serviceDates}>
          <Text>
            <Text style={styles.label}>Fecha de recepción:</Text>{' '}
            {formatDate(data.receptionDate)}
          </Text>
          {data.deliveryDate && (
            <Text>
              <Text style={styles.label}>Fecha de entrega:</Text>{' '}
              {formatDate(data.deliveryDate)}
            </Text>
          )}
        </View>
      </View>

      <View style={styles.workDescription}>
        <Text style={styles.sectionTitle}>DESCRIPCIÓN DEL TRABAJO</Text>
        <View style={styles.workItem}>
          <Text style={styles.workLabel}>Motivo:</Text>
          <Text style={styles.workText}>{data.cause}</Text>
        </View>
        <View style={styles.workItem}>
          <Text style={styles.workLabel}>Detalles:</Text>
          <Text style={styles.workText}>{data.details}</Text>
        </View>
      </View>

      {data.spareParts.length > 0 ? <SpareParts data={data.spareParts} /> : ''}

      <View style={styles.totalSection}>
        <View style={styles.totalBox}>
          <View style={styles.totalRow}>
            <Text style={styles.totalLabel}>TOTAL A PAGAR:</Text>
            <Text style={styles.totalAmount}>
              {formatCurrency(data.finalAmount)}
            </Text>
          </View>
        </View>
      </View>

      <View style={styles.footer}>
        <Text>Esta factura ha sido generada electrónicamente.</Text>
      </View>
    </Page>
  </Document>
)
export const SeePDF = ({ data }: { data: JobType }) => {
  return (
    <PDFViewer>
      <InvoicePDF data={data} />
    </PDFViewer>
  )
}

export const DownloadPDF = ({ data }: { data: JobType }) => {
  return (
    <>
      <PDFDownloadLink
        document={<InvoicePDF data={data} />}
        className="hover:scale-110 "
        fileName={`factura-${data.client.firstName}_${data.client.lastName}-${
          new Date().toISOString().split('T')[0]
        }.pdf`}>
        {() => <DownloadIcon className="w-5 h-5 text-blue-600" />}
      </PDFDownloadLink>
    </>
  )
}
