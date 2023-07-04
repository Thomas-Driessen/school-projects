package nl.thomas.minecraftpanelplugin.events;


import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.microsoft.signalr.HubConnection;
import nl.thomas.minecraftpanelplugin.classes.SignalRConnector;
import nl.thomas.minecraftpanelplugin.interfaces.SignalRInterface;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.AsyncPlayerChatEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;

public class PlayerEvents implements Listener {

    private SignalRConnector _hubConnection;

    public PlayerEvents(SignalRConnector hubConnection) {
        _hubConnection = hubConnection;
    }

    @EventHandler
    public void onPlayerJoin(PlayerJoinEvent playerJoinEvent) {
        //_hubConnection.Invoke("ReceiveMessage", "Event", playerJoinEvent.getPlayer().getDisplayName() + " joined the server!");

        _hubConnection.Invoke("PlayerJoinEvent", playerJoinEvent.getPlayer().getDisplayName());
    }

    @EventHandler
    public void onPlayerLeave(PlayerQuitEvent playerQuitEvent) {
        //_hubConnection.InvokeStuff("ReceiveMessage", "Event", playerQuitEvent.getPlayer().getDisplayName() + " left the server!");

        _hubConnection.Invoke("PlayerQuitEvent", playerQuitEvent.getPlayer().getDisplayName());
    }
}
