using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace Inventorye
{

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Items> inventory = new List<Items>();
        [SerializeField] private bool showIMGUIInventory = true;
        [NonSerialized] public Items selectedItem = null;

        #region Canvas Inventory
        [SerializeField] private Button ButtonPrefab;
        [SerializeField] private GameObject InventoryGameObject;
        [SerializeField] private GameObject InventoryContent;
        [SerializeField] private GameObject FilterContent;

        [Header("Selected Item Display")]
        [SerializeField] private RawImage itemImage;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text ItemDescription;
        #endregion

        #region Display Inventory
        private Vector2 scrollPosition;
        private string sortType = "All";
        #endregion

        
        public void AddItem(Items _item)
        {
            AddItem(_item, _item.Amount);
        }

        public void AddItem(Items _item, int count)
        {
            Items foundItem = inventory.Find((x) => x.Name == _item.Name);
            if(foundItem == null)
            {
                inventory.Add(_item);
            }
            else
            {
                foundItem.Amount += count;
            }
            DisplayItemCanvas();
            DisplaySelectedItemCanvas(selectedItem);
        }

        public void RemoveItem(Items _item)
        {
            if (inventory.Contains(_item))
                inventory.Remove(_item);

            DisplayItemCanvas();
            DisplaySelectedItemCanvas(selectedItem);
        }
        private void Start()
        {
            DisplayFiltersCanvas();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {

                if (InventoryGameObject.activeSelf)
                {
                    InventoryGameObject.SetActive(false);
                }
                else
                {
                    InventoryGameObject.SetActive(true);
                    DisplayItemCanvas();
                }
            }
        }
        private void ChangeFilter(string itemType)
        {
            sortType = itemType;
            DisplayItemCanvas();
        }
        private void DisplayFiltersCanvas()
        {
            List<string> ItemTypes = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
            ItemTypes.Insert(0, "All");

            for(int i = 0; i < ItemTypes.Count; i++)
            {
                Button buttonID = Instantiate<Button>(ButtonPrefab, FilterContent.transform);
                TMP_Text buttonText = buttonID.GetComponentInChildren<TMP_Text>();
                buttonID.name = ItemTypes[i] + " filter ";
                buttonText.text = ItemTypes[i];

                int x = i;
                //buttonID.onClick.AddListener(() => { ChangeFilter(ItemTypes[x]); });
                buttonID.onClick.AddListener(delegate { ChangeFilter(ItemTypes[x]); });
            }
        }
        void DestroyAllChildren(Transform parent)
        {

            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
       
        private void DisplayItemCanvas()
        {
            DestroyAllChildren(InventoryContent.transform);
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Type.ToString() == sortType || sortType == "All")
                {
                    Button buttonID = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                    TMP_Text buttonText = buttonID.GetComponentInChildren<TMP_Text>();
                    buttonID.name = inventory[i].Name + " button ";
                    buttonText.text = inventory[i].Name;

                    Items item = inventory[i];
                    buttonID.onClick.AddListener(delegate { DisplaySelectedItemCanvas(item); });
                }
            }

        }

        void DisplaySelectedItemCanvas(Items item)
        {
            selectedItem = item;

            itemImage.texture = selectedItem.Icon;
            itemName.text = selectedItem.Name;
            ItemDescription.text = selectedItem.Description + 
                "\nValue: " + selectedItem.Value +
                "\nAmount: " + selectedItem.Amount;

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
                        selectedItem.OnClicked();
                    }
                    count++;
                }
            }
            GUI.EndScrollView();
        }

    }
}

