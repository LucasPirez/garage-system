import { PATHS } from './paths'

export const NAV_LINKS = [
  { href: PATHS.JOBS, label: 'Trabajos' },
  {
    href: PATHS.REGISTER,
    label: 'Registrar',
  },
  { href: PATHS.EDITS, label: 'Editar' },
  { href: PATHS.HISTORICAL, label: 'Historial' },
] as const
