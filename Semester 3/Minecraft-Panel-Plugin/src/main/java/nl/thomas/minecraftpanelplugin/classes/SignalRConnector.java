package nl.thomas.minecraftpanelplugin.classes;

import com.microsoft.signalr.Action;
import com.microsoft.signalr.HubConnection;
import com.microsoft.signalr.HubConnectionBuilder;
import com.microsoft.signalr.Subscription;
import nl.thomas.minecraftpanelplugin.interfaces.SignalRInterface;

public class SignalRConnector {

    private HubConnection hubConnection;

    public SignalRConnector() {
        hubConnection = HubConnectionBuilder.create("").build();
        hubConnection.start().blockingAwait();
        ReceiveConnectionID();
        Receive();

        hubConnection.on("ReceiveMessage", (message) -> {
            System.out.println("New Message: " + message);
        }, String.class);
    }

    public void Invoke(String method, String username) {
        hubConnection.invoke(method, username);
    }

    public void Invoke(String method, String username, String message) {
        hubConnection.invoke(method, username, message);
    }

    public void ReceiveConnectionID() {
        hubConnection.on("ReceiveConnID", (message) -> {
            System.out.println("Received connection ID: " + message);
        }, String.class);
    }

    public void Receive() {
        hubConnection.on("ReceiveMessage", (message) -> {
            System.out.println("New Message: " + message);
        }, String.class);
    }


    public HubConnection getHubConnection() {
        return hubConnection;
    }
}
