using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomisationSet : MonoBehaviour
{
    [Header("Character Name")]
    public string characterName;
    [Header("Character Class")]
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedClassIndex = 8;
    [System.Serializable]
    public struct Stats
    {
        public string baseStatsName;
        public int baseStats;
        public int tempStats;

    };
    public Stats[] characterStats;
    [Header("DropDown Menu")]
    public bool showDropdown;
    public Vector2 scrollPos;
    public string classButton = "";
    public int statPoints = 10;
    [Header("Texture Lists")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    [Header("current texture Index")]
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex;
    [Header("Renderer")]
    public Renderer characterRenderer;
    [Header("Max amount of textures per type")]
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;
    [Header("Mat Name")]
    public string[] matName = new string[6];

    private void Start()
    {
        matName = new string[] { "Skin", "Eyes", "Mouth", "Hair", "Armour", "Clothes" };

        selectedClass = new string[] { "Barbarian", "Bard", "Druid", "Monk", "Paladin", "Ranger", "Sorcerer", "Warlock" };
        
        for (int i = 0; i < skinMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Skin_" + i) as Texture2D;
            skin.Add(tempTexture);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(tempTexture);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(tempTexture);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(tempTexture);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(tempTexture);

        }            
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(tempTexture);
        }
    }
    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        
        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 2;
                break;
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 4;
                break;
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 5;
                break;
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 6;
                break;
        }
        index += dir;
        if(index < 0)
        {
            index = max - 1;
        }
        if(index > max - 1)
        {
            index = 0;
        }
        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[index];
        characterRenderer.materials = mat;

        switch(type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
        }    
    }
    void ChooseClass(int classIndex)
    {
        switch(classIndex)
        {
            case 0:
                characterClass = CharacterClass.Barbarian;
                break;
            case 1:
                characterClass = CharacterClass.Bard;
                break;
            case 2:
                characterClass = CharacterClass.Druid;
                break;
            case 3:
                characterClass = CharacterClass.Monk;
                break;
            case 4:
                characterClass = CharacterClass.Paladin;
                break;
            case 5:
                characterClass = CharacterClass.Ranger;
                break;
            case 6:
                characterClass = CharacterClass.Sorcerer;
                break;
            case 7:
                characterClass = CharacterClass.Warlock;
                break;
        }
        
    }
    private void OnGUI()
    {
        #region Gui Value
        Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);
        //int i = 0;
        // Starting positions
        float left = 0.25f * scr.x;
        float mid = 0.75f * scr.x;
        float right = 2.25f * scr.x;
        //sizes
        float x = 0.5f * scr.x;
        float y = 0.5f * scr.y;
        float lable = 1.5f * scr.x;
        #endregion
        for (int i = 0; i < matName.Length; i++)
        {
            if (GUI.Button(new Rect(left, y + i * y, x, y), "<"))
            {
                SetTexture(matName[i], -1);
            }
            GUI.Box(new Rect(mid, y + i * y, lable, y), matName[i]);
            if (GUI.Button(new Rect(right, y + i * y, x, y), ">"))
            {
                SetTexture(matName[i], 1);
            }
        }

    }
}
public enum CharacterClass
{
    Barbarian,
    Bard,
    Druid,
    Monk,
    Paladin,
    Ranger,
    Sorcerer,
    Warlock
}

// https://learn.sydneytafe.edu.au/mod/page/view.php?id=571440 
// Character customisation for character model