using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PlayerBinary
{
    public static void SavePlayerData(Transform playerTransform, PlayerStats playerStats)
    {
        // new binary formatter creation
        BinaryFormatter formatter = new BinaryFormatter();
        //location where the file will be saved
        string path = Application.dataPath + "/BinaryData"; 
        // Creating a file at file path
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerTransform, playerStats);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayerData(Transform playerTransform, PlayerStats playerStats)
    {
        string path = Application.dataPath + "/BinaryData"; 

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            // Deserializing data
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            //loading the data back into unity
            data.LoadPlayerData(playerTransform, playerStats);
            return data;
        }
        else
        {
            return null;
        }
    }
}
