import { Component, OnDestroy, OnInit } from '@angular/core';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';
import { switchMap, startWith } from 'rxjs/operators';


@Component({
  selector: 'app-device-list',
  standalone: false,
  templateUrl: './device-list.html',
  styleUrls: ['./device-list.less']
})
export class DeviceListComponent implements OnInit, OnDestroy {
  devices: Device[] = [];
  displayedColumns: string[] = ['id', 'name', 'version', 'detail'];
  private refreshSubscription!: Subscription;

  constructor(
    private deviceService: DeviceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadDevices();
    //this.setupAutoRefresh();
  }

  ngOnDestroy(): void {
    // отписаться при уничтожении компонента
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
    }
  }

  //#region Метод периодического обновления (каждые 5 сек) для подгрузки новых данных 
  // (в случае, если данные приходят очень часто)
  // private setupAutoRefresh(): void {
  //   this.refreshSubscription = interval(5000) // 5 секунд
  //     .pipe(
  //       startWith(0), // Запустить сразу при инициализации
  //       switchMap(() => this.deviceService.getAllDevices())
  //     )
  //     .subscribe(
  //       (data) => this.devices = data,
  //       (error) => console.error('Error loading devices', error)
  //     );
  // }
  //#endregion

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