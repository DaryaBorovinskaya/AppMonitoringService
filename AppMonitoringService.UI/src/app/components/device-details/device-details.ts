import { Component, OnDestroy, OnInit } from '@angular/core';
import {Router, ActivatedRoute } from '@angular/router';
import { DeviceService } from '../../services/device.service';
import { Device, DeviceSession } from '../../models/device';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

@Component({
  selector: 'app-device-details',
  standalone: false,
  templateUrl: './device-details.html',
  styleUrl: './device-details.less'
})

export class DeviceDetailsComponent implements OnInit {
  devices: Device[] = [];
  sessions: DeviceSession[] = [];
  device!: Device;
  selectedDate: string = "";
  deviceId: string = "";

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private deviceService: DeviceService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadDevice(id);
    }
  }

  /**Перенаправление на другой HTML-файл (списка всех устройств) */
  backToList(): void {
    this.router.navigate(['/']); 
  }

  /**Загрузка данных о конкретном устройстве*/
  loadDevice(id: string): void {
    this.deviceId = id;
    this.deviceService.getDeviceSessions(id).subscribe(
      (data) => {  
          this.device = data; 
          this.sessions = this.device.sessions
          console.log(`Данные об устройстве ${this.deviceId} загружены успешно`);
      },
      (error) => {
        console.error(`Ошибка при загрузке данных об устройстве ${this.deviceId}`, error);
        this.backToList();
      }

    );
  }

  /**Форматирование даты в удобный для пользователя вид*/
  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleString();
  }

  /**событие выбранной даты в datepicker*/
  onDateChange(event: MatDatepickerInputEvent<Date>) {
    this.selectedDate = event.value ? event.value.toISOString() : "";
  }

  /**Удаление старых записей о сессиях активности конкретного устройства */
  deleteOldRecords(date: string): void {
  if (confirm('Вы уверены, что хотите удалить записи старше, чем ' + this.formatDate(date) + '?')) {
    this.deviceService.deleteOldRecords(this.deviceId, date).subscribe(() => {
      // Обновляем данные после удаления
      this.loadDevice(this.deviceId);
      console.log("Старые записи успешно удалены");
    });
  }
}

}
