/**Данные об устройстве */
export interface Device {
  id: string;
  name: string; 
  version: string;
  sessions: DeviceSession[];
}

/**Сессии активности устройства*/
export interface DeviceSession {
  startTime: string;
  endTime: string;
}