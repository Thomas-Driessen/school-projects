# Minecraft Panel Plugin

The **Minecraft Panel Plugin** is a Spigot/Bukkit plugin that exposes REST endpoints to extract data from the Spigot/Bukkit API or execute certain functionality.  
The target API version of this plugin is Spigot/Bukkit 1.16.*. Running a server above/below that might not work.


## Dependencies

This project relies on the [Minecraft Panel API](https://git.fhict.nl/I436237/minecraft-panel-api) to interact with a database, connect to the SignalR hub and take care of authorization. Please ensure the API is set up correctly and running before running this project. Otherwise the socket connection & authorization might fail. Also users for the SignalR client have to be present in the database and specified in the `config.yml`.

## Installation

To install the plugin simple clone the project and run `mvn pakcage`.  
A `target` folder will be created with in there the `minecraft-panel-plugin-*.jar`.  
Simply upload this plugin to your `plugins` folder of your minecraft server and then run/restart the server.  
The plugin will generate a folder with a `config.yml`, in here are the URL's for the API. You can change these to point to the URL running the API's.  
It's also possible to change the port the plugin opens to connect to. Be sure this port is accessible from the outside (or just locally when running this project local). The default port is `8500`.

## Usage

When the plugin is up and running you are able to make REST requests to `http(s)://IP-ADRESS:PORT/{endpoint}`.  
If you want to post data for example please ensure you are sending it as a `JSON` body.
