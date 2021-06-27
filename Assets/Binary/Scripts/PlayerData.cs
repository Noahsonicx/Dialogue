using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string characterName;
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedClassIndex = 8;
    public struct Stats
    {
        public string baseStatsName;
        public int baseStats;
        public int tempStats;

    };
    public Stats[] characterStats;
    public bool showDropdown;
    public Vector2 scrollPos;
    public string classButton = "";
    public int statPoints = 10;
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex;
    public Renderer characterRenderer;
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;
    public string[] matName = new string[6];

    public PlayerData(Transform playerTransform, PlayerStats playerStats)
    {

    }

    public void LoadPlayerData(Transform playerTransform, PlayerStats playerStats)
    {

    }
}
