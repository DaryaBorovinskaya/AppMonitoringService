import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device } from '../models/device';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor() { }  

  private http = inject(HttpClient);   

  private apiUrl = environment.apiURL + '/api/Device'; 

  private devices:Observable<Device[]> = new Observable<Device[]>;

  /**Получение всех данных об устройствах (HTTP Get)*/
  getAllDevices(): Observable<Device[]> {
    this.devices  = this.http.get<Device[]>(this.apiUrl);
    return this.devices;
  }

  /**Получение данных о конкретном устройстве (HTTP Get)*/
  getDeviceSessions(id: string): Observable<Device> {
    return this.http.get<Device>(`${this.apiUrl}/${id}`);
  }

  /**Удаление старых записей о сессиях активности конкретного устройства (HTTP Delete)*/
  deleteOldRecords(id: string, date: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}/old?date=${date}`);
  }
}