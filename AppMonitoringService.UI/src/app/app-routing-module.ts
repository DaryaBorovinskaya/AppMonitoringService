import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeviceDetailsComponent } from './components/device-details/device-details';
import { DeviceListComponent } from './components/device-list/device-list';

const routes: Routes = [
  { path: '', component: DeviceListComponent },
  { path: 'device/:id', component: DeviceDetailsComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
