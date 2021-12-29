import { IMeasurementPoint } from './measurementPoint.model';
import { IShop } from './shop.model';
import { IVehicle } from './vehicle.model';

export interface IMeasurement {
  id: number;
  gap?: number;
  flush?: number;
  date: Date;
  vehicleId: number;
  vehicle?: IVehicle;
  measurementPointId: number;
  measurementPoint?: IMeasurementPoint;
  shopId: number;
  shop?: IShop;
}
