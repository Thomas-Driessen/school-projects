package nl.thomas.minecraftpanelplugin.controllers;

import com.google.gson.Gson;
import nl.thomas.minecraftpanelplugin.models.PlayerLocation;
import nl.thomas.minecraftpanelplugin.models.PlayerModel;
import org.bukkit.Bukkit;
import org.bukkit.entity.Player;

import java.util.LinkedList;
import java.util.List;

import static spark.Spark.*;

public class PlayerController {

    public void APIService()
    {
        get("/player/players", (request, response) -> {
            List<PlayerModel> playerList = new LinkedList<PlayerModel>();

            for (Player player : Bukkit.getOnlinePlayers())
            {
                if (player.isValid())
                {
                    PlayerModel playerModel = new PlayerModel();
                    playerModel.ID = player.getUniqueId();
                    playerModel.userName = player.getName();
                    playerModel.playerLocation = new PlayerLocation(player.getLocation().getX(), player.getLocation().getY(), player.getLocation().getZ());
                    //playerModel.Armor = Arrays.asList(player.getInventory().getArmorContents());
                    //playerModel.Inventory = Arrays.asList(player.getInventory().getContents());
                    playerList.add(playerModel);
                }
            }

            return new Gson().toJson(playerList);
        });

        post("/player", (request, response) -> {
            return "Hello post: " + request.body();
        });
    }

}
