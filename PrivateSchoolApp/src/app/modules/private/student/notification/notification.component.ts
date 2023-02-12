import { Component } from '@angular/core';
import { INotification } from 'src/app/modules/shared/interfaces/interface';
import { NotificationService } from 'src/app/modules/shared/services/notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss'],
  providers: [NotificationService]
})
export class NotificationComponent {

  public notifications: INotification[]
  
  constructor(private readonly notifService:NotificationService){}

  ngOnInit()
  {
    this.notifService.getAllNotificatins().subscribe(s=>this.notifications=s)
  }
}
