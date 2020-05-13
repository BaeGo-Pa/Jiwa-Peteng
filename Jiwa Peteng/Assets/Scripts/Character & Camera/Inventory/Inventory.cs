using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jiwa.Peteng
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> characterItems = new List<Item>();
        private ItemDatabase itemDatabase;
        private UIInventory inventoryUI;

        public static bool inventoryIsShown = false;

        private void Awake()
        {
            itemDatabase = transform.Find("Robot2").transform.Find("Player Cam").Find("Canvas").Find("ItemDatabase").gameObject.GetComponent<ItemDatabase>();
            inventoryUI = transform.Find("Robot2").transform.Find("Player Cam").Find("Canvas").Find("InventoryPanel").gameObject.GetComponent<UIInventory>();
            inventoryUI.gameObject.SetActive(false);
        }
        private void Start()
        {
            GiveItem("Sword");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if(inventoryUI.gameObject.activeSelf)
                {
                    HideInventory();
                }
                else
                {
                    ShowInventory();
                }
            }
        }

        private void ShowInventory()
        {
            inventoryUI.gameObject.SetActive(true);
            inventoryIsShown = true;
        }

        private void HideInventory()
        {
            inventoryUI.gameObject.SetActive(false);
            inventoryIsShown = false;
        }

        public void GiveItem(int id)
        {
            Item itemToAdd = itemDatabase.GetItem(id);
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added item: " + itemToAdd.Name);
        }

        public void GiveItem(string itemname)
        {
            Item itemToAdd = itemDatabase.GetItem(itemname);
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added item: " + itemToAdd.Name);
        }

        public Item CheckForItem(int id)
        {
            return characterItems?.Find(item => item.Id == id);
        }

        public void RemoveItem(int id)
        {
            Item itemToRemove = CheckForItem(id);
            if(itemToRemove != null)
            {
                characterItems.Remove(itemToRemove);
                inventoryUI.RemoveItem(itemToRemove);
                Debug.Log("Item removed: " + itemToRemove.Name);
            }
        }

        public Item CheckForItem(string name)
        {
            return characterItems?.Find(item => item.Name == name);
        }

        public void RemoveItem(string name)
        {
            Item itemToRemove = CheckForItem(name);
            if (itemToRemove != null)
            {
                characterItems.Remove(itemToRemove);
                inventoryUI.RemoveItem(itemToRemove);
                Debug.Log("Item removed: " + itemToRemove.Name);
            }
        }
    }
}
