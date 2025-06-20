import { Text, View, StyleSheet } from '@react-pdf/renderer'
import { JobStatusType } from '../../../core/constants/jobs-status'
import { useEffect, useState } from 'react'
import { AuthResponseDto } from '../../../core/dtos/auth/auth-response.dto'
import { localKeys, localStorageService } from '../../../core/storage/storages'

// const formatDate = (dateString: string) => {
//   return new Date(dateString).toLocaleDateString('es-ES', {
//     year: 'numeric',
//     month: '2-digit',
//     day: '2-digit',
//   })
// }

const styles = StyleSheet.create({
  header: {
    borderBottomWidth: 2,
    borderBottomColor: '#2563eb',
    paddingBottom: 3,
    marginBottom: 8,
  },
  headerRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'flex-start',
  },
  companyInfo: {
    flex: 1,
  },
  companyTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#2563eb',
    marginBottom: 8,
  },
  companyDetails: {
    color: '#6b7280',
    lineHeight: 1,
  },
  invoiceInfo: {
    alignItems: 'flex-end',
  },
  invoiceTitle: {
    backgroundColor: '#2563eb',
    color: '#ffffff',
    padding: 8,
    borderRadius: 4,
    marginBottom: 15,
    fontSize: 12,
    fontWeight: 'bold',
    textAlign: 'center',
    minWidth: 80,
  },
  invoiceDetails: {
    fontSize: 9,
    lineHeight: 1.4,
  },
  statusCompleted: {
    backgroundColor: '#dcfce7',
    color: '#166534',
    padding: 4,
    borderRadius: 2,
    fontSize: 8,
    marginLeft: 5,
  },
  statusInProgress: {
    backgroundColor: '#fef3c7',
    color: '#92400e',
    padding: 4,
    borderRadius: 2,
    fontSize: 8,
    marginLeft: 5,
  },
})

export const HeaderPDF = ({ status }: { status: JobStatusType | string }) => {
  const [workshopData, setWorkshopData] = useState<
    AuthResponseDto['workShop'] | null
  >(null)

  useEffect(() => {
    const workshop = localStorageService.getItem(localKeys.WORKSHOP)

    setWorkshopData(workshop)
  }, [])

  return (
    <>
      <View style={styles.header}>
        <View style={styles.headerRow}>
          <View style={styles.companyInfo}>
            <Text style={styles.companyTitle}>
              {workshopData?.name ?? 'TALLER MECÁNICO'}
            </Text>
            <View style={styles.companyDetails}>
              <Text>{workshopData?.address ?? 'Sin Direccion'}</Text>
              <Text>
                {workshopData?.phoneNumber?.length &&
                  'Teléfono: ' + workshopData?.phoneNumber}
              </Text>
              <Text>
                {workshopData?.email?.length && 'Email: ' + workshopData.email}
              </Text>
            </View>
          </View>
          <View style={styles.invoiceInfo}>
            {/* <View style={styles.invoiceTitle}>
              <Text>FACTURA</Text>
            </View> */}
            <View style={styles.invoiceDetails}>
              <Text>Fecha: {new Date().toISOString().split('T')[0]}</Text>
              <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                <Text>Estado:</Text>
                <Text
                  style={
                    status === 'Completed'
                      ? styles.statusCompleted
                      : styles.statusInProgress
                  }>
                  {status === 'Completed' ? 'Completado' : 'En Progreso'}
                </Text>
              </View>
            </View>
          </View>
        </View>
      </View>
    </>
  )
}
