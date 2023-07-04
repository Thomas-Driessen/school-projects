package nl.thomas.minecraftpanelplugin;


import com.microsoft.signalr.HubConnection;
import com.microsoft.signalr.HubConnectionBuilder;
import nl.thomas.minecraftpanelplugin.controllers.PlayerController;
import nl.thomas.minecraftpanelplugin.events.PlayerEvents;
import nl.thomas.minecraftpanelplugin.classes.SignalRConnector;
import nl.thomas.minecraftpanelplugin.interfaces.SignalRInterface;
import org.bukkit.plugin.java.JavaPlugin;

import static spark.Spark.*;

public class MinecraftPanelPlugin extends JavaPlugin {

    @Override
    public void onEnable() {
        // Plugin startup logic
        getServer().getPluginManager().registerEvents(new PlayerEvents(new SignalRConnector()), this);
        port(8500);
        //webSocket("/socket", PlayerEvents.class);


        //secure(configFolder + "/keystore.jks", "password", null, null);
        //before((request, response) -> {
        //    response.header("Access-Control-Allow-Origin", "*");
        //    response.type("application/json");
        //});
        new PlayerController().APIService();
        System.out.println("Started MinecraftPanelPlugin.");
    }

    @Override
    public void onDisable() {
        // Plugin shutdown logic
    }
}
