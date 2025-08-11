export interface BaseVehicleDto {
  id: string
  plate: string
  model: string
  color: string
  customerId: string
}

export interface VehicleCreateDto extends BaseVehicleDto {}

export interface VehicleUpdateDto
  extends Omit<BaseVehicleDto, 'id' | 'customerId'> {}
