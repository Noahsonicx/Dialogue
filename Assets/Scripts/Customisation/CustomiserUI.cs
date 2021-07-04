using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomiserUI : MonoBehaviour
{
    [Header("Texture Lists")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Current texture Index")]
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex,  clothesIndex, armourIndex;
    [Header("Renderer")]
    public Renderer characterRenderer;
    [Header("Character Name")]
    public string characterName;
    [Header("Max amount of textures per type")]
    public int skinMax;
    public int eyesMax, mouthMax, hairMax,  clothesMax, armourMax;

    [System.Serializable]
    public struct StatsPool
    {
        public CustomisationSet.Stats pool;
        public Text nameDisplayer;
        public GameObject posButton;
        public GameObject negButton;

    };
    public StatsPool[] characterPool;
    [Header("Character Class")]
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedIndex = 0;
    public string classButton = "";
    public int statPoints = 10;
    public string[] matName = new string[6];

    private void Start()
    {
        //Loop to pull textures from the files
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
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(tempTexture);

        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(tempTexture);
        }


        #region StatPoint Setup
        for (int i = 0; i < characterPool.Length; i++)
        {
            characterPool[i].nameDisplayer.text = characterPool[i].pool.baseStatsName + " : "
                + (characterPool[i].pool.baseStats + characterPool[i].pool.tempStats);
            characterPool[i].negButton.SetActive(false);
        }
        #endregion
    }
    // UI skins
    public void SetTexturePositive(string type)
    {
        SetTexture(type, 1);
    }
    public void SetTextureNegative(string type)
    {
        SetTexture(type, -1);
    }
    public void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        //switch statement that swaps with the string name mat
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
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 5;
                break;
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 6;
                break;
        }
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }

        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[index];
        characterRenderer.materials = mat;

        switch (type)
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
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
        }
    }
    //Base Stats for the class 
    public void ChooseClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                characterPool[0].pool.baseStats = 18;
                characterPool[1].pool.baseStats = 10;
                characterPool[2].pool.baseStats = 15;
                characterPool[3].pool.baseStats = 6;
                characterPool[4].pool.baseStats = 6;
                characterPool[5].pool.baseStats = 5;

                characterClass = CharacterClass.Barbarian;
                break;
            case 1:
                characterPool[0].pool.baseStats = 6;
                characterPool[1].pool.baseStats = 13;
                characterPool[2].pool.baseStats = 7;
                characterPool[3].pool.baseStats = 10;
                characterPool[4].pool.baseStats = 6;
                characterPool[5].pool.baseStats = 18;

                characterClass = CharacterClass.Bard;
                break;
            case 2:
                characterPool[0].pool.baseStats = 5;
                characterPool[1].pool.baseStats = 8;
                characterPool[2].pool.baseStats = 8;
                characterPool[3].pool.baseStats = 9;
                characterPool[4].pool.baseStats = 15;
                characterPool[5].pool.baseStats = 15;

                characterClass = CharacterClass.Druid;
                break;
            case 3:
                characterPool[0].pool.baseStats = 8;
                characterPool[1].pool.baseStats = 15;
                characterPool[2].pool.baseStats = 10;
                characterPool[3].pool.baseStats = 15;
                characterPool[4].pool.baseStats = 8;
                characterPool[5].pool.baseStats = 4;

                characterClass = CharacterClass.Monk;
                break;
            case 4:
                characterPool[0].pool.baseStats = 15;
                characterPool[1].pool.baseStats = 6;
                characterPool[2].pool.baseStats = 10;
                characterPool[3].pool.baseStats = 8;
                characterPool[4].pool.baseStats = 5;
                characterPool[5].pool.baseStats = 18;

                characterClass = CharacterClass.Paladin;
                break;
            case 5:
                characterPool[0].pool.baseStats = 8;
                characterPool[1].pool.baseStats = 16;
                characterPool[2].pool.baseStats = 8;
                characterPool[3].pool.baseStats = 12;
                characterPool[4].pool.baseStats = 8;
                characterPool[5].pool.baseStats = 8;

                characterClass = CharacterClass.Ranger;
                break;
            case 6:
                characterPool[0].pool.baseStats = 6;
                characterPool[1].pool.baseStats = 8;
                characterPool[2].pool.baseStats = 16;
                characterPool[3].pool.baseStats = 8;
                characterPool[4].pool.baseStats = 8;
                characterPool[5].pool.baseStats = 14;

                characterClass = CharacterClass.Sorcerer;
                break;
            case 7:
                characterPool[0].pool.baseStats = 6;
                characterPool[1].pool.baseStats = 6;
                characterPool[2].pool.baseStats = 6;
                characterPool[3].pool.baseStats = 10;
                characterPool[4].pool.baseStats = 14;
                characterPool[5].pool.baseStats = 18;

                characterClass = CharacterClass.Warlock;
                break;
        }
        for (int i = 0; i < characterPool.Length; i++)
        {
            //buttons for reset points
            characterPool[i].pool.tempStats = 0;
            statPoints = 10;
            //changing display
            characterPool[i].nameDisplayer.text = characterPool[i].pool.baseStatsName + ": " + (characterPool[i].pool.baseStats + characterPool[i].pool.tempStats);
            //buttons for reset
            characterPool[i].negButton.SetActive(false);
            characterPool[i].posButton.SetActive(true);
        }
    }
    public void SetPointsPositive(int i)
    {
        statPoints--;
        characterPool[i].pool.tempStats++;
        //Hiding the positive button if the player runs out of points
        if (statPoints <= 0)
        {
            for (int button = 0; button < characterPool.Length; button++)
            {
                characterPool[button].posButton.SetActive(false);
            }
        }
        // If the player has no negative buttons for stats then it turns itself on
        if (characterPool[i].negButton.activeSelf == false)
        {
            characterPool[i].negButton.SetActive(true);
        }
        characterPool[i].nameDisplayer.text = characterPool[i].pool.baseStatsName + ": " + (characterPool[i].pool.baseStats + characterPool[i].pool.tempStats);
    }
    // Setting points for our negative UI input 
    public void SetPointsNegative(int i)
    {
        statPoints++;
        characterPool[i].pool.tempStats--;
        // Hiding the negative button if the player has no tempStat values
        if (characterPool[i].pool.tempStats <= 0)
        {
            characterPool[i].negButton.SetActive(false);
        }
        // If the player has no points to spend, it shows all the positive buttons
        if (characterPool[i].posButton.activeSelf == false)
        {
            for (int button = 0; button < characterPool.Length; button++)
            {
                characterPool[button].posButton.SetActive(true);
            }
        }
        characterPool[i].nameDisplayer.text = characterPool[i].pool.baseStatsName + ": " + (characterPool[i].pool.baseStats + characterPool[i].pool.tempStats);
    }
}
