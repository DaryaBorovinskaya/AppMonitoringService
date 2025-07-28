import { Component, inject, signal } from '@angular/core';
import { DeviceService } from './services/device.service';
import { Device } from './models/device';
@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.less'
})
export class App {
  protected readonly title = signal('AppMonitoringService.UI');

  //7
  deviceService = inject(DeviceService)
  devices: Device[] = []

  constructor(){
    this.deviceService.getAllDevices().subscribe(devices=>{
      this.devices = devices;
    })
  }

  //7
}
