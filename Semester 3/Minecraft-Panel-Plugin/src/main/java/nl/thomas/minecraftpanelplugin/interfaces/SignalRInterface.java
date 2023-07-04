package nl.thomas.minecraftpanelplugin.interfaces;

import nl.thomas.minecraftpanelplugin.classes.SignalRConnector;

public interface SignalRInterface {
    void Invoke(String method, String username, String message);
}
