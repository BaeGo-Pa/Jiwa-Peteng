using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jiwa.Peteng
{
    public class UIQuickAccess : MonoBehaviour
    {
        public List<UIItem> uIItems = new List<UIItem>();
        public GameObject slotPrefab;
        public Transform slotPanel;

        private void Awake()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject instance = Instantiate(slotPrefab);
                instance.transform.SetParent(slotPanel);
                uIItems.Add(instance.GetComponentInChildren<UIItem>());
                instance.transform.Find("Item").GetComponent<Image>().color = Color.clear;
            }
        }

        public void UpdateSlot(int slot, Item item)
        {
            if (item.Type == Item.ItemType.CONSUMABLE)
                uIItems[slot].UpdateItem(item);
        }

        public void AddNewItem(Item item)
        {
            UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
        }

        public void RemoveItem(Item item)
        {
            UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
        }

        public void UseItem(int slot)
        {
            if (uIItems[slot].item.Type == Item.ItemType.CONSUMABLE)
            {
                switch (uIItems[slot].item.Name)
                {
                    case "Health Potion":
                        transform.parent.parent.parent.GetComponentInParent<PlayerManager>().Heal();
                        break;
                    case "Attack Potion":
                        transform.parent.parent.parent.GetComponentInParent<PlayerManager>().BoostAttack();
                        break;
                    case "Defense Potion":
                        transform.parent.parent.parent.GetComponentInParent<PlayerManager>().GiveArmor();
                        break;
                }
                Debug.Log("Using " + uIItems[slot].item.Name);
            }
        }
    }
}
