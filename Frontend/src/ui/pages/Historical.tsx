import withAuth from '../components/hoc/with-auth'

const Historical = () => {
  return (
    <section>
      <h1>Histórico</h1>
      <p>Esta página mostrará el histórico de trabajos realizados.</p>
      <p>Pronto estará disponible.</p>
      <p>¡Gracias por tu paciencia!</p>
    </section>
  )
}

const HistoricalPage = withAuth(Historical)

export { HistoricalPage as Historical }
