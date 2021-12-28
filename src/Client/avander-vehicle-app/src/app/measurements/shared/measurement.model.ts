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

interface IVehicle {
  vehicleId: number;
  jsn: string;
  vehicleModel: string;
}

interface IMeasurementPoint {
  measurementPointId: number;
  name: string;
}

interface IShop {
  shopId: number;
  name: string;
}
