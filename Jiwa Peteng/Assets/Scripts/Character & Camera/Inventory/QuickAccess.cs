using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class QuickAccess : MonoBehaviour
    {
        public List<Item> characterItems = new List<Item>(3);
        private ItemDatabase itemDatabase;
        private UIQuickAccess quickAccessUI;


        private void Awake()
        {
            itemDatabase = transform.Find("Robot2").transform.Find("Player Cam").Find("Canvas").Find("ItemDatabase").gameObject.GetComponent<ItemDatabase>();
            quickAccessUI = transform.Find("Robot2").transform.Find("Player Cam").Find("Canvas").Find("QuickAccess").gameObject.GetComponent<UIQuickAccess>();
        }

        private void Start()
        {
            GiveItem("Defense Potion");
        }


        private void Update()
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && quickAccessUI.uIItems[0] != null)
                    quickAccessUI.UseItem(0);
                if (Input.GetKeyDown(KeyCode.Alpha2) && quickAccessUI.uIItems[1] != null)
                    quickAccessUI.UseItem(1);
                if (Input.GetKeyDown(KeyCode.Alpha3) && quickAccessUI.uIItems[2] != null)
                    quickAccessUI.UseItem(2);
            }
            catch (Exception _e)
            {
                Debug.Log("null");
            }
        }

        public void GiveItem(string itemname)
        {
            Item itemToAdd = itemDatabase.GetItem(itemname);
            characterItems.Add(itemToAdd);
            quickAccessUI.AddNewItem(itemToAdd);
            Debug.Log("Added item: " + itemToAdd.Name);
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
                quickAccessUI.RemoveItem(itemToRemove);
                Debug.Log("Item removed: " + itemToRemove.Name);
            }
        }
    }
}
