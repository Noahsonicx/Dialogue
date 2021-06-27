using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private void OnGUI()
    {
        //StatsOnGUI();
        //ProfessionOnGUI();
        SaveOnGUI();
    }
    
    private void SaveOnGUI()
    {
        if(GUI.Button(new Rect(150,10,100,20), "Save"))
        {
            PlayerBinary.SavePlayerData(transform, this);
        }
        if (GUI.Button(new Rect(150, 40, 100, 20), "Load"))
        {
            PlayerData playerData = PlayerBinary.LoadPlayerData(transform, this);
        }
    }
}
