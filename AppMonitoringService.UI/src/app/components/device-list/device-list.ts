import { Component, OnInit } from '@angular/core';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device';
import { Router } from '@angular/router';


@Component({
  selector: 'app-device-list',
  standalone: false,
  templateUrl: './device-list.html',
  styleUrls: ['./device-list.less']
})
export class DeviceListComponent implements OnInit {
  devices: Device[] = [];
  displayedColumns: string[] = ['id', 'name', 'version', 'detail'];

  constructor(
    private deviceService: DeviceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadDevices();
  }

  /**Загрузка всех данных об устройствах*/
  loadDevices(): void {
    this.deviceService.getAllDevices().subscribe(
      (data) => {
        this.devices = data;
        console.log("Данные об устройствах загружены успешно");
      },
      (error) => console.error('Ошибка при загрузке всех данных об устройствах', error)
    );
  }

  /**Перенаправление на другой HTML-файл (конкретного устройства)*/
  viewDetails(id: string): void {
    this.router.navigate(['/device', id]); 
  }

  /**Бэкап данных*/
  backup():void{
    this.deviceService.backup().subscribe({
      next: () => {
        alert("Бэкап создан");
        console.log("Бэкап создан");
      }
    });
  }
}