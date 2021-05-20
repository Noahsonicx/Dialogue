using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Inventory
{

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Items> inventory = new List<Items>();
        [SerializeField] private bool showIMGUIInventory = true;
        private Items selectedItem = null;

        #region Canvas Inventory
        [SerializeField] private Button ButtonPrefab;
        [SerializeField] private GameObject InventoryGameObject;
        [SerializeField] private GameObject InventoryContent;
        [SerializeField] private GameObject FilterContent;
        #endregion

        #region Display Inventory
        private Vector2 scrollPosition;
        private string sortType = "All";
        #endregion

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                InventoryGameObject.SetActive(true);
                DisplayItemCanvas();
            }
        }
        private void ChangeFilter(string itemType)...
        void DestroyAllChildren(Transform parent)...
        private void DisplayItemCanvas()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Type.ToString() == sortType || sortType == "All")
                {
                    Button buttonID = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                    Text buttonText = buttonID.GetComponentInChildren<Text>();
                    buttonID.name = inventory[i].Name + " button ";
                    buttonText.text = inventory[i].Name;
                }
            }

        }
         
        private void OnGUI()
        {
            if(showIMGUIInventory)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                List<string> ItemTypes = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
                ItemTypes.Insert(0, "All");

                for(int i = 0; i < ItemTypes.Count; i++)
                {
                    if(GUI.Button(new Rect(
                        (Screen.width / ItemTypes.Count) * i
                        , 10
                        , Screen.width / ItemTypes.Count 
                        , 20), ItemTypes[i]))
                    {
                        //Debug.Log(ItemTypes[i]);
                        sortType = ItemTypes[i];

                    }
                }
                Display();
                if(selectedItem != null)
                {
                    DisplaySelectedItem();
                }
            }
        }

        private void DisplaySelectedItem()
        {
            GUI.Box(new Rect(Screen.width/ 4, Screen.height / 3,
                Screen.width / 5, Screen.height / 5),
                selectedItem.Icon);

            GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5),
                Screen.width / 7, Screen.height / 15),
                selectedItem.Name);

            GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3), 
                Screen.width / 5, Screen.height / 5), selectedItem.Description +
                "\nValue: " + selectedItem.Value +
                "\nAmount: " + selectedItem.Amount);
        }
        private void Display()
        {
            scrollPosition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40),
                scrollPosition,
                new Rect(0, 0, 0, inventory.Count * 30),
                false,
                true);
            int count = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i].Type.ToString() == sortType || sortType == "All")
                {
                    if(GUI.Button(new Rect(30, 0 + (count * 30), 200, 30), inventory[i].Name))
                    {
                        selectedItem = inventory[i];
                    }
                    count++;
                }
            }
            GUI.EndScrollView();
        }

    }
}

