import { VehicleType } from '../../../core/type/vehicle'
import { BaseModal } from './base-modal'
import { ModalPortal } from './modal-portal'

interface Props {
  vehicle: VehicleType
  handleUnSelectVehicle: () => void
}

export const ModalDeleteVehicle = ({
  vehicle,
  handleUnSelectVehicle,
}: Props) => {
  const onSubmit = () => {}

  return (
    <ModalPortal isOpen={vehicle !== null} onClose={handleUnSelectVehicle}>
      <BaseModal
        onClose={handleUnSelectVehicle}
        onSave={onSubmit}
        title="Seguro que desea eliminar?"
        disabledSaveButton={false}>
        <div className="mb-3 text-red-900 font-semibold text-xl">
          <span>{vehicle.model}</span>
          <span className="block mt-2">{vehicle.plate}</span>
        </div>
      </BaseModal>
    </ModalPortal>
  )
}
