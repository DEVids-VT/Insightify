import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private hubConnection!: signalR.HubConnection; // Add the ! operator here

  constructor() { }

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5121/notificationhub')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection.start()
      .then(() => {
        console.log('SignalR connection started.');
      })
      .catch(err => {
        console.error('Error while starting SignalR connection:', err);
      });
  }

  public addNotificationListener = () => {
    this.hubConnection.on('ReceiveNotification', (notification: any) => {
      console.log('Received notification:', notification);
      // Handle the received notification as per your requirement
    });
  }
}
