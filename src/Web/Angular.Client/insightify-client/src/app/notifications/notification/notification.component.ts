import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/services/notification.service'; // Update the import path accordingly

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit{
  constructor(private notificationService: NotificationService) {}

  ngOnInit() {
    this.notificationService.startConnection();
    this.notificationService.addNotificationListener();
  }
}
