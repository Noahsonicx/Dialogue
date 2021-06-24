using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PlayerBinary
{
    public static void SavePlayerData(/*"Enter Something Here"*/)
    {
        PlayerData data = new PlayerData(/*"Enter Something Here"*/);

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.dataPath; //+ //For example "/BloodySave/.this"

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayerData(/*Enter Something here*/)
    {
        string path = Application.dataPath; //+ //For example "/BloodySave/.this" 

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            data.LoadPlayerData(/*Enter Something Here*/);
            return data;
        }
        else
        {
            return null;
        }
    }
}
