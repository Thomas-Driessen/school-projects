package nl.thomas.minecraftpanelplugin.models;

import org.bukkit.Location;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.PlayerInventory;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;


public class PlayerModel {
    public UUID ID;
    public String userName;
    public PlayerLocation playerLocation;
    public List<ItemStack> Armor;
    public List<ItemStack> Inventory;
}

