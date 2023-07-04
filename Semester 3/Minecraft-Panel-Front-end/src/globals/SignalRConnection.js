import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import APIConnection from '@/globals/APIConnectionURL'

let user = JSON.parse(localStorage.getItem('user'));
let connection;

if (user && user.Token) {
    connection = new HubConnectionBuilder()
        .withUrl(APIConnection.APISignalRHub + "/chathub", { accessTokenFactory: () => JSON.parse(localStorage.getItem('user')).Token })
        .configureLogging(LogLevel.Debug)
        .withAutomaticReconnect()
        .build();

    connection.start().catch(err => console.error(err.toString()));
}

export default connection;